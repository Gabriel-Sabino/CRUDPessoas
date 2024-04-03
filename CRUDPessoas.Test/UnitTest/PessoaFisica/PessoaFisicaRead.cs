using CRUDPessoas.API.Controllers;
using CRUDPessoas.Application.DTOs.InputModels.PessoaFisica;
using CRUDPessoas.Application.DTOs.ViewModels;
using CRUDPessoas.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CRUDPessoas.UnitTests.Controllers
{
    public class PessoaFisicaRead
    {
        private readonly PessoaFisicaController _controller;
        private readonly Mock<IPessoaFisicaService> _mockService;

        public PessoaFisicaRead()
        {
            _mockService = new Mock<IPessoaFisicaService>();
            _controller = new PessoaFisicaController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkWithPessoaFisicaViewModelList_WhenServiceReturnsData()
        {
            // Arrange
            var mockData = new List<PessoaFisicaViewModel>
            {
                new() { Id = 1, Nome = "Teste 1" },
                new() { Id = 2, Nome = "Teste 2" }
            };
            _mockService.Setup(x => x.GetAllPessoaFisicaAsync()).ReturnsAsync(mockData);

            // Act
            var result = await _controller.GetAllAsync() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);

            if (result != null)
            {
                var data = result.Value as IEnumerable<PessoaFisicaViewModel>;
                Assert.NotNull(data);
                Assert.Equal(mockData.Count, data?.Count());
            }
        }


        [Fact]
        public async Task GetAllAsync_ReturnsInternalServerError_WhenServiceThrowsException()
        {
            // Arrange
            _mockService.Setup(x => x.GetAllPessoaFisicaAsync()).ThrowsAsync(new Exception("Internal Server Error"));

            // Act
            var result = await _controller.GetAllAsync() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsOkWithPessoaFisicaViewModel_WhenServiceReturnsData()
        {
            // Arrange
            int id = 1;
            var mockPessoaFisica = new PessoaFisicaViewModel { Id = id, Nome = "Teste" };
            _mockService.Setup(x => x.GetPessoaFisicaByIdAsync(id)).ReturnsAsync(mockPessoaFisica);

            // Act
            var result = await _controller.GetByIdAsync(id) as OkObjectResult;
            var data = result?.Value as PessoaFisicaViewModel;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.NotNull(data);
            Assert.Equal(mockPessoaFisica, data);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNotFound_WhenServiceReturnsNull()
        {
            // Arrange
            int id = 1;
            _mockService.Setup(x => x.GetPessoaFisicaByIdAsync(id)).ReturnsAsync((PessoaFisicaViewModel?)null);


            // Act
            var result = await _controller.GetByIdAsync(id) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal("Pessoa Fisica Nao Encontrado", result.Value);
        }

        [Fact]
        public async Task GetByNameAsync_ReturnsOkWithPessoaFisicaViewModelList_WhenServiceReturnsData()
        {
            // Arrange
            string name = "Teste";
            var mockData = new List<PessoaFisicaViewModel>
            {
                new() { Id = 1, Nome = name },
                new() { Id = 2, Nome = name }
            };
            _mockService.Setup(x => x.GetPessoaFisicaByNameAsync(name)).ReturnsAsync(mockData);

            // Act
            var result = await _controller.GetByNameAsync(name) as OkObjectResult;
            var data = result?.Value as IEnumerable<PessoaFisicaViewModel>;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.NotNull(data);
            Assert.Equal(mockData.Count, data.Count());
        }

        [Fact]
        public async Task GetByNameAsync_ReturnsNotFound_WhenServiceReturnsEmptyList()
        {
            // Arrange
            string name = "Teste";
            _mockService.Setup(x => x.GetPessoaFisicaByNameAsync(name)).ReturnsAsync(new List<PessoaFisicaViewModel>());

            // Act
            var result = await _controller.GetByNameAsync(name) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal("Pessoa Fisica Nao Encontrado", result.Value);
        }

    }
}