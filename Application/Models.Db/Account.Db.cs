using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db;

[Table("accounts")]
public class AccountDb
{
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("amount")]
    public float Amount { get; set; }
    
    [ForeignKey("client_id")]
    public ClientDb Client { get; set; }
    
    [ForeignKey("currency_id")]
    public CurrencyDb Currency { get; set; }

}