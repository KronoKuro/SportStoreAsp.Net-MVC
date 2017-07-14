using System.Collections.Generic;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Models
{
    //класс для предоставления свеждений о товаре
    public class ProductsListViewModel 
    {
        public IEnumerable<Product> Products { get; set; } // продукты  перечисление их
        public PagingInfo PagingInfo { get; set; } //поле для  вкладок
        public string CurrentCategory { get; set; }
    }
}