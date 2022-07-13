﻿using AutoMapper;
using InventoryManSys.Data;
using InventoryManSys.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManSys.Controllers
{
    [Authorize]
    [Route("PurchaseOrder")]
    public class PurchaseOrderController : Controller
    {

        private readonly ApplicationDbContext _Db;
        private readonly IMapper _mapper;

        public PurchaseOrderController(ApplicationDbContext db, IMapper mapper)
        {
            _Db = db;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = _Db.Products.ToList();
            var productsVM = new List<ProductVM>();

            foreach (var product in products)
            {
                var prouductVM = _mapper.Map<ProductVM>(product);
                productsVM.Add(prouductVM);
            }

            return View(productsVM.AsEnumerable());
        }

        [HttpPost("AddToCart")]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart([Bind("Id, Name, Price, Quantity")]ProductVM productVM)
        {

            return View();
        }
    }
}