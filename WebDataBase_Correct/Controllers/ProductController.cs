using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebDataBase_Correct.DB;
using WebDataBase_Correct.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebDataBase_Correct.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        private EcommerceContext _db;

        public Product Product { get; private set; }

        public ProductController(EcommerceContext db)
        {
            _db = db;
        }

        public ActionResult Index(string name ,int? priceMin,int? priceMax, int page = 0)
        {
            IQueryable<Product> products;
            
            if (name == null)
            {
                if (Request.Cookies.ContainsKey("NameFiltr"))
                    name = Request.Cookies["NameFiltr"];
            }
            else 
            {
                if (name == "~~clear~~")
                {
                    Response.Cookies.Delete("NameFiltr");
                    Response.Cookies.Delete("PriceMax");
                    Response.Cookies.Delete("PriceMin");                    
                    priceMax = null;
                    priceMin = null;
                    
                }
                else
                    Response.Cookies.Append("NameFiltr", name);
                    
            }
            if (priceMax == null)
            {
                if (Request.Cookies.ContainsKey("PriceMax") && name!= "~~clear~~")
                    priceMax = Int32.Parse(Request.Cookies["PriceMax"]);
            }
            else
            {
                Response.Cookies.Append("PriceMax", priceMax.ToString());
            }
            if (priceMin == null)
            {
                if (Request.Cookies.ContainsKey("PriceMin") && name != "~~clear~~")
                    priceMin = Int32.Parse(Request.Cookies["PriceMin"]);
            }
            else
            {
                Response.Cookies.Append("PriceMin", priceMin.ToString());
            }

            if (name == "~~clear~~")
            {
                name = null;
            }


            if (name != null && name != "")
                products = _db.Products.Where(p => p.Name.StartsWith(name));
            
            else
                products = _db.Products;

            if(priceMin!=null)
            {
                products = products.Where(p => p.Price >= priceMin);
            }
            if (priceMax != null)
            {
                products = products.Where(p => p.Price <= priceMax);
            }
            

            products = products.Include(pr => pr.Category);
            //Response.Cookies.Append("NameFiltr", name);
            //Request.Cookies.TryGetValue("NameFilr", name.ToString());
            ViewBag.CurrentFiltr = name;
            ViewBag.PriceMin = priceMin;
            ViewBag.PriceMax = priceMax;
            

            int count = products.Count();
            int ProductOnPage = 3;
            int PageCount = (int)Math.Ceiling(count / (double)ProductOnPage);
            ViewBag.PageCount = PageCount;
            ViewBag.CurrentPage = page;

            //ViewData["PageCount"] = PageCount;


            products = products.Skip(page * ProductOnPage).Take(ProductOnPage);
            return View( "ProductList", products.ToList());
        }

        public ActionResult ShowProductByCategory(int categoryId)
        {
            var products = _db.Products.Where(x => x.CategoryId == categoryId);
            return View("ProductList", products.ToList());
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            Product product = _db.Products.Include(pr=>pr.Category).Where(pr => pr.Id == id).FirstOrDefault();
            if (product == null) return NotFound();
            return View(product);
        }

        // GET: ProductController/Create
        [HttpGet]
        public ActionResult Create()
        {
            Product product = new Product();
            ViewBag.categoryList = GetCategoryList(product.CategoryId);
            product.ProductionDate = DateTime.Now;
            product.Amount = 1;
            product.Discount = 0;
            return View(product);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product item)
        {
            try
            {
                if (!ModelState.IsValid) throw new Exception();
                
                _db.Products.Add(item);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(item);
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = _db.Products.Include(pr=>pr.Category).Where(pr => pr.Id == id).FirstOrDefault();
            ViewBag.categoryList = GetCategoryList(product.CategoryId.Value);
            if (product == null) return View("Error", new ErrorViewModel() { ErrorMessage = " No such Product" });
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product item)
        {
            try
            {
                _db.Products.Update(item);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            Product product = _db.Products.Include(pr => pr.Category).Where(pr=>pr.Id==id).First();
            if (product == null) return View("Error", new ErrorViewModel() { ErrorMessage = " No such Product" });

            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, string confirm)
        {
            try
            {
                if (confirm == "Delete")
                {
                    _db.Products.Remove(_db.Products.Find(id));
                    _db.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View("Error", new ErrorViewModel() { ErrorMessage = ex.Message });
            }
        }
        [NonAction]
        public SelectList GetCategoryList(int? categoryId)
        {
            _db.Categories.Load();
            return new SelectList(_db.Categories, "CategoryId", "Name", categoryId);
            
        }
    }
}
