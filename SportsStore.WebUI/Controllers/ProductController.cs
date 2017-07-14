using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Web.Mvc;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository; // объект интерфейса для внедрения зависимости
        public int PageSize = 4; //количество отображаемых элементов на странице

        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository; //инициализируем объект интерфейса с помощью конструктора
        }

        // передаем кол страниц в конструктор page1
        public ViewResult List(string category,int page = 1)
        {
            //создадим экземпляр класса 
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products.Where(p => category == null || p.Category == category).OrderBy(p => p.ProductID).Skip((page-1)*PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()

                },
                CurrentCategory = category
            };
            return View(model);
            /*тут мы берем хранилище получаем от туда списов товаров и выводим сортируем по ID далее , skip 
             дает нам возможность получить товары если не указана страница с товарами и Take взять 4 элемента*/
        }
    }
}