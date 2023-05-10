using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoForca
{
    internal class Jogo
    {
        public Jogo()
        {
        }

        public Jogo(List<char> palavraChar, List<char> letrasCertas, string palavra, string categoria)
        {
            PalavraChar = palavraChar;
            LetrasCertas = letrasCertas;
            Palavra = palavra;
            Categoria = categoria;
        }

        public List<char> PalavraChar { get; set; }
        public List<char> LetrasCertas{ get; set; }
        public string Palavra { get; set; }
        public string Categoria { get; set;}
        int QuantidadeLetras()
        {
            return PalavraChar.Count;
        }

    }
}
