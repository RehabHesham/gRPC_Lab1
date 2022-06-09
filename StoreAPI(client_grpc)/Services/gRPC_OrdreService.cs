using Grpc.Core;
using StoreAPI_client_grpc_.Models;
using StoreAPI_client_grpc_.Protos;
using static StoreAPI_client_grpc_.Protos.APIOrderService;

namespace StoreAPI_client_grpc_.Services
{
    public class gRPC_OrdreService : APIOrderServiceBase
    {
        PaymentService paymentService;
        StockService stockService;

        public gRPC_OrdreService()
        {
            paymentService = new PaymentService();
            stockService = new StockService();
        }
        public override async Task<OrderResponse> MakeOrder(OrderDetail orderDetails, ServerCallContext context)
        {
            if (orderDetails == null)
            {
                return new OrderResponse()
                {
                    ResponseMessage = false,
                    Message = "Empty order"
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
                await stockService.CallService(order, undo: true);
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

        private Order ConvertOrderDetailsToOrder(OrderDetail orderDatails)
        {
            Order order = new Order() { UserId = orderDatails.CustomerId };
            return order;
        }
    }
}
