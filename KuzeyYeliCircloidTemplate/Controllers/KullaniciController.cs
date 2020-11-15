using KuzeyYeliCircloidTemplate.App_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KuzeyYeliCircloidTemplate.Controllers
{
    [Authorize]
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        public ActionResult Index()
        {
            
            MembershipUserCollection users=Membership.GetAllUsers();
            return View(users);
        }

        [AllowAnonymous]
        public ActionResult Ekle()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Ekle(Kullanici k)
        {
            MembershipCreateStatus durum;
            Membership.CreateUser(k.KullaniciAdi, k.Parola, k.Email, k.GizliSoru, k.GizliCevap, true, out durum);
            string mesaj = "";
            switch (durum)
            {
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    mesaj += " Kullanılmış Kullanıcı Adı Girildi";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    mesaj += " Kullanılmış Parola Girildi";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    mesaj += " Kullanılmış Gizli Soru Girildi";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    mesaj += " Kullanılmış Gizli Cevap Girildi";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    mesaj += " Kullanılmış Mail Adresi Girildi";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    mesaj += " Kullanılmış Kullanıcı Adı Girildi";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    mesaj += " Kullanılmış Mail Adresi Girildi";
                    break;
                case MembershipCreateStatus.UserRejected:
                    mesaj += " Kullanıcı Engel Hatası";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    mesaj += " Kullanılmış Kullanıcı Key Hatası";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    mesaj += " Kullanılmış Kullanıcı Key Hatası";
                    break;
                case MembershipCreateStatus.ProviderError:
                    mesaj += " Üye Yönetimi Sağlayıcısı Hatası";
                    break;
                default:
                    break;
            }

            ViewBag.Mesaj = mesaj;
            if (durum==MembershipCreateStatus.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles="Admin")]
        public ActionResult RolAta()
        {
            ViewBag.Roller = Roles.GetAllRoles().ToList();
            ViewBag.Kullanicilar = Membership.GetAllUsers();
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult RolAta(string KullaniciAdi, string RolAdi)
        {
            Roles.AddUserToRole(KullaniciAdi, RolAdi);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public string UyeRolleri(string kullaniciAdi)
        {
           List<string> roller= Roles.GetRolesForUser(kullaniciAdi).ToList();
            string rol = "";
            foreach (string r in roller)
            {
                rol += r + "-";
            }

            rol = rol.Remove(rol.Length - 1, 1);
            return rol;
        }
        [AllowAnonymous]
        public ActionResult ParolamiUnuttum()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ParolamiUnuttum(Kullanici k)
        {
            MembershipUser mu = Membership.GetUser(k.KullaniciAdi);

            
            
           if ((mu.PasswordQuestion==k.GizliSoru))
            {
                string pwd = mu.ResetPassword(k.GizliCevap);
                mu.ChangePassword(pwd, k.Parola);
                return RedirectToAction("Girisyap","Uye");
            }
            else
            {
                ViewBag.Mesaj = "Girdiğiniz bilgiler hatalıydı";
                return View();
            }
            
        }
    }
}