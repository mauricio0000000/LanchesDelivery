using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class UsuarioLogado : IdentityUser
    {
        public int nivelAcesso { get; set; }
        public int idNormal { get; set; }

    }
}
