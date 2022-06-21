﻿using InventoryManSys.Data;
using InventoryManSys.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManSys.Controllers
{
    [Route("Category")]
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _Db;

        public CategoryController(ApplicationDbContext db)
        {
            _Db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _Db.Categories;
            return View(categories);
        }

        [Route("Details/{id}")]
        // GET: CategoryController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        [Route("Create")]
        [HttpGet]
        // GET: CategoryController/Create
        public IActionResult Create()
        {
            var WNames = from w in _Db.Warehouses select w.Name;
            ViewData["Warehouses"] = WNames;
            //Console.WriteLine($"---------------> {WNames} <-------------");

            return View();
        }

        [Route("Create")]
        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("Edit/{id}")]
        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoryController/Edit/5
        [HttpPost("[action]/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("[action]/{id}")]
        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost("[action]/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}