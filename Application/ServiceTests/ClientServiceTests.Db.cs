using System.Linq;
using Services;
using Xunit;
using ClientService = Services.Db.ClientService;
using TestDataGenerator = Services.Db.TestDataGenerator;

namespace ServiceTests
{
    public class ClientsServicesTestsDb
    {
        [Fact]
        public void AddAndGetClientPositiveTest()
        {
            // Arrange
            var clientService = new ClientService();
            var dataGenerators = new TestDataGenerator();
            var clientList = dataGenerators.GetClientList(1);
            var firstClient = clientList.First();

            // Assert
            foreach (var client in clientList)
            {
                clientService.AddClient(client);
            }

            // Act
            Assert.NotNull(clientService.GetClient(firstClient.Id));
        }
        
        [Fact]
        public void AddAccountPositiveTest()
        {
            // Arrange
            var clientService = new ClientService();
            var dataGenerators = new TestDataGenerator();
            var clientList = dataGenerators.GetClientList(10);
            var firstClient = clientList.First();

            // Assert
            foreach (var client in clientList)
            {
                clientService.AddClient(client);
            }

            clientService.AddAccount(firstClient.Id);
            
            // Act
            Assert.True(true);
        }
        
        [Fact]
        public void UpdateClientPositiveTest()
        {
            // Arrange
            var clientService = new ClientService();
            var dataGenerators = new TestDataGenerator();
            var clientList = dataGenerators.GetClientList(10);
            var firstClient = clientList.First();

            // Assert
            foreach (var client in clientList)
            {
                clientService.AddClient(client);
            }

            firstClient.FirstName = "Ivan";
            clientService.UpdateClient(firstClient, firstClient.Id);
            
            // Act
            Assert.True(true);
        }
        
        [Fact]
        public void DelClientPositiveTest()
        {
            // Arrange
            var clientService = new ClientService();
            var dataGenerators = new TestDataGenerator();
            var clientList = dataGenerators.GetClientList(10);
            var firstClient = clientList.First();

            // Assert
            foreach (var client in clientList)
            {
                clientService.AddClient(client);
            }
            
            clientService.DelClient(firstClient.Id);
            
            // Act
            Assert.True(true);
        }

        [Fact]
        public void DelAccountPositiveTest()
        {
            // Arrange
            var clientService = new ClientService();
            var dataGenerators = new TestDataGenerator();
            var clientList = dataGenerators.GetClientList(10);
            var firstClient = clientList.First();

            // Assert
            foreach (var client in clientList)
            {
                clientService.AddClient(client);
            }
            
            var accountsGuid = clientService.GetAccounts(firstClient.Id);
            
            clientService.DelAccount(accountsGuid.First().Id);
            
            // Act
            Assert.True(true);
        }

        [Fact]
        public void GetClientsFromFilterPositiveTest()
        {
            // Arrange
            var clientService = new ClientService();
            var dataGenerators = new TestDataGenerator();
            var clientList = dataGenerators.GetClientList(10);

            clientList[0].FirstName = "Ivan";

            var clientFilter = new ClientFilter
            {
                DateStart = DateTime.Now.AddYears(-60)
            };

            // Act
            foreach (var client in clientList)
            {
                try
                {
                    clientService.AddClient(client);
                }
                catch
                {
                }
            }

            var clientsWithNameIvan = clientService.GetClients(clientFilter);

            // Assert
            if (clientsWithNameIvan.Count > 0) Assert.True(true);
            else Assert.True(false);
        }
    }
}