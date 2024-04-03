using CRUDPessoas.Application.DTOs.InputModels.PessoaJuridica;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CRUDPessoas.Application.Validators.PessoaJuridica
{
    public class PessoaJuridicaValidator : AbstractValidator<PessoaJuridicaInputModel>
    {
        public PessoaJuridicaValidator()
        {
            RuleFor(x => x.RazaoSocial)
                .MaximumLength(100)
                    .WithMessage("Razao Social deve ser menor que 100")
                .NotEmpty()
                    .WithMessage("Razao Social nao pode ser nulo");

            RuleFor(x => x.NomeFantasia)
                .MaximumLength(100)
                    .WithMessage("Nome Fantasia deve ser menor que 100")
                .NotEmpty()
                    .WithMessage("Nome Fantasia nao pode ser nulo");

            RuleFor(dt => new DateTime(dt.DataAbertura.Year, dt.DataAbertura.Month, dt.DataAbertura.Day))
                .LessThan(DateTime.Now.Date)
                    .WithMessage("Data de Abertura deve ser anterior a data de hoje");
            RuleFor(x => x.CNPJ)
                .Must(IsValidCNPJ)
                    .WithMessage("CNPJ inválido")
                .NotEmpty()
                    .WithMessage("CNPJ não pode ser nulo")
                .MaximumLength(14)
                    .WithMessage("Digite o CNPJ somente com numeros");
        }

        private static bool IsValidNumber(string number, int length)
        {
            var numberWithoutNonDigits = Regex.Replace(number, "[^0-9]", "");

            return numberWithoutNonDigits.Length == length &&
                   numberWithoutNonDigits.All(char.IsDigit);
        }

        private bool IsValidCNPJ(string cnpj)
        {
            return IsValidNumber(cnpj, 14);
        }
    }
}
