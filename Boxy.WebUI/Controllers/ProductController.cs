using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Boxy.Domain.Entities;
using Boxy.BPM;
using System.IO;
using System.Data.Entity;
using System.Threading.Tasks;
using PagedList.Mvc;
using PagedList;
using Microsoft.AspNet.Identity;

namespace Boxy.WebUI.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        public ProductController()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            SelectList category = new SelectList(db.Categories, "Id", "Name");
            ViewBag.Category = category;
        }
        public int PageSize = 25;

        public ActionResult Index(int? page, string term)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                int pageSize = 20;
                int pageNumber = (page ?? 1);
                ViewBag.count = db.Products.ToList().Count();
                if (term != null)
                {
                    return PartialView(db.Products.ToList().Where(p => p.Name.ToLower().Contains(term.ToLower())).ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    return View(db.Products.ToList().ToPagedList(pageNumber, pageSize));
                }
                
            }
        }

        public ActionResult AutocompleteSearch(string term)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var models = db.Products.ToList().Where(a => a.Name.ToLower().Contains(term.ToLower()))
                                .Select(a => new { value = a.Name })
                                .Distinct();

                return Json(models, JsonRequestBehavior.AllowGet);
            }
                
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Product product, IEnumerable<HttpPostedFileBase> Files)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                
                foreach (var a in Files)
                {
                    if (a != null)
                    {
                        string fileName = Path.GetFileName(a.FileName);
                        a.SaveAs(Server.MapPath("~/Files/" + fileName));
                        product.Files.Add(new UploadFile { Path = "Files/" + fileName });
                    }
                }


                product.ApplicationUserId = User.Identity.GetUserId();
                product.Alias = Functions.AliasGenerator(product.Name, context.Products.ToList().Select(a => a.Alias));
                context.Products.Add(product);
                context.SaveChanges();
                return RedirectToAction("Add");
            }

          
        }


        public ActionResult AddCategory(int? page)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                int pageSize = 20;
                int pageNumber = (page ?? 1);
                ViewBag.count = db.Categories.ToList().Count();
                return View(db.Categories.ToList().ToPagedList(pageNumber, pageSize));
            }
        }


        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                category.Alias = Functions.AliasGenerator(category.Name, context.Categories.ToList().Select(a => a.Alias));
                context.Categories.Add(category);
                context.SaveChanges();
            }
            return RedirectToAction("AddCategory");
        }

        [HttpGet]
        public ActionResult EditProduct(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                Product product = context.Products.Find(id);
                if (product != null)
                {
                    return View(product);
                }

                return HttpNotFound();
            }

        }

        [HttpPost]
        public ActionResult EditProduct(Product product, HttpPostedFileBase uploadImage, int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Entry(product).State = EntityState.Modified;
                //меняем изображение
               // if (uploadImage != null)
               // {
               //     byte[] imageData = null;
               //     using (var binData = new BinaryReader(uploadImage.InputStream))
               //     {
               //         imageData = binData.ReadBytes(uploadImage.ContentLength);
                //    }
                //    product.Image = imageData;
               // }
                //если не запостили новое изображение, то оставляем все как есть
                //else
                //{
               //     product.Image = context.Products.Where(a => a.Id == id).Select(a => a.Image).FirstOrDefault();
               // }


                product.Alias = Functions.AliasGenerator(product.Name, context.Products.ToList().Select(a => a.Alias));
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult EditCategory(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                if (id == null)
                {
                    return HttpNotFound();
                }

                Category category = context.Categories.Find(id);
                if (category != null)
                {
                    return View(category);
                }
                return HttpNotFound();
            }
        }


        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Entry(category).State = EntityState.Modified;
                category.Alias = Functions.AliasGenerator(category.Name, context.Categories.ToList().Select(a => a.Alias));
                context.SaveChanges();
                return RedirectToAction("AddCategory");
            }

        }

        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                try
                {
                    Product product = context.Products.Find(id);
                    return View(product);
                }
                catch
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost, ActionName("DeleteProduct")]
        public ActionResult DeleteProductConfirmed(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Product product = context.Products.Find(id);
                context.Products.Remove(product);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public ActionResult DeleteProducts(List<int> id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {

                int index = 0;
                List<Product> product = new List<Product>();
                foreach (var i in id)
                {
                    product.Add(context.Products.Find(id.ElementAt(index)));
                    index++;
                }
                return View(product);
            }

        }


        [HttpPost, ActionName("DeleteProducts")]
        public ActionResult DeleteProductArray(List<Int32> id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                int index = 0;
                List<Product> product = new List<Product>();
                foreach (var i in id)
                {
                    product.Add(context.Products.Find(id.ElementAt(index)));
                    index++;
                }
                foreach (var a in product)
                {
                    context.Products.Remove(a);
                }
                context.SaveChanges();
                index = 0;
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                try
                {
                    Category category = context.Categories.Find(id);
                    return View(category);
                }
                catch
                {
                    return HttpNotFound();
                }
            }
        }
        [HttpPost, ActionName("DeleteCategory")]
        public ActionResult DeleteCategoryConfirmed(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                try
                {
                    Category category = context.Categories.Find(id);
                    context.Categories.Remove(category);
                    context.SaveChanges();
                    return RedirectToAction("AddCategory");
                }
                catch
                {
                    return RedirectToAction("Error");
                }
            }
        }


        [HttpGet]
        public ActionResult DeleteCategories(List<int> id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                int index = 0;
                List<Category> category = new List<Category>();
                foreach (var i in id)
                {
                    category.Add(context.Categories.Find(id.ElementAt(index)));
                    index++;
                }
                return View(category);
            }
        }
        [HttpPost, ActionName("DeleteCategories")]
        public ActionResult DeleteCategoryArray(List<int> id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                int index = 0;
                List<Category> category = new List<Category>();
                foreach (var i in id)
                {
                    category.Add(context.Categories.Find(id.ElementAt(index)));
                    index++;
                }
                foreach (var a in category)
                {
                    context.Categories.Remove(a);
                }

                try
                {
                    context.SaveChanges();
                    index = 0;
                    return RedirectToAction("AddCategory");
                }
                catch
                {
                    return RedirectToAction("Error");
                }
            }
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}