using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC_SampleApp.Models
{
    public class ProductDTO : IMappable<Product, ProductDTO>
    {
        public int ProductID { get; set; }

        [Required]
        [Display(Name = "Number")]
        public int ProductNumber { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int DepartmentID { get; set; }

        public Department Department { get; set; }
        public ICollection<CustomerReview> CustomerReviews { get; set; }

        [DisplayName("Avg Rating")]
        public decimal AverageRating { get; set; }

        //TODO: Add Orders->ProductOrders
        //public ICollection<ProductOrders> Sales { get; set; }

        //TODO replace with AutoMapper
        
        //Only map Fields that are allowed to be edited externally
        public Product MapToEntity(Product p)
        {
            p.ProductNumber = ProductNumber;
            p.Price = Price;
            p.Name = Name;
            p.DepartmentID = DepartmentID;

            return p;
        }

        //Map all fields to be exposed externally
        public ProductDTO MapToDTO(Product p)
        {
            AverageRating = p.AverageRating;
            CustomerReviews = p.CustomerReviews;
            Department = p.Department;
            DepartmentID = p.DepartmentID;
            Name = p.Name;
            Price = p.Price;
            ProductID = p.ProductID;
            ProductNumber = p.ProductNumber;

            return this;
        }
    }
}