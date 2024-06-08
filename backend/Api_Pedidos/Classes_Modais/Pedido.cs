using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Pedidos.Classes_Modais
{
    public class Pedido
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id_pedido { get; set; }
        public int qtdeProduto { get; set; }
        public DateTime Data_entrada_pedido { get; set; }
        public DateTime Data_saida_pedido { get; set; }
        public string FormaPagamento { get; set; }
        public string StatusPedido { get; set; }


        public Cliente Cliente { get; set; }

        // Propriedade de navegação para os produtos associados ao pedido
        public List<PedidoProduto> Produtos { get; set; }
    }
}
