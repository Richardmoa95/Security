using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataService.Core.Exception;

public class UserNotFoundException : System.Exception
{
    public UserNotFoundException() : base("Usuario no encontrado") 
    { 
    }
}
