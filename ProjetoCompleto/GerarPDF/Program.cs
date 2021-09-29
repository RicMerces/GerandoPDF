using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace GeradorDeArquivosEmPDF
{
    class Program
    {
        static List<Pessoa> pessoas = new List<Pessoa>();

        static void Main(string[] args) { 

            DesserializarPessoas();
            foreach(var p in pessoas)
            {
                Console.WriteLine($"{p.IdPessoa} - {p.Nome} {p.Sobrenome}");
            }
            
        }

        static void DesserializarPessoas()
        {
            if (File.Exists("pessoas.json"))
            {
                using (var sr = new StreamReader("pessoas.json"))
                {
                    var dados = sr.ReadToEnd();
                    pessoas = JsonSerializer.Deserialize(dados, typeof(List<Pessoa>)) as List<Pessoa>;
                }
            }
        }

        static void GerarRelatorioEmPDF (int qtdePessoas)
        {
            var pessoasSelecionadas = pessoas.Take(qtdePessoas).ToList();
            if (pessoasSelecionadas.Count > 0)
            {
                //Configurar do documento PDF
                var pxPorMm = 72 / 25.2F;
                var pdf = new Document(PageSize.A4, 15 * pxPorMm, 15 * pxPorMm, 15 * pxPorMm, 20 * pxPorMm);
                var nomeArquivo = $"pessoas.{DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss")}";
                var arquivo = new FileStream(nomeArquivo, FileMode.Create);
                var writer = PdfWriter.GetInstance(pdf, arquivo);

            }

        }
    }
}
