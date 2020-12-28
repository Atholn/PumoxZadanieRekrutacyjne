using CompanyOrganizer.Core.Models;
using CompanyOrganizer.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyOrganizer.Infrastructure.Services
{
    public class UserService : IUserService
    {       
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" },
            new User { Id = 2, FirstName = "Test1", LastName = "User1", Username = "test1", Password = "test1" }
        };

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            if (user == null)
                return null;
         
            return user.WithoutPassword();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(() => _users.WithoutPasswords());
        }
    }
}
