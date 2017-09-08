using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Net;
using Robolife_Admin_Data_Layer;
namespace KURSATCAKAL_admin_template.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Calendar()
        {

            return View();
        }
        public ActionResult DataTableFoods()
        {

            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();
            List<Food> listFood = DB.Food.ToList();

            return View(listFood);
        }
        public ActionResult NutrationProgram()
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();
            
                List<NutrationProgram> listNutrationProgram = DB.NutrationProgram.ToList();
                return View(listNutrationProgram);
            

        }
        public ActionResult FoodManage()
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();
            ViewBag.Seasons = DB.Season.ToList();
            return View();
        }

    }
}