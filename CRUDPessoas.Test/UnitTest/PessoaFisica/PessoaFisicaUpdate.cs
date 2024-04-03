using CRUDPessoas.API.Controllers;
using CRUDPessoas.Application.DTOs.InputModels.PessoaFisica;
using CRUDPessoas.Application.DTOs.ViewModels.PessoaFisica;
using CRUDPessoas.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPessoas.Test.UnitTest.PessoaFisica
{
    public class PessoaFisicaUpdate
    {
        private readonly PessoaFisicaController _controller;
        private readonly Mock<IPessoaFisicaService> _mockService;

        public PessoaFisicaUpdate()
        {
            _mockService = new Mock<IPessoaFisicaService>();
            _controller = new PessoaFisicaController(_mockService.Object);
        }

        [Fact]
        public async Task CreateAsync_ReturnsOkWithCreatedProduct_WhenModelStateIsValid()
        {
            // Arrange
            var pessoaFisicaInputModel = new PessoaFisicaInputModel
            {
                Nome = "Teste",
                RG = "111111111",
                CPF = "22222222222",
                DataNascimento = DateTime.UtcNow.Date
            };
            _controller.ModelState.Clear();

            var createdPessoaFisica = new PessoaFisicaViewModel
            {
                Id = 1,
                Nome = pessoaFisicaInputModel.Nome,
                RG = pessoaFisicaInputModel.RG,
                CPF = pessoaFisicaInputModel.CPF,
                DataNascimento = pessoaFisicaInputModel.DataNascimento
            };
            _mockService.Setup(x => x.CreatePessoaFisicaAsync(It.IsAny<PessoaFisicaInputModel>())).ReturnsAsync(createdPessoaFisica);

            // Act
            var result = await _controller.CreateAsync(pessoaFisicaInputModel) as OkObjectResult;
            var createdProduct = result?.Value as PessoaFisicaViewModel;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.NotNull(createdProduct);
            Assert.Equal(createdPessoaFisica, createdProduct);
        }


        [Fact]
        public async Task CreateAsync_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var pessoaFisicaInputModel = new PessoaFisicaInputModel();
            _controller.ModelState.AddModelError("Nome", "O nome é obrigatório");

            // Act
            var result = await _controller.CreateAsync(pessoaFisicaInputModel) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsOk_WhenPessoaFisicaUpdatedSuccessfully()
        {
            // Arrange
            int id = 1;
            var pessoaFisicaInputModel = new PessoaFisicaUpdateInputModel { Nome = "Novo Nome" };

            _mockService.Setup(x => x.UpdatePessoaFisicaAsync(id, pessoaFisicaInputModel)).ReturnsAsync(1);

            // Act
            var result = await _controller.UpdateAsync(id, pessoaFisicaInputModel) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal("Pessoa Fisica atualizado com Sucesso", result.Value);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsBadRequest_WhenPessoaFisicaUpdateFails()
        {
            // Arrange
            int id = 1;
            var pessoaFisicaInputModel = new PessoaFisicaUpdateInputModel { Nome = "Novo Nome" };

            _mockService.Setup(x => x.UpdatePessoaFisicaAsync(id, pessoaFisicaInputModel)).ReturnsAsync(0);

            // Act
            var result = await _controller.UpdateAsync(id, pessoaFisicaInputModel) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenPessoaFisicaIsDeletedSuccessfully()
        {
            // Arrange
            int id = 1;
            _mockService.Setup(x => x.DeletePessoaFisicaAsync(id)).ReturnsAsync(1); // Supondo que a exclusão tenha sido bem-sucedida

            // Act
            var result = await _controller.Delete(id) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsBadRequest_WhenPessoaFisicaIsNotDeleted()
        {
            // Arrange
            int id = 1;
            _mockService.Setup(x => x.DeletePessoaFisicaAsync(id)).ReturnsAsync(0); // Supondo que a exclusão tenha falhado

            // Act
            var result = await _controller.Delete(id) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

    }
}
