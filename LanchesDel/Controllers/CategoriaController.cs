using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Repository;
using Microsoft.AspNetCore.Authorization;

namespace LanchesDel.Controllers
{
    [Authorize]
    public class CategoriaController : Controller
    {
        private readonly CategoriaDAO categoriaDAO;
        public CategoriaController(CategoriaDAO categoriaDAO)
        {
            this.categoriaDAO = categoriaDAO;
        }
        public IActionResult Index()
        {
            return View(categoriaDAO.ListarTodos());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if (categoriaDAO.Cadastrar(categoria))
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Essa categoria já existe!");
            }
            return View(categoria);
        }

        public IActionResult Edit(int id)
        {
            return View(categoriaDAO.BuscarPorId(id));
        }

        [HttpPost]
        public IActionResult Edit(Categoria categoria)
        {
            categoriaDAO.Alterar(categoria);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int id)
        {
            categoriaDAO.Remover(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
