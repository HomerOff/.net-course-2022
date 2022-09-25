using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db;

[Table("accounts")]
public class Account
{
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("amount")]
    public float Amount { get; set; }
    
    [ForeignKey("client_id")] 
    public Guid ClientId { get; set; }
    public Client Client { get; set; }

    public ICollection<Currency> Currencies { get; set; }
}