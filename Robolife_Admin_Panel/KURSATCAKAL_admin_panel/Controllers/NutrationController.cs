using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Robolife_Admin_Data_Layer;
namespace KURSATCAKAL_admin_template.Controllers
{
    public class NutrationController : Controller
    {
        // GET: Nutration
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetCalender()
        {
            return View();
        }
        public ActionResult CalenderNutration()
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();
            List<NutrationProgram> listNutration = DB.NutrationProgram.ToList();
            return View(listNutration);
        }
        public ActionResult ProgramList()
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();
            List<NutrationProgram> programList = new List<NutrationProgram>();
            programList = DB.NutrationProgram.ToList();
            ViewBag.Person_To_Customer = DB.User.ToList();
            ViewBag.Foods = DB.Food.ToList();
            ViewBag.MealTime = DB.MealTime.ToList();



            return View(programList);
        }
        public ActionResult NutrationPieceDelete(int pieceID)
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();
            NutrationProgram piece = DB.NutrationProgram.Where(x => x.PersonID == pieceID).First();
            try
            {
                DB.NutrationProgram.Remove(piece);
                DB.SaveChanges();
                TempData["DeleteSuccess"] = "Silme İşlemi Başarılı";

            }
            catch (Exception)
            {
                TempData["NutrationPieceDeleteException"] = "Bu parça başka bir değer ile bağlantılı olduu için silinemez.";

            }


            return RedirectToAction("ProgramList");

        }
        public ActionResult NutrationAddUpdate(int? guncelleID, int foodIDGiden, int mealTimeID, int dayIDGiden, int personIDGiden, string inputGram = "")
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();
            NutrationProgram takeNutrationPiece = new NutrationProgram();
            try
            {
                takeNutrationPiece.FoodID = foodIDGiden;
                takeNutrationPiece.MealTimeID = mealTimeID;
                takeNutrationPiece.Gram = Convert.ToInt16(inputGram);
                takeNutrationPiece.PersonID = personIDGiden;
                takeNutrationPiece.DayID = dayIDGiden;

                DB.NutrationProgram.Add(takeNutrationPiece);
                DB.SaveChanges();
                TempData["AdditionSuccess"] = "Ekleme İşlemi Başarılı";
            }
            catch (Exception)
            {
                TempData["NutrationAddUpdateException"] = "Veri girdilerinde type hatası yaptınız.";
            }
            return RedirectToAction("ProgramList");


        }
    }
}