using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETicaretApp.DbAccess.Abstract;
using ETicaretApp.DbAccess.Concrete;
using ETicaretApp.Models;

namespace ETicaretApp.Controllers
{
    public class UrunController : Controller
    {
        private IUrunDal urunServis;
        private IKategoriDal kategoriServis;
        private IMarkaDal markaServis;

        public UrunController()
        {
            urunServis = new UrunDal();
            kategoriServis = new KategoriDal();
            markaServis = new MarkaDal();
        }

        private void BilgileriAyarla()
        {
            ViewBag.kategoriler = (from x in kategoriServis.GetEntities(null)
                                   select new SelectListItem
                                   {
                                       Text = x.KategoriAdi,
                                       Value = x.Id.ToString()
                                   }).ToList();
            ViewBag.markalar = (from x in markaServis.GetEntities(null)
                                select new SelectListItem
                                {
                                    Text = x.MarkaAdi,
                                    Value = x.Id.ToString()
                                }).ToList();
        }

        public ActionResult Index(string val = null)
        {
            if (!String.IsNullOrEmpty(val))
            {
                var result = urunServis.GetEntities(c => c.UrunAdi.ToLower().Contains(val.ToLower()));
                if (result != null && result.Count > 0)
                {
                    return View(result);
                }
            }
            return View(urunServis.GetEntities(null));
        }

        [HttpGet]
        public ActionResult Add()
        {
            BilgileriAyarla();
            return View(new Urun());
        }

        [HttpPost]
        public ActionResult Add(Urun model)
        {
            try
            {
                urunServis.Add(model);
                return RedirectToAction("Index", urunServis.GetEntities(null));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", urunServis.GetEntities(null));
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            try
            {
                var result = urunServis.GetEntity(c => c.Id == id);
                if (result != null)
                {
                    BilgileriAyarla();
                    return View(result);
                }
                return RedirectToAction("Index", urunServis.GetEntities(null));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", urunServis.GetEntities(null));
            }
        }

        [HttpPost]
        public ActionResult Update(Urun model)
        {
            try
            {
                urunServis.Update(model);
                return RedirectToAction("Index", urunServis.GetEntities(null));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", urunServis.GetEntities(null));
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = urunServis.GetEntity(c => c.Id == id);
                if (result != null)
                {
                    urunServis.Delete(result);
                }
                return RedirectToAction("Index", urunServis.GetEntities(null));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", urunServis.GetEntities(null));
            }
        }
    }
}