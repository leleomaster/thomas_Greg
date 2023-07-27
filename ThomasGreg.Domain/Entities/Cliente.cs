using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThomasGreg.Domain.Entities
{
    public class Cliente : Base
    {
        [Column("nome")]
        [Required]
        [StringLength(150)]
        public string Nome { get; set; }

        [Column("email")]
        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        [Column("logotipo")]
        [Required]
        public byte[] Logotipo { get; set; }

        [ForeignKey("LogradouroId")]
        public IList<Logradouro> Logradouros { get; set; }


        [NotMapped]
        public IEnumerable<int> LogradouroId
        {
            get
            {
                List<int> ids = new List<int>();
                if (Logradouros != null)
                {                    
                    foreach (var item in Logradouros)
                    {
                        ids.Add(item.Id);
                    }
                }
                return ids;
            }
        }
    }
}
