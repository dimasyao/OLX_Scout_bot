using Microsoft.EntityFrameworkCore.Update.Internal;
using SB_Models.Models;

namespace SB_DataAccess.Repository.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        public void Update(User user);
    }
}
