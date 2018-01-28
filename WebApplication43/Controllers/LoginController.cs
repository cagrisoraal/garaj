using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication43.Models;

namespace WebApplication43.Controllers
{
    public class LoginController : Controller
    {
        kitapciEntities db = new kitapciEntities();
        // GET: Login
        public ActionResult Login(string K_adi, string K_sifre)
        {
            Session["Uye_adi"] = null;


            var sorgu = db.Kullanici.Where(a => a.KullaniciAdi == K_adi && a.KullaniciSifre == K_sifre);
            if (sorgu.Count()==1)
            {
                Session["Uye_adi"] =K_adi;
                return  RedirectToAction("Index", "Anasayfa");
            }

            return View();
       
        }

        public ActionResult KitapEkleme(string isimktp,string yzrktp, HttpPostedFileBase file)
        {
          
            if (file != null)
            {
                Kitaplar yeni = new Kitaplar();
                yeni.KitapAdi = isimktp;
                yeni.KitapYazari = yzrktp;
               
                file.SaveAs(Server.MapPath("~/Templatedosyalari/images/home") + file.FileName);
                yeni.KitapUrl = file.FileName;
                yeni.KitapDurumu = 1;
                yeni.KitapSontarih = DateTime.Today;
                db.Kitaplar.Add(yeni);
                db.SaveChanges();
                return RedirectToAction("Index");
               
            }


            return View();
             
        }


       



    }
}