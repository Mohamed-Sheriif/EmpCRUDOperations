using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace EmployeeApp.Models
{
    [Table("Department" , Schema ="dbo")]
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Department Id")]
        public int DepartmentId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        [Display(Name = "Department Name")]
        public String DepartmentName { get; set; }
        
        [Required]
        [Column(TypeName = "nvarchar(5)")]
        [Display(Name = "Department Abbreviation")]
        public String DepartmentAbbr { get; set; }
    }
}
