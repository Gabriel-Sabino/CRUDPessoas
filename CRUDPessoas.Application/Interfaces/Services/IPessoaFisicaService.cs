using CRUDPessoas.Application.DTOs.InputModels.PessoaFisica;
using CRUDPessoas.Application.DTOs.ViewModels.PessoaFisica;
using CRUDPessoas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPessoas.Application.Interfaces.Services
{
    public interface IPessoaFisicaService
    {
        Task<IEnumerable<PessoaFisicaViewModel>> GetAllPessoaFisicaAsync();
        Task<PessoaFisicaViewModel> GetPessoaFisicaByIdAsync(int id);
        Task<IEnumerable<PessoaFisicaViewModel>> GetPessoaFisicaByNameAsync(string nome);
        Task<PessoaFisicaViewModel> CreatePessoaFisicaAsync(PessoaFisicaInputModel pessoaFisica);
        Task<int> UpdatePessoaFisicaAsync(int id, PessoaFisicaUpdateInputModel pessoaFisica);
        Task<int> DeletePessoaFisicaAsync(int id);
    }
}
