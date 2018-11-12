using BibliotecaOnline.Data.Context;
using BibliotecaOnline.Modelo.ViewModels;
using Modelo.Modelo;
using System.Data.Entity;
using System.Linq;

namespace BibliotecaOnline.Data.DAL
{
    public class LivroDAL
    {
        private BibliotecaContext db = new BibliotecaContext();

        public IQueryable<Livro> ListarLivros()
        {
            return db.Livros.OrderBy(c => c.Titulo);
        }

        public Livro ObterLivroPorId(long id)
        {
            return db.Livros.Where(c => c.LivroId == id).First();
        }

        public void GravarLivro(Livro livro)
        {
            db.Livros.Add(livro);
            db.SaveChanges();
        }

        public void EditarLivro(Livro livro)
        {
            db.Entry(livro).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Livro DeletarLivro(long id)
        {
            Livro livro = db.Livros.Find(id);
            db.Livros.Remove(livro);
            db.SaveChanges();
            return livro;
        }
    }
}
