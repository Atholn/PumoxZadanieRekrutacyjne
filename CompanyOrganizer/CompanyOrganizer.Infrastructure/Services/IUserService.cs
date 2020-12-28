using CompanyOrganizer.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanyOrganizer.Infrastructure.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
    }
}
