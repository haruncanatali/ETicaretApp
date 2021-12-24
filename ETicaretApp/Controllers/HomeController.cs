using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETicaretApp.DbAccess.Abstract;
using ETicaretApp.DbAccess.Concrete;

namespace ETicaretApp.Controllers
{
    public class HomeController : Controller
    {
        private IMusteriDal musteriServis;
        private IKategoriDal kategoriServis;
        private IMarkaDal markaServis;
        private IUrunDal urunServis;
        private IAlisverisDal alisverisServis;
        private ISepetDal sepetServis;

        public HomeController()
        {
            musteriServis = new MusteriDal();
            kategoriServis = new KategoriDal();
            markaServis = new MarkaDal();
            urunServis = new UrunDal();
            alisverisServis = new AlisverisDal();
            sepetServis = new SepetDal();
        }

        public void AyarlariSakla()
        {
            ViewBag.musteriSayisi = musteriServis.GetEntities(null).Count.ToString();
            ViewBag.kategoriSayisi = kategoriServis.GetEntities(null).Count.ToString();
            ViewBag.markaSayisi = markaServis.GetEntities(null).Count.ToString();
            ViewBag.urunSayisi = urunServis.GetEntities(null).Count.ToString();
            ViewBag.alisverisSayisi = alisverisServis.GetEntities(null).Count.ToString();
            ViewBag.kasa = sepetServis.GetEntities(null).Sum(c => c.ToplamFiyat).ToString();
        }

        public ActionResult Index()
        {
            AyarlariSakla();
            return View();
        }

        
    }
}