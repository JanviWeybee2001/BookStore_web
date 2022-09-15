using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using Microsoft.Extensions.Configuration;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration _configuration)
        {
            _logger = logger;
            configuration = _configuration;
        }
        [ViewData] // by that we don't have to need of writing ViewData["customeProperty"], we can access normal property by @ViewData["customeProperty"]
        public string customeProperty { get; set; }

        [ViewData]
        public string Title { get; set; }

        public IActionResult Index()
        {
            //ViewBag.appName = "BookStore";
            //dynamic data = new ExpandoObject();

            //data.id = 1;
            //data.name = "Janvi";                  // This is all about ViewBag

            //ViewBag.Data = data;

            //ViewBag.Type = new BookModel() { id= 5, Author = "This is author" };
            //ViewBag.Janvi = "janvi";

            ViewData["appName"] = "BookStore";
            //ViewData["book"] = new BookModel() { Author = "Janvi", id = 1 };      // it is all about viewdata

            ViewData["customeProperty"] = "Custom value";

            customeProperty = "Custom value";
            // This customeProperty & Title are the attributes of ViewData
            Title = "Home";


            var NewBookAlert = new NewBookAlertConfig();
            configuration.Bind("NewBookAlert", NewBookAlert);

            bool isDisplay = NewBookAlert.DisplayNewBookAlert;

            //var newbook = configuration.GetSection("NewBookAlert");
            //var result = newbook.GetValue<bool>("DisplayNewBookAlert");
            //var key1 = configuration["infoObj:key1"];
            //var key2 = configuration["infoObj:key2"];
            //var key3 = configuration["infoObj:key3:key3obj1"];
            return View();
        }

        public ViewResult ContactUs()
        {
            Title = "Contace Us";

            return View();
        }

        public IActionResult AboutUS()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


//private readonly IConfiguration configuration;

//public HomeController(IConfiguration _configuration)
//{
//    configuration = _configuration;
//}

//public ViewResult Index()
//{
//    ViewBag.Title = "Home";

//    var result = configuration["AppName"];
//    var key1 = configuration["infoObj:key1"];
//    var key2 = configuration["infoObj:key2"];
//    var key3 = configuration["infoObj:key3:key3obj1"];

//    return View();
//}
