using Telegram.Bot.Types.ReplyMarkups;

using TelegramBotShit.Bot;
using TelegramBotShit.Bot.Buttons;

using TelegramBotStarter.Repository;

using TelegramFinancialGameBot.Service.Attributes;
using TelegramFinancialGameBot.Service.Router.Transmitted;
using TelegramFinancialGameBot.Util;

namespace TelegramBotShit.Service.ServiceRealization;

[BotMethodsClass]
public class ExampleService
{
    private ExampleRepository _exampleRepository;

    public ExampleService()
    {
        _exampleRepository = new();
    }


    [BotMethod(State.OutOfRoom.CmdStart, BotMethodType.Message)]
    public MessageToSend ProcessStart(TransmittedInfo info)
    {
        if (info.Request != "/start")
        {
            return new MessageToSend("Ошибка ввода. Вы ввели " + info.Request + ", а ожидался /start");
        }
        else
        {
            info.Data.State = State.OutOfRoom.InputName;

            return new MessageToSend("Введите ваше имя", false);
        }
    }


    [BotMethod(State.OutOfRoom.InputName, BotMethodType.Message)]
    public MessageToSend ProcessInputName(TransmittedInfo info)
    {
        var name = info.Request;

        info.Data.Storage.Add(ConstantsStorage.AccountName, name);

        info.Data.State = State.OutOfRoom.InputtedNameCorrect;

        var keyboard = BotKeyboardCreator.Instance.GetKeyboardMarkup(
            InlineKeyboardButton.WithCallbackData("Да", "Yes"),
            InlineKeyboardButton.WithCallbackData("Нет", "No"));

        return new MessageToSend($"{name}. Правильно?", keyboard, false);
    }


    [BotMethod(State.OutOfRoom.InputtedNameCorrect, BotMethodType.Callback)]
    public MessageToSend ProcessInputNameCorrect(TransmittedInfo info)
    {
        if (info.Request == "Yes")
        {
            info.Data.State = State.InRoom.MainMenu;

            var keyboard = BotKeyboardCreator.Instance.GetKeyboardMarkup(
                InlineKeyboardButton.WithCallbackData("Профиль", "Profile"),
                InlineKeyboardButton.WithCallbackData("Выйти", "Exit"));

            return new MessageToSend("Ваш профиль создан", keyboard, false);
        }
        
        if (info.Request == "No")
        {
            return ProcessStart(info.GetNewWithRequest("/start"));
        }

        throw new NotSupportedException(info.Request);
    }


    [BotMethod(State.InRoom.MainMenu, BotMethodType.Callback)]
    public MessageToSend ProcessButtonsForMainMenu(TransmittedInfo info)
    {
        var name = info.Data.Storage.GetOrThrow<string>(ConstantsStorage.AccountName);

        if (info.Request == "Profile")
        {
            return new MessageToSend($"Ваш профиль:\nИмя {name}");
        }

        if (info.Request == "Exit")
        {
            return ProcessStart(info.GetNewWithRequest("/start"));
        }

        throw new NotSupportedException(info.Request);
    }
}