using CRUDPessoas.API.Controllers;
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
    public class PessoaJuridicaRead
    {
        private readonly PessoaJuridicaController _controller;
        private readonly Mock<IPessoaJuridicaService> _mockService;

        public PessoaJuridicaRead()
        {
            _mockService = new Mock<IPessoaJuridicaService>();
            _controller = new PessoaJuridicaController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkWithPessoaJuridicaViewModelList_WhenServiceReturnsData()
        {
            // Arrange
            var mockData = new List<PessoaJuridicaViewModel>
            {
                new() { Id = 1, RazaoSocial = "Empresa 1" },
                new() { Id = 2, RazaoSocial = "Empresa 2" }
            };
            _mockService.Setup(x => x.GetAllPessoaJuridicaAsync()).ReturnsAsync(mockData);

            // Act
            var result = await _controller.GetAllAsync() as OkObjectResult;
            var data = result?.Value as IEnumerable<PessoaJuridicaViewModel>;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.NotNull(data);
            Assert.Equal(mockData.Count, data.Count());
        }

        [Fact]
        public async Task GetAllAsync_ReturnsInternalServerError_WhenServiceThrowsException()
        {
            // Arrange
            _mockService.Setup(x => x.GetAllPessoaJuridicaAsync()).ThrowsAsync(new ArgumentException("Simulated exception"));

            // Act
            var actionResult = await _controller.GetAllAsync();
            var result = actionResult as ObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsOkWithPessoaJuridicaViewModel_WhenServiceReturnsData()
        {
            // Arrange
            int id = 1;
            var mockPessoaJuridica = new PessoaJuridicaViewModel { Id = id, RazaoSocial = "Teste" };
            _mockService.Setup(x => x.GetPessoaJuridicaByIdAsync(id)).ReturnsAsync(mockPessoaJuridica);

            // Act
            var result = await _controller.GetByIdAsync(id) as OkObjectResult;
            var data = result?.Value as PessoaJuridicaViewModel;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.NotNull(data);
            Assert.Equal(mockPessoaJuridica, data);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNotFound_WhenServiceReturnsNull()
        {
            // Arrange
            int id = 1;
            _mockService.Setup(x => x.GetPessoaJuridicaByIdAsync(id)).ReturnsAsync((PessoaJuridicaViewModel?)null);

            // Act
            var result = await _controller.GetByIdAsync(id) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal("Pessoa Juridica Nao Encontrado", result.Value);
        }

        [Fact]
        public async Task GetByNameAsync_ReturnsOkWithPessoaJuridicaViewModel_WhenServiceReturnsData()
        {
            // Arrange
            string razaoSocial = "Teste";
            var mockPessoaJuridica = new PessoaJuridicaViewModel { RazaoSocial = razaoSocial };
            _mockService.Setup(x => x.GetPessoaJuridicaByRazaoSocialAsync(razaoSocial)).ReturnsAsync(mockPessoaJuridica);

            // Act
            var result = await _controller.GetByNameAsync(razaoSocial) as OkObjectResult;
            var data = result?.Value as PessoaJuridicaViewModel;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.NotNull(data);
            Assert.Equal(mockPessoaJuridica, data);
        }

        [Fact]
        public async Task GetByNameAsync_ReturnsNotFound_WhenServiceReturnsNull()
        {
            // Arrange
            string razaoSocial = "Inexistente";
            _mockService.Setup(x => x.GetPessoaJuridicaByRazaoSocialAsync(razaoSocial)).ReturnsAsync((PessoaJuridicaViewModel?)null);

            // Act
            var result = await _controller.GetByNameAsync(razaoSocial) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal("Pessoa Juridica Nao Encontrado", result.Value);
        }


    }
}
