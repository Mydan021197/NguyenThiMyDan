using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CNPMnangcao.Models;
namespace CNPMnangcao.Controllers
{
    public class BookStoreController : Controller
    {
        //
        dbQLBansachDataContext data = new dbQLBansachDataContext();
        
        private List<SACH> Laysachmoi(int count)
        {
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }

        // GET: /BookStore/
        public ActionResult Index()
        {
            var sachmoi = Laysachmoi(6);
            return View(sachmoi);
        }
        public ActionResult Chude()
        {
            var chude = from cd in data.CHUDEs select cd;
            return View(chude);
        }
        public ActionResult Nhaxuatban()
        {
            var nhaxuatban = from a in data.NHAXUATBANs select a;
            return View(nhaxuatban);
        }
        public ActionResult SanPhamTheoChuDe(int id)
        {
            var sach = from s in data.SACHes where s.MaCD == id select s;
            return PartialView(sach);
        }
        public ActionResult SanPhamTheoNXB(int id)
        {
            var sach = from s in data.SACHes where s.MaCD == id select s;
            return PartialView(sach);
        }
        public ActionResult Details(int id)
        {
            var sach = from s in data.SACHes
                       where s.Masach == id
                       select s;
            return View(sach.Single());
        }
    }
}