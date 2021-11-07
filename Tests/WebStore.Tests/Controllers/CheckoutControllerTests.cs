﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Controllers;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class CheckoutControllerTests
    {
        [TestMethod]
        public void Index_Returns_View()
        {
            var controller = new CheckoutController();

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }
    }
}
