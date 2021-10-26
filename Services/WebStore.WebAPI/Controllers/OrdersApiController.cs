using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersApiController : ControllerBase
    {
        private readonly IOrderService _OrderService;

        public OrdersApiController(IOrderService OrderService) => _OrderService = OrderService;

        [HttpGet("user/{UserName}")]
        public async Task<IActionResult> GetUserOrders(string UserName)
        {
            var orders = await _OrderService.GetUserOrders(UserName);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _OrderService.GetOrderById(id);
            if (order is null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost("{UserName}")]
        public async Task<IActionResult> CreateOrder(string UserName)
        {
            var order = await _OrderService.CreateOrder(UserName,);
            return Ok(order);
        }
    }
}