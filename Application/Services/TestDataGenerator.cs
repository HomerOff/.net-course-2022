using Models;
using Bogus;
using Bogus.DataSets;

namespace Services;

public class TestDataGenerator
{
    
    public List<Client> GetClientList(int count)
    {
        var randClinet = new Faker<Client>()
            .CustomInstantiator(f => new Client())
            .RuleFor(c => c.FirstName, f => f.Name.FirstName(f.PickRandom<Name.Gender>()))
            .RuleFor(c => c.LastName, f => f.Name.LastName(f.PickRandom<Name.Gender>()))
            .RuleFor(c => c.BirthdayDate, f => f.Date.Past(100).ToUniversalTime())
            .RuleFor(c => c.Passport, f => int.Parse(f.Random.ReplaceNumbers("#######")))
            .RuleFor(c => c.PhoneNumber, f => int.Parse(f.Random.ReplaceNumbers("7#######")));
        var genClient = randClinet.Generate(count);
        return new List<Client>(genClient.ToList());
    }
    
    public Dictionary<int, Client> GetClientDictionary(List<Client> clients)
    {
        return new Dictionary<int, Client>(clients.ToDictionary(k => k.PhoneNumber, v => v));
    }
    
    public List<Employee> GetEmployeeList(int count)
    {
        var randEmployee = new Faker<Employee>()
            .CustomInstantiator(f => new Employee())
            .RuleFor(e => e.FirstName, f => f.Name.FirstName(f.PickRandom<Name.Gender>()))
            .RuleFor(e => e.LastName, f => f.Name.LastName(f.PickRandom<Name.Gender>()))
            .RuleFor(e => e.BirthdayDate, f => f.Date.Past(100).ToUniversalTime())
            .RuleFor(e => e.Passport, f => int.Parse(f.Random.ReplaceNumbers("#######")))
            .RuleFor(e => e.PhoneNumber, f => int.Parse(f.Random.ReplaceNumbers("7#######")))
            .RuleFor(e => e.Salary, f => f.Random.Int(1000, 20000));
        var genEmployee = randEmployee.Generate(count);
        return new List<Employee>(genEmployee.ToList());
    }

    public Dictionary<Client, List<Account>> GetClientAccountDictionary(List<Client> clients)
    {
        var globUsdCurrency = new Models.Currency
        {
            Code = 840,
            Name = "USD"
        };
        var globEurCurrency = new Models.Currency
        {
            Code = 978,
            Name = "EUR"
        };
        
        var randUsdAccount = new Faker<Account>()
            .CustomInstantiator(f => new Account())
            .RuleFor(a => a.Amount, f => f.Random.Float(0F, 99999F))
            .RuleFor(a => a.Currency, globUsdCurrency);
        var usdAccountList = randUsdAccount.Generate(clients.Count);

        var randEurAccount = new Faker<Account>()
            .CustomInstantiator(f => new Account())
            .RuleFor(a => a.Amount, f => f.Random.Float(0F, 99999F))
            .RuleFor(a => a.Currency, globEurCurrency);
        var eurAccountList = randEurAccount.Generate(clients.Count);
        
        var clientAccountDict = new Dictionary<Client, List<Account>>();
        for (var i = 0; i < clients.Count; i++)
        {
            var accounts = new List<Account>
            {
                usdAccountList[i],
                eurAccountList[i],
            };
            clientAccountDict.Add(clients[i], accounts);
        }

        return clientAccountDict;
    }
}