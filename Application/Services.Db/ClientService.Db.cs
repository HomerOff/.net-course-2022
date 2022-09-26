using Microsoft.EntityFrameworkCore;
using Models.Db;

namespace Services.Db;

public class ClientService
{
   private readonly ApplicationContext _dbContext;

    public ClientService()
    {
        _dbContext = new ApplicationContext();
    }

    public void AddClient(ClientDb client)
    {
        _dbContext.Clients.Add(client);
        _dbContext.SaveChanges();
        
        var defaultAccount = new AccountDb
        {
            Id = Guid.NewGuid(),
            Amount = 0,
            Client = client
        };
        _dbContext.Accounts.Add(defaultAccount);
        
        _dbContext.SaveChanges();
    }
    
    public void UpdateClient(ClientDb client, Guid clientId)
    {
        var oldDataClient = GetClient(clientId);
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

    public void DelClient(Guid clientId)
    {
        foreach (var account in GetAccounts(clientId))
        {
            _dbContext.Accounts.Remove(account);
        }
        _dbContext.SaveChanges();
        
        _dbContext.Clients.Remove(GetClient(clientId));
        
        _dbContext.SaveChanges();
    }
    
    public ClientDb GetClient(Guid clientId)
    {
        return _dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
    }
    
    public List<ClientDb> GetClients(ClientFilter clientFilter)
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
    
    public void AddAccount(Guid clientId)
    {
        var newAccount = new AccountDb
        {
            Id = Guid.NewGuid(),
            Amount = 0,
            Client = GetClient(clientId)
        };
        _dbContext.Accounts.Add(newAccount);
        
        _dbContext.SaveChanges();
    }
    
    public void DelAccount(Guid accountId)
    {
        var account = _dbContext.Accounts.FirstOrDefault(c => c.Id == accountId);
        _dbContext.Accounts.Remove(account);
        
        _dbContext.SaveChanges();
    }
    
    public List<AccountDb> GetAccounts(Guid clientId)
    {
        return GetClient(clientId).Accounts.ToList();
    }
}