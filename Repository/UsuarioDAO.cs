using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UsuarioDAO : IRepository<Usuario>
    {
        private readonly Context context;

        public UsuarioDAO(Context context)
        {
            this.context = context;
        }

        public Usuario BuscarPorId(int id)
        {
            return context.usuarios.Find(id);
        }

        public Usuario BuscarPorNome(String nome)
        {
            return context.usuarios.Find(nome);
        }

        public Usuario BuscarPorEmail(Usuario u)
        {
            return context.usuarios.FirstOrDefault(x => x.Email.Equals(u.Email));
        }

        public Cozinha BuscarPorEndereco(Usuario u)
        {
            return context.cozinhas.FirstOrDefault(x => x.Endereco.Equals(u.Endereco));
        }

        public bool Cadastrar(Usuario u)
        {
            if (BuscarPorEmail(u) == null)
            {
                u.CriadoEm = DateTime.Now;
                context.usuarios.Add(u);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Usuario> ListarTodos()
        {
            return context.usuarios.ToList();
        }

        public void Alterar(Usuario u)
        {
            context.usuarios.Update(u);
            context.SaveChanges();
        }

        public void Remover(int id)
        {
            context.usuarios.Remove(BuscarPorId(id));
            context.SaveChanges();
        }
    }
}
