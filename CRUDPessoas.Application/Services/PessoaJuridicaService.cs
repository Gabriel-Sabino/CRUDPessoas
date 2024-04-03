using AutoMapper;
using CRUDPessoas.Application.DTOs.InputModels.PessoaFisica;
using CRUDPessoas.Application.DTOs.InputModels.PessoaJuridica;
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
    public class PessoaJuridicaService : IPessoaJuridicaService
    {
        private readonly IPessoaJuridicaRepository _pessoaJuridicaRepository;
        private readonly IPessoaFisicaRepository _pessoaFisicaRepository;
        private readonly IMapper _mapper;
        public PessoaJuridicaService(IMapper mapper, IPessoaJuridicaRepository pessoaJuridicaRepository, IPessoaFisicaRepository pessoaFisicaRepository)
        {
            _mapper = mapper;
            _pessoaJuridicaRepository = pessoaJuridicaRepository;
            _pessoaFisicaRepository = pessoaFisicaRepository;
        }
        public async Task<PessoaJuridicaViewModel> CreatePessoaJuridicaAsync(int pessoaFisicaId, PessoaJuridicaInputModel pessoaJuridica)
        {

            PessoaJuridica pessoaJuridicaEntity = _mapper.Map<PessoaJuridica>(pessoaJuridica);

            _ = await _pessoaFisicaRepository.GetPessoaFisicaByIdAsync(pessoaFisicaId)
                ?? throw new ArgumentException("Necessario haver uma Pessoa Fisica para criar uma Pessoa Juridica");

            bool verificaCNPJ = await _pessoaJuridicaRepository.GetVerificaPessoaJuridicaByCNPJAsync(pessoaJuridicaEntity.CNPJ);
            bool verificaRazaoSocial = await _pessoaJuridicaRepository.GetVerificaPessoaJuridicaByRazaoSocialAsync(pessoaJuridicaEntity.RazaoSocial);

            if (verificaCNPJ || verificaRazaoSocial)
                throw new ArgumentException("É Necessario informar CNPJ e RazaoSocial que não existam");


            pessoaJuridicaEntity.AdicionaPessoaFisicaId(pessoaFisicaId);

            PessoaJuridica pessoaJuridicaRepository = await _pessoaJuridicaRepository.CreatePessoaJuridicaAsync(pessoaJuridicaEntity);

            PessoaJuridicaViewModel pessoaJuridicaViewModel = _mapper.Map<PessoaJuridicaViewModel>(pessoaJuridicaRepository);

            return pessoaJuridicaViewModel;
        }

        public async Task<int> DeletePessoaJuridicaAsync(int id)
        {
            PessoaJuridica pessoaToDelete = await _pessoaJuridicaRepository.GetPessoaJuridicaByIdAsync(id)
                ?? throw new ArgumentException("Pessoa Juridica não encontrada para deletar.");

            int deletedPessoaJuridica = await _pessoaJuridicaRepository.DeletePessoaJuridicaAsync(pessoaToDelete);

            return deletedPessoaJuridica;
        }

        public async Task<int> UpdatePessoaJuridicaAsync(int id, PessoaJuridicaUpdateInputModel pessoaJuridicaInputModel)
        {

            bool verificaRazaoSocial = await _pessoaJuridicaRepository.GetVerificaPessoaJuridicaByRazaoSocialAsync(pessoaJuridicaInputModel.RazaoSocial);

            if (verificaRazaoSocial)
                throw new InvalidOperationException("Razao Social ja existente");


            PessoaJuridica existingPessoaJuridica = await _pessoaJuridicaRepository.GetPessoaJuridicaByIdAsync(id)
                ?? throw new InvalidOperationException("Pessoa Juridica não encontrada para atualizar.");

            existingPessoaJuridica.UpdateRazaoSocial(pessoaJuridicaInputModel.RazaoSocial);
            existingPessoaJuridica.UpdateNomeFantasia(pessoaJuridicaInputModel.NomeFantasia);

            int updatedPessoaJuridica = await _pessoaJuridicaRepository.UpdatePessoaJuridicaAsync(existingPessoaJuridica);

            return updatedPessoaJuridica;
        }

        public async Task<IEnumerable<PessoaJuridicaViewModel>> GetAllPessoaJuridicaAsync()
        {

            IEnumerable<PessoaJuridica> pessoaJuridicaRepository = await _pessoaJuridicaRepository.GetAllPessoaJuridicaAsync();

            IEnumerable<PessoaJuridicaViewModel> pessoaJuridicaViewModel = _mapper.Map<IEnumerable<PessoaJuridicaViewModel>>(pessoaJuridicaRepository);

            return pessoaJuridicaViewModel;

        }

        public async Task<PessoaJuridicaViewModel> GetPessoaJuridicaByIdAsync(int id)
        {
            PessoaJuridica pessoaJuridicaRepository = await _pessoaJuridicaRepository.GetPessoaJuridicaByIdAsync(id);

            PessoaJuridicaViewModel pessoaJuridicaViewModel = _mapper.Map<PessoaJuridicaViewModel>(pessoaJuridicaRepository);

            return pessoaJuridicaViewModel;
        }

        public async Task<PessoaJuridicaViewModel> GetPessoaJuridicaByRazaoSocialAsync(string razaoSocial)
        {
            PessoaJuridica pessoaJuridicaRepository = await _pessoaJuridicaRepository.GetPessoaJuridicaByRazaoSocialAsync(razaoSocial);

            PessoaJuridicaViewModel pessoaJuridicaViewModel = _mapper.Map<PessoaJuridicaViewModel>(pessoaJuridicaRepository);

            return pessoaJuridicaViewModel;
        }
    }
}
