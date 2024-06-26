﻿using Biblioteca.Models;
using Biblioteca.Servico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Biblioteca.Controllers
{
    [Authorize]
    public class LivroController : Controller
    {
        private readonly ServicoLivros _servicoLivros;
        private readonly string _caminhoServidor;

        public LivroController(ServicoLivros servicoLivros, IWebHostEnvironment servidor)
        {
            _servicoLivros = servicoLivros;
            _caminhoServidor = servidor.WebRootPath;
        }

        // GET
        public IActionResult Index()
        {
            var todosOsLivros = _servicoLivros.GetAllLivros();
            return View(todosOsLivros);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Livro livro, IFormFile? imagem)
        {
            if (ModelState.IsValid)
            {
                CreateImg(livro, imagem);
                _servicoLivros.Create(livro);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var livro = _servicoLivros.GetLivroById(id.Value);
            if (livro != null)
            {
                return View(livro);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Livro livro, IFormFile? imagem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var livroExistente = _servicoLivros.GetLivroById(livro.LivroID);
                    if (livroExistente == null)
                    {
                        return NotFound("Livro não encontrado.");
                    }

                    if (imagem != null)
                    {
                        CreateImg(livroExistente, imagem);
                    }

                    _servicoLivros.Edit(livroExistente);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao editar livro: " + ex.Message);
            }
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var livroRemover = _servicoLivros.GetLivroById(id.Value);
            return View(livroRemover);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var livroRemover = _servicoLivros.GetLivroById(id);
            if (livroRemover != null)
            {
                _servicoLivros.Remove(livroRemover.LivroID);
                return RedirectToAction(nameof(Index));
            }

            return NotFound("Não foi possível remover o livro");
        }

        public IActionResult Details(int id)
        {
            var detalheLivro = _servicoLivros.GetLivroById(id);
            if (detalheLivro != null)
            {
                return View(detalheLivro);
            }

            return BadRequest("Livro não encontrado");
        }

        private void CreateImg(Livro livro, IFormFile? imagem)
        {
            string caminhoParaSalvarImg = Path.Combine(_caminhoServidor, "Imagens");
            if (imagem != null)
            {
                string novoNomeImg = Guid.NewGuid().ToString().Substring(0, 8) + imagem.FileName;
                if (!Directory.Exists(caminhoParaSalvarImg))
                {
                    Directory.CreateDirectory(caminhoParaSalvarImg);
                }

                string caminhoCompleto = Path.Combine(caminhoParaSalvarImg, novoNomeImg);
                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    imagem.CopyTo(stream);
                }

                livro.CaminhoImagem = "/Imagens/" + novoNomeImg;
            }
        }
    }
}