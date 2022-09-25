using Models.Db;

namespace Services.Db;

public class ClientService
{
   private readonly ApplicationContext _dbContext;

    public ClientService()
    {
        _dbContext = new ApplicationContext();
    }

    public Guid AddClient(Client client)
    {
        var defaultAccount = new Account
        {
            Id = Guid.NewGuid(),
            Amount = 0,
            ClientId = client.Id
        };
        
        _dbContext.Clients.Add(client);
        _dbContext.Accounts.Add(defaultAccount);
        
        _dbContext.SaveChanges();

        return defaultAccount.Id;
    }
    
    public void UpdateClient(Client client, Guid clientId)
    {
        var oldDataClient = _dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
        oldDataClient = new Client
        {
            FirstName = client.FirstName,
            LastName = client.LastName,
            Passport = client.Passport,
            BirthdayDate = client.BirthdayDate,
            PhoneNumber = client.PhoneNumber,
            Bonus = client.Bonus
        };
        
        _dbContext.SaveChanges();
    }

    public void DelClient(Guid clientId)
    {
        var client = _dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
        _dbContext.Clients.Remove(client);
        _dbContext.SaveChanges();
    }
    
    public Client? GetClient(Guid clientId)
    {
        return _dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
    }
    
    public List<Client> GetClients(ClientFilter clientFilter)
    {
        if (clientFilter.DateEnd == DateTime.MinValue)
        {
            clientFilter.DateEnd = DateTime.Today;
        }
        
        var selection = _dbContext.Clients.
            Where(c => c.BirthdayDate >= clientFilter.DateStart.ToUniversalTime()).
            Where(c => c.BirthdayDate <= clientFilter.DateEnd.ToUniversalTime());

        if (!string.IsNullOrEmpty(clientFilter.FirstName))
            selection = selection.Where(c => c.FirstName == clientFilter.FirstName);
        
        if (!string.IsNullOrEmpty(clientFilter.LastName))
            selection = selection.Where(c => c.LastName == clientFilter.LastName);

        if (clientFilter.Passport != 0)
            selection = selection.Where(c => c.Passport == clientFilter.Passport);
        
        if (clientFilter.PhoneNumber != 0)
            selection = selection.Where(c => c.PhoneNumber == clientFilter.PhoneNumber);
        
        if (clientFilter.Bonus != 0)
            selection = selection.Where(c => c.Bonus == clientFilter.Bonus);

        return selection.ToList();
    }
    
    public Guid AddAccount(Guid clientId)
    {
        var newAccount = new Account
        {
            Id = Guid.NewGuid(),
            Amount = 0,
            ClientId = clientId
        };
        _dbContext.Accounts.Add(newAccount);
        
        _dbContext.SaveChanges();
        
        return newAccount.Id;
    }
    
    public void DelAccount(Guid accountId)
    {
        var account = _dbContext.Accounts.FirstOrDefault(c => c.Id == accountId);
        _dbContext.Accounts.Remove(account);
        _dbContext.SaveChanges();
    }
}