using CRUDPessoas.Application.DTOs.InputModels.PessoaJuridica;
using CRUDPessoas.Application.DTOs.ViewModels.PessoaJuridica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPessoas.Application.Interfaces.Services
{
    public interface IPessoaJuridicaService
    {
        Task<IEnumerable<PessoaJuridicaViewModel>> GetAllPessoaJuridicaAsync();
        Task<PessoaJuridicaViewModel> GetPessoaJuridicaByIdAsync(int id);
        Task<PessoaJuridicaViewModel> GetPessoaJuridicaByRazaoSocialAsync(string razaoSocial);
        Task<PessoaJuridicaViewModel> CreatePessoaJuridicaAsync(int pessoaFisicaId, PessoaJuridicaInputModel pessoaJuridica);
        Task<int> UpdatePessoaJuridicaAsync(int id, PessoaJuridicaUpdateInputModel pessoaJuridica);
        Task<int> DeletePessoaJuridicaAsync(int id);
    }
}
