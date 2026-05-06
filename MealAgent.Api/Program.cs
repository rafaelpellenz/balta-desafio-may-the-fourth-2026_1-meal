using Microsoft.Extensions.AI;
using MealAgent.Api.Agentes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MealAgentService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("/receita", async (RequisicaoReceita requisicao, MealAgentService agente) =>
{
    var resposta = await agente.EnviarMensagemAsync(
        $"Tenho os seguintes ingredientes: {requisicao.Ingredientes}. " +
        $"Tenho {requisicao.TempoLivre} minutos livres. Me sugira receitas.");

    return Results.Ok(new { resposta });
});

app.Run();

record RequisicaoReceita(string Ingredientes, int TempoLivre);