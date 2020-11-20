using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    [Table("Categorias")]
    public class Categoria
    {
        public Categoria()
        {
            CriadoEm = DateTime.Now;
        }
        [Key]
        public int idCategoria { get; set; }
        public String Nome { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}
