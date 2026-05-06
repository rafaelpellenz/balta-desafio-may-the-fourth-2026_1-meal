using MealAgent.Agentes;

MealAgentService agente = new MealAgentService();

Console.WriteLine("=================================================");
Console.WriteLine("  MealAgent - Receitas para o seu tempo livre");
Console.WriteLine("  Digite 'sair' para encerrar.");
Console.WriteLine("=================================================\n");

Console.WriteLine("Olá! Me informe seus ingredientes disponíveis e quanto tempo livre você tem.\n");

while (true)
{
    Console.Write("Você: ");
    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input)) continue;
    if (input.Trim().ToLower() == "sair") break;

    Console.WriteLine();
    Console.Write("Agente: ");
    var resposta = await agente.EnviarMenssagemAsync(input);
    Console.WriteLine(resposta);
    Console.WriteLine();
}

Console.WriteLine("\nAté mais! 👋");