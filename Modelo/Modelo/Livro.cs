using System;

namespace Modelo.Modelo
{
    public class Livro
    {
        public long LivroId { get; set; }
        public string Titulo { get; set; }
        public string Sinopse { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public byte[] Imagem { get; set; }
        public DateTime DataLancamento { get; set; }
    }
}
