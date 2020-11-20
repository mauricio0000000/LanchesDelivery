using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;

namespace Domain
{
    [Table("Usuarios")]
    public class Usuario
    {
        public Usuario()
        {
            CriadoEm = DateTime.Now;
        }
        [Key]
        public int idUsuario { get; set; }
        public String Nome { get; set; }
        [EmailAddress(ErrorMessage = "E-mail inválido!")]
        public String Email { get; set; }
        public String Senha { get; set; }
        [Compare("Senha", ErrorMessage = "Os campos não coincidem!")]
        [NotMapped]
        public String ConfirmaSenha { get; set; }

        public Endereco Endereco { get; set; }
        public DateTime? CriadoEm { get; set; }
    }
}
