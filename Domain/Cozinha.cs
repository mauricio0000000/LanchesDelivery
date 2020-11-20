using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    [Table("Cozinhas")]
    public class Cozinha
    {
        public Cozinha()
        {
            CriadoEm = DateTime.Now;
            Avaliacao = null;
        }

        [Key]
        public int idCozinha { get; set; }
        [Display(Name = "Nome da cozinha:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public String Nome { get; set; }

        [EmailAddress(ErrorMessage = "E-mail inválido!")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public String Telefone { get; set; }
        public String Senha { get; set; }
        [Compare("Senha", ErrorMessage = "Os campos não coincidem!")]
        [NotMapped]
        public String ConfirmaSenha { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(5, ErrorMessage = "No mínimo 5 " +
            "caracteres!")]
        [MaxLength(100, ErrorMessage = "No máximo 100 " +
            "caracteres!")]
        [Display(Name = "Descrição da cozinha:")]
        public String Descricao { get; set; }
        public int? Avaliacao { get; set; }
        public Endereco Endereco { get; set; }
        public DateTime CriadoEm { get; set; }
        public String Imagem { get; set; }
    }
}
