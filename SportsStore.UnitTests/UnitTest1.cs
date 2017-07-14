using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;
using System.Linq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate() //тест на совпадения количества элементов на странице
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>(); // создаем хранилище mock 
            //устанавливаем связь с Psroducts создаес имитируемый массив
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { ProductID = 1, Name = "P1"},
                new Product { ProductID = 2, Name = "P2"},
                new Product { ProductID = 3, Name = "P3"},
                new Product { ProductID = 4, Name = "P4"},
                new Product { ProductID = 5, Name = "P5"}
            });
            ProductController controller = new ProductController(mock.Object); 
        //controller это типо созданый контроллер с переданным в него параметров имитируемой бд
            controller.PageSize = 3; //отобразить 3 элемента на странице
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }
        [TestMethod]
        public void Can_Generate_Page_Links() //тест на совпадение ссылок в текстовом формате с с тем что получилось
        {
            HtmlHelper myHelper = null; //объект HtmlHelper 
            PagingInfo pagingInfo = new PagingInfo //объект pagingInfo типо экземпляр со своими параметрами
            {
                CurrentPage = 2, //Текущая странциа 2
                TotalItems = 28, //всего элементов 28
                ItemsPerPage = 10 //элементов на странице не бьольше 10
            };
            Func<int, string> pageUrlDelegate = i => "Page" + i; //делегат  который берет целочисленное  и переделывает ее в Page и номер стр
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>" + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"+ @"<a class=""btn btn-default"" href=""Page3"">3</a>", result.ToString());
         }

        [TestMethod]
        public void Can_send_pagination_View_Model()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m =>m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "Р1"},
                new Product {ProductID = 2, Name = "Р2"},
                new Product {ProductID = 3, Name = "Р3"},
                new Product {ProductID = 4, Name = "Р4"},
                new Product {ProductID = 5, Name = "Р5"}
            });
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            ProductsListViewModel result = (ProductsListViewModel) controller.List(null, 2).Model;
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalPages,2);
            Assert.AreEqual(pageInfo.TotalItems, 5);
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product{ ProductID = 1, Name = "H1" , Category = "Vasy"},
                new Product{ ProductID = 2, Name = "H5" , Category = "Vasy"},
                new Product{ ProductID = 3, Name = "H3" , Category = "Jobs"},
                new Product{ ProductID = 4, Name = "H2" , Category = "Vasy"},
                new Product{ ProductID = 5, Name = "H12" , Category = "Jobs"}

            });
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            Product[] result = ((ProductsListViewModel)controller.List("Vasy", 1).Model).Products.ToArray();  
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "H1" && result[0].Category == "Vasy");
            Assert.IsTrue(result[4].Name == "H12" && result[4].Category == "Jobs");
        }
    }
    
}

