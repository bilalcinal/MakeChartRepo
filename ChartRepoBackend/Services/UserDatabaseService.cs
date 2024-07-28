using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChartRepoBackend.Data;

namespace ChartRepoBackend.Services
{
    public class UserDatabaseService
{
    private readonly ApplicationDbContext _context;

    public UserDatabaseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public UserDatabase Save(UserDatabase userDatabase)
    {
        _context.UserDatabases.Add(userDatabase);
        _context.SaveChanges();
        return userDatabase;
    }

    public List<UserDatabase> GetByUserId(int userId)
    {
        return _context.UserDatabases.Where(db => db.UserId == userId).ToList();
    }
}
}