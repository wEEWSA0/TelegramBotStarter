using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

using TelegramBotShit.Router;

namespace TelegramBotShit.Bot;

public class Bot
{
    private const int CheckNotificationsDelay = 60000;
    private const int CheckStatisticDelay = 10000;
    
    private TelegramBotClient _botClient;
    private CancellationTokenSource _cancellationTokenSource;

    public Bot(string apiToken)
    {
        _botClient = new TelegramBotClient(apiToken);
        _cancellationTokenSource = new CancellationTokenSource();

        var botResponder = new BotResponder(_botClient, _cancellationTokenSource);
        
        if (!BotMessageManager.Create(botResponder))
        {
            throw new ApplicationException("Problems with BotMessageManager.Create");
        }
        
        if (!BotNotificationSender.Create(botResponder))
        {
            throw new ApplicationException("Problems with BotNotificationSender.Create");
        }

        Console.WriteLine("Выполнена инициализация TelegramBotClient");
    }

    public void Start()
    {
        ReceiverOptions receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        ChatsRouter chatsRouter = new ChatsRouter();
        BotRequestHandlers botRequestHandlers = new BotRequestHandlers(chatsRouter);

        _botClient.StartReceiving(
            botRequestHandlers.HandleUpdateAsync,
            botRequestHandlers.HandlePollingErrorAsync,
            receiverOptions,
            _cancellationTokenSource.Token
        );
        
        BotStatisticManager.GetInstance().StartCollectStatistic(CheckStatisticDelay);

        Console.WriteLine("Выполнен запуск бота");
    }

    public string GetBotName()
    {
        string? username = _botClient.GetMeAsync().Result.Username;
        
        if (username == null)
        {
            throw new ApplicationException("Ошибка получение имени бота");
            username = "";
        }
        
        return username;
    }

    public void Stop()
    {
        Console.WriteLine("Начата остановка бота");
        BotNotificationSystem.GetInstance().StopNotificationSystem();

        _cancellationTokenSource.Cancel();
        Console.WriteLine("Выполнена остановка бота");
    }
}