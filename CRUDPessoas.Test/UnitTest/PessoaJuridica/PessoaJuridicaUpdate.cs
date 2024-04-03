
using CRUDPessoas.API.Controllers;
using CRUDPessoas.Application.DTOs.InputModels.PessoaJuridica;
using CRUDPessoas.Application.DTOs.ViewModels;
using CRUDPessoas.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPessoas.Test.UnitTest.PessoaJuridica
{
    public class PessoaJuridicaUpdate
    {
        private readonly PessoaJuridicaController _controller;
        private readonly Mock<IPessoaJuridicaService> _mockService;

        public PessoaJuridicaUpdate()
        {
            _mockService = new Mock<IPessoaJuridicaService>();
            _controller = new PessoaJuridicaController(_mockService.Object);
        }

        [Fact]
        public async Task CreateAsync_ReturnsOkWithCreatedPessoaJuridica_WhenModelStateIsValid()
        {
            // Arrange
            int pessoaFisicaId = 1;
            var pessoaJuridicaInputModel = new PessoaJuridicaInputModel
            {
                RazaoSocial = "Teste Razao Social",
                NomeFantasia = "Teste Nome Fantasia",
                CNPJ = "12345678911111",
                DataAbertura = DateTime.UtcNow.Date
            };
            _controller.ModelState.Clear();

            var mockPessoaJuridica = new PessoaJuridicaViewModel
            {
                RazaoSocial = pessoaJuridicaInputModel.RazaoSocial,
                NomeFantasia = pessoaJuridicaInputModel.NomeFantasia,
                CNPJ = pessoaJuridicaInputModel.CNPJ,
                DataAbertura = pessoaJuridicaInputModel.DataAbertura
            };
            _mockService.Setup(x => x.CreatePessoaJuridicaAsync(pessoaFisicaId, pessoaJuridicaInputModel)).ReturnsAsync(mockPessoaJuridica);

            // Act
            var result = await _controller.CreateAsync(pessoaFisicaId, pessoaJuridicaInputModel) as OkObjectResult;
            var createdPessoaJuridica = result?.Value as PessoaJuridicaViewModel;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.NotNull(createdPessoaJuridica);
            Assert.Equal(mockPessoaJuridica, createdPessoaJuridica);
        }

        [Fact]
        public async Task CreateAsync_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            int pessoaFisicaId = 1;
            var pessoaJuridicaInputModel = new PessoaJuridicaInputModel(); // ModelState inválido, nenhum campo preenchido

            _controller.ModelState.AddModelError("RazaoSocial", "O campo RazaoSocial é obrigatório.");

            // Act
            var result = await _controller.CreateAsync(pessoaFisicaId, pessoaJuridicaInputModel) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsOk_WhenPessoaJuridicaUpdateSucceeds()
        {
            // Arrange
            int id = 1;
            var pessoaJuridicaUpdateInputModel = new PessoaJuridicaUpdateInputModel
            {
                RazaoSocial = "Nova Razao Social",
                NomeFantasia = "Novo Nome Fantasia"
            };
            _mockService.Setup(x => x.UpdatePessoaJuridicaAsync(id, pessoaJuridicaUpdateInputModel)).ReturnsAsync(1);

            // Act
            var result = await _controller.UpdateAsync(id, pessoaJuridicaUpdateInputModel) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal("Pessoa Juridica atualizado com Sucesso", result.Value);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsBadRequest_WhenPessoaJuridicaUpdateFails()
        {
            // Arrange
            int id = 1;
            var pessoaJuridicaUpdateInputModel = new PessoaJuridicaUpdateInputModel
            {
                RazaoSocial = "Nova Razao Social",
                NomeFantasia = "Novo Nome Fantasia"
            };
            _mockService.Setup(x => x.UpdatePessoaJuridicaAsync(id, pessoaJuridicaUpdateInputModel)).ReturnsAsync(0);

            // Act
            var result = await _controller.UpdateAsync(id, pessoaJuridicaUpdateInputModel) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenPessoaJuridicaDeleteSucceeds()
        {
            // Arrange
            int id = 1;
            _mockService.Setup(x => x.DeletePessoaJuridicaAsync(id)).ReturnsAsync(1);

            // Act
            var result = await _controller.Delete(id) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsBadRequest_WhenPessoaJuridicaDeleteFails()
        {
            // Arrange
            int id = 1;
            _mockService.Setup(x => x.DeletePessoaJuridicaAsync(id)).ReturnsAsync(0);

            // Act
            var result = await _controller.Delete(id) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }


    }
}
