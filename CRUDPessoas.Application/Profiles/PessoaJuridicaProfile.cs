using AutoMapper;
using CRUDPessoas.Application.DTOs.InputModels.PessoaJuridica;
using CRUDPessoas.Application.DTOs.ViewModels.PessoaJuridica;
using CRUDPessoas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPessoas.Application.Profiles
{
    public class PessoaJuridicaProfile : Profile
    {
        public PessoaJuridicaProfile()
        {
            CreateMap<PessoaJuridicaInputModel, PessoaJuridica>();

            CreateMap<PessoaJuridica, PessoaJuridicaViewModel>();
        }
    }
}
