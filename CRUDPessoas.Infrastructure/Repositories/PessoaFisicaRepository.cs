using CRUDPessoas.Domain.Entities;
using CRUDPessoas.Domain.Interfaces.Repository;
using CRUDPessoas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPessoas.Infrastructure.Repositories
{
    public class PessoaFisicaRepository : IPessoaFisicaRepository
    {
        private readonly PessoaDbContext _dbContext;
        public PessoaFisicaRepository(PessoaDbContext context)
        {
            _dbContext = context;
        }

        public async Task<PessoaFisica> CreatePessoaFisicaAsync(PessoaFisica pessoaFisica)
        {
            await _dbContext.PessoaFisica.AddAsync(pessoaFisica);
            await _dbContext.SaveChangesAsync();
            return pessoaFisica;
        }

        public async Task<int> DeletePessoaFisicaAsync(PessoaFisica pessoaFisica)
        {
                _dbContext.PessoaFisica.Remove(pessoaFisica);
                return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PessoaFisica>> GetAllPessoaFisicaAsync()
        {
            return await _dbContext.PessoaFisica.ToListAsync();
        }

        public async Task<bool> GetPessoaFisicaByCPFAsync(string cpf)
        {
            return await _dbContext.PessoaFisica.AnyAsync(x => x.CPF == cpf);
        }
        public async Task<bool> GetPessoaFisicaByRGAsync(string rg)
        {
            return await _dbContext.PessoaFisica.AnyAsync(x => x.RG == rg);
        }

        public async Task<PessoaFisica> GetPessoaFisicaByIdAsync(int id)
        {
            return await _dbContext.PessoaFisica
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<PessoaFisica>> GetPessoaFisicaByNameAsync(string nome)
        {
            return await _dbContext.PessoaFisica
                .AsNoTracking()
                .Where(x => x.Nome == nome).ToListAsync();
        }

        

        public async Task<int> UpdatePessoaFisicaAsync(PessoaFisica pessoaFisica)
        {
            _dbContext.Entry(pessoaFisica).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }
    }
}
