

using Grpc.Net.Client;
using Payment_server_gRPC_.Protos;
using static Payment_server_gRPC_.Protos.AccountUpdate;

namespace StoreAPI_client_grpc_.Services
{
    public class PaymentService
    {
        public async Task<ProcessStatus> CallService(int userId,double cost)
        {
            var paymentChannel = GrpcChannel.ForAddress("https://localhost:7290");
            var accountClient = new AccountUpdateClient(paymentChannel);

            return await accountClient.SendMessageAsync(new AccountData()
            {
                UserId = userId,
                OrderCost = cost
            });
        }
    }
}
