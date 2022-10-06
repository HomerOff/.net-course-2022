using Microsoft.EntityFrameworkCore;
using Models.Db;
using Models;

namespace Services.Db;

public class ClientService
{
   private readonly ApplicationContext _dbContext;
   private readonly Guid usdCurrencyGuid;
    
    public ClientService()
    {
        _dbContext = new ApplicationContext();
        usdCurrencyGuid = Guid.Parse("e0ac3c2a-e395-4170-b2fe-441994419238");
    }

    public Guid AddClient(Client client)
    {
        var usdCurrency = new CurrencyDb
        {
            Id = usdCurrencyGuid,
            Code = 840,
            Name = "USD"
        };
        
        if (!_dbContext.Currencies.Contains(usdCurrency))
        {
            _dbContext.Currencies.Add(usdCurrency);
            _dbContext.SaveChanges();
        }
       
        var clientDb = new ClientDb
        {
            Id = Guid.NewGuid(),
            FirstName = client.FirstName,
            LastName = client.LastName,
            BirthdayDate = client.BirthdayDate,
            Bonus = client.Bonus,
            Passport = client.Passport,
            PhoneNumber = client.PhoneNumber
        };
        _dbContext.Clients.Add(clientDb);
        _dbContext.SaveChanges();

        var defaultAccount = new AccountDb
        {
            Id = Guid.NewGuid(),
            Amount = 0,
            Client = clientDb,
            Currency = _dbContext.Currencies.FirstOrDefault(c => c.Id == usdCurrencyGuid),
        };
        _dbContext.Accounts.Add(defaultAccount);
        _dbContext.SaveChanges();
        
        return clientDb.Id; 
    }
    
    public void UpdateClient(Client client, Guid clientId)
    {
        var oldDataClient = _dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
        oldDataClient = new ClientDb
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

    public void DeleteClient(Guid clientId)
    {
        var clientDb = _dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
        foreach (var account in clientDb.Accounts)
        {
            _dbContext.Accounts.Remove(account);
        }
        _dbContext.SaveChanges();

        var client = _dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
        _dbContext.Clients.Remove(client);
        
        _dbContext.SaveChanges();
    }
    
    public Client GetClient(Guid clientId)
    {
        var clientDb = _dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
        var client = new Client
        {
            FirstName = clientDb.FirstName,
            LastName = clientDb.LastName,
            Passport = clientDb.Passport,
            BirthdayDate = clientDb.BirthdayDate,
            PhoneNumber = clientDb.PhoneNumber,
            Bonus = clientDb.Bonus
        };
        
        return client;
    }
    
    public List<Client> GetClients(ClientFilter clientFilter, int page, int limit)
    {
        if (clientFilter.DateEnd == DateTime.MinValue)
        {
            clientFilter.DateEnd = DateTime.Today;
        }
        
        var selection = _dbContext.Clients.
            Where(c => c.BirthdayDate >= clientFilter.DateStart.ToUniversalTime()).
            Where(c => c.BirthdayDate <= clientFilter.DateEnd.ToUniversalTime())
            .AsNoTracking();

        if (!string.IsNullOrEmpty(clientFilter.FirstName))
            selection = selection.Where(c => c.FirstName == clientFilter.FirstName)
                .AsNoTracking();
        
        if (!string.IsNullOrEmpty(clientFilter.LastName))
            selection = selection.Where(c => c.LastName == clientFilter.LastName)
                .AsNoTracking();

        if (clientFilter.Passport != 0)
            selection = selection.Where(c => c.Passport == clientFilter.Passport)
                .AsNoTracking();
        
        if (clientFilter.PhoneNumber != 0)
            selection = selection.Where(c => c.PhoneNumber == clientFilter.PhoneNumber)
                .AsNoTracking();
        
        if (clientFilter.Bonus != 0)
            selection = selection.Where(c => c.Bonus == clientFilter.Bonus)
                .AsNoTracking();
        
        var clientsDb = selection.Skip((page - 1) * limit).Take(limit).ToList();
        var clients = new List<Client>();
        
        for (int i = 0; i < clientsDb.Count; i++)
        {
            var client = new Client
            {
                FirstName = clientsDb[i].FirstName,
                LastName = clientsDb[i].LastName,
                Passport = clientsDb[i].Passport,
                BirthdayDate = clientsDb[i].BirthdayDate,
                PhoneNumber = clientsDb[i].PhoneNumber,
                Bonus = clientsDb[i].Bonus
            };
            clients.Add(client);
        }
        
        return clients;
    }
    
    public void AddAccount(Guid clientId)
    {
        var usdCurrencyGuid = Guid.Parse("e0ac3c2a-e395-4170-b2fe-441994419238");
        var client = _dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
        var newAccount = new AccountDb
        {
            Id = Guid.NewGuid(),
            Amount = 0,
            Client = client,
            Currency = _dbContext.Currencies.FirstOrDefault(c => c.Id == usdCurrencyGuid),
        };
        _dbContext.Accounts.Add(newAccount);
        
        _dbContext.SaveChanges();
    }
    
    public void DeleteAccount(Guid accountId)
    {
        var account = _dbContext.Accounts.FirstOrDefault(c => c.Id == accountId);
        _dbContext.Accounts.Remove(account);
        
        _dbContext.SaveChanges();
    }
    
    public List<Guid> GetAccounts(Guid clientId)
    {
        var accountsDb = _dbContext.Clients.FirstOrDefault(c => c.Id == clientId).Accounts.ToList();
        var accounts = new List<Guid>();

        foreach (var account in accountsDb)
        {
            accounts.Add(account.Id);
        }
        return accounts;
    }
}