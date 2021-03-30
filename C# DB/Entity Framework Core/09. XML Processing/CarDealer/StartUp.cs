using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.DTO.Input;
using CarDealer.DTO.Output;
using CarDealer.Models;
using Newtonsoft.Json;
using Remotion.Linq.Clauses;
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

            //JSON
            //var suppliersJson = File.ReadAllText("../../../Datasets/suppliers.json");
            //ImportSuppliers(context, suppliersJson);

            //XML
            var suppliersXml = File.ReadAllText("../../../Datasets/suppliers.xml");
            ImportSuppliers(context, suppliersXml);

            // 10. Import Parts

            //JSON
            //var partsJson = File.ReadAllText("../../../Datasets/parts.json");
            //ImportParts(context, partsJson);

            //XML
            var partsXml = File.ReadAllText("../../../Datasets/parts.xml");
            ImportParts(context, partsXml);

            // 11. Import Cars

            //JSON
            //var carsJson = File.ReadAllText("../../../Datasets/cars.json");
            //ImportCars(context, carsJson);

            //XML
            var carsXml = File.ReadAllText("../../../Datasets/cars.xml");
            ImportCars(context, carsXml);

            // 12. Import Customers

            //JSON
            //var customersJson = File.ReadAllText("../../../Datasets/customers.json");
            //ImportCustomers(context, customersJson);

            //XML
            var customersXml = File.ReadAllText("../../../Datasets/customers.xml");
            ImportCustomers(context, customersXml);

            // 13. Import Sales

            //JSON
            //var salesJson = File.ReadAllText("../../../Datasets/sales.json");
            //ImportSales(context, salesJson);

            //XML
            var salesXml = File.ReadAllText("../../../Datasets/sales.xml");
            ImportSales(context, salesXml);

            // 14. Export Ordered Customers
            //JSON
            //Console.WriteLine(GetOrderedCustomers(context));

            // 14. Export Cars With Distance
            //XML
            //Console.WriteLine(GetCarsWithDistance(context));

            // 15.Export Cars From Make Toyota
            //JSON
            //Console.WriteLine(GetCarsFromMakeToyota(context));

            // 15. Export Cars From Make BMW
            //XML
            //Console.WriteLine(GetCarsFromMakeBmw(context));

            // 16. Export Local Suppliers
            //JSON  //XML
            //Console.WriteLine(GetLocalSuppliers(context));

            // 17. Export Cars With Their List Of Parts
            //JSON  //XML
            //Console.WriteLine(GetCarsWithTheirListOfParts(context));

            // 18. Export Total Sales By Customer
            //JSON  //XML
            //Console.WriteLine(GetTotalSalesByCustomer(context));

            // 19. Export Sales With Applied Discount
            //JSON  //XML
            Console.WriteLine(GetSalesWithAppliedDiscount(context));
        }

        // 19. Export Sales With Applied Discount
        //JSON
        //public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        //{
        //    var topSales = context.Sales
        //        .Select(s => new
        //        {
        //            car = new
        //            {
        //                s.Car.Make,
        //                s.Car.Model,
        //                s.Car.TravelledDistance
        //            },
        //            customerName = s.Customer.Name,
        //            Discount = s.Discount.ToString("f2"),
        //            price = s.Car.PartCars.Sum(pc => pc.Part.Price).ToString("F2"),
        //            priceWithDiscount = ((s.Car.PartCars.Sum(pc => pc.Part.Price)) * (1 - s.Discount * 0.01m))
        //            .ToString("F2")
        //        })
        //        .Take(10)
        //        .ToList();

        //    var json = JsonConvert.SerializeObject(topSales, Formatting.Indented);

        //    return json;
        //}
        //XML
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var topSales = context.Sales
                .Select(s => new TopSalesOutputModel
                {
                    Car = new CarSaleOutputModel
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    Discount = s.Discount,
                    CustomerName = s.Customer.Name,
                    Price = s.Car.PartCars.Sum(pc => pc.Part.Price),
                    PriceWithDiscount = s.Car.PartCars.Sum(pc => pc.Part.Price) - s.Car.PartCars.Sum(pc => pc.Part.Price) * s.Discount / 100m
                })
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(TopSalesOutputModel[]), new XmlRootAttribute("sales"));
            var ns = new XmlSerializerNamespaces();
            var textWriter = new StringWriter();
            ns.Add("", "");
            xmlSerializer.Serialize(textWriter, topSales, ns);

            return textWriter.ToString();
        }

        // 18. Export Total Sales By Customer
        //JSON
        //public static string GetTotalSalesByCustomer(CarDealerContext context)
        //{
        //    var totalSales = context.Customers
        //        .Where(c => c.Sales.Any())
        //        .Select(c => new
        //        {
        //            fullName = c.Name,
        //            boughtCars = c.Sales.Count(),
        //            spentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price))
        //        })
        //        .OrderByDescending(x => x.spentMoney)
        //        .ThenByDescending(x => x.boughtCars)
        //        .ToList();

        //    var json = JsonConvert.SerializeObject(totalSales, Formatting.Indented);

        //    return json;
        //}
        //XML
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var totalSales = context.Customers
                .Where(c => c.Sales.Any())
                .Select(c => new TotalSalesOutputModel
                {
                    Name = c.Name,
                    CarsCount = c.Sales.Count,
                    SpentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price))
                })
                .OrderByDescending(x => x.SpentMoney)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(TotalSalesOutputModel[]), new XmlRootAttribute("customers"));
            var ns = new XmlSerializerNamespaces();
            var textWriter = new StringWriter();
            ns.Add("", "");
            xmlSerializer.Serialize(textWriter, totalSales, ns);

            return textWriter.ToString();
        }

        // 17. Export Cars With Their List Of Parts
        //JSON
        //public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        //{
        //    var carsWithParts = context.Cars
        //        .Select(cp => new
        //        {
        //            car = new
        //            {
        //                cp.Make,
        //                cp.Model,
        //                cp.TravelledDistance,
        //            },
        //            parts = cp.PartCars.Select(p => new
        //            {
        //                p.Part.Name,
        //                Price = p.Part.Price.ToString("F2"),
        //            })
        //        })
        //        .ToList();

        //    var json = JsonConvert.SerializeObject(carsWithParts, Formatting.Indented);

        //    return json;
        //}
        //XML
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(car => new CarPartOutputModel
                {
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TravelledDistance,
                    Parts = car.PartCars.Select(p => new PartOutputModel
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price
                    })
                        .OrderByDescending(p => p.Price)
                        .ToArray()
                })
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(CarPartOutputModel[]), new XmlRootAttribute("cars"));
            var ns = new XmlSerializerNamespaces();
            var textWriter = new StringWriter();
            ns.Add("", "");
            xmlSerializer.Serialize(textWriter, cars, ns);

            return textWriter.ToString();
        }

        // 16. Export Local Suppliers
        //JSON
        //public static string GetLocalSuppliers(CarDealerContext context)
        //{
        //    var suppliers = context.Suppliers
        //        .Where(x => !x.IsImporter)
        //        .Select(c => new
        //        {
        //            c.Id,
        //            c.Name,
        //            PartsCount = c.Parts.Count
        //        })
        //        .ToList();

        //    var json = JsonConvert.SerializeObject(suppliers, Formatting.Indented);
        //    return json;
        //}

        //XML
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(x => !x.IsImporter)
                .Select(s => new LocalSuppliersOutputModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(LocalSuppliersOutputModel[]), new XmlRootAttribute("suppliers"));
            var textWriter = new StringWriter();
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            xmlSerializer.Serialize(textWriter, suppliers, ns);
            var xml = textWriter.ToString();
            return xml;
        }

        // 15. Export Cars From Make Toyota
        //JSON
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
        //15. Export Cars From Make BMW
        //XML
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "BMW")
                .Select(c => new BMWOutputModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(BMWOutputModel[]), new XmlRootAttribute("cars"));
            var textWriter = new StringWriter();
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            xmlSerializer.Serialize(textWriter, cars, ns);

            var xml = textWriter.ToString();
            return xml;
        }

        // 14. Export Ordered Customers
        //JSON
        //public static string GetOrderedCustomers(CarDealerContext context)
        //{
        //    var customers = context.Customers
        //        .OrderBy(c => c.BirthDate)
        //        .ThenBy(c => c.IsYoungDriver)
        //        .Select(c => new
        //        {
        //            c.Name,
        //            BirthDate = c.BirthDate.ToString("dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo),
        //            c.IsYoungDriver
        //        })
        //        .ToList();

        //    var json = JsonConvert.SerializeObject(customers);
        //    return json;
        //}

        // 14. Export Cars With Distance
        //XML
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.TravelledDistance > 2_000_000)
                .Select(c => new CarOutputModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(CarOutputModel[]), new XmlRootAttribute("cars"));
            var textWriter = new StringWriter();
            var ns = new XmlSerializerNamespaces();
            ns.Add("","");
            xmlSerializer.Serialize(textWriter, cars, ns);

            var xml = textWriter.ToString();
            return xml;
        }
        
        // 13. Import Sales

        //JSON
        //public static string ImportSales(CarDealerContext context, string inputJson)
        //{
        //    var salesDto = JsonConvert.DeserializeObject<IEnumerable<SalesImportModel>>(inputJson);

        //    var carsIds = context.Cars.Select(x => x.Id).ToList();
        //    var sales = salesDto
        //      .Where(x => carsIds.Contains(x.CarId))
        //      .Select(x => new Sale
        //        {
        //            CarId = x.CarId,
        //            CustomerId = x.CustomerId,
        //            Discount = x.Discount
        //        })
        //      .ToList();

        //    context.Sales.AddRange(sales);
        //    context.SaveChanges();

        //    return $"Successfully imported {sales.Count}.";
        //}

        //XML
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(SaleDto[]), new XmlRootAttribute("Sales"));
            var textReader = new StringReader(inputXml);
            var salesDto = xmlSerializer.Deserialize(textReader) as SaleDto[];

            var carsIds = context.Cars.Select(x => x.Id).ToList();

            var sales = salesDto
                .Where(x => carsIds.Contains(x.CarId))
                .Select(x => new Sale
                {
                    CarId = x.CarId,
                    CustomerId = x.CustomerId,
                    Discount = x.Discount
                })
                .ToList();

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}";
        }

        // 12. Import Customers

        //JSON
        //public static string ImportCustomers(CarDealerContext context, string inputJson)
        //{
        //    var customerDto = JsonConvert.DeserializeObject<IEnumerable<CustomerImportModel>>(inputJson);

        //    var customers = customerDto.Select(x => new Customer()).ToList();

        //    context.Customers.AddRange(customers);
        //    context.SaveChanges();

        //    return $"Successfully imported {customers.Count}.";
        //}

        //XML
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(CustomerDto[]), new XmlRootAttribute("Customers"));
            var textReader = new StringReader(inputXml);
            var customerDto = xmlSerializer.Deserialize(textReader) as CustomerDto[];

            var customers = customerDto.Select(x => new Customer()).ToList();

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}";
        }

        // 11. Import Cars

        // JSON
        //public static string ImportCars(CarDealerContext context, string inputJson)
        //{
        //    var carsDto = JsonConvert.DeserializeObject<IEnumerable<CarsImportModel>>(inputJson);

        //    var cars = new List<Car>();

        //    foreach (var car in carsDto)
        //    {
        //        var currentCar = new Car
        //        {
        //            Make = car.Make,
        //            Model = car.Model,
        //            TravelledDistance = car.TravelledDistance,
        //        };

        //        foreach (var partId in car.PartsId.Distinct())
        //        {
        //            currentCar.PartCars.Add(new PartCar
        //            {
        //                PartId = partId
        //            });
        //        }

        //        cars.Add(currentCar);
        //    }

        //    context.Cars.AddRange(cars);
        //    context.SaveChanges();

        //    return $"Successfully imported {cars.Count}.";
        //}

        //XML
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(CarDto[]), new XmlRootAttribute("Cars"));
            var textReader = new StringReader(inputXml);
            var carsDto = xmlSerializer.Deserialize(textReader) as CarDto[];

            var cars = new List<Car>();

            foreach (var car in carsDto)
            {
                var currentCar = new Car
                {
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TravelledDistance,
                };

                foreach (var partId in car.PartsId.Select(p => p.Id).Distinct())
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

            return $"Successfully imported {cars.Count}";
        }

        // 10. Import Parts

        //JSON
        //public static string ImportParts(CarDealerContext context, string inputJson)
        //{
        //    var partsDto = JsonConvert.DeserializeObject<IEnumerable<PartsImportModel>>(inputJson);

        //    var suppliersIds = context.Suppliers.Select(s => s.Id).ToList();

        //    var parts = partsDto
        //        .Where(x => suppliersIds.Contains(x.SupplierId))
        //        .Select(x => new Part
        //        {
        //            Name = x.Name,
        //            Price = x.Price,
        //            Quantity = x.Quantity,
        //            SupplierId = x.SupplierId
        //        })
        //        .ToList();

        //    context.Parts.AddRange(parts);
        //    context.SaveChanges();

        //    return $"Successfully imported {parts.Count}.";
        //}

        //XML
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(PartDto[]), new XmlRootAttribute("Parts"));
            var textReader = new StringReader(inputXml);
            var partsDto = xmlSerializer.Deserialize(textReader) as PartDto[];

            var suppliersIds = context.Suppliers.Select(s => s.Id).ToList();

            var parts = partsDto
                .Where(x => suppliersIds.Contains(x.SupplierId))
                .Select(x => new Part
                {
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    SupplierId = x.SupplierId
                })
                .ToList();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}";
        }

        // 09. Import Suppliers

        // JSON
        //public static string ImportSuppliers(CarDealerContext context, string inputJson)
        //{
        //    var supplierDto = JsonConvert.DeserializeObject<IEnumerable<SupplierImportModel>>(inputJson);

        //    var suppliers = supplierDto.Select(x => new Supplier()).ToList();

        //    context.Suppliers.AddRange(suppliers);
        //    context.SaveChanges();

        //    return $"Successfully imported {suppliers.Count}.";
        //}

        //XML
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(SupplierDto[]), new XmlRootAttribute("Suppliers"));
            var textReader = new StringReader(inputXml);
            var supplierDto = xmlSerializer.Deserialize(textReader) as SupplierDto[];

            var suppliers = supplierDto.Select(x => new Supplier()).ToList();

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}";
        }
    }
}