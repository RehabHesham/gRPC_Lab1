namespace StoreAPI_client_grpc_.Models
{
    public class Order
    {
        public int id { get; set; }
        public int UserId { get; set; }
        public decimal TotalPayment { get; set; }
        public List<OrderDatails> orderDatails { get; set; } = new List<OrderDatails>();
    }

    public class OrderDatails
    {
        public int productId { get; set; }
        public int Quantity { get; set; }
    }
}
