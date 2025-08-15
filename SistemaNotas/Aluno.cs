using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaNotas
{
    public class Aluno
    {
       
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public int? Idade { get; set; }
        public DateOnly? DataNascimento { get; set; }
        public string? Cpf { get; set; }
        public string? Cep { get; set; }
        public string? Endereco { get; set; }
        public int? Numero { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public double? Nota1 { get; set; }
        public double? Nota2 { get; set; }
        public double? Media { get; set; }
    }
}
