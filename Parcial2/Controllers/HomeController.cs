using Microsoft.AspNetCore.Mvc;
using Parcial2.Models;
using System.Diagnostics;

namespace Parcial2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {


            return View();
        }

        public IActionResult LogIn()
        {


            return View();
        }
        public IActionResult ViewUser()
        {
            ViewBag.UserList = DatabaseHelper.DatabaseHelper.GetUsers();
            return View();
        }

        public IActionResult Registrarse()
        {
            return View();
        }

        public IActionResult EditUser(int id)
        {
            ViewBag.User = DatabaseHelper.DatabaseHelper.GetUser(id);

            return View();
        }

        public IActionResult DeleteUser(int id)
        {
            DatabaseHelper.DatabaseHelper.DeleteUser(id);

            return RedirectToAction("Index", "Home");
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

        [HttpPost]
        public IActionResult SaveUser(string txtName,
                                   string txtLastName,
                                   string txtEmail,
                                   string txtPhone,
                                   string txtAddres)
        {
            DatabaseHelper.DatabaseHelper.InsertUser(new Models.User()
            {
                Name = txtName,
                LastName = txtLastName,
                Email = txtEmail,
                Phone = Convert.ToInt32(txtPhone),
                Addres = txtAddres,
            });

            return RedirectToAction("Index", "Home");
        }


        public IActionResult UpdateUser(string txtId,
                                       string txtName,
                                       string txtLastName,
                                       string txtEmail,
                                       string txtPhone,
                                       string txtAddress)
        {
            DatabaseHelper.DatabaseHelper.UpdateUser(new Models.User()
            {
                Id = Convert.ToInt16(txtId),
                Name = txtName,
                LastName = txtLastName,
                Email = txtEmail,
                Phone = Convert.ToInt32(txtPhone),
                Addres = txtAddress


            });


            return RedirectToAction("Index", "Home");
        }

        
    }
}