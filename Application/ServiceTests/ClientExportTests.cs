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
            var pathToDirectory = Path.Combine("C:", "Users", "HomerOff", "Desktop");
            var csvFileName = "Clients.csv";
            var exportService = new ExportService(pathToDirectory, csvFileName);

            var testDataGenerator = new TestDataGenerator();
            var clientsList = testDataGenerator.GetClientList(100);
            var firstClientFromList = clientsList.First();
            
            //Act
            exportService.WriteClientToCsv(clientsList);
            var firstClientFromCsv = exportService.ReadClientsFromCsv().First();

            //Assert
            Assert.Equal(firstClientFromList.FirstName, firstClientFromCsv.FirstName);
        }
        
        [Fact]
        public void ExportNewClientsToCsvPositiveTest()
        {
            //Arrange
            var pathToDirectory = Path.Combine("C:", "Users", "HomerOff", "Desktop");
            var csvFileName = "Clients.csv";
            var exportService = new ExportService(pathToDirectory, csvFileName);
            
            var clientService = new ClientService();
            var clientsGuid = new List<Guid>();
            
            var testDataGenerator = new TestDataGenerator();
            var clientsList = testDataGenerator.GetClientList(100);
            var firstClientFromList = clientsList.First();
            
            //Act
            exportService.WriteClientToCsv(clientsList);
            
            foreach (var client in exportService.ReadClientsFromCsv())
            {
                clientsGuid.Add(clientService.AddClient(client));
            } 
            
            //Assert
            Assert.Equal(firstClientFromList.FirstName, clientService.GetClient(clientsGuid[0]).FirstName);
        }
    }
}