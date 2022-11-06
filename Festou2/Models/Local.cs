using Festou2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Festou2.Models
{
    [Table("Local")]
    public class Local : LinksHATEOS
    {

        [Key]
        public int LocalId { get; set; }
        public Ambiente ambiente { get; set; }
        public int QtdPessoas { get; set; }
        public TipoFesta tipoFesta { get; set; }

    }
    public enum TipoFesta
    {
        Casamento,
        Noivado,
        Formatura,
        Evento,
        Aniversário
    }
    public enum Ambiente
    {
        sítio,
        salão,
        casa,
        fazenda,
        hotel,
        pousada
    }
}
