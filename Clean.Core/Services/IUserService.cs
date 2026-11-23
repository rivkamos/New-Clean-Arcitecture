using Clean.Core.Entities;

namespace Clean.Core.Services
{
    public interface IUserService
    {
        List<User> GetUsers();

        User GetUser(int id);

        void AddUser(User user);
    }
}
