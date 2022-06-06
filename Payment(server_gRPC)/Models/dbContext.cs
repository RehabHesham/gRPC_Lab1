

namespace Payment_server_gRPC_.Models
{
    public class dbContext
    {
        public List<UserAccount> accounts;
        public dbContext()
        {
            accounts = new List<UserAccount>()
            {
                new UserAccount(){userId=1,Balance=3000},
                new UserAccount(){userId=2,Balance=20000},
                new UserAccount(){userId=3,Balance=30100},
                new UserAccount(){userId=4,Balance=25000},
                new UserAccount(){userId=5,Balance=19000},
                new UserAccount(){userId=6,Balance=20900},
            };
        }
        public List<UserAccount> GetAll()
        {
            return accounts;
        }

        public UserAccount? GetById(int id)
        {
            return accounts.SingleOrDefault(s => s.userId == id);
        }

        public int UpdateBalance(UserAccount record)
        {
            if (record != null)
            {
                try
                {
                    accounts.Single(s => s.userId == record.userId).Balance = record.Balance;
                    return 1;
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    return -1;
                }
            }
            return -1;
        }
    }
}
