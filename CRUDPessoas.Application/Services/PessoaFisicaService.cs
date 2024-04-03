using AutoMapper;
using CRUDPessoas.Application.DTOs.InputModels.PessoaFisica;
using CRUDPessoas.Application.DTOs.ViewModels;
using CRUDPessoas.Application.Interfaces.Services;
using CRUDPessoas.Domain.Entities;
using CRUDPessoas.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPessoas.Application.Services
{
    public class PessoaFisicaService : IPessoaFisicaService
    {
        private readonly IPessoaFisicaRepository _pessoaFisicaRepository;
        private readonly IMapper _mapper;
        public PessoaFisicaService(IMapper mapper,IPessoaFisicaRepository pessoaFisicaRepository)
        {
            _mapper = mapper;
            _pessoaFisicaRepository = pessoaFisicaRepository;
        }
        public async Task<PessoaFisicaViewModel> CreatePessoaFisicaAsync(PessoaFisicaInputModel pessoaFisica)
        {
            //PessoaFisica pessoaFisicaEntity = new(pessoaFisica.Nome, pessoaFisica.RG, pessoaFisica.CPF, pessoaFisica.DataNascimento);

            PessoaFisica pessoaFisicaEntity = _mapper.Map<PessoaFisica>(pessoaFisica);

            bool verificaCPF = await _pessoaFisicaRepository.GetPessoaFisicaByCPFAsync(pessoaFisicaEntity.CPF);
            bool verificaRG = await _pessoaFisicaRepository.GetPessoaFisicaByRGAsync(pessoaFisicaEntity.RG);

            if(verificaCPF || verificaRG)
                throw new ArgumentException("Não é possivel realizar a criacao de RG e CPF repetidos");
            

            PessoaFisica pessoaFisicaRepository = await _pessoaFisicaRepository.CreatePessoaFisicaAsync(pessoaFisicaEntity);

            //PessoaFisicaViewModel pessoaFisicaViewModel = new() {
            //Id = pessoaFisicaRepository.Id,
            //Nome = pessoaFisicaRepository.Nome,
            //RG = pessoaFisicaRepository.RG,
            //CPF = pessoaFisicaRepository.CPF,
            //DataNascimento = pessoaFisicaRepository.DataNascimento
            //};

            PessoaFisicaViewModel pessoaFisicaViewModel = _mapper.Map<PessoaFisicaViewModel>(pessoaFisicaRepository);

            return pessoaFisicaViewModel;
        }

        public async Task<int> DeletePessoaFisicaAsync(int id)
        {
            PessoaFisica pessoaToDelete = await _pessoaFisicaRepository.GetPessoaFisicaByIdAsync(id)
                ?? throw new ArgumentException("Pessoa Fisica não encontrada para deletar.");

            int deletedPessoaFisica = await _pessoaFisicaRepository.DeletePessoaFisicaAsync(pessoaToDelete);

            return deletedPessoaFisica;
        }

        public async Task<int> UpdatePessoaFisicaAsync(int id, PessoaFisicaUpdateInputModel pessoaFisicaInputModel)
        {
            var existingPessoaFisica = await _pessoaFisicaRepository.GetPessoaFisicaByIdAsync(id)
                ?? throw new InvalidOperationException("Pessoa Fisica não encontrada para atualizar.");

            existingPessoaFisica.UpdateNome(pessoaFisicaInputModel.Nome);

            var updatedPessoaFisica = await _pessoaFisicaRepository.UpdatePessoaFisicaAsync(existingPessoaFisica);

            return updatedPessoaFisica;
        }

        public async Task<IEnumerable<PessoaFisicaViewModel>> GetAllPessoaFisicaAsync()
        {
            
            IEnumerable<PessoaFisica> pessoaFisicaRepository = await _pessoaFisicaRepository.GetAllPessoaFisicaAsync();

            IEnumerable<PessoaFisicaViewModel> pessoaFisicaViewModel = _mapper.Map<IEnumerable<PessoaFisicaViewModel>>(pessoaFisicaRepository);

            return pessoaFisicaViewModel;

        }

        public async Task<PessoaFisicaViewModel> GetPessoaFisicaByIdAsync(int id)
        {
            PessoaFisica pessoaFisicaRepository =  await _pessoaFisicaRepository.GetPessoaFisicaByIdAsync(id);

            PessoaFisicaViewModel pessoaFisicaViewModel = _mapper.Map<PessoaFisicaViewModel>(pessoaFisicaRepository);

            return pessoaFisicaViewModel;
        }

        public async Task<IEnumerable<PessoaFisicaViewModel>> GetPessoaFisicaByNameAsync(string nome)
        {
            IEnumerable<PessoaFisica> pessoaFisicaRepository = await _pessoaFisicaRepository.GetPessoaFisicaByNameAsync(nome);

            IEnumerable<PessoaFisicaViewModel> pessoaFisicaViewModel = _mapper.Map<IEnumerable<PessoaFisicaViewModel>>(pessoaFisicaRepository);

            return pessoaFisicaViewModel;
        }
    }
}
