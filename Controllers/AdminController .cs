﻿using KuaforYonetim.Data;
using KuaforYonetim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KuaforYonetim.Controllers
{
    [Authorize(Roles = "Admin")] // Sadece admin erişimi
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Calisanlar()
        {
            var calisanlar = _context.Calisanlar.ToList();
            return View(calisanlar);
        }

        [HttpGet]
        public IActionResult CalisanEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CalisanEkle(Calisan calisan)
        {
            if (ModelState.IsValid)
            {
                _context.Calisanlar.Add(calisan);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Çalışan başarıyla eklendi.";
                return RedirectToAction("Calisanlar");
            }
            return View(calisan);
        }

        public IActionResult CalisanSil(int id)
        {
            var calisan = _context.Calisanlar.Find(id);
            if (calisan != null)
            {
                _context.Calisanlar.Remove(calisan);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Çalışan başarıyla silindi.";
            }
            return RedirectToAction("Calisanlar");
        }

        public IActionResult Randevular()
        {
            var randevular = _context.Randevular
                .Include(r => r.Calisan)
                .Include(r => r.Hizmet)
                .Where(r => r.Durum == RandevuDurumu.Bekliyor)
                .ToList();
            return View(randevular);
        }

        public IActionResult Onayla(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu != null)
            {
                randevu.Durum = RandevuDurumu.Onaylandi;
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Randevu başarıyla onaylandı.";
            }
            return RedirectToAction("Randevular");
        }

        public IActionResult Reddet(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu != null)
            {
                randevu.Durum = RandevuDurumu.Reddedildi;
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Randevu başarıyla reddedildi.";
            }
            return RedirectToAction("Randevular");
        }
    }
}