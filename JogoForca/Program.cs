using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoForca
{
    internal class Program
    {
        public static string caminhoArquivo = "C:\\Users\\Grisa\\Desktop\\Allog\\C#\\JogoForca\\palavras.txt";
        public static void CadastroPalavras()
        {
            int categoria;
            string palavra;
            bool continua = true;
            while (continua)
            {

                try
                {
                    Console.Clear();
                    Console.WriteLine("[0] - FILME" +
                        "\n[1] - JOGO" +
                        "\n[2] - PAIS");
                    categoria = LeInt("\nEscolha a categoria: ");
                    Console.WriteLine("Escreva a Palavra:");
                    palavra = Console.ReadLine();


                    StreamWriter arquivo = File.AppendText(caminhoArquivo);
                    arquivo.WriteLine($"{palavra.ToUpper()};{categoria};");
                    arquivo.Close();

                    Console.Clear();
                    Console.WriteLine("Continuar Cadastrando?");
                    Console.WriteLine("[1] - SIM");
                    Console.WriteLine("[2] - NAO");
                    int escolha = Convert.ToInt32(Console.ReadLine());
                    if (escolha != 1) continua = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Algo deu errado, tente novamente");
                }
                
            }

        }

        public static List<Jogo> RetornaPalavras()
        {
            List<Jogo> jogo = new List<Jogo>();
            string linha;
            string[] separadas;
            int contador = 0;
            try
            {
                StreamReader sr = new StreamReader(caminhoArquivo);
                linha = sr.ReadLine();
                while (linha != null)
                {
                    Jogo jogoAux = new Jogo();
                    separadas = linha.Split(';');
                    jogoAux.Palavra = separadas[0];
                    jogoAux.PalavraChar = separadas[0].ToList();
                    switch (Convert.ToInt32(separadas[1]))
                    {
                        case 0:
                            jogoAux.Categoria = "FILME";
                            break;
                        case 1:
                            jogoAux.Categoria = "JOGO";
                            break;
                        case 2:
                            jogoAux.Categoria = "PAIS";
                            break;
                    }
                    jogo.Add(jogoAux);
                    linha = sr.ReadLine();
                    contador++;
                }
                sr.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Algo deu errado, tente novamente");
            }
            return jogo;
        }
        public static char LeChar(string frase)
        {
            bool escolhaValida = false;
            char resposta = '0';
            while (!escolhaValida)
            {
                try
                {
                    Console.WriteLine(frase);
                    resposta = Convert.ToChar(Console.ReadLine().ToUpper());
                    escolhaValida = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Algo deu errado");
                }
            }
            return resposta;
        }
        public static int LeInt(string frase)
        {
            bool escolhaValida = false;
            int resposta = 0;
            while (!escolhaValida)
            {
                try
                {
                    Console.WriteLine(frase);
                    resposta = Convert.ToInt32(Console.ReadLine());
                    escolhaValida = true;
                }
                catch (Exception)
                {
                }
            }
            return resposta;
        }

        static void Main(string[] args)
        {

            List<Jogo> jogo = new List<Jogo>();
            Jogo jogoEscolhido = new Jogo();
            Jogador jogador = new Jogador();
            jogo = RetornaPalavras();
            int escolha;
            bool sair = false, ganhou = false, perdeu = false;

            while (!sair)
            {
                Console.WriteLine("Bem Vindo ao Forca Super Ultra Blast 3000!");
                Console.WriteLine("\n[1] - JOGAR");
                Console.WriteLine("[2] - CADASTRAR PALAVRA");
                Console.WriteLine("[3] - SAIR");

                escolha = LeInt("\nOque deseja fazer?");

                switch (escolha)
                {
                    case 1:
                        Random rdm = new Random();
                        int escolhaJogo;
                        escolhaJogo = rdm.Next(3);

                        ganhou = false; 
                        perdeu = false;
                        int escolhaDificuldade;
                        jogoEscolhido = jogo[escolhaJogo];
                        jogoEscolhido.LetrasCertas = new List<char>();
                        jogador.LetrasUtilizadas = new List<char>();

                        Console.Clear();
                        Console.WriteLine("[1] - FACIL -> 7 tentativas");
                        Console.WriteLine("[2] - MEDIO -> 6 tentativas");
                        Console.WriteLine("[3] - DIFICIL -> 5 tentativas");
                        escolhaDificuldade = LeInt("Escolha sua dificuldade:");

                        if (escolhaDificuldade == 1) jogador.TentativasRestantes = 7;
                        else if (escolhaDificuldade == 2) jogador.TentativasRestantes = 6;
                        else if (escolhaDificuldade == 3) jogador.TentativasRestantes = 5;
                        else jogador.TentativasRestantes = 7;


                        while (!ganhou && !perdeu)
                        {
                            Console.Clear();
                            Console.WriteLine($"Tentativas restantes: {jogador.TentativasRestantes}");
                            Console.WriteLine($"Categoria: {jogoEscolhido.Categoria}");
                            Console.Write($"Letras erradas:");
                            foreach (var letra in jogador.LetrasUtilizadas)
                            {
                                Console.Write($"{letra} ");
                            }
                            Console.WriteLine();
                            foreach (var letra in jogoEscolhido.PalavraChar)
                            {
                                if (jogoEscolhido.LetrasCertas.Contains(letra)) Console.Write($"{letra} ");
                                else if(letra == ' ') Console.Write("  ");
                                else Console.Write("_ ");
                            }

                            char tentativa = LeChar("\nSua letra: ");
                            if (jogoEscolhido.PalavraChar.Contains(tentativa))
                            {
                                jogoEscolhido.LetrasCertas.Add(tentativa);
                            }
                            else
                            {
                                jogador.LetrasUtilizadas.Add(tentativa);
                                jogador.TentativasRestantes--;
                            }

                            if (jogoEscolhido.PalavraChar.Count() == jogoEscolhido.LetrasCertas.Count())
                            {
                                ganhou = true;
                            }
                            else if (jogador.TentativasRestantes == 0)
                            {
                                perdeu = true;
                            }
                        }
                        Console.Clear();
                        if (ganhou)
                        {
                            Console.WriteLine("Parabens Voce GANHOU!!");
                            Console.WriteLine($"A palavra era {jogoEscolhido.Palavra}");
                        }
                        else
                        {
                            if (perdeu) Console.WriteLine("Acabaram suas chances");
                            Console.WriteLine("Voce PERDEU!!");
                            Console.WriteLine("QUE PENA PERDEDOR HAHAHAHAHH!!");
                            Console.WriteLine($"A palavra era {jogoEscolhido.Palavra}");
                        }
                        Console.Read();
                        Console.Clear();

                        break;
                    case 2:
                        Console.Clear();
                        CadastroPalavras();
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Muito obrigado por jogar o Bem Vindo ao Forca Super Ultra Blast 3000!");
                        Console.ReadLine();
                        sair = true; break;
                    default:
                        Console.Clear();
                        break;
                }
            }
            Console.Read();

        }
    }
}
