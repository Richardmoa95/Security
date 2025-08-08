using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenService.Core.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() 
        : base ($"Email o contraseña inválidos.")
    {
    }
}
