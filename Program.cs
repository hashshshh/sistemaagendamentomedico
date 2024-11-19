using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SistemaAgendamentoMedico.Services;
using SistemaAgendamentoMedico.Data;
using SistemaAgendamentoMedico.Models;
using Microsoft.Extensions.Configuration;

// Cria o builder para configurar o aplicativo
var builder = WebApplication.CreateBuilder(args);

// Adiciona configurações de MongoDB ao sistema de injeção de dependências
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));

// Adiciona serviços ao contêiner de injeção de dependência
builder.Services.AddSingleton<MongoDBContext>();
builder.Services.AddSingleton<MedicoService>();
builder.Services.AddSingleton<PacienteService>();
builder.Services.AddSingleton<AgendamentoService>();

// Configurações de controladores e endpoints para API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // para documentação com Swagger

var app = builder.Build();

// Configuração do middleware para a aplicação
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles(); // Serve os arquivos estáticos em wwwroot

app.UseDefaultFiles();  // Opção para servir index.html por padrão

app.UseRouting();

// Executa a aplicação
app.Run();
