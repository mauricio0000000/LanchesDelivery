using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Domain;
using Repository;
using Microsoft.AspNetCore.Authorization;

namespace LanchesDel.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioDAO usuarioDAO;
        private readonly UserManager<UsuarioLogado> userManager;
        private readonly SignInManager<UsuarioLogado> signInManager;

        public UsuarioController(UsuarioDAO usuarioDAO,
            UserManager<UsuarioLogado> userManager,
            SignInManager<UsuarioLogado> signInManager)
        {
            this.usuarioDAO = usuarioDAO;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View(usuarioDAO.ListarTodos());
        }

        [AllowAnonymous]
        public IActionResult Create()
        {
            Usuario u = new Usuario();
            if (TempData["Usuario"] != null)
            {
                string resultado = TempData["Usuario"].ToString();
                //Newtonsoft.Json
                u.Endereco = JsonConvert.
                    DeserializeObject<Endereco>(resultado);
            }
            return View(u);
        }

        
        [HttpPost]
        [AllowAnonymous]
        public IActionResult BuscarCep(Usuario u)
        {
            string url = $"https://viacep.com.br/ws/{u.Endereco.Cep}/json/";
            WebClient client = new WebClient();
            TempData["Usuario"] = client.DownloadString(url);
            return RedirectToAction("Create");
        }


        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(Usuario u)
        {
            if (ModelState.IsValid)
            {
                UsuarioLogado uLogado = new UsuarioLogado
                {
                    UserName = u.Nome,
                    Email = u.Email,
                    nivelAcesso = 1,
                    idNormal = u.idUsuario
                };

                IdentityResult result = await userManager.CreateAsync(uLogado, u.Senha);
                if (result.Succeeded)
                {

                    if (usuarioDAO.Cadastrar(u))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Esse usuário já existe!");
                }
                AdicionarErros(result);
            }
            return View(u);
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
            return View(usuarioDAO.BuscarPorNome(nome));
        }

        [HttpPost]
        public IActionResult Edit(Usuario usuario)
        {
            usuarioDAO.Alterar(usuario);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int id)
        {
            usuarioDAO.Remover(id);
            return RedirectToAction("Index");
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
        public async Task<IActionResult> Login(Usuario u)
        {
            var result = await signInManager.PasswordSignInAsync
                (u.Nome, u.Senha, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Falha no login!");
            return View(u);
        }

    }
}
