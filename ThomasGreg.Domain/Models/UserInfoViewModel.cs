using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThomasGreg.Domain.Models
{
    public class UserInfoViewModel
    {
        [DisplayName("Login")]
        public string? UserID { get; set; }

        [DisplayName("Senha")]
        public string? Password { get; set; }

        [DisplayName("Manter logado")]
        public bool ManterLogado { get; set; }
    }
}
