using Clean.Core.Entities;
using Clean.Core.Repositories;
using Clean.Core.Services;

namespace Clean.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        // שימי לב: ה-Service מקבל interface, לא את UserRepository עצמו.
        // לכן הוא לא יודע אם הנתונים באים מרשימה בזיכרון או ממסד נתונים.
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public User GetUser(int id)
        {
            return _userRepository.GetById(id);
        }

        public void AddUser(User user)
        {
            _userRepository.Add(user);
        }
    }
}
