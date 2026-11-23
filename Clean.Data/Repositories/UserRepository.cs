using Clean.Core.Entities;
using Clean.Core.Repositories;

namespace Clean.Data.Repositories
{
    // הנתונים יושבים ברשימה בזיכרון.
    // כאן המקום שבו ייכנס EF בהמשך: הרשימה תוחלף ב-DbContext,
    // ואף שורה ב-Clean.Core, ב-Clean.Service או ב-Clean.Api לא תשתנה בגלל זה.
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "רבקה", Email = "rivka@example.com", Password = "1234", Address = "חיפה" },
            new User { Id = 2, Name = "שרה",  Email = "sara@example.com",  Password = "1234", Address = "ירושלים" }
        };

        private int _nextId = 3;

        public List<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(user => user.Id == id);
        }

        public void Add(User user)
        {
            // ה-Id נקבע כאן, כי השכבה שמחזיקה את הנתונים היא זו שיודעת מה פנוי
            user.Id = _nextId;
            _nextId++;
            _users.Add(user);
        }
    }
}
