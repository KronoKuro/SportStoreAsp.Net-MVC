using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Web.Mvc;
using Ninject;
using SportsStore.Domain.Concrete;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam; //прокидываем через объект интерфейса для инициализации
            AddBindings();
        }

        public object GetService(Type serviceType) //стандартный метод для создания обекта нужного типа
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType); // получения множества объектов
        }



        private void AddBindings()
        {
            kernel.Bind<IProductRepository>().To<EFProductRepository>();

        }
    }
}