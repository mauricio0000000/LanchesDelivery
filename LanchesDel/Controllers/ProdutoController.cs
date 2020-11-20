using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace LanchesDel.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {
        private readonly ProdutoDAO produtoDAO;
        private readonly CategoriaDAO categoriaDAO;
        private readonly CozinhaDAO cozinhaDAO;
        private readonly IHostingEnvironment hosting;
        private readonly UserManager<UsuarioLogado> userManager;
        private readonly SignInManager<UsuarioLogado> signInManager;

        public ProdutoController(ProdutoDAO produtoDAO, CategoriaDAO categoriaDAO, CozinhaDAO cozinhaDAO, UserManager
            <UsuarioLogado> userManager, SignInManager<UsuarioLogado> signInManager, IHostingEnvironment hosting)
        {
            this.produtoDAO = produtoDAO;
            this.categoriaDAO = categoriaDAO;
            this.cozinhaDAO = cozinhaDAO;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.hosting = hosting;
        }

        public IActionResult Index()
        {
            ViewBag.DataHora = DateTime.Now;
            return View(produtoDAO.ListarTodos());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categorias = new SelectList(categoriaDAO.ListarTodos(), "idCategoria", "Nome");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Produto p, int drpCategorias, string nomeCozinha, IFormFile fupImagem)
        {
            ViewBag.Categorias = new SelectList(categoriaDAO.ListarTodos(), "idCategoria", "Nome");

            if (ModelState.IsValid)
            {
                if (fupImagem != null)
                {
                    string arquivo = Guid.NewGuid().ToString() + Path.GetExtension(fupImagem.FileName);
                    string caminho = Path.Combine(hosting.WebRootPath, "deliveryimagens", arquivo);
                    fupImagem.CopyTo(new FileStream(caminho, FileMode.Create));
                    p.Imagem = arquivo;
                }
                else
                {
                    p.Imagem = "semimagem.jfif";
                }
                Cozinha cozinha = cozinhaDAO.BuscarPorNome(nomeCozinha);
                p.idCozinha = cozinha.idCozinha;
                p.Categoria = categoriaDAO.BuscarPorId(drpCategorias);

                if (produtoDAO.Cadastrar(p))
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Esse produto já existe!");
            }
            return View(p);
        }

        public IActionResult Delete(int id)
        {
            produtoDAO.Remover(id);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit(int id)
        {
            return View(produtoDAO.BuscarPorId(id));
        }

        [HttpPost]
        public IActionResult Edit(Produto p)
        {
            produtoDAO.Alterar(p);
            return RedirectToAction("Index", "Home");
        }
    }
}
