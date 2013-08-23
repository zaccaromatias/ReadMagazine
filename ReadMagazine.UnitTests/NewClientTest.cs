using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReadMagazine.Domain.Abstract;
using ReadMagazine.Domain.Entities;
using ReadMagazine.WebUI.Controllers;
using ReadMagazine.WebUI.Models;

namespace ReadMagazine.UnitTests
{
    [TestClass]
    public class NewClientTest
    {
        [TestMethod]
        public void Can_Save_New_Client_Valid()
        {
            //// Arrange - create mock repository
            //Mock<IClientRepository> mock = new Mock<IClientRepository>();
            //// Arrange - create the controller
            //ClientController target = new ClientController(mock.Object);
            //// Arrange - create a client
            //ClientExtendedViewModel client = new ClientExtendedViewModel() { UserName = "Test", Email = "matias.zaccaro@gmail.com", Password = "123456" };
            //// Act - try to save the client
            //ActionResult result = target.AddClient(client);
            //// Assert - check that the repository was called
            ////mock.Verify(m => m.SaveClient(client));
            //// Assert - check the method result type
            //Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }
    }
}
