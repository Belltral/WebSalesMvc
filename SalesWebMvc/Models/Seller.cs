using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations; //Namespace para utilizar annotations.

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        //[StringLength(60, MinimumLength = 3, ErrorMessage = "Name size should be between 3 and 60")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }
                
        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid {0}")]
        [DataType(DataType.EmailAddress)]//Utilizado para definir um formato de exibição para o atributo da classe.
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]//Utilizado para definir um formato de exibição para o atributo da classe conforme está entre as chaves..
        [Display(Name = "Birth Date")] //Utilizado para definir um nome de exibição para o atributo da classe.
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Display(Name = "Base Salary")]
        public double BaseSalary { get; set; }

        public Department Department { get; set; }

        [Display(Name = "Departament")]
        public int DepartmentId { get; set; }

        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
