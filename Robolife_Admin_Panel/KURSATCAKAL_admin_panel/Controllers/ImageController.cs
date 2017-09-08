using Robolife_Admin_Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KURSATCAKAL_admin_template.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ImageList()
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();
            List<Image> listImage = DB.Image.ToList();
            return View(listImage);
        }
        public ActionResult ImageAddUpdate(int? guncelleID,string longPath,string mediumPath,string shortPath,string imageName)
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();

            try
            {
                Image eklenecek = new Image
                {
                    LongPath = longPath,
                    MiddlePath = mediumPath,
                    ShortPath = shortPath,
                    CreationDate = DateTime.Now,
                    Name = imageName,
                };
                DB.Image.Add(eklenecek);
                DB.SaveChanges();
                TempData["AdditionSuccess"] = "Ekleme İşlemi Başarılı";
            }
            catch (Exception)
            {
                TempData["ImageAddUpdateException"] = "Lütfen girdi formatlarını doğru giriniz.";
            }
            return RedirectToAction("ImageList");
        }
        public ActionResult ImageDelete(int imageID)
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();

            try
            {
                Image silinecekResim = DB.Image.Where(x => x.ID == imageID).First();
                DB.Image.Remove(silinecekResim);
                DB.SaveChanges();
                TempData["DeleteSuccess"] = "Silme İşlemi Başarılı";
            }
            catch (Exception)
            {
                TempData["ImageDeleteException"] = "Bu resim başka bir tablo tarafından kullanılıyor.";
            }
            return RedirectToAction("ImageList");
        }
    }
}