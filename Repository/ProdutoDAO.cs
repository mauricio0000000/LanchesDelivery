using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ProdutoDAO : IRepository<Produto>
    {
        private readonly Context context;
        public ProdutoDAO(Context context)
        {
            this.context = context;
        }
        public bool Cadastrar(Produto p)
        {
            if (BuscarProdutoPorNome(p) == null)
            {
                p.CriadoEm = DateTime.Now;
                context.produtos.Add(p);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public Produto BuscarProdutoPorNome(Produto p)
        {
            return context.produtos.FirstOrDefault(x => x.Nome.Equals(p.Nome));
        }
        public List<Produto> ListarTodos()
        {
            return context.produtos.Include(x => x.Categoria).ToList();
        }
        public Produto BuscarPorId(int id)
        {
            return context.produtos.Find(id);
        }
        public void Remover(int id)
        {
            context.produtos.Remove(BuscarPorId(id));
            context.SaveChanges();
        }
        public void Alterar(Produto p)
        {
            context.produtos.Update(p);
            context.SaveChanges();
        }

        public List<Produto> ListarPorCategoria(int? id)
        {
            return context.produtos.Include(x => x.Categoria).Where(x => x.Categoria.idCategoria == id).ToList();
        }

        public List<Produto> BuscaTodosProdutosPorIdCozinha(int idCozinha)
        {
            return context.produtos.Where(x => x.idCozinha == idCozinha).ToList();
        }
    }
}
