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
using WebApplication1.Addition_Classes;
using PagedList;
using PagedList.Mvc;
using System.Web.Helpers;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public class MyRenderOptions : PagedList.Mvc.PagedListRenderOptions
        {
            public MyRenderOptions() : base()
            {
                DisplayLinkToFirstPage = PagedListDisplayMode.IfNeeded;
                DisplayLinkToLastPage = PagedListDisplayMode.IfNeeded;
                DisplayLinkToPreviousPage = PagedListDisplayMode.Never;
                DisplayLinkToNextPage = PagedListDisplayMode.Never;
                LinkToFirstPageFormat = "{0}";
                LinkToLastPageFormat = "{0}";
                MaximumPageNumbersToDisplay = 3;
                DisplayEllipsesWhenNotShowingAllPageNumbers = true;
            }
        }
   
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login() //Login View
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CheckLogin(string Login, string Password) //Check Login and Password for Authenti
        {
            using (var context = new SushiTest1Entities1())
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    string hash = UserAuthentication.GetMd5Hash(md5Hash, Password); //Get entered password hash 
                    var dbHashPassword = context.Administrators.Select(i => i.Password).FirstOrDefault();//Get hasg of Password from DB
                    var dbLogin = context.Administrators.Select(i => i.Login).FirstOrDefault();//Get login from DB
                    bool passwordsSame, loginsSame;
                    if (UserAuthentication.VerifyMd5Hash(md5Hash, dbHashPassword, hash))
                    {
                        passwordsSame = true;
                    } // The hashes are the same.
                    else
                    {
                        passwordsSame = false;
                    } // The hashes are not same.
                    if (UserAuthentication.VerifyLogin(Login, dbLogin))
                    {
                        loginsSame = true;
                    }
                    else
                    {
                        loginsSame = false;
                    }
                    if (UserAuthentication.CheckLoginAndPassword(loginsSame, passwordsSame))
                    {
                        FormsAuthentication.SetAuthCookie(Login, true);
                        return RedirectToAction("Index", "Admin");
                    }
                    return RedirectToAction("Login", "Admin");
                }
            }
        }

        public ActionResult Dishes(int page = 1, int pageSize = 10) //Show all dishes on Dishes view                                         
        {
            using (var context = new SushiTest1Entities1())
            {
               PagedList<AllDishes_Result> allDishesModel = new PagedList<AllDishes_Result>(context.AllDishes().ToList(), page, pageSize);
               return View(allDishesModel);
            }
        }

        public ActionResult DishesInBlock(int page = 1, int pageSize = 10) //Show all dishes in Block's view                                      
        {
            using (var context = new SushiTest1Entities1())
            {
                PagedList<AllDishes_Result> allDishesModel = new PagedList<AllDishes_Result>(context.AllDishes().ToList(), page, pageSize);
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
        public ActionResult EditOrder(int? orderId) //Fill EditOrderView
        {
            if (orderId == null) //Check for orderId is not empty in adress line
            {
                return RedirectToAction("Orders", "Admin");
            }
            using (var context = new SushiTest1Entities1())
            {
                var contactInfo = context.SelectOrderContactInfo(orderId).ToList();
                if (!contactInfo.Any()) //Check for cont info not null
                {
                    return RedirectToAction("Orders", "Admin");
                }
                var category = context.Categories.ToList();
                ViewBag.Categories = category;

                EditOrderModel editOrderModel = new EditOrderModel
                {
                    OrderId = orderId,
                    House = contactInfo[0].House,
                    PhoneNumber = contactInfo[0].PhoneNumber,
                    Room = contactInfo[0].Room,
                    Street = contactInfo[0].Street,
                    TotalSum = contactInfo[0].TotalSum,
                    showAllTimeStatus = context.ShowAllTimeStatus(orderId).ToList(),
                    selectProductsFromOrder = context.SelectProductsFromOrder(orderId).ToList(),
                    productsInModal = context.SelectProductsFromCategoryInModal(1).ToList()
                };
                return View(editOrderModel);
            }
        }

        [HttpPost]
        public ActionResult AddDishToOrderModal(int? orderId)
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
                var category = context.Categories.ToList();
                ViewBag.Categories = category;

                EditOrderModel editOrderModel = new EditOrderModel
                {
                    OrderId = orderId,
                    House = contactInfo[0].House,
                    PhoneNumber = contactInfo[0].PhoneNumber,
                    Room = contactInfo[0].Room,
                    Street = contactInfo[0].Street,
                    TotalSum = contactInfo[0].TotalSum,
                    showAllTimeStatus = context.ShowAllTimeStatus(orderId).ToList(),
                    selectProductsFromOrder = context.SelectProductsFromOrder(orderId).ToList(),
                    productsInModal = context.SelectProductsFromCategoryInModal(4).ToList()
                };
                return View("~/Views/Admin/EditOrder.cshtml", editOrderModel);
            }
        }

        [HttpGet]
        public ActionResult EditDish(int? productId) //Fill EditDish view
        {
            if (productId == null) //Check that adress line with productId is not null
            {
                return RedirectToAction("Dishes", "Admin");
            }
            else
            {
                using (var context = new SushiTest1Entities1())
                {

                    var dish = context.FindDishById(productId).ToList(); //Check exist dish or not
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
        public ActionResult EditDish(int ProductId, string NameRus, string NameUkr, string isHided, int Priority,
            decimal? Price, int? dishCategory, decimal? Weight, string WeightName, string Energy, int? Count,
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
                bool hide;
                if (isHided == "on") //Checking for product is hided or not
                {
                    hide = false;
                }
                else
                {
                    hide = true;
                }
                context.UpdateProductWeightDetails((int) ProductId, WeightName, Weight);
                if (Energy == "")
                {
                    Energy = "0";
                } //Cheking for empty field

                if (uploadPhoto.ContentType == "image/jpeg")
                {
                    WebImage img = new WebImage(uploadPhoto.InputStream);
                    img.Resize(525, 350);
                    img.Save(Server.MapPath("~/Content/Images/Products/" + ProductId + ".jpeg"));

                    context.UpdateProduct(ProductId, dishCategory, NameRus, Price, NameUkr, Count,
                        Convert.ToInt32(Energy),
                        sale, hide, Priority, ingredientsTxtRus, ingredientsTxtUkr);
                    
                }
                else
                {
                    ViewBag.ErrorMessage = "Ошибка загрузки картинки! Допустимый формат - .jpeg";
                    ViewBag.LinkText = "Перейти на страницу с блюдами";
                    ViewBag.LinkHref = "Dishes";
                    return View("~/Views/Admin/UploadingImageError.cshtml");
                }
            }
            return RedirectToAction("Dishes", "Admin");
        }

        [HttpPost]
        public ActionResult AddNewDish(string NameRus, string NameUkr, string isHided, int? dishCategory, byte? prod,
            int? Priority, decimal? Price, decimal? Weight, string Energy,
            int? Count, string ingredientsTxtRus, string ingredientsTxtUkr, byte WeightName,
            HttpPostedFileBase uploadPhoto)
        {
            using (var context = new SushiTest1Entities1())
            {
                var products = context.Products.ToList();

                bool isHidedProduct = (isHided != null) ? false : true;

                if (Weight == null)
                    Weight = 0;
                if (Count == null)
                    Count = 0;

                bool isSale = false;
                if (prod == 1)
                {
                    isSale = true;
                }

                string weightNameString = "мл";
                if (WeightName == 2)
                {
                    weightNameString = "гр";
                }
                

                if (uploadPhoto.ContentType == "image/jpeg")
                {
                    foreach (var product in products)
                    {
                        if (product.NameRus == NameRus || product.NameUkr == NameUkr)
                        {
                            return View("~/Views/Admin/ExistDishPage.cshtml");
                        }
                    }

                    context.AddProduct(dishCategory, NameRus, NameUkr, 0, Count, Energy, 0, Price, isSale, isHidedProduct,
                        Priority, ingredientsTxtRus, ingredientsTxtUkr);

                    int fileName = context.Products.ToList().Last().ProductId; // fileName == ProductId

                    ProductWeightDetail productWeightDetail = new ProductWeightDetail
                    {
                        ProductId = fileName,
                        Name = weightNameString,
                        Value = (decimal)Weight
                    };
                    context.ProductWeightDetails.Add(productWeightDetail);
                    context.SaveChanges();

                    WebImage img = new WebImage(uploadPhoto.InputStream);
                    img.Resize(525, 350);
                    img.Save(Server.MapPath("~/Content/Images/Products/" + fileName + ".jpeg"));
                }
                else
                {
                    ViewBag.ErrorMessage = "Ошибка загрузки картинки! Допустимый формат - .jpeg";
                    ViewBag.LinkText = "Перейти на страницу добавления блюда.";
                    ViewBag.LinkHref = "AddNewDish";
                    return View("~/Views/Admin/UploadingImageError.cshtml");
                }
                
            }
            return RedirectToAction("Dishes");
        }

        [HttpPost]
        public ActionResult AddCategory(Category newCategory) //Action from modal window AddCategory.cshtml, to add category to database
        {
            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                    foreach (var category in context.Categories)
                    {
                        if (category.NameRus == newCategory.NameRus || category.NameUkr == newCategory.NameUkr)
                        {
                            ViewBag.ExistDish = "Такая категория уже существует.";
                            return View("~/Views/Admin/ExistCategoryPage.cshtml");
                        }
                    }

                    context.Categories.Add(newCategory);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Category", "Admin");
        }

        [HttpGet]
        public ActionResult AddNewDish() //Add new product
        {
            using (var context = new SushiTest1Entities1())
            {
                var category = context.Categories.ToList();
                ViewBag.Categories = category;
                return View();
            }
        }

        public ActionResult Orders(int page = 1, int pagesize =10) //Show all orders with pageList
        {
            using (var context = new SushiTest1Entities1())
            {
                PagedList<ShowAllOrders_Result> showAllOrdersModel = new PagedList<ShowAllOrders_Result>(context.ShowAllOrders().ToList(), page, pagesize);
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
            PagedList<AllDishes_Result> allDishesModelWithPagedList = new PagedList<AllDishes_Result>(allDishesModel, 1, 99);
            return View("~/Views/Admin/Dishes.cshtml", allDishesModelWithPagedList);
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
            PagedList<AllDishes_Result> model = new PagedList<AllDishes_Result>(allDishesModel, 1, 99);
            return View("~/Views/Admin/DishesInBlock.cshtml", model);
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
                        allOrdersModel[i].House = findOrdersModel[i].House;
                        allOrdersModel[i].Room = findOrdersModel[i].Room;
                        allOrdersModel[i].MaxStatusTime = findOrdersModel[i].MaxStatusTime;
                        allOrdersModel[i].TotalPrice = findOrdersModel[i].TotalPrice;
                        allOrdersModel[i].StatusNameRus = findOrdersModel[i].StatusNameRus;
                    }
                }
            }
            PagedList<ShowAllOrders_Result> model = new PagedList<ShowAllOrders_Result>(allOrdersModel, 1, 99);//Model with PagedList
            return View("~/Views/Admin/Orders.cshtml", model);
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
                        context.DeleteCategory(selectedIdItem);
                    }
                }
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
                        context.HideDish(selectedIdItem);
                    }
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
                    }
                    for(int i = 0; i < idSelected.Length; i++)// Del dish from table
                    {
                        int selectedIdItem = idSelected[i];
                        context.DeleteDish(selectedIdItem);
                    }
                }
            }
            return Json(new { Success = true, Url = Url.Action("Dishes", "Admin") });
        }

        [HttpPost]
        public ActionResult ModifyCategoryModal(int CategoryId, string NameRus, string NameUkr, int Priority) 
        //Action from modal window ModifyCategoryModal, to change category values
        {
            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                    foreach (var category in context.Categories)
                    {
                        if (category.NameRus == NameRus || category.NameUkr == NameUkr)
                        {
                            return View("~/Views/Admin/ExistCategoryPage.cshtml");
                        }
                    }
                    context.UpdateCategory(CategoryId, NameRus, NameUkr, Priority);
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
        public ActionResult Logout() //Admin Logout
        { 
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult GetData() //Get count of unprocessed orders
        {
            using (var context = new SushiTest1Entities1())
            {
                return Content(context.ShowUnprocessedOrders().Count().ToString()); 
            }
        }
    }
}