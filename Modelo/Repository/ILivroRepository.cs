using Modelo.Modelo;
using System;
using System.Collections.Generic;

namespace Modelo.Repository
{
    public interface ILivroRepository : IDisposable
    {
        IEnumerable<Livro> GetLivros();
        Livro GetLivroPorId(long id);
        void GravarLivro(Livro livro);
        void AtualizarLivro(Livro livro);
        void DeletarLivro(long id);
        void Salvar();
    }
}
