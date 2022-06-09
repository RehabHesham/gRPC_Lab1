
using Grpc.Core;
using StoreAPI_client_grpc_.Models;
using StoreAPI_client_grpc_.Services;

namespace API.Services
{
    public class APIOrderService : APIOrderServiceBase
    {
        PaymentService paymentService;
        StockService StockService;

        public APIOrderService()
        {
            paymentService = new PaymentService();
            StockService = new StockService();
        }
        public override async Task<OrderResponse> MakeOrder(OrderDatail orderDetails, ServerCallContext context)
        {
            if (orderDetails == null)
            {
                return new OrderResponse()
                {
                    ResponseMessage = false
                };
            }

            Order order = ConvertOrderDetailsToOrder(orderDetails);
            var stockResult = await stockService.CallService(order, undo: false);
            if (!stockResult.Sucess)
            {
                return new OrderResponse()
                {
                    ResponseMessage = false,
                    Message = "Out of Stock"
                };
            }

            var paymentResult = await paymentService.CallService(order.UserId, stockResult.OrderCost);
            if (!paymentResult.Sucess)
            {
                await StockService.CallService(order, undo: true);
                return new OrderResponse()
                {
                    ResponseMessage = false,
                    Message = "Account Balance is not enough"
                };
            }

            order.TotalPayment = (decimal)stockResult.OrderCost;
            return new OrderResponse()
            {
                ResponseMessage = true,
                Message = "Success"
            };

        }

        private Order ConvertOrderDetailsToOrder(OrderDatails orderDatails)
        {
            Order order = new Order() { UserId = orderDatails.CustomerId };
            return order;
        }
    }
}
