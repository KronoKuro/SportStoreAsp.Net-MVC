using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Collections.Generic;
namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext(); //здесь создаетется объект context
        // этот объект содержит данные из таблицы Product. короче объект подключение к бд
        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }
        //это реализация интерфейса IProductRepositiry
        /*в этом интерфейсе есть метод которые делает перечисление продуктов вот тут то мы его
         и реализцем берем системное перечисление типа продуки называем его Продукты
         и возвращаем все продукты из  таблицы продукты или дургими словами поле продуктс 
         которое есть в классе
         */

    }
}