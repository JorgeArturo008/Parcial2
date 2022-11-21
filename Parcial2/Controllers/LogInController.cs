
using Microsoft.AspNetCore.Mvc;
using Parcial2.Models;
using System.Diagnostics;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

using System.Text.Json;


namespace Parcial2.Controllers
{

    public class LoginController : Controller
    {
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(string email, string password)
        {
            User user = GetUser(email, password);

            if (user != null)
            {
                HttpContext.Session.SetString("userName", JsonSerializer.Serialize(user));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = new Models.Error()
                {
                   Console.log("Try again?");
                    Text = 
                };

                return View("Error");
            }
        }

        public static User? GetUser(string Email, string Addres)
        {
            DataTable ds = ExecuteStoreProcedure("spGetUser", new List<MyqlParameter>()
            {
                new MySqlParameter("pId", id)
            });

            if (ds.Rows.Count == 0)
            {
                return null;
            }

            return new User()
            {
                Id = Convert.ToInt32(ds.Rows[0]["Id"]),
                Name = ds.Rows[0]["Name"].ToString(),
                LastName = ds.Rows[0]["LastName"].ToString(),
                Email = ds.Rows[0]["Email"].ToString(),
                Phone = Convert.ToInt32(ds.Rows[0]["Phone"]),
                Addres = ds.Rows[0]["Addres"].ToString(),


            };
        }
    }
}