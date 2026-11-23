using Clean.Core.Entities;

namespace Clean.Core.Repositories
{
    // ההצהרה על מה שצריך מהשכבה ששומרת נתונים.
    // המימוש יושב ב-Clean.Data, לא כאן.
    public interface IUserRepository
    {
        List<User> GetAll();

        User GetById(int id);

        void Add(User user);
    }
}
