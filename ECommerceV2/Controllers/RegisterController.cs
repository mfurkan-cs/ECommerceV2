using ECommerceV2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceV2.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(RegisterModel model)
        {
            string con = @"Data Source=DESKTOP-E26QU5P\SQLEXPRESS;Initial Catalog=ecommerce_db_updated;Integrated Security=True";

            using (SqlConnection sql = new SqlConnection(con))
            {
                string insertQuery = "INSERT INTO users(password, email) VALUES ('" + model.Password + "', '" + model.UserName + "')";

                using (SqlCommand com = new SqlCommand(insertQuery, sql))
                {
                    sql.Open();
                    com.ExecuteNonQuery();
                    ViewData["Message"] = "The new user with e-mail \"" + model.UserName + "\" is registered successfully!";
                }

            }
            return RedirectPermanent("/Product/Index");
            return View(model);
        }
    }
}
