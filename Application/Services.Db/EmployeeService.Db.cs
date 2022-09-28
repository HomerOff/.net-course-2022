using Microsoft.EntityFrameworkCore;
using Models.Db;
using Models;

namespace Services.Db;

public class EmployeeService
{
    private readonly ApplicationContext _dbContext;

    public EmployeeService()
    {
        _dbContext = new ApplicationContext();
    }

    public Guid AddEmployee(Employee employee)
    {
        var employeeDb = new EmployeeDb()
        {
            Id = Guid.NewGuid(),
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            BirthdayDate = employee.BirthdayDate,
            Bonus = employee.Bonus,
            Passport = employee.Passport,
            PhoneNumber = employee.PhoneNumber,
            Contract = employee.Contract,
            Salary = employee.Salary
        };
        
        _dbContext.Employees.Add(employeeDb);
        _dbContext.SaveChanges();

        return employeeDb.Id;
    }

    public void UpdateEmployee(Employee employee, Guid employeeId)
    {
        var oldDataEmployee = _dbContext.Employees.FirstOrDefault(c => c.Id == employeeId);
        oldDataEmployee = new EmployeeDb
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Passport = employee.Passport,
            BirthdayDate = employee.BirthdayDate,
            PhoneNumber = employee.PhoneNumber,
            Bonus = employee.Bonus,
            Contract = employee.Contract,
            Salary = employee.Salary
        };
        
        _dbContext.SaveChanges();
    }

    public void DeleteEmployee(Guid employeeId)
    {
        var employee = _dbContext.Employees.FirstOrDefault(c => c.Id == employeeId);
        _dbContext.Employees.Remove(employee);
        _dbContext.SaveChanges();
    }
        
    public Employee GetEmployee(Guid employeeId)
    {
        var employeeDb = _dbContext.Employees.FirstOrDefault(e => e.Id == employeeId);
        var employee = new Employee()
        {
            FirstName = employeeDb.FirstName,
            LastName = employeeDb.LastName,
            Passport = employeeDb.Passport,
            BirthdayDate = employeeDb.BirthdayDate,
            PhoneNumber = employeeDb.PhoneNumber,
            Bonus = employeeDb.Bonus,
            Contract = employeeDb.Contract,
            Salary = employeeDb.Salary
        };
        
        return employee;
    }
    
    public List<Employee> GetEmployees(EmployeeFilter employeeFilter, int page, int limit)
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
        
        var employeesDb =  selection.Skip((page - 1) * limit).Take(limit).ToList();
        var employees = new List<Employee>();
        
        for (int i = 0; i < employeesDb.Count; i++)
        {
            var employee = new Employee
            {
                FirstName = employeesDb[i].FirstName,
                LastName = employeesDb[i].LastName,
                Passport = employeesDb[i].Passport,
                BirthdayDate = employeesDb[i].BirthdayDate,
                PhoneNumber = employeesDb[i].PhoneNumber,
                Bonus = employeesDb[i].Bonus,
                Contract = employeesDb[i].Contract,
                Salary = employeesDb[i].Salary
            };
            
            employees.Add(employee);
        }
        
        return employees;
    }
}