using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPessoas.Domain.Entities
{
    public class PessoaJuridica : Pessoa
    {
        public string RazaoSocial { get; private set; }
        public string NomeFantasia { get; private set; }
        public string CNPJ { get; private set; }
        public DateTime DataAbertura { get; private set; }

        public PessoaFisica PessoaFisica { get; private set; }
        public int PessoaFisicaId { get; private set; }

        public void UpdateNomeFantasia(string nomeFantasia)
        {
            if (string.IsNullOrEmpty(nomeFantasia))
                throw new ArgumentException("Nome Fantasia não pode estar nulo");

            NomeFantasia = nomeFantasia;
        }

        public void UpdateRazaoSocial(string razaoSocial)
        {
            if (string.IsNullOrEmpty(razaoSocial))
                throw new ArgumentException("Razao Social não pode estar nulo");

            RazaoSocial = razaoSocial;
        }

        public void AdicionaPessoaFisicaId(int pessoaFisicaId)
        {
            PessoaFisicaId = pessoaFisicaId;
        }
    }
}
