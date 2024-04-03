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
    public class PessoaJuridicaRepository : IPessoaJuridicaRepository
    {
        private readonly PessoaDbContext _dbContext;
        public PessoaJuridicaRepository(PessoaDbContext context)
        {
            _dbContext = context;
        }
        public async Task<PessoaJuridica> CreatePessoaJuridicaAsync(PessoaJuridica pessoaJuridica)
        {
            await _dbContext.PessoaJuridica.AddAsync(pessoaJuridica);
            await _dbContext.SaveChangesAsync();
            return pessoaJuridica;
        }

        public async Task<int> DeletePessoaJuridicaAsync(PessoaJuridica pessoaJuridica)
        {
            _dbContext.PessoaJuridica.Remove(pessoaJuridica);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<PessoaJuridica>> GetAllPessoaJuridicaAsync()
        {
            return await _dbContext.PessoaJuridica.ToListAsync();
        }

        public async Task<bool> GetVerificaPessoaJuridicaByCNPJAsync(string cnpj)
        {
            return await _dbContext.PessoaJuridica.AnyAsync(x => x.CNPJ == cnpj);
        }

        public async Task<PessoaJuridica> GetPessoaJuridicaByIdAsync(int id)
        {
            return await _dbContext.PessoaJuridica
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PessoaJuridica> GetPessoaJuridicaByRazaoSocialAsync(string razaoSocial)
        {
            return await _dbContext.PessoaJuridica
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.RazaoSocial == razaoSocial);
        }

        public async Task<bool> GetVerificaPessoaJuridicaByRazaoSocialAsync(string razaoSocial)
        {
            return await _dbContext.PessoaJuridica.AnyAsync(x => x.RazaoSocial == razaoSocial);
        }

        public async Task<int> UpdatePessoaJuridicaAsync(PessoaJuridica pessoaJuridica)
        {
            _dbContext.Entry(pessoaJuridica).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }
    }
}
