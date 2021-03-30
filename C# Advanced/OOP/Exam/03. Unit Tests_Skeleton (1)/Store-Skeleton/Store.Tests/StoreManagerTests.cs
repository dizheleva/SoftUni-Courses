using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Store.Tests
{
    public class StoreManagerTests
    {
        private const string NotPositiveQuantityExceptionMessage = "Product count can't be below or equal to zero.";
        private const string NoSuchProductExceptionMessage = "There is no such product.";
        private const string NotEnoughQuantityExceptionMessage = "There is not enough quantity of this product.";

        private const string testName = "MyName";
        private const int testQuantity = 10;
        private const decimal testPrice = 100;
        private Product testProduct;

        private StoreManager testStoreManager;

        [SetUp]
        public void Setup()
        {

            testProduct = new Product(testName, testQuantity, testPrice);
            testStoreManager = new StoreManager();
        }

        [Test]
        public void ConstructorShouldCreateObject()
        {
            Assert.AreEqual(0, testStoreManager.Count);
        }

        [Test]
        public void CountShouldGiveCorrectCountWhenCountAdded()
        {
            var expectedCount = 1;

            testStoreManager.AddProduct(testProduct);

            Assert.AreEqual(expectedCount, testStoreManager.Count);
        }

        [Test]
        public void AddShouldThrowExceptionWithNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => testStoreManager.AddProduct(null));
        }

        [Test]
        public void AddMethodShouldThrowExceptionWithNegativeQuantity()
        {
            var negativeQuantityProduct = new Product(testName, -10, testPrice);
            var zeroQuantityProduct = new Product(testName, 0, testPrice);

            Assert.Throws<ArgumentException>(() => testStoreManager.AddProduct(negativeQuantityProduct), NotPositiveQuantityExceptionMessage);
            Assert.Throws<ArgumentException>(() => testStoreManager.AddProduct(zeroQuantityProduct), NotPositiveQuantityExceptionMessage);
        }

        [Test]
        public void BuyMethodShouldGiveTotalPrice()
        {
            var finalTestPrice = testProduct.Price * testQuantity;

            testStoreManager.AddProduct(testProduct);
            var result = testStoreManager.BuyProduct(testName, testQuantity);

            Assert.AreEqual(finalTestPrice, result);
            Assert.AreEqual(0, testProduct.Quantity);
        }

        [Test]
        public void BuyMethodShouldDecreaseQuantity()
        {
            testStoreManager.AddProduct(testProduct);
            testStoreManager.BuyProduct(testName, testQuantity);


            Assert.AreEqual(0, testProduct.Quantity);
        }

        [Test]
        public void BuyMethodShouldThrowNullException()
        {
            testStoreManager.AddProduct(testProduct);

            Assert.Throws<ArgumentNullException>(() => testStoreManager.BuyProduct(null, testQuantity), NoSuchProductExceptionMessage);
        }
        [Test]
        public void BuyMethodShouldThrowInvalidExceptionWithQuantity()
        {
            var invalidQuantity = testQuantity + 1;

            testStoreManager.AddProduct(testProduct);

            Assert.Throws<ArgumentException>(() => testStoreManager.BuyProduct(testName, invalidQuantity), NotEnoughQuantityExceptionMessage);
        }

        [Test]
        public void GetTheMostExpensiveProductShouldGiveCorrectProduct()
        {
            var testProductTwo = new Product("Test2", testQuantity, 200);
            var mostExpensiveProduct = new Product("Test3", testQuantity, 500);

            testStoreManager.AddProduct(testProduct);
            testStoreManager.AddProduct(testProductTwo);
            testStoreManager.AddProduct(mostExpensiveProduct);

            Assert.AreEqual(mostExpensiveProduct, testStoreManager.GetTheMostExpensiveProduct());
        }

        [Test]
        public void ComputersShouldGiveCorrectComputersList()
        {
            var testProductTwo = new Product("Test2", testQuantity, 200);
            var testProductThree = new Product("Test3", testQuantity, 500);
            var testList = new List<Product> { testProduct, testProductTwo, testProductThree };

            testStoreManager.AddProduct(testProduct);
            testStoreManager.AddProduct(testProductTwo);
            testStoreManager.AddProduct(testProductThree);

            Assert.AreEqual(testList, testStoreManager.Products);
        }
    }
}