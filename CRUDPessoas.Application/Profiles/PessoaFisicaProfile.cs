using AutoMapper;
using CRUDPessoas.Application.DTOs.InputModels.PessoaFisica;
using CRUDPessoas.Application.DTOs.ViewModels.PessoaFisica;
using CRUDPessoas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPessoas.Application.Profiles
{
    public class PessoaFisicaProfile : Profile
    {
        public PessoaFisicaProfile()
        {
            CreateMap<PessoaFisicaInputModel, PessoaFisica>();

            CreateMap<PessoaFisica, PessoaFisicaViewModel>();

        }
    }
}
