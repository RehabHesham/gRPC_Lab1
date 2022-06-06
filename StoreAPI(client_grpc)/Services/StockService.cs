using Grpc.Net.Client;
using Inventory_server_gRPC.Protos;
using StoreAPI_client_grpc_.Models;
using static Inventory_server_gRPC.Protos.StockUpdate;

namespace StoreAPI_client_grpc_.Services
{
    public class StockService
    {
        public async Task<ProcessStatus> CallService(Order order,bool undo)
        {
            var stockChannel = GrpcChannel.ForAddress("https://localhost:7052");
            var stockClient = new StockUpdateClient(stockChannel);

            var stockMessage = new OrderData()
            {
                Undo = undo
            };
            foreach (var item in order.orderDatails)
            {
                stockMessage.Products.Add(convertOrderDetailsToData(item));
            }

            return await stockClient.SendMessageAsync(stockMessage);
        }

        private Product convertOrderDetailsToData(OrderDatails orderDatail)
        {
            return (new Product()
            {
                ProductId = orderDatail.productId,
                ProductQuantity = orderDatail.Quantity
            });
        }
    }
}
