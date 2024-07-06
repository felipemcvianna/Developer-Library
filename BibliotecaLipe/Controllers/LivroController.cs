using Biblioteca.Models;
using Biblioteca.Models.Enums;
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

        public IActionResult Index()
        {
            var todosOsLivros = _servicoLivros.GetAllLivros();
            return View(todosOsLivros);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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

                    livroExistente.Titulo = livro.Titulo;
                    livroExistente.Autor = livro.Autor;
                    livroExistente.ISBN = livro.ISBN;
                    livroExistente.DataPublicacao = livro.DataPublicacao;
                    livroExistente.Categoria = livro.Categoria;
                    livroExistente.Descricao = livro.Descricao;

                    if (imagem != null)
                    {
                        CreateImg(livroExistente, imagem);
                    }

                    _servicoLivros.Edit(livroExistente);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao editar livro: " + ex.Message);
            }

            return View(livro);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public IActionResult Delete(int? id)
        {
            var livroRemover = _servicoLivros.GetLivroById(id.Value);
            return View(livroRemover);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
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
        [HttpGet("cat: Categorias")]
        public IActionResult Categorias(Categorias cat)
        {
            if (cat == null)
            {
                Console.WriteLine("Categoria recebida é nula.");
                return BadRequest("Categoria não pode ser nula.");
            }

            Console.WriteLine($"Categoria passada no controlador é {cat}");
            ViewBag.Categoria = cat;
            var livros = _servicoLivros.ListCategories(cat);
            if (livros.Count == 0)
            {
                Console.WriteLine($"A lista de livros da categoria {cat} está vazia.");
            }
            else
            {
                foreach (var livro in livros)
                {
                    Console.WriteLine($"Livro encontrado: {livro.Titulo}");
                }
            }
            return View(livros);
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