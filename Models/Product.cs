using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_SampleApp.Models
{
    public class Product: IEntity
    {
        public int ProductID { get; set; }
        
        [Required]
        [Display(Name = "Number")]
        public int ProductNumber { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        public int DepartmentID { get; set; }

        public Department Department { get; set; }
        public ICollection<CustomerReview> CustomerReviews { get; set; }

        [NotMapped]
        [DisplayName("Avg Rating")]
        public decimal AverageRating { get; set; }

        //TODO: Add Orders->ProductOrders
        //public ICollection<ProductOrders> Sales { get; set; }
    }
}