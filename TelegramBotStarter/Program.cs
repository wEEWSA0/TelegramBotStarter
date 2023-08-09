using TelegramFinancialGameBot;

BotStartup.Start();

await ProjectStartup.WaitExit(ProcessExit);

void ProcessExit(object sender, EventArgs e)
{
    Console.WriteLine("Bot will stopped");

    BotStartup.Bot.Stop();

    Console.WriteLine("Bot stopped");
}