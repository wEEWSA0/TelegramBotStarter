namespace TelegramFinancialGameBot.Util.Reply;

internal class OutOfRoomStateReplyStorage
{
    public readonly string InputYourName =
        "Как мне к Вам обращаться?";

    public string InputUserName(string name) =>
        $"{name}, правильно?";

    public readonly string ErrorInput =
        "Команда не распознана. Для начала работы с ботом введите /start";

    public readonly string MainMenu =
        "Главное меню";

}
