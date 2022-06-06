using Grpc.Core;
using Inventory_server_gRPC.Protos;
using Inventory_server_gRPC_.Models;
using static Inventory_server_gRPC.Protos.StockUpdate;

namespace Inventory_server_gRPC.Services
{
    public class stockserviceclient : StockUpdateBase
    {
        private readonly ILogger<stockserviceclient> logger;
        private readonly dbContext db;

        public stockserviceclient(ILogger<stockserviceclient> logger)
        {
            this.logger = logger;
            this.db = new dbContext();
        }
        public override Task<ProcessStatus> SendMessage(OrderData request, ServerCallContext context)
        {
            logger.LogInformation($"Request with {request.Products.Count()} products with Undo: {request.Undo}");
            List<StockRecord> stockRecords = new List<StockRecord>();
            foreach (var product in request.Products)
            {
                logger.LogInformation($"\tProduct {product.ProductId} with Quantity {product.ProductQuantity}");
                StockRecord? record = db.GetById(product.ProductId);
                if (record == null)
                {
                    return Task.FromResult(new ProcessStatus()
                    {
                        Sucess = false
                    });
                }
                if (product.ProductQuantity > record.Quantity)
                {
                    logger.LogInformation("Invaild Operation");
                    return Task.FromResult(new ProcessStatus()
                    {
                        Sucess = false
                    });
                }
                stockRecords.Add(record);
            }
            logger.LogInformation("Vaild Operation");
            double totalCost = 0;
            for (int i = 0; i < stockRecords.Count; i++)
            {
                totalCost += request.Products[i].ProductQuantity * stockRecords[i].Price;
                if (request.Undo)
                {
                    stockRecords[i].Quantity += request.Products[i].ProductQuantity;

                }
                else
                {
                    stockRecords[i].Quantity -= request.Products[i].ProductQuantity;
                }
                db.UpdateQuantity(stockRecords[i]);
            }
            return Task.FromResult(new ProcessStatus() { Sucess = true, OrderCost = totalCost });
        }
    }
}
