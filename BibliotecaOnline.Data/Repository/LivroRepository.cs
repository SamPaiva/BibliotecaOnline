using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BibliotecaOnline.Data.Context;
using Modelo.Modelo;
using Modelo.Repository;

namespace BibliotecaOnline.Data.Repository
{
    public class LivroRepository : ILivroRepository
    {
        private BibliotecaContext db = new BibliotecaContext();

        public LivroRepository(BibliotecaContext db)
        {
            this.db = db;
        }

        public void AtualizarLivro(Livro livro)
        {
            db.Entry(livro).State = EntityState.Modified;
        }

        public void DeletarLivro(long id)
        {
            var livro = GetLivroPorId(id);
            db.Livros.Remove(livro);

        }

        public Livro GetLivroPorId(long id)
        {
            return db.Livros.Find(id);
        }

        public IEnumerable<Livro> GetLivros()
        {
            return db.Livros.OrderBy(c => c.Titulo).ToList();
        }

        public void GravarLivro(Livro livro)
        {
            db.Livros.Add(livro);
        }

        public void Salvar()
        {
            db.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
