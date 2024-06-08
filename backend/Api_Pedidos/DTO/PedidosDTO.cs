using Api_Pedidos.Classes_Modais;

namespace Api_Pedidos.DTO
{
    public class PedidosDTO
    {
        public int qtdeProduto { get; set; }
        public DateTime Data_entrada_pedido { get; set; }
        public DateTime Data_saida_pedido { get; set; }
        public string FormaPagamento { get; set; }
        public string StatusPedido { get; set; }

        public Cliente Cliente { get; set; }

        public int[] ProductsId { get; set; }
    }
}
