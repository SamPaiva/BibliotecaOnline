using BibliotecaOnline.Data.Context;
using BibliotecaOnline.Data.DAL;
using BibliotecaOnline.Modelo.ViewModels;
using Modelo.Modelo;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace BibliotecaOnline.Web.Controllers
{
    public class LivroController : Controller
    {
        private LivroDAL livrosDAL = new LivroDAL();

        public ActionResult ListarLivros()
        {
            return View(livrosDAL.ListarLivros());
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
            else if(!imageTypes.Contains(model.ImageUpload.ContentType))
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

                livrosDAL.GravarLivro(livro);
                    return RedirectToAction("ListarLivros");
                }

            return View(model);
        }

        #endregion
    }
}