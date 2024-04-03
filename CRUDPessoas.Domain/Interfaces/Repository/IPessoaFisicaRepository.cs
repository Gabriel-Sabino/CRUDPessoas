using CRUDPessoas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPessoas.Domain.Interfaces.Repository
{
    public interface IPessoaFisicaRepository
    {
        Task<IEnumerable<PessoaFisica>> GetAllPessoaFisicaAsync();
        Task<PessoaFisica> GetPessoaFisicaByIdAsync(int id);
        Task<IEnumerable<PessoaFisica>> GetPessoaFisicaByNameAsync(string nome);
        Task<bool> GetPessoaFisicaByCPFAsync(string cpf);
        Task<bool> GetPessoaFisicaByRGAsync(string rg);
        Task<PessoaFisica> CreatePessoaFisicaAsync(PessoaFisica pessoaFisica);
        Task<int> UpdatePessoaFisicaAsync(PessoaFisica pessoaFisica);
        Task<int> DeletePessoaFisicaAsync(PessoaFisica pessoaFisica);
    }
}
