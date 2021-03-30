using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // 09. Import Suppliers
            var suppliersJson = File.ReadAllText("../../../Datasets/suppliers.json");
            //ImportSuppliers(context, suppliersJson);

            // 10. Import Parts
            var partsJson = File.ReadAllText("../../../Datasets/parts.json");
            //ImportParts(context, partsJson);

            // 11. Import Cars
            var carsJson = File.ReadAllText("../../../Datasets/cars.json");
            //ImportCars(context, carsJson);

            // 12. Import Customers
            var customersJson = File.ReadAllText("../../../Datasets/customers.json");
            //ImportCustomers(context, customersJson);

            // 13. Import Sales
            var salesJson = File.ReadAllText("../../../Datasets/sales.json");
            //ImportSales(context, salesJson);

            // 14. Export Ordered Customers
            Console.WriteLine(GetOrderedCustomers(context));

            // 15.Export Cars From Make Toyota
            //Console.WriteLine(GetCarsFromMakeToyota(context));

            // 16. Export Local Suppliers
            //Console.WriteLine(GetLocalSuppliers(context));

            // 17. Export Cars With Their List Of Parts
            //Console.WriteLine(GetCarsWithTheirListOfParts(context));

            // 18. Export Total Sales By Customer
            //Console.WriteLine(GetTotalSalesByCustomer(context));

            // 19. Export Sales With Applied Discount
            //Console.WriteLine(GetTotalSalesByCustomer(context));
        }

        // 19. Export Sales With Applied Discount
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var topSales = context.Sales
                .Select(s => new
                {
                    car = new
                    {
                        s.Car.Make,
                        s.Car.Model,
                        s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    Discount = s.Discount.ToString("f2"),
                    price = s.Car.PartCars.Sum(pc => pc.Part.Price).ToString("F2"),
                    priceWithDiscount = ((s.Car.PartCars.Sum(pc => pc.Part.Price)) * (1 - s.Discount * 0.01m))
                    .ToString("F2")
                })
                .Take(10)
                .ToList();

            var json = JsonConvert.SerializeObject(topSales, Formatting.Indented);

            return json;
        }

        // 18. Export Total Sales By Customer
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var totalSales = context.Customers
                .Where(c => c.Sales.Any())
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count(),
                    spentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price))
                })
                .OrderByDescending(x => x.spentMoney)
                .ThenByDescending(x => x.boughtCars)
                .ToList();

            var json = JsonConvert.SerializeObject(totalSales, Formatting.Indented);

            return json;
        }

        // 17. Export Cars With Their List Of Parts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carsWithParts = context.Cars
                .Select(cp => new
                {
                    car = new
                    {
                        cp.Make,
                        cp.Model,
                        cp.TravelledDistance,
                    },
                    parts = cp.PartCars.Select(p => new
                    {
                        p.Part.Name,
                        Price = p.Part.Price.ToString("F2"),
                    })
                })
                .ToList();

            var json = JsonConvert.SerializeObject(carsWithParts, Formatting.Indented);

            return json;
        }

        // 16. Export Local Suppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(x => !x.IsImporter)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    PartsCount = c.Parts.Count
                })
                .ToList();

            var json = JsonConvert.SerializeObject(suppliers, Formatting.Indented);
            return json;
        }

        // 15. Export Cars From Make Toyota
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(x => x.Make == "Toyota")
                .Select(c => new
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    c.TravelledDistance
                })
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .ToList();

            var json = JsonConvert.SerializeObject(cars, Formatting.Indented);
            return json;
        }

        // 14. Export Ordered Customers
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo),
                    c.IsYoungDriver
                })
                .ToList();

            var json = JsonConvert.SerializeObject(customers);
            return json;
        }

        // 13. Import Sales
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var salesDto = JsonConvert.DeserializeObject<IEnumerable<SalesImportModel>>(inputJson);

            var sales = salesDto.Select(x => new Sale()).ToList();

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}.";
        }

        // 12. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customerDto = JsonConvert.DeserializeObject<IEnumerable<CustomerImportModel>>(inputJson);

            var customers = customerDto.Select(x => new Customer()).ToList();

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}.";
        }

        // 11. Import Cars
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var carsDto = JsonConvert.DeserializeObject<IEnumerable<CarsImportModel>>(inputJson);

            var cars = new List<Car>();

            foreach (var car in carsDto)
            {
                var currentCar = new Car
                {
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TravelledDistance,
                };

                foreach (var partId in car.PartsId.Distinct())
                {
                    currentCar.PartCars.Add(new PartCar
                    {
                        PartId = partId
                    });
                }

                cars.Add(currentCar);
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}.";
        }

        // 10. Import Parts
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var partsDto = JsonConvert.DeserializeObject<IEnumerable<PartsImportModel>>(inputJson);

            var suppliersIds = context.Suppliers.Select(s => s.Id).ToList();

            var parts = partsDto
                .Where(x => suppliersIds.Contains(x.SupplierId))
                .Select(x => new Part())
                .ToList();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}.";
        }

        // 09. Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var supplierDto = JsonConvert.DeserializeObject<IEnumerable<SupplierImportModel>>(inputJson);

            var suppliers = supplierDto.Select(x => new Supplier()).ToList();

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}.";
        }
    }
}