using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChartRepoBackend.Data
{
    public class UserDatabase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}