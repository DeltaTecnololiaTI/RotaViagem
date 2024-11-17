using System.ComponentModel.DataAnnotations.Schema;

namespace RotaViagemModel.Model
{
    public class TabRota
    {
        public int Id { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }
    }
}
