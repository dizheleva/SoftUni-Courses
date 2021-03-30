# Exercises: XML Processing

This document defines the **exercise assignments** for the [&quot;Databases Advanced – EF Core&quot; course @ Software University](https://softuni.bg/trainings/3221/entity-framework-core-february-2021).

# Product Shop Database

A products shop holds **users** , **products** and **categories** for the products. Users can **sell** and **buy** products.

- Users have an **id** , **first**** name**(optional) and**last ****name** and **age** (optional).
- Products have an **id** , **nam** , **price** , **buyerId** (optional) and **sellerId** as IDs of users.
- Categories have an **id** and **name**.
- Using Entity Framework Code First create a database following the above description.

![](RackMultipart20210325-4-1ky6l4d_html_7b37463b1014bc3e.png)

- **Users** should have **many products sold** and **many products bought**.
- **Products** should have **many categories**
- **Categories** should have **many products**
- **CategoryProducts** should **map products** and **categories**

1.
# Import Data

1.
# Import Users

**NOTE** : You will need method publicstaticstring ImportUsers(ProductShopContext context, string inputXml) and publicStartUp class.

Import the users from the provided file **users.xml**.

Your method should return string with message $&quot;Successfully imported {Users.Count}&quot;;

1.
# Import Products

**NOTE** : You will need method publicstaticstring ImportProducts(ProductShopContext context, string inputXml) and publicStartUp class.

Import the products from the provided file **products.xml**.

Your method should return string with message $&quot;Successfully imported {Products.Count}&quot;;

1.
# Import Categories

**NOTE** : You will need method publicstaticstring ImportCategories(ProductShopContext context, string inputXml) and publicStartUp class.

Import the categories from the provided file **categories.xml**.

Some of the names will be null, so you don&#39;t have to add them in the database. Just skip the record and continue.

Your method should return string with message $&quot;Successfully imported {Categories.Count}&quot;;

1.
# Import Categories and Products

**NOTE** : You will need method publicstaticstring ImportCategoryProducts(ProductShopContext context, string inputXml) and publicStartUp class.

Import the categories and products ids from the provided file **categories-products.xml**. If provided category or product id, doesn&#39;t exists, skip the whole entry!

Your method should return string with message $&quot;Successfully imported {CategoryProducts.Count}&quot;;

1.
# Query and Export Data

Write the below described queries and **export** the returned data to the specified **format**. Make sure that Entity Framework generates only a **single query** for each task.

1.
# Products In Range

**NOTE** : You will need method publicstaticstring GetProductsInRange(ProductShopContext context) and publicStartUp class.

Get all products in a specified **price range** between 500 and 1000 (inclusive). Order them by price (from lowest to highest). Select only the **product name** , **price** and the **full name**** of the buyer **. Take top** 10** records.

**Return** the list of suppliers **to XML** in the format provided below.

| **products-in-range.xml** |
| --- |
| \&lt;?xmlversion=&quot;1.0&quot;encoding=&quot;utf-16&quot;?\&gt;\&lt;Products\&gt;\&lt;Product\&gt;\&lt;name\&gt;TRAMADOL HYDROCHLORIDE\&lt;/name\&gt;\&lt;price\&gt;516.48\&lt;/price\&gt;\&lt;/Product\&gt;\&lt;Product\&gt;\&lt;name\&gt;Allopurinol\&lt;/name\&gt;\&lt;price\&gt;518.5\&lt;/price\&gt;\&lt;buyer\&gt;Wallas Duffyn\&lt;/buyer\&gt;\&lt;/Product\&gt;\&lt;Product\&gt;\&lt;name\&gt;Parsley\&lt;/name\&gt;\&lt;price\&gt;519.06\&lt;/price\&gt;\&lt;buyer\&gt;Brendin Predohl\&lt;/buyer\&gt;\&lt;/Product\&gt;…\&lt;/Products\&gt; |

1.
# Sold Products

**NOTE** : You will need method publicstaticstring GetSoldProducts(ProductShopContext context) and publicStartUp class.

Get all users who have **at least 1 sold item**. Order them by **last name** , then by **first name**. Select the person&#39;s **first** and **last name**. For each of the **sold products** , select the product&#39;s **name** and **price**. Take top **5** records.

**Return** the list of suppliers **to XML** in the format provided below.

| **users-sold-products.xml** |
| --- |
| \&lt;?xmlversion=&quot;1.0&quot;encoding=&quot;utf-16&quot;?\&gt;\&lt;Users\&gt;\&lt;User\&gt;\&lt;firstName\&gt;Almire\&lt;/firstName\&gt;\&lt;lastName\&gt;Ainslee\&lt;/lastName\&gt;\&lt;soldProducts\&gt;\&lt;Product\&gt;\&lt;name\&gt;olio activ mouthwash\&lt;/name\&gt;\&lt;price\&gt;206.06\&lt;/price\&gt;\&lt;/Product\&gt;\&lt;Product\&gt;\&lt;name\&gt;Acnezzol Base\&lt;/name\&gt;\&lt;price\&gt;710.6\&lt;/price\&gt;\&lt;/Product\&gt;\&lt;Product\&gt;\&lt;name\&gt;ENALAPRIL MALEATE\&lt;/name\&gt;\&lt;price\&gt;210.42\&lt;/price\&gt;\&lt;/Product\&gt;\&lt;/soldProducts\&gt;\&lt;/User\&gt;...\&lt;/Users\&gt; |

1.
# Categories By Products Count

**NOTE** : You will need method publicstaticstring GetCategoriesByProductsCount(ProductShopContext context) and publicStartUp class.

Get **all**** categories **. For each category select its** name **, the** number of products **, the** average price of those products **and the** total revenue**(total price sum) of those products (regardless if they have a buyer or not). Order them by the**number of products**(**descending**) then by total revenue.

**Return** the list of suppliers **to XML** in the format provided below.

| **categories-by-products.xml** |
| --- |
| \&lt;?xmlversion=&quot;1.0&quot;encoding=&quot;utf-16&quot;?\&gt;\&lt;Categories\&gt;\&lt;Category\&gt;\&lt;name\&gt;Garden\&lt;/name\&gt;\&lt;count\&gt;23\&lt;/count\&gt;\&lt;averagePrice\&gt;709.94739130434782608695652174\&lt;/averagePrice\&gt;\&lt;totalRevenue\&gt;16328.79\&lt;/totalRevenue\&gt;\&lt;/Category\&gt;\&lt;Category\&gt;\&lt;name\&gt;Adult\&lt;/name\&gt;\&lt;count\&gt;22\&lt;/count\&gt;\&lt;averagePrice\&gt;704.41\&lt;/averagePrice\&gt;\&lt;totalRevenue\&gt;15497.02\&lt;/totalRevenue\&gt;\&lt;/Category\&gt;...\&lt;/Categories\&gt; |

1.
# Users and Products

**NOTE** : You will need method publicstaticstring GetUsersWithProducts(ProductShopContext context) and publicStartUp class.

Select users who have **at least 1 sold product**. Order them by the **number of sold products** (from highest to lowest). Select only their **first** and **last name** , **age, count** of sold products and for each product - **name** and **price** sorted by price (descending). Take top **10** records.

Follow the format below to better understand how to structure your data.

**Return** the list of suppliers **to XML** in the format provided below.

| **users-and-products.xml** |
| --- |
| \&lt;Users\&gt;\&lt;count\&gt;54\&lt;/count\&gt;\&lt;users\&gt;\&lt;User\&gt;\&lt;firstName\&gt;Cathee\&lt;/firstName\&gt;\&lt;lastName\&gt;Rallings\&lt;/lastName\&gt;\&lt;age\&gt;33\&lt;/age\&gt;\&lt;SoldProducts\&gt;\&lt;count\&gt;9\&lt;/count\&gt;\&lt;products\&gt;\&lt;Product\&gt;\&lt;name\&gt;Fair Foundation SPF 15\&lt;/name\&gt;\&lt;price\&gt;1394.24\&lt;/price\&gt;\&lt;/Product\&gt;\&lt;Product\&gt;\&lt;name\&gt;IOPE RETIGEN MOISTURE TWIN CAKE NO.21\&lt;/name\&gt;\&lt;price\&gt;1257.71\&lt;/price\&gt;\&lt;/Product\&gt;\&lt;Product\&gt;\&lt;name\&gt;ESIKA\&lt;/name\&gt;\&lt;price\&gt;879.37\&lt;/price\&gt;\&lt;/Product\&gt;\&lt;Product\&gt;\&lt;name\&gt;allergy eye\&lt;/name\&gt;\&lt;price\&gt;426.91\&lt;/price\&gt;\&lt;/Product\&gt;...\&lt;/Users\&gt; |

# Car Dealer Database

1.
# Setup Database

A car dealer needs information about cars, their parts, parts suppliers, customers and sales.

- **Cars** have **make, model** , travelled distance in kilometers
- **Parts** have **name** , **price** and **quantity**
- Part **supplier** have **name** and info whether he **uses imported parts**
- **Customer** has **name** , **date of birth** and info whether he **is young driver**
- **Sale** has **car** , **customer** and **discount percentage**

A **price of a car** is formed by **total price of its parts**.

![](RackMultipart20210325-4-1ky6l4d_html_b6e69f23ed7bdacf.png)

- A **car** has **many parts** and **one part** can be placed **in many cars**
- **One supplier** can supply **many parts** and each **part** can be delivered by **only one supplier**
- In **one sale** , only **one car** can be sold
- **Each sale** has **one customer** and **a customer** can buy **many cars**

1.
# Import Data

Import data from the provided files ( **suppliers.xml, parts.xml, cars.xml, customers.xml** ).

1.
# Import Suppliers

**NOTE** : You will need method publicstaticstring ImportSuppliers(CarDealerContext context, string inputXml) and publicStartUp class.

Import the suppliers from the provided file **suppliers.xml**.

Your method should return string with message $&quot;Successfully imported {suppliers.Count}&quot;;

1.
# Import Parts

**NOTE** : You will need method publicstaticstring ImportParts(CarDealerContext context, string inputXml) and publicStartUp class.

Import the parts from the provided file **parts.xml**. If the supplierId doesn&#39;t exists, skip the record.

Your method should return string with message $&quot;Successfully imported {parts.Count}&quot;;

1.
# Import Cars

**NOTE** : You will need method publicstaticstring ImportCars(CarDealerContext context, string inputXml) and publicStartUp class.

Import the cars from the provided file **cars.xml**. Select unique car part ids. If the part id doesn&#39;t exists, skip the part record.

Your method should return string with message $&quot;Successfully imported {cars.Count}&quot;;

1.
# Import Customers

**NOTE** : You will need method publicstaticstring ImportCustomers(CarDealerContext context, string inputXml) and publicStartUp class.

Import the customers from the provided file **customers.xml**.

Your method should return string with message $&quot;Successfully imported {customers.Count}&quot;;

1.
# Import Sales

**NOTE** : You will need method publicstaticstring ImportSales(CarDealerContext context, string inputXml) and publicStartUp class.

Import the sales from the provided file **sales.xml**. If car doesn&#39;t exists, skip whole entity.

Your method should return string with message $&quot;Successfully imported {sales.Count}&quot;;

1.
# Query and Export Data

Write the below described queries and **export** the returned data to the specified **format**. Make sure that Entity Framework generates only a **single query** for each task.

1.
# Cars With Distance

**NOTE** : You will need method publicstaticstring GetCarsWithDistance(CarDealerContext context) and publicStartUp class.

Get all **cars** with distance more than 2,000,000. Order them by make, then by model alphabetically. Take top 10 records.

**Return** the list of suppliers **to XML** in the format provided below.

| **cars.xml** |
| --- |
| \&lt;?xmlversion=&quot;1.0&quot;encoding=&quot;utf-16&quot;?\&gt;\&lt;cars\&gt;\&lt;car\&gt;\&lt;make\&gt;BMW\&lt;/make\&gt;\&lt;model\&gt;1M Coupe\&lt;/model\&gt;\&lt;travelled-distance\&gt;39826890\&lt;/travelled-distance\&gt;\&lt;/car\&gt;\&lt;car\&gt;\&lt;make\&gt;BMW\&lt;/make\&gt;\&lt;model\&gt;E67\&lt;/model\&gt;\&lt;travelled-distance\&gt;476830509\&lt;/travelled-distance\&gt;\&lt;/car\&gt;\&lt;car\&gt;\&lt;make\&gt;BMW\&lt;/make\&gt;\&lt;model\&gt;E88\&lt;/model\&gt;\&lt;travelled-distance\&gt;27453411\&lt;/travelled-distance\&gt;\&lt;/car\&gt; ...\&lt;/cars\&gt; |

1.
# Cars from make BMW

**NOTE** : You will need method publicstaticstring GetCarsFromMakeBmw(CarDealerContext context) and publicStartUp class.

Get all **cars** from make **BMW** and **order them by model alphabetically** and by **travelled distance descending**.

**Return** the list of suppliers **to XML** in the format provided below.

| **bmw-cars.xml** |
| --- |
| \&lt;cars\&gt;\&lt;carid=&quot;7&quot;model=&quot;1M Coupe&quot;travelled-distance=&quot;39826890&quot; /\&gt;\&lt;carid=&quot;16&quot;model=&quot;E67&quot;travelled-distance=&quot;476830509&quot; /\&gt;\&lt;carid=&quot;5&quot;model=&quot;E88&quot;travelled-distance=&quot;27453411&quot; /\&gt;...\&lt;/cars\&gt; |

1.
# Local Suppliers

**NOTE** : You will need method publicstaticstring GetLocalSuppliers(CarDealerContext context) and publicStartUp class.

Get all **suppliers** that **do not import parts from abroad**. Get their **id** , **name** and **the number of parts they can offer to supply**.

**Return** the list of suppliers **to XML** in the format provided below.

| **local-suppliers.xml** |
| --- |
| \&lt;?xmlversion=&quot;1.0&quot;encoding=&quot;utf-16&quot;?\&gt;\&lt;suppliers\&gt;\&lt;suplier id=&quot;2&quot; name=&quot;VF Corporation&quot; parts-count=&quot;3&quot; /\&gt;\&lt;suplier id=&quot;5&quot; name=&quot;Saks Inc&quot; parts-count=&quot;2&quot;/\&gt;...\&lt;/suppliers\&gt; |

1.
# Cars with Their List of Parts

**NOTE** : You will need method publicstaticstring GetCarsWithTheirListOfParts(CarDealerContext context) and publicStartUp class.

Get all **cars along with their list of parts**. For the **car** get only **make, model** and **travelled distance** and for the **parts** get only **name** and **price** and sort all parts by price (descending). Sort all cars by travelled distance ( **descending** ) then by model ( **ascending** ). Select top 5 records.

**Return** the list of suppliers **to XML** in the format provided below.

| **cars-and-parts.xml** |
| --- |
| \&lt;?xmlversion=&quot;1.0&quot;encoding=&quot;utf-16&quot;?\&gt;\&lt;cars\&gt;\&lt;carmake=&quot;Opel&quot;model=&quot;Astra&quot;travelled-distance=&quot;516628215&quot;\&gt;\&lt;parts\&gt;\&lt;partname=&quot;Master cylinder&quot;price=&quot;130.99&quot; /\&gt;\&lt;partname=&quot;Water tank&quot;price=&quot;100.99&quot; /\&gt;\&lt;partname=&quot;Front Right Side Inner door handle&quot;price=&quot;100.99&quot; /\&gt;\&lt;/parts\&gt;\&lt;/car\&gt;...\&lt;/cars\&gt; |

1.
# Total Sales by Customer

**NOTE** : You will need method publicstaticstring GetTotalSalesByCustomer(CarDealerContext context) and publicStartUp class.

Get all **customers** that have bought **at least 1 car** and get their **names** , **bought cars**** count **and** total spent money **on cars.** Order **the result list** by total spent money descending**.

**Return** the list of suppliers **to XML** in the format provided below.

| **customers-total-sales.xml** |
| --- |
| \&lt;?xmlversion=&quot;1.0&quot;encoding=&quot;utf-16&quot;?\&gt;\&lt;customers\&gt;\&lt;customerfull-name=&quot;Hai Everton&quot;bought-cars=&quot;1&quot;spent-money=&quot;2544.67&quot; /\&gt;\&lt;customerfull-name=&quot;Daniele Zarate&quot;bought-cars=&quot;1&quot;spent-money=&quot;2014.83&quot; /\&gt;\&lt;customerfull-name=&quot;Donneta Soliz&quot;bought-cars=&quot;1&quot;spent-money=&quot;1655.57&quot; /\&gt;...\&lt;/customers\&gt; |

1.
# Sales with Applied Discount

**NOTE** : You will need method publicstaticstring GetSalesWithAppliedDiscount(CarDealerContext context) and publicStartUp class.

Get all **sales** with information about the **car** , **customer** and **price** of the sale **with and without discount**.

**Return** the list of suppliers **to XML** in the format provided below.

| **sales-discounts.xml** |
| --- |
| \&lt;?xmlversion=&quot;1.0&quot;encoding=&quot;utf-16&quot;?\&gt;\&lt;sales\&gt;\&lt;sale\&gt;\&lt;carmake=&quot;BMW&quot;model=&quot;M5 F10&quot;travelled-distance=&quot;435603343&quot; /\&gt;\&lt;discount\&gt;30.00\&lt;/discount\&gt;\&lt;customer-name\&gt;Hipolito Lamoreaux\&lt;/customer-name\&gt;\&lt;price\&gt;707.97\&lt;/price\&gt;\&lt;price-with-discount\&gt;495.58\&lt;/price-with-discount\&gt;\&lt;/sale\&gt;...\&lt;/sales\&gt; |

![](RackMultipart20210325-4-1ky6l4d_html_9b0988e43f50c79b.gif) ![](RackMultipart20210325-4-1ky6l4d_html_8e84094ace6df644.gif) ![](RackMultipart20210325-4-1ky6l4d_html_75bb621a2d054d6e.gif) ![](RackMultipart20210325-4-1ky6l4d_html_f746d52952cd7e91.gif)[![](RackMultipart20210325-4-1ky6l4d_html_3aa486326bfa75e9.png)](https://softuni.org/)

© SoftUni – [about.softuni.bg](https://about.softuni.bg/). Copyrighted document. Unauthorized copy, reproduction or use is not permitted.

[![](RackMultipart20210325-4-1ky6l4d_html_9b17934bfedeb713.png)](https://softuni.org/)[![](RackMultipart20210325-4-1ky6l4d_html_c9db196993f48ff8.png)](https://softuni.bg/)[![Software University @ Facebook](RackMultipart20210325-4-1ky6l4d_html_94be3df36d913358.png)](https://www.facebook.com/softuni.org)[![](RackMultipart20210325-4-1ky6l4d_html_7e8e605369b4ad74.png)](https://www.instagram.com/softuni_org)[![Software University @ Twitter](RackMultipart20210325-4-1ky6l4d_html_ff9c629b0a21eb6b.png)](https://twitter.com/SoftUni1)[![Software University @ YouTube](RackMultipart20210325-4-1ky6l4d_html_7db86a748da0e575.png)](https://www.youtube.com/channel/UCqvOk8tYzfRS-eDy4vs3UyA)[![](RackMultipart20210325-4-1ky6l4d_html_95554caa563bbdb3.png)](https://www.linkedin.com/company/softuni/)[![](RackMultipart20210325-4-1ky6l4d_html_4df51bfadcab813.png)](https://github.com/SoftUni)

Follow us:

Page 3 of 3