using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETicaretApp.DbAccess.Abstract;
using ETicaretApp.DbAccess.Concrete;
using ETicaretApp.Models;
using Microsoft.Ajax.Utilities;

namespace ETicaretApp.Controllers
{
    public class KategoriController : Controller
    {
        private IKategoriDal kategoriServis;

        public KategoriController()
        {
            kategoriServis = new KategoriDal();
        }

        public ActionResult Index(string val = null)
        {
            if (!String.IsNullOrEmpty(val))
            {
                var result = kategoriServis.GetEntities(c => c.KategoriAdi.ToLower().Contains(val.ToLower()));
                if (result!=null && result.Count>0)
                {
                    return View(result);
                }
            }
            return View(kategoriServis.GetEntities(null));
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new Kategori());
        }

        [HttpPost]
        public ActionResult Add(Kategori model)
        {
            try
            {
                kategoriServis.Add(model);
                ViewBag.msg = true;
                return View("Index", kategoriServis.GetEntities(null));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", kategoriServis.GetEntities(null));
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            try
            {
                var result = kategoriServis.GetEntity(c => c.Id == id);
                if (result != null)
                {
                    return View(result);
                }

                return RedirectToAction("Index", kategoriServis.GetEntities(null
                ));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", kategoriServis.GetEntities(null
                ));
            }
        }

        [HttpPost]
        public ActionResult Update(Kategori model)
        {
            try
            {
                kategoriServis.Update(model);
                return RedirectToAction("Index", kategoriServis.GetEntities(null
                ));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", kategoriServis.GetEntities(null
                ));
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = kategoriServis.GetEntity(c => c.Id == id);
                if (result != null)
                {
                    kategoriServis.Delete(result);
                }
                return RedirectToAction("Index", kategoriServis.GetEntities(null
                ));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", kategoriServis.GetEntities(null
                ));
            }
        }
    }
}