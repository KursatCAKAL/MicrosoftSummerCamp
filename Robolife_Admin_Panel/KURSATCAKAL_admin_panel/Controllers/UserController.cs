using Robolife_Admin_Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KURSATCAKAL_admin_template.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Users()
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();

            List<User> userList = DB.User.ToList();

            ViewBag.Images = DB.Image.ToList();

            return View(userList);

        }
        public ActionResult UserAddUpdate(int? guncelleID, string inputName, string inputSurname, string inputEmail, int inputAge, string inputGender, int imageID, string isActive)
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();
            try
            {
                User eklenecekUser = new User
                {
                    Name = inputName,
                    Surname = inputSurname,
                    Email = inputEmail,
                    Age = inputAge,
                    Gender = Convert.ToBoolean(inputGender),
                    ImageID = imageID,
                    IsActive = Convert.ToBoolean(isActive),
                    UserIP = System.Web.HttpContext.Current.Request.UserHostAddress,
                    CreationDate = DateTime.Now,
            };

                DB.User.Add(eklenecekUser);
                DB.SaveChanges();
                TempData["AdditionSuccess"] = "Ekleme İşlemi Başarılı";
            }
            catch (Exception)
            {

                TempData["UserAddUpdateException"] = "Lütfen değerleri formatına uygun olarak giriniz.";
            }
            return RedirectToAction("Users");
        }
        public ActionResult UserDelete(int userID)
        {
            Xamarin_KURSATCAKALEntities DB = new Xamarin_KURSATCAKALEntities();

            User silinecekUser = DB.User.Where(x => x.ID == userID).First();
            try
            {
                DB.User.Remove(silinecekUser);
                DB.SaveChanges();
                TempData["DeleteSuccess"] = "Silme İşlemi Başarılı";
            }
            catch (Exception)
            {
                TempData["UserDeleteException"] = "Bu kullanici diğer tablolardaki veriler tarafından kullanılıyor.";
            }


            return RedirectToAction("Users");
        }
    }
}