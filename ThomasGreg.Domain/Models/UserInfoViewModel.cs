using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThomasGreg.Domain.Models
{
    public class UserInfoViewModel
    {
        public string? UserID { get; set; }
        public string? Password { get; set; }
        public bool ManterLogado { get; set; }
    }
}
