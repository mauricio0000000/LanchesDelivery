using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using LanchesDel.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace LanchesDel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProdutoDAO produtoDAO;
        private readonly CozinhaDAO cozinhaDAO;
        private readonly CategoriaDAO categoriaDAO;
        private readonly ItemPedidoDAO itemPedidoDAO;
        private readonly UsuarioDAO usuarioDAO;
        private readonly UtilsSession utilsSession;

        public HomeController(ProdutoDAO produtoDAO, CozinhaDAO cozinhaDAO, CategoriaDAO categoriaDAO, ItemPedidoDAO itemPedidoDAO,UsuarioDAO usuarioDAO,
         UtilsSession utilsSession)
        {
            this.produtoDAO = produtoDAO;
            this.cozinhaDAO = cozinhaDAO;
            this.categoriaDAO = categoriaDAO;
            this.itemPedidoDAO = itemPedidoDAO;
            this.usuarioDAO = usuarioDAO;
            this.utilsSession = utilsSession;
        }

        [AllowAnonymous]
        public IActionResult Index(int? id)
        {
            ViewBag.Categorias = categoriaDAO.ListarTodos();
            if (id == null)
            {
                return View(cozinhaDAO.ListarTodos());
            }
            return View(cozinhaDAO.ListarTodos());
        }

        public IActionResult Detalhes(int id)
        {
            return View(cozinhaDAO.BuscarPorId(id));
        }

        public IActionResult RemoverDoCarrinho(int id)
        {
            itemPedidoDAO.Remover(id);
            return RedirectToAction("CarrinhoCompras");
        }


        public IActionResult CarrinhoCompras()
        {
            ViewBag.TotalCarrinho = itemPedidoDAO.RetornarTotalCarrinho(utilsSession.RetornarCarrinhoId());

            return View(itemPedidoDAO.ListarItensPorCarrinhoId(utilsSession.RetornarCarrinhoId()));
        }

        public IActionResult AumentarQuantidade(int id)
        {
            itemPedidoDAO.AumentarQuantidade(id);
            return RedirectToAction("CarrinhoCompras");
        }

        public IActionResult DiminuirQuantidade(int id)
        {
            itemPedidoDAO.DiminuirQuantidade(id);
            return RedirectToAction("CarrinhoCompras");
        }

        public IActionResult AdicionarAoCarrinho(int id)
        {
            //Adicionar os produtos dentro do carrinho
            Produto p = produtoDAO.BuscarPorId(id);
            ItemPedido i = new ItemPedido
            {
                Produto = p,
                Quantidade = 1,
                Preco = p.Preco,
                sacolaId = utilsSession.RetornarCarrinhoId()
            };
            //Gravar o objeto na tabela
            itemPedidoDAO.Cadastrar(i);
            return RedirectToAction("CarrinhoCompras");
        }
    }
}
