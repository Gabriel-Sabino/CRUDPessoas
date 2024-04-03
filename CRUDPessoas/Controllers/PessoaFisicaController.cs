using CRUDPessoas.Application.DTOs.InputModels.PessoaFisica;
using CRUDPessoas.Application.DTOs.ViewModels;
using CRUDPessoas.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CRUDPessoas.API.Controllers
{
    /// <summary>
    /// Controller responsável por lidar com operações relacionadas a pessoas físicas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaFisicaController : ControllerBase
    {
        private readonly IPessoaFisicaService _pessoaFisicaService;

        /// <summary>
        /// Construtor da classe <see cref="PessoaFisicaController"/>.
        /// </summary>
        /// <param name="pessoaFisicaService">Serviço de pessoa física utilizado pelo controlador.</param>
        public PessoaFisicaController(IPessoaFisicaService pessoaFisicaService)
        {
            _pessoaFisicaService = pessoaFisicaService;
        }

        /// <summary>
        /// Obter todas as Pessoas Fisicas
        /// </summary>
        /// <returns>Coleção de Pessoas Fisicas</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                IEnumerable<PessoaFisicaViewModel> pessoaFisica = await _pessoaFisicaService.GetAllPessoaFisicaAsync();

                return Ok(pessoaFisica);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Obter uma Pessoa Fisica por Id
        /// </summary>
        /// <param name="id">Identificador da Pessoa Fisica</param>
        /// <returns>Dados da Pessoa Fisica especificada</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("GetById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {

            try
            {
                PessoaFisicaViewModel pessoaFisica = await _pessoaFisicaService.GetPessoaFisicaByIdAsync(id);

                if (pessoaFisica != null)
                    return Ok(pessoaFisica);

                return NotFound("Pessoa Fisica Nao Encontrado");
            }
            catch
            {
                return NotFound("Pessoa Fisica Nao Encontrado");
            }

        }

        /// <summary>
        /// Obter Pessoas que tenham o mesmo Nome
        /// </summary>
        /// <param name="name">Campo para trazer a/as Pessoas Fisicas em especifico</param>
        /// <returns>Dados das Pessoas Fisicas especificada</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("GetByName/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            try
            {
                var pessoaFisica = await _pessoaFisicaService.GetPessoaFisicaByNameAsync(name);

                if (pessoaFisica.Any())
                    return Ok(pessoaFisica);

                return NotFound("Pessoa Fisica Nao Encontrado");
            }
            catch
            {
                return NotFound("Pessoa Fisica Nao Encontrado");
            }
        }

        /// <summary>
        /// Cadastrar uma Pessoa Fisica
        /// </summary>
        /// <remarks>
        /// {
        ///"nome": "teste",
        ///"rg": "111111111",
        ///"cpf": "22222222222",
        ///"dataNascimento": "2023-04-02T22:20:15.031Z"
        ///}
        /// </remarks>
        /// <param name="pessoaFisicaInputModel">Dados da Pessoa Fisica</param>
        /// <returns>Pessoa Fisica recém criada</returns>
        /// <response code="200">Sucesso</response>
        /// /// <response code="400">Sucesso</response>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(PessoaFisicaInputModel pessoaFisicaInputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdProduct = await _pessoaFisicaService.CreatePessoaFisicaAsync(pessoaFisicaInputModel);
                return Ok(createdProduct);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualizar uma Pessoa Fisica
        /// </summary>
        /// <remarks>
        /// {
        /// "nome": "string"
        /// }
        /// </remarks>
        /// <param name="id">Identificador da Pessoa Fisica</param>
        /// <param name="pessoaFisicaInputModel">Dados da Pessoa Fisica que deseja atualizar</param>
        /// <returns>Sem Conteudo</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Bad Request</response>
        [HttpPut("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(int id, PessoaFisicaUpdateInputModel pessoaFisicaInputModel)
        {
            try
            {

                var existingPessoaFisica = await _pessoaFisicaService.UpdatePessoaFisicaAsync(id, pessoaFisicaInputModel);
                if (existingPessoaFisica == 0)
                    return BadRequest();

                return Ok("Pessoa Fisica atualizado com Sucesso");

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletar uma Pessoa Fisica
        /// </summary>
        /// <param name="id">Identificador da Pessoa Fisica</param>
        /// <returns>Sem Conteudo</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="400">Bad Request</response>
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                int pessoaFisica = await _pessoaFisicaService.DeletePessoaFisicaAsync(id);
                if (pessoaFisica == 0)
                    return BadRequest();

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
