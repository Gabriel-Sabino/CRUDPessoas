using CRUDPessoas.Application.DTOs.InputModels.PessoaFisica;
using CRUDPessoas.Application.Interfaces.Services;
using CRUDPessoas.Application.Profiles;
using CRUDPessoas.Application.Services;
using CRUDPessoas.Application.Validators.PessoaFisica;
using CRUDPessoas.Application.Validators.PessoaJuridica;
using CRUDPessoas.Domain.Entities;
using CRUDPessoas.Domain.Interfaces.Repository;
using CRUDPessoas.Infrastructure.Data;
using CRUDPessoas.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PessoaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPessoaFisicaRepository, PessoaFisicaRepository>();
builder.Services.AddScoped<IPessoaJuridicaRepository, PessoaJuridicaRepository>();

builder.Services.AddScoped<IPessoaFisicaService, PessoaFisicaService>();
builder.Services.AddScoped<IPessoaJuridicaService, PessoaJuridicaService>();

builder.Services.AddAutoMapper(typeof(PessoaFisicaProfile));
builder.Services.AddAutoMapper(typeof(PessoaJuridicaProfile));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<PessoaFisicaValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PessoaJuridicaValidator>();

builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//documentacao API
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CRUDPessoas",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Gabriel Sabino",
            Email = "gabrielsabino1505@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/gabriel-sabino1/")
        }
    });

    var xmlFile = "CRUDPessoas.API.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
