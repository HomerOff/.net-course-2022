using Models.Db;

namespace Services.Db;

public class EmployeeService
{
    private readonly ApplicationContext _dbContext;

    public EmployeeService()
    {
        _dbContext = new ApplicationContext();
    }

    public void AddEmployee(Employee employee)
    {
        _dbContext.Employees.Add(employee);
        _dbContext.SaveChanges();
    }

    public void UpdateEmployee(Employee employee, Guid employeeId)
    {
        var oldDataEmployee = _dbContext.Employees.FirstOrDefault(c => c.Id == employeeId);
        oldDataEmployee = new Employee
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
        var employee = _dbContext.Employees.FirstOrDefault(c => c.Id == employeeId);
        _dbContext.Employees.Remove(employee);
        _dbContext.SaveChanges();
    }
        
    public Employee? GetEmployee(Guid employeeId)
    {
        return _dbContext.Employees.FirstOrDefault(c => c.Id == employeeId);
    }
    
    public List<Employee> GetEmployees(EmployeeFilter employeeFilter)
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