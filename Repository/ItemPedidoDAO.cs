using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ItemPedidoDAO : IRepository<ItemPedido>
    {
        public readonly Context context;

        public ItemPedidoDAO(Context context)
        {
            this.context = context;
        }

        public ItemPedido BuscarPorId(int id)
        {
            return context.itemPedidos.Find(id);
        }

        public bool Cadastrar(ItemPedido i)
        {
            ItemPedido itemAux = context.itemPedidos.FirstOrDefault(x => x.Produto.idProduto == i.Produto.idProduto && x.sacolaId.Equals(i.sacolaId));
            if (itemAux == null)
            {
                context.itemPedidos.Add(i);
            }
            else
            {
                itemAux.Quantidade++;
            }
            context.SaveChanges();
            return true;
        }

        public List<ItemPedido> ListarTodos()
        {
            return context.itemPedidos.ToList();
        }

        public List<ItemPedido> ListarItensPorCarrinhoId(string sacolaId)
        {
            return context.itemPedidos.Include(x => x.Produto.Categoria).Where(x => x.sacolaId.Equals(sacolaId)).ToList();
        }

        public double RetornarTotalCarrinho(string sacolaId)
        {
            return context.itemPedidos.Where(x => x.sacolaId.Equals(sacolaId)).Sum(x => x.Quantidade * x.Preco);
        }

        public void Remover(int id)
        {
            context.itemPedidos.Remove(BuscarPorId(id));
            context.SaveChanges();
        }

        public void Alterar(ItemPedido i)
        {
            context.itemPedidos.Update(i);
            context.SaveChanges();
        }

        public void AumentarQuantidade(int id)
        {
            ItemPedido i = BuscarPorId(id);
            i.Quantidade++;
            Alterar(i);
        }

        public void DiminuirQuantidade(int id)
        {
            ItemPedido i = BuscarPorId(id);
            if (i.Quantidade > 1)
            {
                i.Quantidade--;
                Alterar(i);
            }
        }
    }
}
