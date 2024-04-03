using CRUDPessoas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPessoas.Domain.Interfaces.Repository
{
    public interface IPessoaJuridicaRepository
    {
        Task<List<PessoaJuridica>> GetAllPessoaJuridicaAsync();
        Task<PessoaJuridica> GetPessoaJuridicaByIdAsync(int id);
        Task<PessoaJuridica> GetPessoaJuridicaByRazaoSocialAsync(string razaoSocial);
        Task<bool> GetPessoaJuridicaByCNPJAsync(string cnpj);
        Task<PessoaJuridica> CreatePessoaJuridicaAsync(PessoaJuridica pessoaJuridica);
        Task<int> UpdatePessoaJuridicaAsync(PessoaJuridica pessoaJuridica);
        Task<int> DeletePessoaJuridicaAsync(PessoaJuridica pessoaJuridica);
    }
}
