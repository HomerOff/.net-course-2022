using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db;

[Table("employees")]
public class EmployeeDb
{
    [Key]
    [Column("employee_id")]
    public Guid Id { get; set; }
    
    [Required]
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
    
    [Column("salary")]
    public int Salary { get; set; }
    
    [Column("contract")]
    public string Contract { get; set; }
}