using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using System.IO;
using iTextSharp.text; 
using iTextSharp.text.pdf;

namespace GeradorDePDF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Document doc = new Document(PageSize.A4);
            doc.SetMargins(40, 40, 40, 80);
            string caminho= @"C:\pdf\" + "teste.pdf";

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

            doc.Open();

            Paragraph titulo = new Paragraph();
            titulo.Font = new Font(Font.FontFamily.COURIER, 40);
            titulo.Add("teste\n\n");
            doc.Add(titulo);


            Paragraph paragrafo = new Paragraph("", new Font(Font.NORMAL, 12));
            string conteudo = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse porttitor ligula eget mauris sodales bibendum. Pellentesque ut tempor massa. Pellentesque non odio ultrices, sollicitudin magna vel, congue diam. Proin sed ullamcorper risus, nec faucibus lorem. Nunc ligula nulla, placerat at elit ac, iaculis lacinia nibh. Duis feugiat tempus diam, non dapibus urna pulvinar in. Nunc in metus risus. Aenean diam elit, vehicula vel nunc id, vehicula eleifend neque. Duis malesuada est purus, non rutrum nunc viverra elementum. Praesent egestas mauris et libero euismod ullamcorper. Etiam non odio lobortis, malesuada nisl nec, pharetra mauris. Donec non pulvinar orci. Maecenas lacinia lectus in volutpat rutrum. In cursus a eros sed molestie. Curabitur mauris metus, iaculis quis risus sit amet, ornare vulputate quam.";
            paragrafo.Add(conteudo);
            doc.Add(paragrafo);

            PdfPTable table = new PdfPTable(3);
            
            for(int i = 1; i <= 10; i++)
            {
                table.AddCell($"Linha {i}, Coluna 1");
                table.AddCell($"Linha {i}, Coluna 2");
                table.AddCell($"Linha {i}, Coluna 3");

            }
            doc.Add(table);
           /* string simg = "https://www.petz.com.br/cachorro/racas/akita-inu/img/akita-inu-escovacao-pelos.webp";
            Image img = Image.GetInstance(simg);
            img.ScaleAbsolute(100, 130);


            doc.Add(img);
           */
            doc.Close();
            System.Diagnostics.Process.Start(caminho);




            System.Text.Ecoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
