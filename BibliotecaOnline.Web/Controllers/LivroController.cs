using BibliotecaOnline.Data.Context;
using BibliotecaOnline.Data.Repository;
using BibliotecaOnline.Modelo.ViewModels;
using Modelo.Modelo;
using Modelo.Repository;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BibliotecaOnline.Web.Controllers
{
    public class LivroController : Controller
    {
        private ILivroRepository _livroRepository;

        public LivroController()
        {
            this._livroRepository = new LivroRepository(new BibliotecaContext());
        }

        public ActionResult ListarLivros()
        {
            var livros = _livroRepository.GetLivros();
            return View(livros);
        }

        #region - Gravar Livro
        public ActionResult GravarLivro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GravarLivro(LivroViewModel model)
        {

            var imageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (model.ImageUpload == null || model.ImageUpload.ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "Este campo é obrigatório");
            }
            else if (!imageTypes.Contains(model.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Escolha uma imagem GIF, JPG ou PNG");
            }

            if (ModelState.IsValid)
            {
                var livro = new Livro();
                livro.Titulo = model.Titulo;
                livro.Autor = model.Autor;
                livro.DataLancamento = model.DataLancamento;
                livro.Genero = model.Genero;
                livro.Sinopse = model.Sinopse;

                using (var binaryReader = new BinaryReader(model.ImageUpload.InputStream))
                    livro.Imagem = binaryReader.ReadBytes(model.ImageUpload.ContentLength);

                _livroRepository.GravarLivro(livro);
                _livroRepository.Salvar();
                return RedirectToAction("ListarLivros");
            }

            return View(model);
        }

        #endregion

        #region - Editar Livro

        public ActionResult EditarLivro(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Livro livro = _livroRepository.GetLivroPorId((long)id);

            if (livro == null)
            {
                return HttpNotFound();
            }

            return View(livro);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult EditarLivro([Bind(Include = "Livro.Id")] Livro model)
        {
            if (ModelState.IsValid)
            {
                var livro = _livroRepository.GetLivroPorId(model.LivroId);

                if (livro == null)
                {
                    return HttpNotFound();
                }

                _livroRepository.AtualizarLivro(livro);
                _livroRepository.Salvar();
                return RedirectToAction("ListarLivros");

            }

            return RedirectToAction("EditarLivro");

        }

        #endregion

        #region - Detalhes Livro

        public ActionResult DetalhesLivro(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var livro = _livroRepository.GetLivroPorId((long)id);

            if (livro == null)
            {
                return HttpNotFound();
            }

            return View(livro);
        }

        #endregion

        #region - Deletar Livro

        public ActionResult DeletarLivro(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var livro = _livroRepository.GetLivroPorId((long)id);

            if (livro == null)
            {
                return HttpNotFound();
            }

            return View(livro);
        }

        [HttpPost]
        public ActionResult DeletarLivro(long id)
        {
            _livroRepository.DeletarLivro(id);
            _livroRepository.Salvar();
            return RedirectToAction("ListarLivros");
        }

        #endregion
    }
}