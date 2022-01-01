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
    public class MarkaController : Controller
    {
        private IMarkaDal markaServis;

        public MarkaController()
        {
            markaServis = new MarkaDal();
        }

        public ActionResult Index(string val = null)
        {
            if (!String.IsNullOrEmpty(val))
            {
                var result = markaServis.GetEntities(c => c.MarkaAdi.ToLower().Contains(val.ToLower()));
                if (result!=null && result.Count>0)
                {
                    return View(result);
                }
            }
            return View(markaServis.GetEntities(null));
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new Marka());
        }

        [HttpPost]
        public ActionResult Add(Marka model)
        {
            try
            {
                markaServis.Add(model);
                ViewBag.msg = true;
                return View("Index", markaServis.GetEntities(null));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", markaServis.GetEntities(null));
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            try
            {
                var result = markaServis.GetEntity(c => c.Id == id);
                if (result != null)
                {
                    return View(result);
                }
                return RedirectToAction("Index", markaServis.GetEntities(null));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", markaServis.GetEntities(null));
            }
        }

        [HttpPost]
        public ActionResult Update(Marka model)
        {
            try
            {
                markaServis.Update(model);
                return RedirectToAction("Index", markaServis.GetEntities(null));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", markaServis.GetEntities(null));
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = markaServis.GetEntity(c => c.Id == id);
                if (result != null)
                {
                    markaServis.Delete(result);
                }
                return RedirectToAction("Index", markaServis.GetEntities(null));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", markaServis.GetEntities(null));
            }
        }
    }
}