using CRUDPessoas.Application.DTOs.InputModels.PessoaFisica;
using CRUDPessoas.Application.DTOs.InputModels.PessoaJuridica;
using CRUDPessoas.Application.DTOs.ViewModels;
using CRUDPessoas.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDPessoas.API.Controllers
{
    /// <summary>
    /// Controller responsável por lidar com operações relacionadas a pessoas juridicas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaJuridicaController : ControllerBase
    {
        private readonly IPessoaJuridicaService _pessoaJuridicaService;

        /// <summary>
        /// Construtor da classe <see cref="PessoaJuridicaController"/>.
        /// </summary>
        /// <param name="pessoaJuridicaService">Serviço de pessoa juridica utilizado pelo controlador.</param>
        public PessoaJuridicaController(IPessoaJuridicaService pessoaJuridicaService)
        {
            _pessoaJuridicaService = pessoaJuridicaService;
        }


        /// <summary>
        /// Obter todas as Pessoas Juridicas
        /// </summary>
        /// <returns>Coleção de Pessoas Juridicas</returns>
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
                IEnumerable<PessoaJuridicaViewModel> pessoaJuridica = await _pessoaJuridicaService.GetAllPessoaJuridicaAsync();

                return Ok(pessoaJuridica);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Obter uma Pessoa Juridica por Id
        /// </summary>
        /// <param name="id">Identificador da Pessoa Juridica</param>
        /// <returns>Dados da Pessoa Juridica especificada</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("GetById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {

            try
            {
                PessoaJuridicaViewModel pessoaJuridica = await _pessoaJuridicaService.GetPessoaJuridicaByIdAsync(id);

                if (pessoaJuridica != null)
                    return Ok(pessoaJuridica);

                return NotFound("Pessoa Juridica Nao Encontrado");
            }
            catch
            {
                return NotFound("Pessoa Juridica Nao Encontrado");
            }

        }

        /// <summary>
        /// Obter uma Pessoa Juridica que tenha o mesmo Nome
        /// </summary>
        /// <param name="razaoSocial">Campo para trazer a Pessoa Juridica em especifico</param>
        /// <returns>Dados da Pessoa Juridica especificada</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("GetByName/{razaoSocial}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByNameAsync(string razaoSocial)
        {
            try
            {
                PessoaJuridicaViewModel pessoaJuridica = await _pessoaJuridicaService.GetPessoaJuridicaByRazaoSocialAsync(razaoSocial);

                if (pessoaJuridica != null)
                    return Ok(pessoaJuridica);

                return NotFound("Pessoa Juridica Nao Encontrado");
            }
            catch
            {
                return NotFound("Pessoa Juridica Nao Encontrado");
            }
        }

        /// <summary>
        /// Cadastrar uma Pessoa Juridica
        /// </summary>
        /// <remarks>
        /// {
        ///"razaoSocial": "teste",
        ///"nomeFantasia": "teste",
        ///"cnpj": "12345678911111",
        ///"dataAbertura": "2023-04-02T22:20:15.031Z"
        ///}
        /// </remarks>
        /// <param name="pessoaFisicaId">Id da Pessoa Fisica vinculada</param>
        /// <param name="pessoaJuridicaViewModel">Dados da Pessoa Juridica</param>
        /// <returns>Pessoa Juridica recém criada</returns>
        /// <response code="200">Sucesso</response>
        /// /// <response code="400">Sucesso</response>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(int pessoaFisicaId, PessoaJuridicaInputModel pessoaJuridicaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                PessoaJuridicaViewModel pessoaJuridica = await _pessoaJuridicaService.CreatePessoaJuridicaAsync(pessoaFisicaId, pessoaJuridicaViewModel);
                return Ok(pessoaJuridica);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualizar uma Pessoa Juridica
        /// </summary>
        /// <remarks>
        /// {
        /// "razaoSocial": "teste",
        /// "nomeFantasia": "teste"
        /// }
        /// </remarks>
        /// <param name="id">Identificador da Pessoa Juridica</param>
        /// <param name="pessoaJuridicaViewModel">Dados da Pessoa Juridica que deseja atualizar</param>
        /// <returns>Sem Conteudo</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Bad Request</response>
        [HttpPut("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(int id, PessoaJuridicaUpdateInputModel pessoaJuridicaViewModel)
        {
            try
            {

                int existingPessoaFisica = await _pessoaJuridicaService.UpdatePessoaJuridicaAsync(id, pessoaJuridicaViewModel);
                if (existingPessoaFisica == 0)
                    return BadRequest();

                return Ok("Pessoa Juridica atualizado com Sucesso");

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletar uma Pessoa Juridica
        /// </summary>
        /// <param name="id">Identificador da Pessoa Juridica</param>
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
                int pessoaJuridica = await _pessoaJuridicaService.DeletePessoaJuridicaAsync(id);
                if (pessoaJuridica == 0)
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
