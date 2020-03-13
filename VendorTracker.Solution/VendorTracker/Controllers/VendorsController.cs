using Microsoft.AspNetCore.Mvc;
using VendorTracker.Models;
using System;
using System.Collections.Generic;

namespace VendorTracker.Controllers
{
  public class VendorsController : Controller
  {

    [HttpGet("/vendors")]
    public ActionResult Index()
    {
      List<Vendor> allVendors = Vendor.GetAll();
      return View(allVendors);
    }

    [HttpGet("/vendors/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/vendors")]
    public ActionResult Create(string name, string description)
    {
      Vendor newVendor = new Vendor(name, description);
      return RedirectToAction("Index");
    }

    [HttpGet("/vendors/{vendorID}")]
    public ActionResult Show(int vendorID)
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();
      Vendor selectedVendor = Vendor.Find(vendorID);
      List<Order> selectedOrderList = selectedVendor.Orders;
      model.Add("vendor", selectedVendor);
      model.Add("orders", selectedOrderList);
      return View(model);
    }

    [HttpPost("/vendors/{vendorID}/orders")]
    public ActionResult Create(int vendorID, string title, string description, int quantity, int price, DateTime deliveryDate)
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();
      Vendor foundVendor = Vendor.Find(vendorID);
      Order newOrder = new Order(title, description, quantity, price, deliveryDate);
      foundVendor.AddOrder(newOrder);
      List<Order> vendorOrders = foundVendor.Orders;
      model.Add("vendor", foundVendor);
      model.Add("order", vendorOrders);
      return View("Show", model);
    }

  }
}