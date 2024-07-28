using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChartRepoBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace ChartRepoBackend.Services
{
    public class UserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public User Register(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public User GetByUsername(string username)
    {
        return _context.Users.FirstOrDefault(u => u.Username == username);
    }
}

}