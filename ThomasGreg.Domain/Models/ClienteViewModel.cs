using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using ThomasGreg.Domain.Entities;

namespace ThomasGreg.Domain.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nome")]
        public string Nome { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Logradouros")]
        public IEnumerable<int> LogradouroId { get; set; }

        [DisplayName("Logotipo")]
        public byte[]? Logotipo { get; set; }

        public string? ImgDataURL { get; set; }
        public IList<LogradouroViewModel> Logradouros { get; set; }
    }
}
