using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_SampleApp.Data;
using MVC_SampleApp.Models;

namespace MVC_SampleApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductContext _context;

        public ProductsController(ProductContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["ProductNumSortParm"] = sortOrder == "productnum" ? "productnum_desc" : "productnum";
            ViewData["RatingSortParm"] = sortOrder == "rating" ? "rating_desc" : "rating";
            ViewData["DeptSortParm"] = sortOrder == "dept" ? "dept_desc" : "dept";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var products = from p in _context.Products select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                string lowerSearchString = searchString.ToLower();
                products = products.Where(p => p.ProductNumber.ToString() == searchString
                                       || p.Name.ToLower().Contains(lowerSearchString)
                                       || p.Department.Name.ToLower().Equals(lowerSearchString));
            }

            var reviewAvg = _context.CustomerReviews
                                    .GroupBy(cr => cr.ProductID).Select(g => new { ProductID = g.Key, Rtg = g.Average(cr => (decimal)cr.Rating) });

            var query =
               from prods in products
               join reviews in reviewAvg on prods.ProductID equals reviews.ProductID into joinedT
               from reviews in joinedT.DefaultIfEmpty()
               select (new ProductDTO()
               {
                   AverageRating = reviews.Rtg,
                   CustomerReviews = prods.CustomerReviews,
                   Department = prods.Department,
                   DepartmentID = prods.DepartmentID,
                   Name = prods.Name,
                   Price = prods.Price,
                   ProductNumber = prods.ProductNumber,
                   ProductID = prods.ProductID
               });

            switch (sortOrder)
            {
                case "name":
                    query = query.OrderBy(s => s.Name).ThenByDescending(s => s.AverageRating);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(s => s.Name).ThenByDescending(s => s.AverageRating);
                    break;
                case "price":
                    query = query.OrderBy(s => s.Price).ThenByDescending(s => s.AverageRating);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(s => s.Price).ThenByDescending(s => s.AverageRating);
                    break;
                case "rating":
                    query = query.OrderBy(s => s.AverageRating);
                    break;
                case "rating_desc":
                    query = query.OrderByDescending(s => s.AverageRating);
                    break;
                case "productnum":
                    query = query.OrderBy(s => s.ProductNumber).ThenByDescending(s => s.AverageRating);
                    break;
                case "productnum_desc":
                    query = query.OrderByDescending(s => s.ProductNumber).ThenByDescending(s => s.AverageRating);
                    break;
                case "dept":
                    query = query.OrderBy(s => s.Department.Name).ThenByDescending(s => s.AverageRating);
                    break;
                case "dept_desc":
                    query = query.OrderByDescending(s => s.Department.Name).ThenByDescending(s => s.AverageRating);
                    break;
                default:
                    query = query.OrderByDescending(s => s.AverageRating);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<ProductDTO>.CreateAsync(query.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(c => c.Department)
                .Where(m => m.ProductID == id)
                .AsNoTracking()
                .Select( p => new ProductDTO().MapToDTO(p))
                .FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                _context.Add( product.MapToEntity(new Product()));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDepartmentsDropDownList(product.DepartmentID);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .AsNoTracking()
                .Where(m => m.ProductID == id)
                .Select( m => new ProductDTO().MapToDTO(m))
                .FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }
            PopulateDepartmentsDropDownList(product.DepartmentID);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(ProductDTO productDto, int? id)
        {
            if (id == null || productDto == null)
            {
                return NotFound();
            }

            var productToUpdate = await _context.Products
                .FirstOrDefaultAsync(c => c.ProductID == id);
            
            try
            {
                productToUpdate = productDto.MapToEntity(productToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }

            PopulateDepartmentsDropDownList(productToUpdate.DepartmentID);
            return View(productToUpdate);
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = from d in _context.Departments
                                   orderby d.Name
                                   select d;
            ViewBag.DepartmentID = new SelectList(departmentsQuery.AsNoTracking(), "DepartmentID", "Name", selectedDepartment);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(c => c.Department)
                .Where(m => m.ProductID == id)
                .AsNoTracking()
                .Select(m => new ProductDTO().MapToDTO(m))
                .FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
