using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db;

[Table("clients")]
public class ClientDb
{
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("first_name")]
    public string FirstName { get; set; }
    
    [Column("last_name")]
    public string LastName { get; set; }
    
    [Column("passport")]
    public int Passport { get; set; }
    
    [Column("birthday_date")]
    public DateTime BirthdayDate { get; set; }
    
    [Column("phone_number")]
    public int PhoneNumber { get; set; }
    
    [Column("bonus")]
    public int Bonus { get; set; }
    
    public ICollection<AccountDb> Accounts { get; set; }
}