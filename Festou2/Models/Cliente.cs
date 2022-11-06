using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Festou2.Models
{
    [Table("Clientes")]
    public class Cliente
    {

        [Key]

        public int ClienteId { get; set; }
        [Required]
        public string ClienteNome { get; set; }
        [Required]
        public DateTime ClienteIdade { get; set; }
        [Required]
        public int ClienteCPF { get; set; }
        [Required]
        [Column(TypeName = "decimal(38,10)")]
        public decimal ClienteBudget { get; set; }
        [Required]
        public ClienteFesta Tipo { get; set; }
    }
    public enum ClienteFesta
    {
        Casamento,
        Noivado,
        Formatura,
        Evento,
        Aniversário
    }
}
