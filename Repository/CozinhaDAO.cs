using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class CozinhaDAO : IRepository<Cozinha>
    {
        public readonly Context context;

        public CozinhaDAO(Context context)
        {
            this.context = context;
        }

        public Cozinha BuscarPorEndereco(Cozinha c)
        {
            return context.cozinhas.FirstOrDefault(x => x.Endereco.Equals(c.Endereco));
        }

        public Cozinha BuscarPorId(int id)
        {
            return context.cozinhas.Find(id);
        }

        public Cozinha BuscarPorNome(String nome)
        {
            return context.cozinhas.FirstOrDefault(x => x.Nome.Equals(nome));
        }

        public bool Cadastrar(Cozinha c)
        {
            if (BuscarPorEndereco(c) == null)
            {
                c.CriadoEm = DateTime.Now;
                context.cozinhas.Add(c);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Cozinha> ListarTodos()
        {
            return context.cozinhas.ToList();
        }

        public void Remover(int id)
        {
            context.cozinhas.Remove(BuscarPorId(id));
            context.SaveChanges();
        }
        public void Alterar(Cozinha c)
        {
            context.cozinhas.Update(c);
            context.SaveChanges();
        }
    }
}
