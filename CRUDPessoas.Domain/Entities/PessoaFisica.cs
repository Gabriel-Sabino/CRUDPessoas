using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPessoas.Domain.Entities
{
    public class PessoaFisica : Pessoa
    {
        public string Nome { get; private set; }
        public string RG { get; private set; }
        public string CPF { get; private set; }
        public DateTime DataNascimento { get; private set; }

        //public PessoaFisica(string nome, string rg, string cpf, DateTime dataNascimento)
        //{
        //    Nome = nome;
        //    RG = rg;
        //    CPF = cpf;
        //    DataNascimento = dataNascimento;
        //}

        public void UpdateNome(string nome)
        {
            if(string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("Nome não pode estar nulo");
            }

            Nome = nome;
        }
    }
}
