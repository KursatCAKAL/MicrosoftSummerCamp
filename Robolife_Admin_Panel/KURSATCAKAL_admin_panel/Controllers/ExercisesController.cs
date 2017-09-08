using Robolife_Admin_Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KURSATCAKAL_admin_template.Controllers
{
    public class ExercisesController : Controller
    {
        // GET: Exercises
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ExerciseList()
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();
            List<ExercisesAct> listExercise = DB.ExercisesAct.ToList();
            ViewBag.CrashArea = DB.CrashArea.ToList();
            ViewBag.Image = DB.Image.ToList();
            return View(listExercise);
        }
        public ActionResult ExerciseAddUpdate(int? guncelleID,string description,string descriptionURL,string exerciseName,int targetID,int imageID)
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();
            try
            {
                ExercisesAct eklenecek = new ExercisesAct
                {
                    TargetRegionID=targetID,
                    Name=exerciseName,
                    Description=description,
                    DescriptionURL=descriptionURL,
                    ImageID=imageID,
                    
                };
                DB.ExercisesAct.Add(eklenecek);
                DB.SaveChanges();

                TempData["AdditionSuccess"] = "Ekleme İşlemi Başarılı";
            }
            catch (Exception)
            {

                TempData["ExerciseAddUpdateException"] = "Lütfen değeleri doğru formatta giriniz.";
            }
            return RedirectToAction("ExerciseList");
        }
        public ActionResult ExerciseDelete(int exerciseID)
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();
            try
            {
                ExercisesAct silinecek = DB.ExercisesAct.Where(x => x.ID == exerciseID).First();
                DB.ExercisesAct.Remove(silinecek);
                DB.SaveChanges();
                TempData["DeleteSuccess"] = "Silme İşlemi Başarılı";
            }
            catch (Exception)
            {
                TempData["ExerciseDeleteException"] = "Bu egzersiz kullanıcıların programlarında kullanılıyor.";
            }
            return RedirectToAction("ExerciseList");
        }

    }
}