using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CNPMnangcao.Models;


namespace CNPMnangcao.Controllers
{
    public class SachController : Controller
    {
        // GET: Sausinch
        dbQLBansachDataContext data = new dbQLBansachDataContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sach()
        {
            return View(data.SACHes.ToList());
        }

        [HttpGet]
        public ActionResult Themsachmoi()
        {
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude");
            ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themsachmoi(SACH sach, HttpPostedFileBase fileupload)
        {
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude");
            ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            if (fileupload == null)
            {
                ViewBag.Thongbao = "Vui long chon anhr biaf";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var filename = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/images"), filename);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hinh anh da ton tai !";

                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    sach.Hinhminhhoa = filename;
                    data.SACHes.InsertOnSubmit(sach);
                    data.SubmitChanges();
                }
                return RedirectToAction("Sach", "Sach");
            }
        }

        [HttpGet]
        public ActionResult Xoasach(int id)
        {
            SACH sach = data.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpPost, ActionName("Xoasach")]
        public ActionResult Xacnhanxoa(int id)
        {
            SACH sach = data.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.SACHes.DeleteOnSubmit(sach);
            data.SubmitChanges();
            return RedirectToAction("Sach");
        }
        public ActionResult Chitietsach(int id)
        {
            SACH sach = data.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpGet]
        public ActionResult Suasach(int id)
        {
            SACH sach = data.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Mota = sach.Mota;
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude", sach.MaCD);
            ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);

            return View(sach);
        }
        [HttpPost]
        public ActionResult Suasach(SACH sach, HttpPostedFileBase fileupload)
        {

            SACH s1 = data.SACHes.ToList().Find(n => n.Masach == sach.Masach);
            s1.Masach = sach.Masach;
            data.SACHes.DeleteOnSubmit(s1);
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude");
            ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            SACH s = data.SACHes.ToList().Find(n => n.Masach == sach.Masach);
            if (ModelState.IsValid)
            {
                if (fileupload != null)
                {
                    var filename = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/images"), filename);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hinh anh da ton tai !";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                        
                    }
                    sach.Hinhminhhoa = filename;
                    s.Masach = sach.Masach;
                    s.Tensach = sach.Tensach;
                    s.Donvitinh = sach.Donvitinh;
                    s.Dongia = sach.Dongia;
                    s.Mota = sach.Mota;
                    s.MaCD = sach.MaCD;
                    s.MaNXB = sach.MaNXB;
                  
                    s.Soluongban = sach.Soluongban;
                    s.solanxem = sach.solanxem;
                    s.moi = sach.moi;
                    data.SACHes.InsertOnSubmit(sach);
                    data.SubmitChanges();
                }
                
            }
                return RedirectToAction("Sach","Sach");
            
        }


        //[HttpGet]
        //public ActionResult Suasach(int id)
        //{
        //    SACH sach = data.SACHes.SingleOrDefault(n => n.Masach == id);
        //    ViewBag.Masach = sach.Masach;
        //    ViewBag.Mota = sach.Mota;
        //    ViewBag.Ngaycapnhat = sach.Ngaycapnhat;
        //    if (sach == null)
        //    {
        //        Response.StatusCode = 404;
        //        return null;
        //    }
        //    ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", sach.MaCD);
        //    ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);
        //    return View(sach);
        //}
        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult Suasach(SACH sach, HttpPostedFileBase fileupload, HttpPostedFileBase Ngaycapnhat)
        //{
        //    ViewBag.Masach = sach.Masach;
        //    ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
        //    ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
        //    SACH s = data.SACHes.ToList().Find(n => n.Masach == sach.Masach);
        //    if (ModelState.IsValid)
        //    {
        //        if (fileupload != null)
        //        {
        //            var fileName = Path.GetFileName(fileupload.FileName);
        //            var path = Path.Combine(Server.MapPath("~/images"), fileName);
        //            if (System.IO.File.Exists(path))
        //            {
        //                ViewBag.Thongbao = "Hình ảnh đã tồn tại";
        //            }
        //            else
        //            {
        //                fileupload.SaveAs(path);
        //                s.Hinhminhhoa = fileName;
        //            }
        //        }
        //        s.Masach = sach.Masach;
        //        s.Tensach = sach.Tensach;
        //        s.Donvitinh = sach.Donvitinh;
        //        s.Dongia = sach.Dongia;
        //        s.Mota = sach.Mota;
        //        s.MaCD = sach.MaCD;
        //        s.MaNXB = sach.MaNXB;
        //        if (Ngaycapnhat != null)
        //        {
        //            s.Ngaycapnhat = sach.Ngaycapnhat;
        //        }
        //        else
        //        {
        //            s.Ngaycapnhat = s.Ngaycapnhat;
        //        }
        //        s.Soluongban = sach.Soluongban;
        //        s.solanxem = sach.solanxem;
        //        s.moi = sach.moi;
        //        data.SubmitChanges();
        //    }
        //    return RedirectToAction("Sach");
        //}
    }
}