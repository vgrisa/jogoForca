using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoForca
{
    internal class Jogador
    {
        public Jogador()
        {
        }

        public Jogador(int tentativasRestantes, int tentativasFeitas, List<char> letrasUtilizadas)
        {
            TentativasRestantes = tentativasRestantes;
            TentativasFeitas = tentativasFeitas;
            LetrasUtilizadas = letrasUtilizadas;
        }


        public int TentativasRestantes { get; set; }
        public int TentativasFeitas { get; set; }
        public List<char> LetrasUtilizadas { get; set; }
    }
}
