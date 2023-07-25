using System.ComponentModel;

namespace ThomasGreg.Domain.Models
{
    public class LogradouroViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nome")]
        public string Nome { get; set; }

        [DisplayName("Numero")]
        public string Numero { get; set; }
    }
}
