using Robolife_Admin_Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KURSATCAKAL_admin_template.Controllers
{
    public class FoodController : Controller
    {
        // GET: Food
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DataFoodList()
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();
            ViewBag.Seasons = DB.Season.ToList();
            List<Food> listFood = DB.Food.ToList();

            return View(listFood);
        }
        public ActionResult FoodAddUpdate(int? foodID, string inputFoodCalori, string inputFoodCarbonhydrate, string inputFoodProtein, string inputFoodFat, string inputFoodGram, string inputFoodName, int? seasonIDGiden)
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();
            Food eklenecekFood = new Food();
            try
            {
                eklenecekFood.Calori = Convert.ToDouble(inputFoodCalori);
                eklenecekFood.Carbonhydrate = Convert.ToDouble(inputFoodCarbonhydrate);
                eklenecekFood.Protein = Convert.ToDouble(inputFoodProtein);
                eklenecekFood.Fat = Convert.ToDouble(inputFoodFat);
                eklenecekFood.Name = inputFoodName.ToString();
                eklenecekFood.Gram = Convert.ToInt16(inputFoodGram);
                eklenecekFood.SeasonID = seasonIDGiden;


                DB.Food.Add(eklenecekFood);
                DB.SaveChanges();
                TempData["AdditionSuccess"] = "Ekleme İşlemi Başarılı";
            }
            catch (Exception)
            {

                TempData["FoodAddUpdateException"] = "Lütfen değerleri doğru formatta giriniz.";
            }

            return RedirectToAction("DataFoodList");
        }
        public ActionResult FoodDelete(int foodID)
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();
            Food silinecekFood = new Food();
            silinecekFood = DB.Food.Where(x => x.ID == foodID).First();
            try
            {
                DB.Food.Remove(silinecekFood);
                DB.SaveChanges();
                TempData["DeleteSuccess"] = "Silme İşlemi Başarılı";

            }
            catch (Exception)
            {

                TempData["FoodDeleteException"] = "Bu besin başka bir tabloda kullanılıyor.";

            }

            return RedirectToAction("DataFoodList");

        }
    }
}