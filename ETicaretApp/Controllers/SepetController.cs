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
    public class SepetController : Controller
    {
        private ISepetDal sepetServis;
        private IIndirimDal indirimServis;
        private IAlisSekliDal alisSekliServis;

        public SepetController()
        {
            sepetServis = new SepetDal();
            indirimServis = new IndirimDal();
            alisSekliServis = new AlisSekliDal();
        }

        public ActionResult Index(string val = null)
        {
            if (!String.IsNullOrEmpty(val))
            {
                var idx = int.Parse(val);
                var result = sepetServis.GetEntities(c => c.Id == idx);
                if (result!=null && result.Count>0)
                {
                    return View(result);
                }
            }
            return View(sepetServis.GetEntities(null));
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            try
            {
                var result = sepetServis.GetEntity(c => c.Id == id);
                if (result != null)
                {
                    return View(new SepetDto
                    {
                        AlisSekli = result.AlisSekli.Count == 0 ? "" : result.AlisSekli.First().AlisTipi,
                        IndirimOrani = result.Indirimi.Count == 0 ? "" : result.Indirimi.First().IndirimOrani.ToString(),
                        SepetId = id
                    });
                }

                return RedirectToAction("Index", sepetServis.GetEntities(null));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", sepetServis.GetEntities(null));
            }
        }

        [HttpPost]
        public ActionResult Update(SepetDto model)
        {
            try
            {
                var sepet = sepetServis.GetEntity(c => c.Id == model.SepetId);
                if (sepet!=null)
                {
                    if (model.IndirimOrani!=null && model.IndirimOrani.Length>0)
                    {
                        int i_o = int.Parse(model.IndirimOrani);
                        var yeniDeger = ((decimal)sepet.ToplamFiyat) * decimal.Parse(i_o.ToString()) / 100;

                        if (sepet.Indirimi.Count == 0)
                        {
                            indirimServis.Add(new Indirim
                            {
                                SepetId = sepet.Id,
                                IndirimOrani = i_o,
                            });
                        }
                        else
                        {
                            var indirim = indirimServis.GetEntity(c => c.SepetId == sepet.Id);
                            indirimServis.Delete(indirim);
                            indirimServis.Add(new Indirim
                            {
                                SepetId = sepet.Id,
                                IndirimOrani = i_o,
                            });
                        }

                        sepet.ToplamFiyat -= int.Parse(Math.Floor(yeniDeger).ToString());

                        sepetServis.Update(sepet);

                        if (sepet.AlisSekli.Count == 0)
                        {
                            alisSekliServis.Add(new AlisSekli
                            {
                                SepetId = sepet.Id,
                                AlisTipi = model.AlisSekli
                            });
                        }
                        else
                        {
                            var a_sekli = alisSekliServis.GetEntity(c => c.SepetId == sepet.Id);
                            alisSekliServis.Delete(a_sekli);
                            alisSekliServis.Add(new AlisSekli
                            {
                                SepetId = sepet.Id,
                                AlisTipi = model.AlisSekli
                            });
                        }
                    }
                }
                return RedirectToAction("Index", sepetServis.GetEntities(null));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", sepetServis.GetEntities(null));
            }
        }

        [HttpGet]
        public ActionResult Ayrinti(int id)
        {
            return Redirect("/Alisveris/Index?val=" + id.ToString());
        }
    }
}