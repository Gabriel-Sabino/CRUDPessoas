using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPessoas.Application.DTOs.InputModels.PessoaJuridica
{
    public record PessoaJuridicaUpdateInputModel
    {
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
    }
}
