using ExportTool;
using Services;
using Xunit;
using ClientService = Services.Db.ClientService;

namespace ServiceTests
{
    public class ClientExporterTests
    {
        [Fact]
        public void ExportClientPositiveTest()
        {
            //Arrange
            var pathToDirectory = Path.Combine("C:", "Users", "HomerOff", "RiderProjects", 
                ".net-course-2022-slivka", "Application", "Tools", "ExportDataFiles");
            var csvFileName = "Clients.csv";
            var exportService = new ExportService(pathToDirectory, csvFileName);

            var testDataGenerator = new TestDataGenerator();
            var clientsList = testDataGenerator.GetClientList(100);
            var firstClientFromList = clientsList.First();
            
            //Act
            exportService.WriteClientsToCsv(clientsList);
            var firstClientFromCsv = exportService.ReadClientsFromCsv().First();

            //Assert
            Assert.Equal(firstClientFromList.FirstName, firstClientFromCsv.FirstName);
        }
        
        [Fact]
        public void ExportNewClientsToCsvPositiveTest()
        {
            //Arrange
            var pathToDirectory = Path.Combine("C:", "Users", "HomerOff", "RiderProjects", 
                ".net-course-2022-slivka","Application","Tools", "ExportDataFiles");
            var csvFileName = "Clients.csv";
            var exportService = new ExportService(pathToDirectory, csvFileName);
            
            var clientService = new ClientService();

            var testDataGenerator = new TestDataGenerator();
            var clientsList = testDataGenerator.GetClientList(100);
            var firstClientFromList = clientsList.First();
            
            //Act
            exportService.WriteClientsToCsv(clientsList);

            var clientsFromCsv = exportService.ReadClientsFromCsv();

            var firstClientGuid = clientService.AddClient(clientsFromCsv.First());
            
            for (int i = 1; i < clientsFromCsv.Count; i++)
            {
                clientService.AddClient(clientsFromCsv[i]);
            }

            //Assert
            Assert.Equal(firstClientFromList.FirstName, clientService.GetClient(firstClientGuid).FirstName);
        }
    }
}