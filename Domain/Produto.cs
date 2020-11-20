using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    [Table("Produtos")]
    public class Produto
    {
        public Produto()
        {
            CriadoEm = DateTime.Now;
        }
        [Key]
        public int idProduto { get; set; }
        public int idCozinha { get; set; }
        [Display(Name = "Nome do produto:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(5, ErrorMessage = "No mínimo 5 " +
            "caracteres!")]
        [MaxLength(100, ErrorMessage = "No máximo 100 " +
            "caracteres!")]
        [Display(Name = "Descrição do produto:")]
        public String Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Range(1, 1000, ErrorMessage = "Quantidade apenas entre 1 e 1000!")]
        [Display(Name = "Quantidade do produto:")]
        public int? Quantidade { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Preço do produto:")]
        public Double Preco { get; set; }
        public Categoria Categoria { get; set; }
        public DateTime CriadoEm { get; set; }
        public String Imagem { get; set; }
    }
}
