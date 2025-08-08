using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenService.Core.Entities;

namespace TokenService.Core.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
}
