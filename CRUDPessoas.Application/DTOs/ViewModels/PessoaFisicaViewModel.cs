using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPessoas.Application.DTOs.ViewModels
{
    public class PessoaFisicaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
