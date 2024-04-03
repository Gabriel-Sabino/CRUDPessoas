using CRUDPessoas.Application.DTOs.InputModels.PessoaFisica;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CRUDPessoas.Application.Validators.PessoaFisica
{
    public class PessoaFisicaValidator : AbstractValidator<PessoaFisicaInputModel>
    {
        public PessoaFisicaValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                    .WithMessage("Nome nao pode ser nulo")
                .MaximumLength(50)
                    .WithMessage("Nome deve ser menor que 50 caracteres");

            RuleFor(x => x.RG)
                .Must(IsValidRG)
                    .WithMessage("RG inválido")
                .NotEmpty()
                    .WithMessage("RG não pode ser nulo")
                .MaximumLength(10)
                    .WithMessage("Digite somente os numeros para o RG");


            RuleFor(x => x.CPF)
                .Must(IsValidCPF)
                    .WithMessage("CPF invalido")
                .NotEmpty()
                    .WithMessage("CPF nao pode ser nulo")
                .MaximumLength(11)
                    .WithMessage("Digite somente os numeros para o CPF");

            RuleFor(dt => new DateTime(dt.DataNascimento.Year, dt.DataNascimento.Month, dt.DataNascimento.Day))
                .LessThan(DateTime.Now.Date)
                    .WithMessage("Data de Nascimento deve ser anterior a data de hoje");
        }

        private static bool IsValidNumber(string number, int minLength, int maxLength)
        {
            var numberWithoutNonDigits = Regex.Replace(number, "[^0-9]", "");

            if (numberWithoutNonDigits.Length < minLength || numberWithoutNonDigits.Length > maxLength)
            {
                return false;
            }

            foreach (char c in numberWithoutNonDigits)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsValidCPF(string cpf)
        {
            return IsValidNumber(cpf, 11, 11);
        }

        private bool IsValidRG(string rg)
        {
            return IsValidNumber(rg, 8, 10);
        }

    }
}
