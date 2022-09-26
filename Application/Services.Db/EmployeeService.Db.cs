using Microsoft.EntityFrameworkCore;
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
        var oldDataEmployee = _dbContext.Employees.FirstOrDefault(c => c.Id == employeeId);
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
        var employee = _dbContext.Employees.FirstOrDefault(c => c.Id == employeeId);
        _dbContext.Employees.Remove(employee);
        _dbContext.SaveChanges();
    }
        
    public EmployeeDb? GetEmployee(Guid employeeId)
    {
        return _dbContext.Employees.FirstOrDefault(c => c.Id == employeeId);
    }
    
    public List<EmployeeDb> GetEmployees(EmployeeFilter employeeFilter, int page, int limit)
    {
        if (employeeFilter.DateEnd == DateTime.MinValue)
        {
            employeeFilter.DateEnd = DateTime.Today;
        }
        
        var selection = _dbContext.Employees.
            Where(c => c.BirthdayDate >= employeeFilter.DateStart.ToUniversalTime()).
            Where(c => c.BirthdayDate <= employeeFilter.DateEnd.ToUniversalTime())
            .AsNoTracking();

        if (!string.IsNullOrEmpty(employeeFilter.FirstName))
            selection = selection.Where(c => c.FirstName == employeeFilter.FirstName)
                .AsNoTracking();
        
        if (!string.IsNullOrEmpty(employeeFilter.LastName))
            selection = selection.Where(c => c.LastName == employeeFilter.LastName)
                .AsNoTracking();

        if (employeeFilter.Passport != 0)
            selection = selection.Where(c => c.Passport == employeeFilter.Passport)
                .AsNoTracking();
        
        if (employeeFilter.PhoneNumber != 0)
            selection = selection.Where(c => c.PhoneNumber == employeeFilter.PhoneNumber)
                .AsNoTracking();
        
        if (employeeFilter.Salary != 0)
            selection = selection.Where(c => c.PhoneNumber == employeeFilter.PhoneNumber)
                .AsNoTracking();

        if (employeeFilter.Bonus != 0)
            selection = selection.Where(c => c.Bonus == employeeFilter.Bonus)
                .AsNoTracking();

        return selection.Skip((page - 1) * limit).Take(limit).ToList();
    }
}