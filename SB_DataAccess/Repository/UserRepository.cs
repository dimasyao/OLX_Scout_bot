using SB_DataAccess.Data;
using SB_DataAccess.Repository.Interface;
using SB_Models.Models;


namespace SB_DataAccess.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDbContext _database;

        public UserRepository(AppDbContext database) : base(database)
        {
            _database = database;
        }

        public User? Find(long chatId)
        {
            return base.FirstOrDefault(x => x.ChatId == chatId);
        }

        public void Update(User user)
        {
            var userFromDB = base.FirstOrDefault(x => x.ChatId == user.ChatId);

            if (userFromDB != null)
            {
                userFromDB.Name = user.Name;
                userFromDB.Subscriptions = user.Subscriptions;
                userFromDB.MenuState = user.MenuState;
            }
        }
    }
}
