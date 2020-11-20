using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class CategoriaDAO : IRepository<Categoria>
    {
        private readonly Context context;

        public CategoriaDAO(Context context)
        {
            this.context = context;
        }

        public Categoria BuscarPorId(int id)
        {
            return context.categorias.Find(id);
        }

        public Categoria BuscarPorNome(Categoria c)
        {
            return context.categorias.FirstOrDefault(x => x.Nome.Equals(c.Nome));
        }

        public bool Cadastrar(Categoria c)
        {
            if (BuscarPorNome(c) == null)
            {
                c.CriadoEm = DateTime.Now;
                context.categorias.Add(c);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Categoria> ListarTodos()
        {
            return context.categorias.ToList();
        }

        public void Alterar(Categoria c)
        {
            context.categorias.Update(c);
            context.SaveChanges();
        }

        public void Remover(int id)
        {
            context.categorias.Remove(BuscarPorId(id));
            context.SaveChanges();
        }
    }
}
