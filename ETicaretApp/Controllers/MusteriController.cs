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
    public class MusteriController : Controller
    {
        private IMusteriDal musteriServis;
        private ISepetDal sepetServis;
        private IAlisverisDal alisverisServis;
        private IIndirimDal indirimServis;
        private IAlisSekliDal alisSekliServis;

        public MusteriController()
        {
            musteriServis = new MusteriDal();
            sepetServis = new SepetDal();
            alisverisServis = new AlisverisDal();
            indirimServis = new IndirimDal();
            alisSekliServis = new AlisSekliDal();
        }

        public ActionResult Index(string val = null)
        {
            if (!String.IsNullOrEmpty(val))
            {
                var result = musteriServis.GetEntities(c => (c.Ad + " " + c.Soyad).ToLower().Contains(val.ToLower()));
                if (result != null && result.Count>0)
                {
                    return View(result);
                }
            }
            return View(musteriServis.GetEntities(null));
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new Musteri());
        }

        [HttpPost]
        public ActionResult Add(Musteri model)
        {
            try
            {
                musteriServis.Add(model);
                System.Threading.Thread.Sleep(500);
                var result = musteriServis.GetEntity(c => c.Tckn == model.Tckn);
                if (result != null)
                {
                    sepetServis.Add(new Sepet
                    {
                        MusteriId = result.Id
                    });
                }
                return RedirectToAction("Index", musteriServis.GetEntities(null));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", musteriServis.GetEntities(null));
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            try
            {
                var result = musteriServis.GetEntity(c => c.Id == id);
                if (result != null)
                {
                    return View(result);
                }

                return RedirectToAction("Index", musteriServis.GetEntities(null));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        public ActionResult Update(Musteri model)
        {
            try
            {
                musteriServis.Update(model);
                return RedirectToAction("Index", musteriServis.GetEntities(null));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", musteriServis.GetEntities(null));
            }

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = musteriServis.GetEntity(c => c.Id == id);
                if (result != null)
                {
                    int idx = result.Sepeti.First().Id;
                    var alisverisler = alisverisServis.GetEntities(c => c.SepetId == idx);
                    var indirim = indirimServis.GetEntity(c => c.SepetId == idx);
                    var alisSekli = alisSekliServis.GetEntity(c => c.SepetId == idx);

                    if (alisverisler!=null && alisverisler.Count>0)
                    {
                        for (int i = 0; i < alisverisler.Count; i++)
                        {
                            alisverisServis.Delete(new Alisveris
                            {
                                Id = alisverisler[i].Id
                            });
                        }
                    }

                    if (indirim!=null)
                    {
                        indirimServis.Delete(indirim);
                    }

                    if (alisSekli!=null)
                    {
                        alisSekliServis.Delete(alisSekli);
                    }

                    if (result.Sepeti.Count>0)
                    {
                        var sepet = result.Sepeti.First();
                        sepetServis.Delete(sepet);
                    }

                    musteriServis.Delete(result);
                }
                return RedirectToAction("Index", musteriServis.GetEntities(null));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", musteriServis.GetEntities(null));
            }
        }
        
    }
}