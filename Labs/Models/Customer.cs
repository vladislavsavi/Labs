using System.ComponentModel.DataAnnotations.Schema;

namespace Labs.Models
{
    [Table("Customer", Schema = "SalesLT")]
    public class Customer
    {
        public int CustomerID { get; set; }
        public string? MiddleName { get; set; }
    }
}
