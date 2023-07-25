using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace ThomasGreg.Domain.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nome")]
        public string Nome { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        //[DisplayName("Logotipo")]
        //public IFormFile Imagem { get; set; }

        [DisplayName("Logradouros")]
        public IEnumerable<int> IdsLogradouro { get; set; }

        public byte[]? Logotipo { get; set; }

        public string? ImgDataURL { get; set; }
    }
}
