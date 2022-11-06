using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Festou2.Models
{

    [Table("Locadores")]
    public class Locador : LinksHATEOS
    {

        [Key]

        public int LocadorId { get; set; }
        [Required]
        public string LocadorNome { get; set; }
        [Required]
        public string LocadorDescricaoAmbiente { get; set; }
        [Required]
        public LocadorAmbiente Ambiente { get; set; }
        public DateTime LocadorIdade { get; set; }
        [Required]
        public int LocadorCPF { get; set; }
        [Required]
        public decimal LocadorPreco { get; set; }
        [Required]
        public ICollection<Local> Locais { get; set; }
    }
    public enum LocadorAmbiente
    {
        sítio,
        salão,
        casa,
        fazenda,
        hotel,
        pousada
    }
}
