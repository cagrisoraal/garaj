using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication43.Models;
using PagedList;
using PagedList.Mvc;

namespace WebApplication43.Controllers
{
    public class AnasayfaController : Controller
    {
        kitapciEntities db = new kitapciEntities();
        // GET: Anasayfa
        public ActionResult Index(KitapPage model)
        {
            if (Session["Uye_adi"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            int sayfano = model.page ?? 1;
            model.kit = (from a in db.Kitaplar.Where(b =>
                    (String.IsNullOrEmpty(model.KitapAdi) || b.KitapAdi.Contains(model.KitapAdi)) && (String.IsNullOrEmpty(model.YazarAdi) || b.KitapYazari.Contains(model.YazarAdi))

                ).OrderBy(f => f.KitapId)
                         select new KitapPage
                         {

                             KitapAdi = a.KitapAdi,
                             YazarAdi = a.KitapYazari,
                             KitapUrl = a.KitapUrl,
                             Kitapid = a.KitapId

                         }).ToPagedList(sayfano,6);
            if (Request.IsAjaxRequest())
            {
                if (Session["Uye_adi"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                return PartialView("_kitapp", model);
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Kitapicerikleri(string kitapismi)
        {
            if (Session["Uye_adi"] == null)
            {
                return RedirectToAction("Login","Login");
            }
            var sorgu = db.Kitaplar.Where(a => a.KitapAdi == kitapismi).ToList();
            List<Kitaplar> yeni = new List<Kitaplar>();
            foreach (var item in sorgu)
            {
                Kitaplar a = new Kitaplar();
                a.KitapAdi = item.KitapAdi;
                a.KitapId = item.KitapId;
                a.KitapUrl = item.KitapUrl;
                a.KitapYazari = item.KitapYazari;
                a.KitapDurumu = item.KitapDurumu;
                yeni.Add(a);

            }

            return View(yeni);
        }

        public ActionResult KitapListesi()
        {
            var sorgu = db.Kitaplar.ToList();
            List<Kitaplar> yeni = new List<Kitaplar>();
            foreach (var item in sorgu)
            {
                if (Session["Uye_adi"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                Kitaplar a = new Kitaplar();
                a.KitapAdi = item.KitapAdi;
                a.KitapId = item.KitapId;
                a.KitapUrl = item.KitapUrl;
                a.KitapYazari = item.KitapYazari;
                a.KitapDurumu = item.KitapDurumu;
                yeni.Add(a);

            }

            return View(yeni);
        }

        public ActionResult KiralamaIslemi()
        {
            
            
            return View();
        }


      

       
    }
}