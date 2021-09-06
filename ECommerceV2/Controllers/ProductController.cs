using ECommerceV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerceV2.Controllers
{
    public class ProductController : Controller
    {
        public string connectionString = @"Data Source=DESKTOP-E26QU5P\SQLEXPRESS;Initial Catalog=ecommerce_db_updated;Integrated Security=True";
        public static string lang = "en";

        [Authorize]
        public IActionResult Index()
        {
            List<ProductModel> products = new List<ProductModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    // First connection open
                    con.Open();
                }
                string query =$"SELECT * FROM products WHERE language = '{lang}' AND visibility = 1";
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProductModel product = new ProductModel
                    {
                        ProductName = reader["ProductName"].ToString(),
                        ProductInfo = reader["ProductInfo"].ToString(),
                        CategoryName = reader["CategoryName"].ToString(),
                        UserId = Convert.ToInt32(reader["UserId"]),
                        Language = reader["Language"].ToString(),
                        StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                        UnitPrice = Convert.ToDouble(reader["UnitPrice"])
                    };
                    products.Add(product);
                }
                reader.Close();
                con.Close();
            }
            return View(products);
        }

        [Authorize]
        public ActionResult MobilePhone()
        {
            List<ProductModel> products = new List<ProductModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string category = "Mobile Phone";

                if (con.State == ConnectionState.Closed)
                {
                    // First connection open
                    con.Open();
                }

                if (lang == "tr")
                {
                    category = "Cep Telefonu";
                }

                string query = $"SELECT * FROM products WHERE language = '{lang}' AND categoryName = '{category}' AND visibility = 1";
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProductModel product = new ProductModel
                    {
                        ProductName = reader["ProductName"].ToString(),
                        ProductInfo = reader["ProductInfo"].ToString(),
                        CategoryName = reader["CategoryName"].ToString(),
                        UserId = Convert.ToInt32(reader["UserId"]),
                        Language = reader["Language"].ToString(),
                        StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                        UnitPrice = Convert.ToDouble(reader["UnitPrice"])
                    };
                    products.Add(product);
                }
                reader.Close();
                con.Close();
            }
            return View(products);
        }

        [Authorize]
        public ActionResult Notebook()
        {
            List<ProductModel> products = new List<ProductModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string category = "Notebook";

                if (con.State == ConnectionState.Closed)
                {
                    // First connection open
                    con.Open();
                }

                if (lang == "tr")
                {
                    category = "Dizüstü Bilgisayar";
                }

                string query = $"SELECT * FROM products WHERE language = '{lang}' AND categoryName = '{category}' AND visibility = 1";
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProductModel product = new ProductModel
                    {
                        ProductName = reader["ProductName"].ToString(),
                        ProductInfo = reader["ProductInfo"].ToString(),
                        CategoryName = reader["CategoryName"].ToString(),
                        UserId = Convert.ToInt32(reader["UserId"]),
                        Language = reader["Language"].ToString(),
                        StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                        UnitPrice = Convert.ToDouble(reader["UnitPrice"])
                    };
                    products.Add(product);
                }
                reader.Close();
                con.Close();
            }
            return View(products);
        }

        [Authorize]
        public ActionResult SmartWatch()
        {
            List<ProductModel> products = new List<ProductModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string category = "Smart Watch";

                if (con.State == ConnectionState.Closed)
                {
                    // First connection open
                    con.Open();
                }

                if (lang == "tr")
                {
                    category = "Akıllı Saat";
                }

                string query = $"SELECT * FROM products WHERE language = '{lang}' AND categoryName = '{category}' AND visibility = 1";
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProductModel product = new ProductModel
                    {
                        ProductName = reader["ProductName"].ToString(),
                        ProductInfo = reader["ProductInfo"].ToString(),
                        CategoryName = reader["CategoryName"].ToString(),
                        UserId = Convert.ToInt32(reader["UserId"]),
                        Language = reader["Language"].ToString(),
                        StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                        UnitPrice = Convert.ToDouble(reader["UnitPrice"])
                    };
                    products.Add(product);
                }
                reader.Close();
                con.Close();
            }
            return View(products);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteItem(string productName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    // First connection open
                    con.Open();
                }
                string query = $"UPDATE products SET visibility = 0 WHERE productName = '{productName}'";
                SqlCommand command = new SqlCommand(query, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult BuyItem(string productName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    // First connection open
                    con.Open();
                }
                string query = $"UPDATE products SET stockQuantity = (SELECT stockQuantity FROM products WHERE productName = '{productName}' AND language = 'en') - 1, userId = (SELECT id FROM users WHERE email = '{User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString()}') WHERE productName = '{productName}'";
                SqlCommand command = new SqlCommand(query, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            return RedirectToAction("Index");
        }
        
        [Authorize]
        public IActionResult Profile()
        {
            List<ProductModel> products = new List<ProductModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    // First connection open
                    con.Open();
                }
                ViewBag.userName = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
                string query = $"SELECT * FROM products WHERE userId = (SELECT id FROM users WHERE email = '{User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString()}') AND visibility = 1 AND language = '{lang}'";
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProductModel product = new ProductModel
                    {
                        ProductName = reader["ProductName"].ToString(),
                        ProductInfo = reader["ProductInfo"].ToString(),
                        CategoryName = reader["CategoryName"].ToString(),
                        UserId = Convert.ToInt32(reader["UserId"]),
                        Language = reader["Language"].ToString(),
                        StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                        UnitPrice = Convert.ToDouble(reader["UnitPrice"])
                    };
                    products.Add(product);
                }
                reader.Close();
                con.Close();
            }
            return View(products);
        }

        public IActionResult SellItem(string productName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    // First connection open
                    con.Open();
                }
                string query = $"UPDATE products SET userId = -1, stockQuantity = (SELECT stockQuantity FROM products WHERE productName = '{productName}' AND language = 'en') + 1 WHERE productName = '{productName}'";
                SqlCommand command = new SqlCommand(query, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            return RedirectToAction("Index");
        }

        // -----------------------------------------------------
        // --------------------CREATE NEW PRODUCT---------------
        // -----------------------------------------------------
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View(new ProductModel());
        }



        [HttpPost]
        public ActionResult Create(ProductModel product)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO products(productName, productInfo, categoryName, userId, language, stockQuantity, unitPrice) VALUES (@productName, @productInfo, @categoryName, @userId, @language, @stockQuantity, @unitPrice)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@productName", product.ProductName);
                cmd.Parameters.AddWithValue("@productInfo", product.ProductInfo);
                cmd.Parameters.AddWithValue("@categoryName", product.CategoryName);
                cmd.Parameters.AddWithValue("@userId", product.UserId);
                cmd.Parameters.AddWithValue("@language", product.Language);
                cmd.Parameters.AddWithValue("@stockQuantity", product.StockQuantity);
                cmd.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult SwitchToEng()
        {
            lang = "en";
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult SwitchToTr()
        {
            lang = "tr";
            return RedirectToAction("Index");
        }

    }
}
