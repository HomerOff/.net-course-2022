using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db;

[Table("accounts")]
public class AccountDb
{
    [Key]
    [Column("account_id")]
    public Guid Id { get; set; }
    
    [Column("amount")]
    public float Amount { get; set; }
    
    [Required]
    [ForeignKey("client_id")]
    public ClientDb Client { get; set; }
    
    [Required]
    [ForeignKey("currency_id")]
    public CurrencyDb Currency { get; set; }

}