using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Models
{
    [Table("Employee" , Schema = "dbo")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }
        
        [Required]
        [Column(TypeName = "nvarchar(5)")]
        [MaxLength(5)]
        [Display(Name = "Employee Number")]
        public String EmployeeNumber { get; set; }
        
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        [MaxLength(150)]
        [Display (Name = "Employee Name")]
        public String EmployeeName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DOB { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Hiring Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime HiringDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        [Display(Name = "Gross Salary")]
        public decimal GrossSalary { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        [Display(Name = "Net Salary")]
        public decimal NetSalary { get; set; }

        [ForeignKey("Department")]
        [Required]
        public int DepartmentId { get; set; }

        [Display(Name = "Department")]
        [NotMapped]
        public String DepartmentName { get; set; }
        
        public virtual Department Department { get; set; }
    }
}
