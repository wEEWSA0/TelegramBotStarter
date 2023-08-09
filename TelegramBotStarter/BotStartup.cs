using TelegramBotShit.Bot;

namespace TelegramFinancialGameBot;

public static class BotStartup
{
    public static Bot Bot { get; set; }

    private const string _botToken = "6474086629:AAFM9UGP_-2COcE6L0TeXtoRXyFt6dE2Kk0";// yuor bot token given by BotFather
    // Link to example bot https://t.me/starterpackexamplebot

    public static void Start()
    {
        Bot = new Bot(_botToken);

        Bot.Start();

        Console.WriteLine("\nPress 'Ctrl + c' to stop\n");
    }
}
