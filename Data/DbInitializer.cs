using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVC_SampleApp.Models;

namespace MVC_SampleApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ProductContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any customers.
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }

            var customers = new Customer[]
            {
                new Customer { FirstMidName = "Carson",   LastName = "Alexander",
                    EnrollmentDate = DateTime.Parse("2010-09-01") },
                new Customer { FirstMidName = "Meredith", LastName = "Alonso",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Customer { FirstMidName = "Arturo",   LastName = "Anand",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Customer { FirstMidName = "Gytis",    LastName = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Customer { FirstMidName = "Yan",      LastName = "Li",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Customer { FirstMidName = "Peggy",    LastName = "Justice",
                    EnrollmentDate = DateTime.Parse("2011-09-01") },
                new Customer { FirstMidName = "Laura",    LastName = "Norman",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Customer { FirstMidName = "Nino",     LastName = "Olivetto",
                    EnrollmentDate = DateTime.Parse("2005-09-01") }
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();

            var departments = new Department[]
            {
                new Department { Name = "Shoes",     Budget = 350000,
                    StartDate = DateTime.Parse("2020-01-01")},
                new Department { Name = "Electronics", Budget = 100000,
                    StartDate = DateTime.Parse("2020-01-01")},
                new Department { Name = "Automotive", Budget = 350000,
                    StartDate = DateTime.Parse("2020-01-01")},
                new Department { Name = "Health",   Budget = 100000,
                    StartDate = DateTime.Parse("2020-01-01")},
                new Department { Name = "Grocery",   Budget = 250000,
                    StartDate = DateTime.Parse("2020-01-01")}
            };

            context.Departments.AddRange(departments);
            context.SaveChanges();

            var products = new Product[]
            {
                new Product {ProductNumber = 1050, Name = "Truck Battery",      Price = 125.75M,
                    DepartmentID = departments.Single( s => s.Name == "Automotive").DepartmentID
                },
                new Product {ProductNumber = 4022, Name = "Cold Medicine", Price = 3.95M,
                    DepartmentID = departments.Single( s => s.Name == "Health").DepartmentID
                },
                new Product {ProductNumber = 4041, Name = "Knee Brace", Price = 7.75M,
                    DepartmentID = departments.Single( s => s.Name == "Health").DepartmentID
                },
                new Product {ProductNumber = 1045, Name = "Flat Screen TV",       Price = 975.00M,
                    DepartmentID = departments.Single( s => s.Name == "Electronics").DepartmentID
                },
                new Product {ProductNumber = 3141, Name = "Game System",   Price = 499.99M,
                    DepartmentID = departments.Single( s => s.Name == "Electronics").DepartmentID
                },
                new Product {ProductNumber = 2021, Name = "Tennis shoe",    Price = 39.99M,
                    DepartmentID = departments.Single( s => s.Name == "Shoes").DepartmentID
                },
                new Product {ProductNumber = 2075, Name = "Basketball trainers",     Price = 125.95M,
                    DepartmentID = departments.Single( s => s.Name == "Shoes").DepartmentID
                },
                new Product {ProductNumber = 2042, Name = "Hamburger ",     Price = 2.99M,
                    DepartmentID = departments.Single( s => s.Name == "Grocery").DepartmentID
                },
            };

            foreach (Product c in products)
            {
                context.Products.Add(c);
            }
            context.SaveChanges();

            var customerReviews = new CustomerReview[]
            {
                new CustomerReview {
                    CustomerID = customers.Single( s=> s.LastName == "Olivetto").ID,
                    ProductID = products.Single(c => c.Name == "Truck Battery" ).ProductID,
                    Rating = 3,
                    Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Consectetur purus ut faucibus pulvinar elementum integer enim."
                },
                new CustomerReview
                {
                    CustomerID = customers.Single( s=> s.LastName == "Olivetto").ID,
                    ProductID = products.Single(c => c.Name == "Cold Medicine" ).ProductID,
                    Rating = 4,
                    Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Consectetur purus ut faucibus pulvinar elementum integer enim."
                },
                new CustomerReview
                {
                    CustomerID = customers.Single( s=> s.LastName == "Olivetto").ID,
                    ProductID = products.Single(c => c.Name == "Game System" ).ProductID,
                    Rating = 1,
                    Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Consectetur purus ut faucibus pulvinar elementum integer enim."
                },
                new CustomerReview
                {
                    CustomerID = customers.Single( s=> s.LastName == "Olivetto").ID,
                    ProductID = products.Single(c => c.Name == "Tennis shoe" ).ProductID,
                    Rating = 2,
                    Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                },
                new CustomerReview
                {
                    CustomerID = customers.Single( s=> s.LastName == "Alonso").ID,
                    ProductID = products.Single(c => c.Name == "Basketball trainers" ).ProductID,
                    Rating = 1,
                    Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Consectetur purus ut faucibus pulvinar elementum integer enim."
                },
                new CustomerReview
                {
                    CustomerID = customers.Single( s=> s.LastName == "Alonso").ID,
                    ProductID = products.Single(c => c.Name == "Game System" ).ProductID,
                    Rating = 5,
                    Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Consectetur purus ut faucibus pulvinar elementum integer enim."
                },
                new CustomerReview
                {
                    CustomerID = customers.Single( s=> s.LastName == "Alonso").ID,
                    ProductID = products.Single(c => c.Name == "Flat Screen TV" ).ProductID,
                    Rating = 4,
                    Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                },
                new CustomerReview
                {
                    CustomerID = customers.Single( s=> s.LastName == "Barzdukas").ID,
                    ProductID = products.Single(c => c.Name == "Game System" ).ProductID,
                    Rating = 4,
                    Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Consectetur purus ut faucibus pulvinar elementum integer enim."
                },
                new CustomerReview
                {
                    CustomerID = customers.Single( s=> s.LastName == "Barzdukas").ID,
                    ProductID = products.Single(c => c.Name == "Flat Screen TV" ).ProductID,
                    Rating = 2,
                    Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                },
                new CustomerReview
                {
                    CustomerID = customers.Single( s=> s.LastName == "Li").ID,
                    ProductID = products.Single(c => c.Name == "Flat Screen TV" ).ProductID,
                    Rating = 3,
                    Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                },
                new CustomerReview
                {
                    CustomerID = customers.Single( s=> s.LastName == "Norman").ID,
                    ProductID = products.Single(c => c.Name == "Flat Screen TV" ).ProductID,
                    Rating = 1,
                    Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Semper viverra nam libero justo laoreet. A erat nam at lectus. Augue interdum velit euismod in. Pretium lectus quam id leo. Enim ut tellus elementum sagittis vitae et leo. In dictum non consectetur a erat nam at lectus. Scelerisque mauris pellentesque pulvinar pellentesque habitant morbi. Felis eget velit aliquet sagittis id consectetur purus ut faucibus. Imperdiet sed euismod nisi porta lorem mollis aliquam. Eu tincidunt tortor aliquam nulla facilisi cras. Tellus id interdum velit laoreet id donec ultrices tincidunt. Non tellus orci ac auctor augue mauris augue. Faucibus turpis in eu mi bibendum neque egestas congue. Velit euismod in pellentesque massa."
                },
                new CustomerReview
                {
                    CustomerID = customers.Single( s=> s.LastName == "Norman").ID,
                    ProductID = products.Single(c => c.Name == "Game System" ).ProductID,
                    Rating = 4,
                    Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Etiam non quam lacus suspendisse faucibus interdum posuere. Et ultrices neque ornare aenean. A cras semper auctor neque vitae. Tristique senectus et netus et malesuada fames."
                },
                new CustomerReview
                {
                    CustomerID = customers.Single( s=> s.LastName == "Norman").ID,
                    ProductID = products.Single(c => c.Name == "Tennis shoe" ).ProductID,
                    Rating = 3,
                    Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Etiam non quam lacus suspendisse faucibus interdum posuere. Et ultrices neque ornare aenean. A cras semper auctor neque vitae. Tristique senectus et netus et malesuada fames."
                },
                new CustomerReview
                {
                    CustomerID = customers.Single( s=> s.LastName == "Norman").ID,
                    ProductID = products.Single(c => c.Name == "Knee Brace" ).ProductID,
                    Rating = 4,
                    Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                },
                new CustomerReview
                {
                    CustomerID = customers.Single( s=> s.LastName == "Alexander").ID,
                    ProductID = products.Single(c => c.Name == "Game System" ).ProductID,
                    Rating = 4,
                    Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Etiam non quam lacus suspendisse faucibus interdum posuere. Et ultrices neque ornare aenean. A cras semper auctor neque vitae. Tristique senectus et netus et malesuada fames."
                },
            };

            context.CustomerReviews.AddRange(customerReviews);
            context.SaveChanges();          
        }
    }
}