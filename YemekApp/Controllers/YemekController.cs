using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Business.Models;
using Business.Services;
using Business.Services.Bases;

namespace YemekApp.Controllers
{
    [Authorize]
    public class YemekController : Controller
    {
        private readonly IYemekTarifiService _yemekTarifiService;
        private readonly IMalzemeService _malzemeService;

        public YemekController(IYemekTarifiService yemekTarifiService, IMalzemeService malzemeService)
        {
            _yemekTarifiService = yemekTarifiService;
            _malzemeService = malzemeService;
        }

        [AllowAnonymous] 
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return View("Hata", "Id gereklidir!");
            YemekTarifiModel model = _yemekTarifiService.Query().SingleOrDefault(u => u.Id == id.Value);
            if (model == null)
                return View("Hata", "Kayıt bulunamadı!");
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            List<MalzemeModel> malzemeler = _malzemeService.Query().ToList();
            ViewBag.MalzemeId = new SelectList(malzemeler, "Id", "Adi");

            YemekTarifiModel model = new YemekTarifiModel()
            {
                Adi = "Deneme",
                Tarif = "Deneme"
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]        
        public IActionResult Create(YemekTarifiModel yemekTarifi)
        {
            if (ModelState.IsValid)
            {                
                var result = _yemekTarifiService.Add(yemekTarifi);
                if (result.IsSuccessful)
                {
                    TempData["Success"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            ViewBag.MalzemeId = new SelectList(_malzemeService.Query().ToList(), "Id", "Adi", yemekTarifi.MalzemeId);

            return View(yemekTarifi);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View("Hata", "Id gereklidir!");
            YemekTarifiModel model = _yemekTarifiService.Query().SingleOrDefault(u => u.Id == id.Value);
            if (model == null)
                return View("Hata", "Kayıt bulunamadı!");
            ViewBag.MalzemeId = new SelectList(_malzemeService.Query().ToList(), "Id", "Adi", model.MalzemeId);

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(YemekTarifiModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _yemekTarifiService.Update(model);
                if (result.IsSuccessful)
                {
                    TempData["Success"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            ViewBag.MalzemeId = new SelectList(_malzemeService.Query().ToList(), "Id", "Adi", model.MalzemeId);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return View("Hata", "Id gereklidir!");
            YemekTarifiModel model = _yemekTarifiService.Query().SingleOrDefault(u => u.Id == id.Value);
            if (model == null)
                return View("Hata", "Kayıt bulunamadı!");
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _yemekTarifiService.Delete(id);

            YemekTarifiModel model = new YemekTarifiModel()
            {
                Id = id,
            };

            TempData["Success"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
