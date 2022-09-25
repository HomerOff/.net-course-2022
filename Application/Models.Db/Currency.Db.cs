using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db;

[Table("currencies")]
public class Currency
{
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("code")]
    public int Code { get; set; }
    
    [Column("name")]
    public string Name { get; set; }

    public ICollection<Account> Accounts { get; set; }
}