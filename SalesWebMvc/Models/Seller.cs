using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations; //Namespace para utilizar o [Display].

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]//Utilizado para definir um formato de exibição para o atributo da classe.
        public string Email { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]//Utilizado para definir um formato de exibição para o atributo da classe conforme está entre as chaves..
        [Display(Name = "Birth Date")] //Utilizado para definir um nome de exibição para o atributo da classe.
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

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
