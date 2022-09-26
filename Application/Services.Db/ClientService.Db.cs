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

    public void DelClient(Guid clientId)
    {
        foreach (var account in GetAccounts(clientId))
        {
            _dbContext.Accounts.Remove(account);
        }
        _dbContext.SaveChanges();

        var client = _dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
        _dbContext.Clients.Remove(client);
        
        _dbContext.SaveChanges();
    }
    
    public ClientDb GetClient(Guid clientId)
    {
        return _dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
    }
    
    public List<ClientDb> GetClients(ClientFilter clientFilter, int page, int limit)
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

        return selection.Skip((page - 1) * limit).Take(limit).ToList();
    }
    
    public void AddAccount(Guid clientId)
    {
        var client = _dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
        var newAccount = new AccountDb
        {
            Id = Guid.NewGuid(),
            Amount = 0,
            Client = client
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
        return _dbContext.Clients.FirstOrDefault(c => c.Id == clientId).Accounts.ToList();
    }
}