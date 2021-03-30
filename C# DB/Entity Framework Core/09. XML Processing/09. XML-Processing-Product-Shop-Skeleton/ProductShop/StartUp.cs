using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using AutoMapper;
using ProductShop.Data;
using ProductShop.Dtos.Export;
using ProductShop.Dtos.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            var context = new ProductShopContext();

            // 01. Import Users
            //var xml = File.ReadAllText("../../../Datasets/users.xml");
            //Console.WriteLine(ImportUsers(context, xml));

            // 02. Import Products
            //var xml = File.ReadAllText("../../../Datasets/products.xml");
            //Console.WriteLine(ImportProducts(context, xml));

            // 03. Import Categories
            //var xml = File.ReadAllText("../../../Datasets/categories.xml");
            //Console.WriteLine(ImportCategories(context, xml));

            // 04. Import Categories and Products
            //var xml = File.ReadAllText("../../../Datasets/categories-products.xml");
            //Console.WriteLine(ImportCategoryProducts(context, xml));

            // 05. Export Products In Range
            //Console.WriteLine(GetProductsInRange(context));

            // 06. Export Sold Products
            //Console.WriteLine(GetSoldProducts(context));

            // 07. Export Categories By Products Count
            //Console.WriteLine(GetCategoriesByProductsCount(context));

            // 08. Export Users and Products
            Console.WriteLine(GetUsersWithProducts(context));
        }

        // 01. Import Users
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(List<UserInputModel>), new XmlRootAttribute("Users"));

            var usersDto = (List<UserInputModel>)serializer.Deserialize(new StringReader(inputXml));

            var users = usersDto.Select(x => new User()).ToList();

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }

        // 02. Import Products
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var root = new XmlRootAttribute("Products");
            var serializer = new XmlSerializer(typeof(List<ProductInputModel>), root);

            var productsDto = (List<ProductInputModel>)serializer.Deserialize(new StringReader(inputXml));

            var products = productsDto.Select(x => new Product()).ToList();

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }

        // 03. Import Categories
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var root = new XmlRootAttribute("Categories");
            var serializer = new XmlSerializer(typeof(List<CategoryInputModel>), root);

            var categoriesDto = (List<CategoryInputModel>)serializer.Deserialize(new StringReader(inputXml));

            var categories = categoriesDto.Where(c => c.Name != null).Select(x => new Category()).ToList();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        // 04. Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var root = new XmlRootAttribute("CategoryProducts");
            var serializer = new XmlSerializer(typeof(List<CategoryProductInputModel>), root);

            var categoryProductsDto = (List<CategoryProductInputModel>)serializer.Deserialize(new StringReader(inputXml));

            var categoryProducts = categoryProductsDto.Select(x => new CategoryProduct()).ToList();

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        // 05. Export Products In Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new ProductOutputModel
                {
                    Name = p.Name,
                    Price = p.Price,
                    BuyerFullName = p.Buyer.FirstName + " " + p.Buyer.LastName
                })
                .OrderBy(p => p.Price)
                .Take(10)
                .ToList();

            var serializer = new XmlSerializer(typeof(List<ProductOutputModel>), new XmlRootAttribute("Products"));
            var serializerNamespaces = new XmlSerializerNamespaces();
            serializerNamespaces.Add("", "");

            var textWriter = new StringWriter();
            serializer.Serialize(textWriter, products, serializerNamespaces);

            return textWriter.ToString();
        }

        // 06. Export Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            var soldProducts = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .Select(u => new UserOutputModel
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProductOutputModels = u.ProductsSold
                        .Where(ps => ps.BuyerId != null)
                        .Select(ps => new SoldProductOutputModel
                        {
                            Name = ps.Name,
                            Price = ps.Price,
                        })
                        .ToArray()
                })
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)
                .ToList();

            var serializer = new XmlSerializer(typeof(List<UserOutputModel>), new XmlRootAttribute("Users"));
            
            var serializerNamespaces = new XmlSerializerNamespaces();
            serializerNamespaces.Add("", "");

            var textWriter = new StringWriter();
            serializer.Serialize(textWriter, soldProducts, serializerNamespaces);

            return textWriter.ToString();
        }


        // 07. Export Categories By Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(c => new CategoryOutputModel
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts.Average(cp => cp.Product.Price),
                    TotalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price)
                })
                .OrderByDescending(c => c.Count)
                .ThenBy(c => c.TotalRevenue)
                .ToList();

            var serializer = new XmlSerializer(typeof(List<CategoryOutputModel>), new XmlRootAttribute("Categories"));
            
            var serializerNamespaces = new XmlSerializerNamespaces();
            serializerNamespaces.Add("", "");

            var textWriter = new StringWriter();
            serializer.Serialize(textWriter, categories, serializerNamespaces);

            return textWriter.ToString();
        }

        // 08. Export Users and Products
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users.ToArray()
                .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                .Select(u => new UserExportModel
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new SoldProductsContainer
                    {
                        Count = u.ProductsSold.Count,
                        Products = u.ProductsSold
                        .Select(p => new SoldProductOutputModel
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                        .OrderByDescending(p => p.Price)
                        .ToArray()
                    }
                })
                .OrderByDescending(u => u.SoldProducts.Count)
                .Take(10)
                .ToArray();

            var finalModel = new UserProductsFinalModel
            {
                Count = context.Users.Count(u => u.ProductsSold.Any(p => p.BuyerId != null)),
                Users = users
            };

            var xmlSerializer = new XmlSerializer(typeof(UserProductsFinalModel), new XmlRootAttribute("Users"));

            var serializerNamespaces = new XmlSerializerNamespaces();

            serializerNamespaces.Add("", "");

            var textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, finalModel, serializerNamespaces);

            return textWriter.ToString();
        }
    }
}