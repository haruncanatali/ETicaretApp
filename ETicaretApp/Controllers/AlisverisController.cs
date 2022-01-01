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
    public class AlisverisController : Controller
    {
        private IAlisverisDal alisverisServis;
        private IMusteriDal musteriServis;
        private IUrunDal urunServis;
        private ISepetDal sepetServis;

        private int tempFiyat = 0;

        public AlisverisController()
        {
            alisverisServis = new AlisverisDal();
            musteriServis = new MusteriDal();
            urunServis = new UrunDal();
            sepetServis = new SepetDal();
        }

        private void BilgileriAyarla()
        {
            ViewBag.musteriler = (from x in musteriServis.GetEntities() select new SelectListItem
            {
                Text = x.Ad+" "+x.Soyad,
                Value = x.Id.ToString()
            }).ToList();
            ViewBag.urunler = (from x in urunServis.GetEntities(null) select new SelectListItem
            {
                Text = x.UrunAdi,
                Value = x.Id.ToString()
            }).ToList();
        }

        public ActionResult Index(string val = null)
        {
            if (!String.IsNullOrEmpty(val))
            {
                int idx = int.Parse(val);
                var result = alisverisServis.GetEntities(c => c.SepetId == idx);
                if (result !=null && result.Count>0)
                {
                    return View(result);
                }
            }
            return View(alisverisServis.GetEntities(null));
        }

        [HttpGet]
        public ActionResult Add()
        {
            BilgileriAyarla();
            return View(new AlisverisDto());
        }

        [HttpPost]
        public ActionResult Add(AlisverisDto model)
        {
            try
            {

                var musteri = musteriServis.GetEntity(c => c.Id == model.MusteriId);
                if (musteri!=null)
                {
                    var ids = musteri.Sepeti.First().Id;

                    var urun = urunServis.GetEntity(c => c.Id == model.UrunId);
                    if (urun!=null)
                    {
                        var idu = urun.Id;
                        alisverisServis.Add(new Alisveris
                        {
                            SepetId = ids,
                            Miktar = model.Miktar,
                            UrunId = idu
                        });

                        var sepet = sepetServis.GetEntity(c => c.Id == ids);
                        var yapilanAlisverisler = sepet.YapilanAlisverisler as List<Alisveris>;
                        for (int i = 0; i < yapilanAlisverisler.Count; i++)
                        {
                            var urunId = yapilanAlisverisler[i].UrunId;
                            var urunx = urunServis.GetEntity(c => c.Id == urunId);
                            var fiyat = int.Parse(Math.Floor(urunx.Fiyati).ToString());
                            sepet.ToplamFiyat += fiyat*model.Miktar;
                        }
                        sepetServis.Update(sepet);
                        ViewBag.msg = true;
                    }
                }

                return View("Index", alisverisServis.GetEntities(null));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", alisverisServis.GetEntities(null
                ));
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            try
            {
                var result = alisverisServis.GetEntity(c => c.Id == id);
                if (result != null)
                {
                    BilgileriAyarla();
                    tempFiyat = result.Miktar;
                    return View(new AlisverisDto
                    {
                        Id = result.Id,
                        UrunId = result.UrunId,
                        Miktar = result.Miktar,
                        MusteriId = result.Sepeti.MusteriId
                    });
                }

                return RedirectToAction("Index", alisverisServis.GetEntities(null));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", alisverisServis.GetEntities(null));
            }
        }

        [HttpPost]
        public ActionResult Update(Alisveris model)
        {
            try
            {
                var alisveris = alisverisServis.GetEntity(c => c.Id == model.Id);
                if (alisveris!=null)
                {
                    var sepetId = alisveris.SepetId;
                    var sepet = sepetServis.GetEntity(c => c.Id == sepetId);
                    var urun = urunServis.GetEntity(c => c.Id == alisveris.UrunId);
                    sepet.ToplamFiyat -= tempFiyat * int.Parse(Math.Floor(urun.Fiyati).ToString());
                    var fiyat = urun.Fiyati * model.Miktar;
                    sepet.ToplamFiyat = int.Parse(Math.Floor(fiyat).ToString());
                    sepetServis.Update(sepet);

                    alisveris.Miktar = model.Miktar;

                    alisverisServis.Update(alisveris);
                    tempFiyat = 0;
                }

                return RedirectToAction("Index", alisverisServis.GetEntities(null));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", alisverisServis.GetEntities(null));
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = alisverisServis.GetEntity(c => c.Id == id);
                if (result != null)
                {
                    var sepetId = result.SepetId;
                    var sepet = sepetServis.GetEntity(c => c.Id == sepetId);
                    var urunId = result.UrunId;
                    var urun = urunServis.GetEntity(c => c.Id == urunId);
                    sepet.ToplamFiyat -= result.Miktar * int.Parse(Math.Floor(urun.Fiyati).ToString());
                    alisverisServis.Delete(result);
                    sepetServis.Update(sepet);
                }
                return RedirectToAction("Index", alisverisServis.GetEntities(null));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", alisverisServis.GetEntities(null));
            }
        }
    }
}