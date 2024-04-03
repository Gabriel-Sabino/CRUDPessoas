using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPessoas.Application.DTOs.InputModels.PessoaFisica
{
    public record PessoaFisicaUpdateInputModel
    {
        public string Nome { get; set; }
    }
}
