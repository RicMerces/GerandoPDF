using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            GerarRelatorioEmPDF(100);
            
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
                pdf.Open();

                var fonteBase = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

                //Adição de titulo 
                var fonteParagrafo = new iTextSharp.text.Font(fonteBase, 32, iTextSharp.text.Font.NORMAL, BaseColor.Black);
                var titulo = new Paragraph("Relatorio de Pessoas\n\n", fonteParagrafo);
                titulo.Alignment = Element.ALIGN_LEFT;
                pdf.Add(titulo);

                pdf.Close();
                arquivo.Close();

               

                //Adição da imagem
                var caminhoImagem = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img\\youtube.png");
                if (File.Exists(caminhoImagem))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(caminhoImagem);
                    float razaoAlturaLargura = logo.Width / logo.Height;
                    float alturaLogo = 32;
                    float larguraLogo = alturaLogo * razaoAlturaLargura;
                    logo.ScaleToFit(larguraLogo, alturaLogo);
                    var margemEsquerda = pdf.PageSize.Width - pdf.RightMargin - larguraLogo;
                    var margemTopo = pdf.PageSize.Height - pdf.TopMargin - 54;
                    logo.SetAbsolutePosition(margemEsquerda, margemTopo);
                    writer.DirectContent.AddImage(logo, false);
                }

                pdf.Close();
                arquivo.Close();

                //abre o PDF no visualizador padrão
                var caminhoPDF = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomeArquivo);
                if (File.Exists(caminhoPDF))
                {
                    Process.Start(new ProcessStartInfo()
                    {
                        Arguments = $"/c start {caminhoPDF}",
                        FileName = "cmd.exe",
                        CreateNoWindow = true
                    });
                }
            }

        }
    }
}
