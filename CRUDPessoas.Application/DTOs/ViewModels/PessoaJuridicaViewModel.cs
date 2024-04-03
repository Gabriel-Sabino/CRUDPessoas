using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPessoas.Application.DTOs.ViewModels
{
    public class PessoaJuridicaViewModel
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public DateTime DataAbertura { get; set; }

    }
}
