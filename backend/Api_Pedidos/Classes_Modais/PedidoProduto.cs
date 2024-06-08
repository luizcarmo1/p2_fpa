using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api_Pedidos.Classes_Modais
{
    public class PedidoProduto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public Produto Produto { get; set; }
        public double Quantidade { get; set; }
    }
}
