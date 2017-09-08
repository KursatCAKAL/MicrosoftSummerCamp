using Robolife_Admin_Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KURSATCAKAL_admin_template.Controllers
{
    public class SportController : Controller
    {
        // GET: Sport
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SportList()
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();
            List<SportProgram> sportList = DB.SportProgram.ToList();
            ViewBag.Images = DB.Image.ToList();
            ViewBag.Acts = DB.ExercisesAct.ToList();
            ViewBag.MealTime = DB.MealTime.ToList();
            ViewBag.Person_To_Customer = DB.User.ToList();

            return View(sportList);
        }
        public ActionResult SportAddUpdate(int? guncelleID,int dayID,int imageID,int actID,int userID,string inputRepeatPercentage)
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();
            try
            {
                SportProgram eklenecekParca = new SportProgram
                {
                    ActID = actID,
                    DayID=dayID,
                    ImageID=imageID,
                    RepeatPercantage=inputRepeatPercentage,
                    PersonID=userID,
                };
                DB.SportProgram.Add(eklenecekParca);
                DB.SaveChanges();
                TempData["AdditionSuccess"] = "Ekleme İşlemi Başarılı";
            }
            catch (Exception)
            {
                TempData["SportAddUpdateException"] = "Lütfen değer formatlarını doğru giriniz.";
            }
            return RedirectToAction("SportList");
        }
        public ActionResult SportDelete(int sportPieceID)
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();
            SportProgram silinecek = DB.SportProgram.Where(x => x.ID == sportPieceID).First();
            try
            {
                DB.SportProgram.Remove(silinecek);
                DB.SaveChanges();
                TempData["DeleteSuccess"] = "Silme İşlemi Başarılı";
            }
            catch (Exception)
            {
                TempData["SportPieceDeleteException"] = "Bu parça başka bir tablo tarafından kullanılıyor.";
            }
            return RedirectToAction("SportList");
        }
    }
}