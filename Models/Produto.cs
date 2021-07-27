using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C_TEste.Model
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public Categoria Categoria { get; set; }
    }

    public enum Categoria
    {
        Alimento = 1,
        Bebida = 2,
        Outros = 3
    }
}
