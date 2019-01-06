using BibliotecaOnline.Data.Context;
using BibliotecaOnline.Data.Repository;
using BibliotecaOnline.Modelo.ViewModels;
using Modelo.Modelo;
using Modelo.Repository;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using X.PagedList;

namespace BibliotecaOnline.Web.Controllers
{

    public class LivroController : Controller
    {
        private ILivroRepository _livroRepository;
        private BibliotecaContext db = new BibliotecaContext();

        public LivroController()
        {
            this._livroRepository = new LivroRepository(new BibliotecaContext());
        }

        public ActionResult ListarLivros(string busca, string currentFilter, int pagina = 1 )
        {
            var livros = _livroRepository.GetLivros();

            if (busca != null)
            {
                pagina = 1;
            }
            else
            {
                busca = currentFilter;
            }

            ViewBag.CurrentFilter = busca;

            var livrosFiltro = from s in db.Livros
                               select s;
            if (!String.IsNullOrEmpty(busca))
            {
                livrosFiltro = db.Livros.Where(s => s.Titulo.Contains(busca));
                return View(livrosFiltro);
            }

            int pageSize = 10;
            var livrosPaginados = db.Livros.OrderBy(c => c.Titulo).ToPagedList(pagina, pageSize);

            return View(livrosPaginados);
            
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

            var pdf = new string[]
            {
                "application/pdf"
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
                Livro livro = new Livro();
                livro.Titulo = model.Titulo;
                livro.Autor = model.Autor;
                livro.DataLancamento = model.DataLancamento;
                livro.Genero = model.Genero;
                livro.Sinopse = model.Sinopse;

                using (var binaryReader = new BinaryReader(model.ImageUpload.InputStream))
                    livro.Imagem = binaryReader.ReadBytes(model.ImageUpload.ContentLength);
                using (var pdfReader = new BinaryReader(model.Pdf.InputStream))
                    livro.ConteudoAnexo = pdfReader.ReadBytes(model.Pdf.ContentLength);


                _livroRepository.GravarLivro(livro);
                _livroRepository.Salvar();
                TempData["gravar"] = "Livro Adicionaddo com sucesso";
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
                TempData["editar"] = "Livro Editado com Sucesso";
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
            TempData["deletar"] = "Livro Deletado com Sucesso";
            return RedirectToAction("ListarLivros");
        }

        #endregion

        public FileResult DownloadFile(int id)
        {
            Livro livro = new Livro();
            livro = db.Livros.Where(c => c.LivroId == id).FirstOrDefault();
            var pdf = livro.ConteudoAnexo;

            MemoryStream ms = new MemoryStream(pdf, 0, 0, true, true);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + livro.Titulo);
            Response.Buffer = true;
            Response.Clear();
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.End();

            return File(Response.OutputStream, "application/pdf");
        }
    }
}