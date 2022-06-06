namespace Inventory_server_gRPC_.Models
{
    public class dbContext
    {
        private List<StockRecord> stock;
        public dbContext()
        {
            stock = new List<StockRecord>()
            {
                new StockRecord(){productId=1,Quantity=10,Price=100},
                new StockRecord(){productId=2,Quantity=20,Price=200},
                new StockRecord(){productId=3,Quantity=15,Price=150},
                new StockRecord(){productId=4,Quantity=17,Price=250},
                new StockRecord(){productId=5,Quantity=13,Price=110},
                new StockRecord(){productId=6,Quantity=21,Price=190},
            };
        }

        public List<StockRecord> GetAll()
        {
            return stock;
        }

        public StockRecord? GetById(int id)
        {
            return stock.SingleOrDefault(s => s.productId == id);
        }

        public int UpdateQuantity(StockRecord record)
        {
            if (record != null)
            {
                try
                {
                    stock.Single(s => s.productId == record.productId).Quantity = record.Quantity;
                    return 1;
                }catch (Exception ex)
                {
                    string error = ex.Message;
                    return -1;
                }
            }
            return -1;
        }
    }
}
