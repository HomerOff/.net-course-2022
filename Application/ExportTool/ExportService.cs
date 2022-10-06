using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Models;

namespace ExportTool;

public class ExportService
{
    private string _pathToDirecory { get; set; }
    private string _csvFileName { get; set; }
    
    public ExportService(string pathToDirectory, string csvFileName)
    {
        _pathToDirecory = pathToDirectory;
        _csvFileName = csvFileName;
    }
    
    public void WriteClientsToCsv(List<Client> clients)
    {
        var dirInfo = new DirectoryInfo(_pathToDirecory);
        if (!dirInfo.Exists)
        {
            dirInfo.Create();
        }

        var fullPath = Path.Combine(_pathToDirecory, _csvFileName);

        using var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
        using var streamWriter = new StreamWriter(fileStream);
        using var writer = new CsvWriter(streamWriter, CultureInfo.CurrentCulture);

        writer.WriteRecords(clients);
            
        writer.NextRecord();
            
        writer.Flush();
    }
    
    public List<Client> ReadClientsFromCsv()
    {
        var clients = new List<Client>();
        
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
        };
        
        var fullPath = Path.Combine(_pathToDirecory, _csvFileName);
        using var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
        using var streamReader = new StreamReader(fileStream);
        using var reader = new CsvReader(streamReader, config);

        reader.Read();
        
        while (reader.Read())
        {
            clients.Add(new Client
            {
                FirstName = reader.GetField(0),
                LastName = reader.GetField(1),
                Passport =  Convert.ToInt32(reader.GetField(2)),
                BirthdayDate = Convert.ToDateTime(reader.GetField(3)).ToUniversalTime(),
                PhoneNumber = Convert.ToInt32(reader.GetField(4)),
                Bonus = Convert.ToInt32(reader.GetField(5)),
            });
        }
        
        return clients;
    }
}