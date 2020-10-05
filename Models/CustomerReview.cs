using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_SampleApp.Models
{
    public class CustomerReview : IEntity
    {
        public int CustomerReviewID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public Customer Customer { get; set; }
        public Product Product { get; set; }

        [Required]
        [Range(1,5)]
        public int Rating { get; set; }

        [StringLength(2000, MinimumLength = 3)]
        public string Review { get; set;  }
    }
}