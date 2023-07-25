using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThomasGreg.Domain.Entities
{
    public class Logradouro : Base
    {
        [Column("nome")]
        [Required]
        [StringLength(150)]
        public string Nome { get; set; }

        [Column("numero")]
        [Required]
        [StringLength(10)]
        public string Numero { get; set; }

        [ForeignKey("ClienteId")]
        public IEnumerable<Cliente> Clientes{ get; set; }
    }
}
