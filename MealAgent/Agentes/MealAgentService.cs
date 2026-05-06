using Microsoft.Extensions.AI;

namespace MealAgent.Agentes;

public class MealAgentService
{
    private readonly IChatClient _client;
    private readonly List<ChatMessage> _historico;

    public MealAgentService()
    {
        _client = new OllamaChatClient(
            new Uri("http://localhost:11434"),
            modelId: "llama3.2");

        _historico = new List<ChatMessage>
        {
            new ChatMessage(ChatRole.System, """
                Você é um assistente culinário inteligente.
                Sua função é cruzar a lista de ingredientes fornecida pelo usuário
                com o tempo livre disponível e sugerir receitas práticas que se
                encaixem exatamente nesse intervalo de tempo.

                Regras:
                - Sugira receitas que usem preferencialmente os ingredientes fornecidos.
                - O tempo de preparo deve caber no tempo livre informado.
                - Apresente cada receita com: nome, tempo de preparo e ingredientes necessários.
                - Se faltar algum ingrediente essencial, mencione de forma simples.
                - Seja objetivo e prático. O usuário está com pouco tempo.
                - Responda sempre em português do Brasil.
                - Não responda perguntas fora do tema de receitas e culinária.
            """)
        };
    }

    public async Task<string> EnviarMenssagemAsync(string mensagem)
    {
        _historico.Add(new ChatMessage(ChatRole.User, mensagem));

        var resposta = await _client.GetResponseAsync(_historico);
        var texto = resposta.Text;

        _historico.Add(new ChatMessage(ChatRole.Assistant, texto));

        return texto;
    }
}