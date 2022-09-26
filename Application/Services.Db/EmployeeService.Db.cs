using Models.Db;

namespace Services.Db;

public class EmployeeService
{
    private readonly ApplicationContext _dbContext;

    public EmployeeService()
    {
        _dbContext = new ApplicationContext();
    }

    public void AddEmployee(EmployeeDb employee)
    {
        _dbContext.Employees.Add(employee);
        _dbContext.SaveChanges();
    }

    public void UpdateEmployee(EmployeeDb employee, Guid employeeId)
    {
        var oldDataEmployee = GetEmployee(employeeId);
        oldDataEmployee = new EmployeeDb
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Passport = employee.Passport,
            BirthdayDate = employee.BirthdayDate,
            PhoneNumber = employee.PhoneNumber,
            Bonus = employee.Bonus
        };
        
        _dbContext.SaveChanges();
    }

    public void DelEmployee(Guid employeeId)
    {
        _dbContext.Employees.Remove(GetEmployee(employeeId));
        _dbContext.SaveChanges();
    }
        
    public EmployeeDb? GetEmployee(Guid employeeId)
    {
        return _dbContext.Employees.FirstOrDefault(c => c.Id == employeeId);
    }
    
    public List<EmployeeDb> GetEmployees(EmployeeFilter employeeFilter)
    {
        if (employeeFilter.DateEnd == DateTime.MinValue)
        {
            employeeFilter.DateEnd = DateTime.Today;
        }
        
        var selection = _dbContext.Employees.
            Where(c => c.BirthdayDate >= employeeFilter.DateStart.ToUniversalTime()).
            Where(c => c.BirthdayDate <= employeeFilter.DateEnd.ToUniversalTime());

        if (!string.IsNullOrEmpty(employeeFilter.FirstName))
            selection = selection.Where(c => c.FirstName == employeeFilter.FirstName);
        
        if (!string.IsNullOrEmpty(employeeFilter.LastName))
            selection = selection.Where(c => c.LastName == employeeFilter.LastName);

        if (employeeFilter.Passport != 0)
            selection = selection.Where(c => c.Passport == employeeFilter.Passport);
        
        if (employeeFilter.PhoneNumber != 0)
            selection = selection.Where(c => c.PhoneNumber == employeeFilter.PhoneNumber);
        
        if (employeeFilter.Salary != 0)
            selection = selection.Where(c => c.PhoneNumber == employeeFilter.PhoneNumber);

        if (employeeFilter.Bonus != 0)
            selection = selection.Where(c => c.Bonus == employeeFilter.Bonus);

        return selection.ToList();
    }
}