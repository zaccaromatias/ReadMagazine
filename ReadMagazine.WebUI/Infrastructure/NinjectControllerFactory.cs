using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using ReadMagazine.Domain.Abstract;
using ReadMagazine.Domain.Concrete;
using ReadMagazine.Domain.Concrete.ORM;
using ReadMagazine.WebUI.Infrastructure.Abstract;
using ReadMagazine.WebUI.Infrastructure.Concrete;

namespace ReadMagazine.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext,
        Type controllerType)
        {
            return controllerType == null
            ? null
            : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            // Mock implementation of the IProductRepository Interface
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product> {
            //    new Product { Name = "Football", Price = 25 },
            //    new Product { Name = "Surf board", Price = 179 },
            //    new Product { Name = "Running shoes", Price = 95 }}.AsQueryable());
            //ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);

            //EmailSettings emailSettings = new EmailSettings
            //{
            //    WriteAsFile
            //    = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            //};
            //ninjectKernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);
            //ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            //ninjectKernel.Bind<ICategoriesRepository>().To<EFCategoryRepository>();
            ninjectKernel.Bind<IClientRepository>().To<EFClientRepository>();
            ninjectKernel.Bind<IChannelRepository>().To<EFChannelRepository>();
            ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}