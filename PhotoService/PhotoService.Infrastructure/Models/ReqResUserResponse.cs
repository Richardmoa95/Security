using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoService.Infrastructure.Models;

public class ReqResUserResponse
{
    public int Page { get; set; }
    public int Per_Page { get; set; }
    public int Total { get; set; }
    public int Total_Pages { get; set; }
    public List<ReqResUserData> Data { get; set; } = new();
}

public class ReqResUserData
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string First_Name { get; set; } = string.Empty;
    public string Last_Name { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
}