using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        static bool VerifyLogin(string input, string dbval)
        {
            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(input, dbval))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            //string hashOfInput = GetMd5Hash(md5Hash, input);
            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(input, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult CheckLogin(string Login, string Password)
        {
            using (var context = new SushiTest1Entities1())
            {       
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, Password);

                var DBHashPassword = context.Administrators.Select(i => i.Password).FirstOrDefault();
                var DBLogin = context.Administrators.Select(i => i.Login).FirstOrDefault();
                bool passwordsSame = false;
                bool loginsSame = false;
                if (VerifyMd5Hash(md5Hash, DBHashPassword, hash))
                {
                        passwordsSame = true;   // Console.WriteLine("The hashes are the same.");
                }
                else
                {
                    passwordsSame = false; //Console.WriteLine("The hashes are not same.");
                }
                if (VerifyLogin(Login, DBLogin))
                {
                    loginsSame = true;
                }
                else
                {
                    loginsSame = false;
                }
                if (loginsSame == true && passwordsSame == true)
                {
                        FormsAuthentication.SetAuthCookie(Login, true);
                        return RedirectToAction("Index","Admin");
                }
                else
                {
                    return RedirectToAction("Login", "Admin");
                }
              }
            }      
        }

        public ActionResult Dishes() //Show all dishes on Dishes view                                         
        {
            using (var context = new SushiTest1Entities1())
            {
               List<AllDishes_Result> allDishesModel = context.AllDishes().ToList();
               return View(allDishesModel);
            }
        }

        public ActionResult DishesInBlock()                                      
        {
            using (var context = new SushiTest1Entities1())
            {
                List<AllDishes_Result> allDishesModel = context.AllDishes().ToList();
                return View(allDishesModel);
            }
        }

        public ActionResult Category() //Show all categories on Category view                   
        {
            using (var context = new SushiTest1Entities1())
            {
                List<ShowAllCategories_Result> allCategoriesModel = context.ShowAllCategories().ToList();
                return View(allCategoriesModel);
            }
        }

        [HttpGet]
        public ActionResult EditOrder(int? orderId)
        {
            if (orderId == null)
            {
                return RedirectToAction("Orders", "Admin");
            }
            using (var context = new SushiTest1Entities1())
            {
               
                var contactInfo = context.SelectOrderContactInfo(orderId).ToList();
               
                if (!contactInfo.Any())
                {
                    return RedirectToAction("Orders", "Admin");
                }
                EditOrderModel editOrderModel = new EditOrderModel
                {
                    OrderId = orderId,
                    House = contactInfo[0].House,
                    PhoneNumber = contactInfo[0].PhoneNumber,
                    Room = contactInfo[0].Room,
                    Street = contactInfo[0].Street,
                    showAllTimeStatus = context.ShowAllTimeStatus(orderId).ToList()
            };
                return View(editOrderModel);
            }  
        }

        [HttpGet]
        public ActionResult EditDish(int? productId)
        {
            if (productId == null)
            {
                return RedirectToAction("Dishes", "Admin");
            }
            else
            {
                using (var context = new SushiTest1Entities1())
                {
                    
                    var dish = context.FindDishById(productId).ToList();
                    if (!dish.Any())
                    {
                        return RedirectToAction("Dishes", "Admin");
                    }
                    
                    NewDishModel newDishModel = new NewDishModel
                    {
                        ProductId = dish[0].ProductId,
                        CategoryId = dish[0].CategoryId,
                        NameRus = dish[0].NameRus,
                        NameUkr = dish[0].NameUkr,
                        NumberOfOrders = dish[0].NumberOfOrders.ToString(),
                        Energy = dish[0].Energy,
                        Price = dish[0].Price.ToString(),
                        Priority = dish[0].Priority,
                        Sale = dish[0].Sale,
                        Count = dish[0].Count,
                        IsHided = dish[0].IsHided,
                        IngridientsRus = dish[0].IngridientsRus,
                        IngridientsUkr = dish[0].IngridientsUkr,
                        categories = context.Categories.ToList(),
                        productWeightDetails = context.ProductWeightDetails.ToList()
                        
                    };
                    return View(newDishModel);
                }
                
            }
        }

        [HttpPost]
        public ActionResult EditDish(int ProductId,string NameRus, string NameUkr, string isHided, int Priority,
            decimal? Price, int? dishCategory,decimal? Weight,string WeightName, string Energy, int? Count,
            string ingredientsTxtRus, string ingredientsTxtUkr, int? prod, HttpPostedFileBase uploadPhoto)
        {
            using (var context = new SushiTest1Entities1())
            {
                bool sale = true;
                if (prod == 3)
                {
                    sale = true;
                }
                else if (prod == 1)
                {
                    sale = false;
                }
                bool hide = true;
                if (isHided == "on")
                {
                    hide = false;
                }
                else
                {
                    hide = true;
                }
                
                context.UpdateProductWeightDetails((int)ProductId, WeightName, Weight);
                //int energy = Convert.ToInt32(Energy);
                context.UpdateProduct(ProductId, dishCategory, NameRus, Price, NameUkr, Count, Convert.ToInt32(Energy), sale, hide,
                    Priority, ingredientsTxtRus, ingredientsTxtUkr);
                if(uploadPhoto != null)
                { 
                int fileName = ProductId;
                uploadPhoto.SaveAs(Server.MapPath("~/Content/Images/Products/" + fileName + ".jpeg"));
                }

            }

            return RedirectToAction("Dishes","Admin");
        }

        [HttpPost]
        public ActionResult AddNewDish(string NameRus, string NameUkr, string isHided, int? dishCategory, byte? prod, int? Priority, decimal? Price, decimal? Weight, string Energy,
           int? Count, string ingredientsTxtRus, string ingredientsTxtUkr, byte WeightName, HttpPostedFileBase uploadPhoto)
        {
            var context = new SushiTest1Entities1();
            var products = context.Products.ToList();

            bool isHidedProduct = (isHided != null) ? false : true;

            if (Weight == null)
                Weight = 0;
            if (Count == null)
                Count = 0;

            bool isSale = false;
            if (prod == 1)
                isSale = true;

            string weightNameString = "мл";
            if (WeightName == 2)
            {
                weightNameString = "гр";
            }

            foreach (var product in products)
            {
                if (product.NameRus == NameRus || product.NameUkr == NameUkr)
                {
                    ViewBag.ExistDish = "Блюдо с таким названием уже существует.";
                    return View("~/Views/Admin/ExistDishPage.cshtml");
                }
            }

            context.AddProduct(dishCategory, NameRus, NameUkr, 0, Count, Energy, 0, Price, isSale, isHidedProduct, Priority, ingredientsTxtRus, ingredientsTxtUkr);

            int fileName = context.Products.ToList().Last().ProductId;

            ProductWeightDetail pwd = new ProductWeightDetail();
            pwd.ProductId = fileName;
            pwd.Name = weightNameString;
            pwd.Value = (decimal)Weight;
            context.ProductWeightDetails.Add(pwd);
            context.SaveChanges();
            
            uploadPhoto.SaveAs(Server.MapPath("~/Content/Images/Products/" + fileName + ".jpeg"));

            return RedirectToAction("Dishes");
        }
        [HttpPost]
        public ActionResult AddCategory(Category newCategory) //Action from modal window AddCategory.cshtml, to add category to database
        {
            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                    context.Categories.Add(newCategory);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Category", "Admin");
        }

        [HttpGet]
        public ActionResult AddNewDish()
        {
            var context = new SushiTest1Entities1();
            var category = context.Categories.ToList();
            ViewBag.Categories = category;
            
            return View();
        }

       

        public ActionResult AddNewOrder()
        {

            return View();
        }

        public ActionResult Orders()
        {
            using (var context = new SushiTest1Entities1())
            {
                List<ShowAllOrders_Result> showAllOrdersModel = context.ShowAllOrders().ToList();
                return View(showAllOrdersModel);
            }
        }

        [HttpPost]
        public ActionResult FindDishes(Product product) //Find dishes on Dishes view
        {
            List<AllDishes_Result> allDishesModel = new List<AllDishes_Result>();
            if(ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                    List<FindDishes_Result> findDishesModel = context.FindDishes(product.NameRus).ToList();
                    for (int i = 0; i < findDishesModel.Count; i++) //Adding count for first model to be equal another model 
                    {
                        allDishesModel.Add(new AllDishes_Result());
                    }
                    for (int i = 0; i < findDishesModel.Count; i++) //Mapping findDishesModel to allDishesModel
                    {
                        allDishesModel[i].ProductId = findDishesModel[i].ProductId;
                        allDishesModel[i].NameRus = findDishesModel[i].NameRus;
                        allDishesModel[i].Priority = findDishesModel[i].Priority;
                        allDishesModel[i].Category = findDishesModel[i].Category;
                        allDishesModel[i].Weight = findDishesModel[i].Weight;
                        allDishesModel[i].NameOfWeight = findDishesModel[i].NameOfWeight;
                        allDishesModel[i].Price = findDishesModel[i].Price;
                        allDishesModel[i].TotalDishes = findDishesModel[i].TotalDishes;
                    }
                }
            }

            return View("~/Views/Admin/Dishes.cshtml", allDishesModel);
        }

        [HttpPost]
        public ActionResult FindDishesInBlock(Product product)
        {
            List<AllDishes_Result> allDishesModel = new List<AllDishes_Result>();
            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                    List<FindDishes_Result> findDishesModel = context.FindDishes(product.NameRus).ToList();
                    for (int i = 0; i < findDishesModel.Count; i++) //Adding count for first model to be equal another model 
                    {
                        allDishesModel.Add(new AllDishes_Result());
                    }
                    for (int i = 0; i < findDishesModel.Count; i++) //Mapping findDishesModel to allDishesModel
                    {
                        allDishesModel[i].ProductId = findDishesModel[i].ProductId;
                        allDishesModel[i].NameRus = findDishesModel[i].NameRus;
                        allDishesModel[i].Priority = findDishesModel[i].Priority;
                        allDishesModel[i].Category = findDishesModel[i].Category;
                        allDishesModel[i].Weight = findDishesModel[i].Weight;
                        allDishesModel[i].NameOfWeight = findDishesModel[i].NameOfWeight;
                        allDishesModel[i].Price = findDishesModel[i].Price;
                        allDishesModel[i].TotalDishes = findDishesModel[i].TotalDishes;
                    }
                }
            }

            return View("~/Views/Admin/DishesInBlock.cshtml", allDishesModel);
        }

        [HttpPost]
        public ActionResult FindCategory(Category category) //Find Categories on Category view
        {
            List<ShowAllCategories_Result> addCategoriesModel = new List<ShowAllCategories_Result>();
            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                    List<FindCategory_Result> findCategoryModel = context.FindCategory(category.NameRus).ToList();
                    for (int i = 0; i < findCategoryModel.Count; i++) //Adding count for first model to be equal another model 
                    {
                        addCategoriesModel.Add(new ShowAllCategories_Result());
                    }
                    for (int i = 0; i < findCategoryModel.Count; i++) //Mapping findCategoryModel to AddCategoriesModel
                    {
                        addCategoriesModel[i].TotalCategories = findCategoryModel[i].TotalCategories;
                        addCategoriesModel[i].NameRus = findCategoryModel[i].NameRus;
                        addCategoriesModel[i].CategoryId = findCategoryModel[i].CategoryId;
                        addCategoriesModel[i].Priority = findCategoryModel[i].Priority;
                        addCategoriesModel[i].TotalDishes = findCategoryModel[i].TotalDishes;
                        addCategoriesModel[i].NameUkr = findCategoryModel[i].NameUkr;
                    }
                }
            }
            return View("~/Views/Admin/Category.cshtml", addCategoriesModel);
        }

        [HttpPost]
        public ActionResult FindOrders(Order order) //Find Orders on Orders view
        {
            List<ShowAllOrders_Result> allOrdersModel = new List<ShowAllOrders_Result>();
            if (ModelState.IsValid)
            {
                using (var contex = new SushiTest1Entities1())
                {
                    string orderId = order.OrderId.ToString(); //Value to find
                    List<FindOrders_Result> findOrdersModel = contex.FindOrders(orderId).ToList();
                    for (int i = 0; i < findOrdersModel.Count; i++) //Adding count for first model to be equal another model 
                    {
                        allOrdersModel.Add(new ShowAllOrders_Result());
                    }
                    for (int i = 0; i < findOrdersModel.Count; i++) //Mapping findOrdersModel to allOrdersModel
                    {
                        allOrdersModel[i].OrderId = findOrdersModel[i].OrderId;
                        allOrdersModel[i].Street = findOrdersModel[i].Street;
                        allOrdersModel[i].House = findOrdersModel[i].Street;
                        allOrdersModel[i].Room = findOrdersModel[i].Room;
                        allOrdersModel[i].MaxStatusTime = findOrdersModel[i].MaxStatusTime;
                        allOrdersModel[i].TotalPrice = findOrdersModel[i].TotalPrice;
                        allOrdersModel[i].StatusNameRus = findOrdersModel[i].StatusNameRus;
                    }
                }
            }

            return View("~/Views/Admin/Orders.cshtml", allOrdersModel);
        }

        [HttpPost]
        public ActionResult DeleteCategoryItem(int[] idSelected) //Action from modal window DeleteCategoryItem.cshtml, to del category from database
        {
            using (var context = new SushiTest1Entities1())
            {
                if (idSelected != null)// Check idSelected
                {
                    for(int i = 0; i < idSelected.Length; i++) // Find item to delete
                    {
                        int selectedIdItem = idSelected[i];
                       //var removedCategory = context.Categories.First(category => category.CategoryId == selectedIdItem);
                        context.DeleteCategory(selectedIdItem);
                    
                       // context.Categories.Remove(removedCategory);
                      //  context.SaveChanges();
                    }
                }
               //context.SaveChanges(); 
            }

            return RedirectToAction("Category", "Admin");
        }

        [HttpPost]
        public JsonResult HideDishModal(int[] idSelected) //Action from modal window HideDishModal, to hide dish from main page
        {
            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                    for(int i = 0; i < idSelected.Length; i++) // Find item to hide
                    {
                        int selectedIdItem = idSelected[i];
                        //var hidedProduct = context.Products.First(product => product.ProductId == selectedIdItem);
                        context.HideDish(selectedIdItem);
                        //hidedProduct.IsHided = true;
                    }
                    //context.SaveChanges();
                }
            }

            return Json(new { Success = true, Url = Url.Action("Dishes", "Admin") });
        }

        [HttpPost]
        public JsonResult DeleteDishModal(int[] idSelected) //Action from modal window DeleteDishModal, to del dish
        {
            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                    for(int i = 0; i < idSelected.Length; i++)//Delete related item from ProductWeightDetails
                    {
                        int selectedIdItem = idSelected[i];
                        context.DeleteProductWeightDetails(selectedIdItem);
                        //var removedItemFromProductWeightDetails = context.ProductWeightDetails.First(productWeightDetail => productWeightDetail.ProductId == selectedIdItem);
                       // context.ProductWeightDetails.Remove(removedItemFromProductWeightDetails);
                       // context.SaveChanges();
                    }
                    for(int i = 0; i < idSelected.Length; i++)// Del dish from table
                    {
                        int selectedIdItem = idSelected[i];
                        context.DeleteDish(selectedIdItem);
                        //var delProduct = context.Products.First(product => product.ProductId == selectedIdItem);
                       // context.Products.Remove(delProduct);
                        //context.SaveChanges();
                    }
                }
            }

            return Json(new { Success = true, Url = Url.Action("Dishes", "Admin") });
        }

        [HttpPost]
        public ActionResult ModifyCategoryModal(int CategoryId, string NameRus, string NameUkr, int Priority) //Action from modal window ModifyCategoryModal, to change category values
        {
            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                    var updateSelectedCategoryValues = context.UpdateCategory(CategoryId, NameRus, NameUkr, Priority);
                    context.SaveChanges();
                }
            }
     
            return RedirectToAction("Category", "Admin");
        }

        [HttpPost]
        public ActionResult ChangeOrderStatus(int[] idSelected, int drpdwnVal) //Action from modal window CahngeOrderStatus to update ord.status
        {
            using (var context = new SushiTest1Entities1())
            {
                if (idSelected != null)
                {
                    for(int i = 0; i < idSelected.Length; i++)
                    {
                        context.InsertValOrdTimeCh(idSelected[i], drpdwnVal);
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult MostPopularDishesChangeValues(int? categoryId, int topCategoryVal)//Update Index view, selected val from partial view MostPopularDishes
        {
            using (var context = new SushiTest1Entities1())
            {
                var category = context.Categories.ToList();
                ViewBag.Categories = category;//Show all category on drpdwn partialView
                StatisticModel newStatisticModel = new StatisticModel //Create new model for Index view
                {
                    mostPopularDishes = context.MostPopularDishes(topCategoryVal, categoryId).ToList(),
                    showUnprocessedOrders = context.ShowUnprocessedOrders().ToList()
                };
                return View("~/Views/Admin/Index.cshtml", newStatisticModel);
            }

        }

        [HttpPost]
        public ActionResult DeleteOrderModal(int[] idSelected) //Action from this Modal to Del order
        {
            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                    for(int i = 0; i < idSelected.Length; i++) //Del related item from table OrderDetails
                    {
                        string selectedItem = idSelected[i].ToString();
                        context.DelOrdersDetailsId(selectedItem);
                    }
                    for(int i = 0; i < idSelected.Length; i++) //Del related item from table OrderTimeChanged
                    {
                        string selectedItem = idSelected[i].ToString();
                        context.DelOrdersTimeChanged(selectedItem);
                    }
                    for(int i = 0; i < idSelected.Length; i++) //Del order
                    {
                        int selectedItem = idSelected[i];
                        context.DeleteOrder(selectedItem);
                        // var delOrder = context.Orders.First(order => order.OrderId == selectedItem);
                        //context.Orders.Remove(delOrder);
                        //context.SaveChanges();
                    }
                }
            }

            return View();
        }

        public ActionResult Index() //Show statistic in Index view
        {
            using (var context = new SushiTest1Entities1())
            {
                var category = context.Categories.ToList();
                ViewBag.Categories = category; //Show all categories on drpdwn Index view
                StatisticModel statisticModel = new StatisticModel 
                {
                    mostPopularDishes = context.MostPopularDishes(5, category[0].CategoryId).ToList(),
                    showUnprocessedOrders = context.ShowUnprocessedOrders().ToList()
                };

                return View(statisticModel);
            }
        }
        public ActionResult Logout()
        { 
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Admin");
        }

        //public ActionResult Trq()
        //{
        //    var context = new SushiTest1Entities1();
        //    var prodDetList = context.OrderDetails.ToList();
        //}
    }
}