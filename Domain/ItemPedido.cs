using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ItemPedido
    {
        public ItemPedido()
        {
            CriadoEm = DateTime.Now;
        }
        [Key]
        public int idPedido { get; set; }
        public Produto Produto { get; set; }
        public Double Preco { get; set; }
        public int Quantidade { get; set; }
        public DateTime CriadoEm { get; set; }
        public String sacolaId { get; set; }
    }
}
