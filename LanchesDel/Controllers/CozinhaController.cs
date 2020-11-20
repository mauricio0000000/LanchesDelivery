using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Repository;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace LanchesDel.Controllers
{
    public class CozinhaController : Controller
    {
        private readonly CozinhaDAO cozinhaDAO;
        private readonly ProdutoDAO produtoDAO;
        private readonly CategoriaDAO categoriaDAO;
        private readonly UserManager<UsuarioLogado> userManager;
        private readonly SignInManager<UsuarioLogado> signInManager;
        private readonly IHostingEnvironment hosting;

        public CozinhaController(CozinhaDAO cozinhaDAO, ProdutoDAO produtoDAO, UserManager<UsuarioLogado> userManager, 
            SignInManager<UsuarioLogado> signInManager, IHostingEnvironment hosting, CategoriaDAO categoriaDAO)
        {
            this.cozinhaDAO = cozinhaDAO;
            this.produtoDAO = produtoDAO;
            this.categoriaDAO = categoriaDAO;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.hosting = hosting;
        }

        public IActionResult Index()
        {
            return View(cozinhaDAO.ListarTodos());
        }

        public IActionResult Cardapio(int id)
        {
            ViewBag.Categorias = categoriaDAO.ListarTodos();
            ViewBag.Produtos = produtoDAO.BuscaTodosProdutosPorIdCozinha(id);
            return View(cozinhaDAO.BuscarPorId(id));
        }

        [AllowAnonymous]
        public IActionResult Create()
        {
            Cozinha c = new Cozinha();
            if (TempData["Cozinha"] != null)
            {
                string resultado = TempData["Cozinha"].ToString();
                //Newtonsoft.Json
                c.Endereco = JsonConvert.DeserializeObject<Endereco>(resultado);
            }
            return View(c);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult BuscarCep(Usuario u)
        {
            string url = $"https://viacep.com.br/ws/{u.Endereco.Cep}/json/";
            WebClient client = new WebClient();
            TempData["Cozinha"] = client.DownloadString(url);
            return RedirectToAction("Create");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(Cozinha c, IFormFile fupImagem)
        {
            if (fupImagem != null)
            {
                string arquivo = Guid.NewGuid().ToString() + Path.GetExtension(fupImagem.FileName);
                string caminho = Path.Combine(hosting.WebRootPath, "deliveryimagens", arquivo);
                fupImagem.CopyTo(new FileStream(caminho, FileMode.Create));
                c.Imagem = arquivo;
            }
            else
            {
                c.Imagem = "semimagem.jfif";
            }
            UsuarioLogado uLogado = new UsuarioLogado
            {
                UserName = c.Nome,
                Email = c.Email,
                nivelAcesso = 0,
                idNormal = c.idCozinha
            };

            IdentityResult result = await userManager.CreateAsync(uLogado, c.Senha);
            if (result.Succeeded)
            {

                if (cozinhaDAO.Cadastrar(c))
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Essa cozinha já existe!");
            }
            AdicionarErros(result);
            return View(c);
        }

        private void AdicionarErros(IdentityResult result)
        {
            foreach (var erro in result.Errors)
            {
                ModelState.AddModelError("", erro.Description);
            }
        }

        public IActionResult Edit(String nome)
        {
            return View(cozinhaDAO.BuscarPorNome(nome));
        }

        [HttpPost]
        public IActionResult Edit(Cozinha cozinha)
        {
            cozinhaDAO.Alterar(cozinha);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int id)
        {
            cozinhaDAO.Remover(id);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Cozinha c)
        {
            var result = await signInManager.PasswordSignInAsync
                (c.Nome, c.Senha, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                ViewBag.usuarioLogado = c;
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Falha no login!");
            return View(c);
        }

    }
}
