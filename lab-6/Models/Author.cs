using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace lab_6.Models;
[Table("Authors")]
public class Author
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [Column("first_name")]
    [StringLength(30)]
    public string FirstName { get; set; }

    [Column("last_name"), Required, StringLength(20)]
    public string LastName { get; set; }
    [Column("pesel")]
    [StringLength(11)]
    public string PESEL { get; set; }
    public ISet<Book> Books { get; set; }
}