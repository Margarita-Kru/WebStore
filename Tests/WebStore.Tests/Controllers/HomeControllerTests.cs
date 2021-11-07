using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebStore.Controllers;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Status_with_id_404_Returns_View()
        {
            const string id = "404";
            const string expected_view_name = "Error";
            var controller = new HomeController();

            var result = controller.Status(id);

            var view_result = Assert.IsType<ViewResult>(result);

            var actual_view_name = view_result.ViewName;

            Assert.Equal(expected_view_name, actual_view_name);
        }

        [TestMethod]
        public void Status_with_id_123_Returns_View()
        {
            const string id = "123";
            const string expected_view_name = "Status code - " + id;
            var controller = new HomeController();

            var result = controller.Status(id);

            var content_result = Assert.IsType<ContentResult>(result);

            var actual_content = content_result.Content;

            Assert.Equal(expected_view_name, actual_content);
        }
      
        [TestMethod]
        public void Status_thrown_ArgumentNullException_when_id_is_null()
        {
            const string expected_parameter_name = "id";
            var controller = new HomeController();

            var actual_exception = Assert.Throws<ArgumentNullException>(() => controller.Status(null));
            var actual_parameter_name = actual_exception.ParamName;
            Assert.Equal(expected_parameter_name, actual_parameter_name);
        }
    }
}
