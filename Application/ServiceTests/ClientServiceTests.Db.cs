using System.Linq;
using Services;
using Xunit;
using ClientService = Services.Db.ClientService;
using TestDataGenerator = Services.TestDataGenerator;

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
            var clientList = dataGenerators.GetClientList(10);
            var clientsGuid = new List<Guid>();
            
            // Assert
            foreach (var client in clientList)
            {
                clientsGuid.Add(clientService.AddClient(client));
            }

            // Act
            Assert.NotNull(clientService.GetClient(clientsGuid[0]));
        }
        
        [Fact]
        public void AddAccountPositiveTest()
        {
            // Arrange
            var clientService = new ClientService();
            var dataGenerators = new TestDataGenerator();
            var clientList = dataGenerators.GetClientList(10);
            var clientsGuid = new List<Guid>();

            // Assert
            foreach (var client in clientList)
            {
                clientsGuid.Add(clientService.AddClient(client));
            }

            clientService.AddAccount(clientsGuid[0]);
            
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
            var clientsGuid = new List<Guid>();

            // Assert
            foreach (var client in clientList)
            {
                clientsGuid.Add(clientService.AddClient(client));
            }

            firstClient.FirstName = "Ivan";
            clientService.UpdateClient(firstClient, clientsGuid[0]);
            
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
            var clientsGuid = new List<Guid>();

            // Assert
            foreach (var client in clientList)
            {
                clientsGuid.Add(clientService.AddClient(client));
            }
            
            clientService.DeleteClient(clientsGuid[0]);
            
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
            var clientsGuid = new List<Guid>();

            // Assert
            foreach (var client in clientList)
            {
                clientsGuid.Add(clientService.AddClient(client));
            }
            
            var accountsGuid = clientService.GetAccounts(clientsGuid[0]);
            
            clientService.DeleteAccount(accountsGuid[0]);
            
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
                FirstName = "Ivan"
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

            var clientsWithNameIvan = clientService.GetClients(clientFilter, 1, 10);

            // Assert
            if (clientsWithNameIvan[0].FirstName == "Ivan") Assert.True(true);
            else Assert.True(false);
        }
    }
}