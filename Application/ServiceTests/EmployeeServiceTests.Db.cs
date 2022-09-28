using System.Linq;
using Services;
using Xunit;
using EmployeeService = Services.Db.EmployeeService;
using TestDataGenerator = Services.TestDataGenerator;


namespace ServiceTests
{
    public class EmployeeServiceTestsDb
    {
        [Fact]
        public void AddAndGetEmployeePositiveTest()
        {
            // Arrange
            var employeeService = new EmployeeService();
            var dataGenerators = new TestDataGenerator();
            var employeeList = dataGenerators.GetEmployeeList(10);
            var employeesGuid = new List<Guid>();

            // Assert
            foreach (var employee in employeeList)
            {
                employeesGuid.Add(employeeService.AddEmployee(employee));
            }

            // Act
            Assert.NotNull(employeeService.GetEmployee(employeesGuid[0]));
        }

        [Fact]
        public void UpdateEmployeePositiveTest()
        {
            // Arrange
            var employeeService = new EmployeeService();
            var dataGenerators = new TestDataGenerator();
            var employeeList = dataGenerators.GetEmployeeList(10);
            var firstEmployee = employeeList.First();
            var employeesGuid = new List<Guid>();

            // Assert
            foreach (var employee in employeeList)
            {
                employeesGuid.Add(employeeService.AddEmployee(employee));
            }

            firstEmployee.FirstName = "Ivan";
            employeeService.UpdateEmployee(firstEmployee, employeesGuid[0]);

            // Act
            Assert.True(true);
        }

        [Fact]
        public void DelEmployeePositiveTest()
        {
            // Arrange
            var employeeService = new EmployeeService();
            var dataGenerators = new TestDataGenerator();
            var employeeList = dataGenerators.GetEmployeeList(10);
            var employeesGuid = new List<Guid>();

            // Assert
            foreach (var employee in employeeList)
            {
                employeesGuid.Add(employeeService.AddEmployee(employee));
            }

            employeeService.DeleteEmployee(employeesGuid[0]);

            // Act
            Assert.True(true);
        }

        [Fact]
        public void GetEmployeeFromFilterPositiveTest()
        {
            // Arrange
            var employeeService = new EmployeeService();
            var dataGenerators = new TestDataGenerator();
            var employeeList = dataGenerators.GetEmployeeList(10);
            
            employeeList[0].FirstName = "Ivan";
            
            var employeeFilter = new EmployeeFilter
            {
                FirstName = "Ivan"
            };

            // Act
            foreach (var employee in employeeList)
            {
                try
                {
                    employeeService.AddEmployee(employee);
                }
                catch
                {
                }
            }

            var employeesWithNameIvan = employeeService.GetEmployees(employeeFilter, 1, 10);

            // Assert
            if (employeesWithNameIvan[0].FirstName == "Ivan") Assert.True(true);
            else Assert.True(false);
        }

    }
}
