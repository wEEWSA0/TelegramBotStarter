using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotShit.Bot;

public class BotResponder
{
    private TelegramBotClient _botClient;
    private CancellationTokenSource _cancellationTokenSource;

    public BotResponder(TelegramBotClient botClient, CancellationTokenSource tokenSource)
    {
        _botClient = botClient;
        _cancellationTokenSource = tokenSource;
    }
    
    public Task<Message> SendText(MessageToSend message, long chatId)
    {
        return _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: message.Text,
            parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
            replyMarkup: message.KeyboardMarkup,
            cancellationToken: _cancellationTokenSource.Token);
    }

    public Task DeleteMessage(int messageId, long chatId)
    {
        return _botClient.DeleteMessageAsync(
            messageId: messageId,
            chatId: chatId,
            cancellationToken: _cancellationTokenSource.Token);
    }
}