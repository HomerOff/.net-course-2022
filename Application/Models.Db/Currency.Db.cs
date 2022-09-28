using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db;

[Table("currencies")]
public class CurrencyDb
{
    [Key]
    [Column("currency_id")]
    public Guid Id { get; set; }
    
    [Required]
    [Column("code")]
    public int Code { get; set; }
    
    [Required]
    [Column("name")]
    public string Name { get; set; }

    public ICollection<AccountDb> Accounts { get; set; }
}