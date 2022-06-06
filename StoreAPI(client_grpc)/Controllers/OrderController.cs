
using Microsoft.AspNetCore.Mvc;
using StoreAPI_client_grpc_.Models;
using StoreAPI_client_grpc_.Services;

namespace StoreAPI_client_grpc_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        PaymentService paymentService;
        StockService StockService;

        public OrderController()
        {
            paymentService = new PaymentService();
            StockService = new StockService();
        }
        [HttpPost]
        public async Task<IActionResult> SubmitOrder(Order order)
        {
            if(order == null)
            {
                return BadRequest();
            }

            var stockResult = await StockService.CallService(order, undo: false);
            if(!stockResult.Sucess)
            {
                return BadRequest("Out of Stock");
            }

            var paymentResult = await paymentService.CallService(order.UserId, stockResult.OrderCost);
            if (!paymentResult.Sucess)
            {
                await StockService.CallService(order, undo: true);
                return BadRequest("Account Balance is not enough");
            }

            order.TotalPayment = (decimal)stockResult.OrderCost;
            return Ok(order);
        }

        
    }
}
