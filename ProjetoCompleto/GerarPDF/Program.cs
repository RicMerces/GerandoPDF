using System;
using System.IO;

namespace GeradorDeArquivosEmPDF
{
    class Program
    {
        static List<Pessoa> pessoas = new List<Pessoa>();

        static void Main(string[] args) { 
        

            Console.ReadLine();
        }

        static void DesserializarPessoas()
        {
            if (File.Exists("pessoas.json"))
            {
                using (var sr = new StreamReader("pessoas.json"))
                {
                    var dados = sr.ReadToEnd();
                    pessoas = JsonSerializer.Deserialize(dados, typeof(list<Pessoa>) as List<Pessoa>);
                }
            }
        }
    }
}
