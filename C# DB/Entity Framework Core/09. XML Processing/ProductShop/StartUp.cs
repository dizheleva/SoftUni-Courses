using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // 01.Import Users
            var usersJason = File.ReadAllText("../../../Datasets/users.json");
            //Console.WriteLine(ImportUsers(context, usersJason));
            ImportUsers(context, usersJason);

            // 02. Import Products
            var productsJason = File.ReadAllText("../../../Datasets/products.json");
            //Console.WriteLine(ImportProducts(context, productsJason));
            ImportProducts(context, productsJason);

            // 03. Import Categories
            var categoriesJson = File.ReadAllText("../../../Datasets/categories.json");
            //Console.WriteLine(ImportCategories(context, categoriesJson));
            ImportCategories(context, categoriesJson);

            // 04. Import Categories and Products
            var categoriesProductsJson = File.ReadAllText("../../../Datasets/categories-products.json");
            //Console.WriteLine(ImportCategoryProducts(context, categoriesProductsJson));
            ImportCategoryProducts(context, categoriesProductsJson);

            // 05. Export Products in Range
            //Console.WriteLine(GetProductsInRange(context));

            // 06. Export Successfully Sold Products
            //Console.WriteLine(GetSoldProducts(context));

            // 07. Export Categories By Products Count
            //Console.WriteLine(GetCategoriesByProductsCount(context));

            // 08. Export Users and Products
            //Console.WriteLine(GetUsersWithProducts(context));
        }

        // 08. Export Users and Products
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Include(x => x.ProductsSold)
                .ToList()
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .Select(user => new
                {
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    age = user.Age,
                    soldProducts = new
                        {
                            count = user.ProductsSold.Count(p => p.BuyerId != null),
                            products = user.ProductsSold.Where(p => p.BuyerId != null).Select(p => new
                            {
                                name = p.Name,
                                price = p.Price
                            })
                        }
                })
                .OrderByDescending(u => u.soldProducts.count)
                .ToList();

            var resultObject = new
                {
                    usersCount = users.Count,
                    users = users
                };

            var jsonSerializeSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
                
            var result   = JsonConvert.SerializeObject(resultObject, Formatting.Indented, jsonSerializeSettings);

            return result;
        }

        // 07. Export Categories By Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categoriesInfo = context.Categories
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoryProducts.Count,
                    averagePrice = c.CategoryProducts.Average(b => b.Product.Price).ToString("F2"),
                    totalRevenue = c.CategoryProducts.Sum(b => b.Product.Price).ToString("F2")
                })
                .OrderByDescending(c => c.productsCount)
                .ToList();

            var result = JsonConvert.SerializeObject(categoriesInfo, Formatting.Indented);

            return result;
        }

        // 06. Export Successfully Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .Select(user => new
                {
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    soldProducts = user.ProductsSold.Where(p => p.BuyerId != null).Select(b => new
                        {
                            name = b.Name,
                            price = b.Price,
                            buyerFirstName = b.Buyer.FirstName,
                            buyerLastName = b.Buyer.LastName
                        })
                        .ToList()
                })
                .OrderBy(u => u.lastName)
                .ThenBy(u => u.firstName)
                .ToList();

            var result = JsonConvert.SerializeObject(users, Formatting.Indented);

            return result;
        }

        // 05. Export Products in Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(o => new 
                {
                    name = o.Name,
                    price = o.Price,
                    seller = o.Seller.FirstName + " " + o.Seller.LastName

                })
                .OrderBy(p => p.price)
                .ToList();

            var result = JsonConvert.SerializeObject(products, Formatting.Indented);

            return result;
        }

        // 04. Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = JsonConvert.DeserializeObject<List<CategoryProduct>>(inputJson);

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        // 03. Import Categories
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<List<Category>>(inputJson)
                .Where(c => c.Name != null)
                .ToList();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        // 02. Import Products
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<List<Product>>(inputJson);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }

        // 01.Import Users
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(inputJson);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }
    }
}