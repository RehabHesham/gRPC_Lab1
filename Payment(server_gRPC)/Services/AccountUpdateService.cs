using Grpc.Core;
using Payment_server_gRPC_.Models;
using Payment_server_gRPC_.Protos;
using static Payment_server_gRPC_.Protos.AccountUpdate;

namespace Payment_server_gRPC_.Services
{
    public class AccountUpdateService : AccountUpdateBase
    {
        private readonly ILogger<AccountUpdateService> logger;
        private readonly dbContext db;

        public AccountUpdateService(ILogger<AccountUpdateService> logger)
        {
            this.logger = logger;
            this.db = new dbContext();
        }
        public override Task<ProcessStatus> SendMessage(AccountData request, ServerCallContext context)
        {
            logger.LogInformation($"Request from user {request.UserId}, to decrease balance with {request.OrderCost}");
            UserAccount? account = db.GetById(request.UserId);
            
            if(account == null || account.Balance < request.OrderCost)
            {
                return Task.FromResult(new ProcessStatus() { Sucess = false });
            }

            account.Balance -= request.OrderCost;
            db.UpdateBalance(account);
            return Task.FromResult(new ProcessStatus() { Sucess = true });
        }
    }
}
