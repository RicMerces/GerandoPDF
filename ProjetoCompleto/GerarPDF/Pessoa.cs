using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeArquivosEmPDF
{
    [Serializable]
    class Pessoa
    {
        //87
        public int IdPessoa { get; set;}
        //Davi
        public string Nome { get; set;}
        //Simões
        public string Sobrenome { get; set;}
        //2400
        public double salario { get; set; }
        //Historiador
        public Profissao Profissao { get; set; }
        //True
        public bool Empregado { get; set; }

    }
}
