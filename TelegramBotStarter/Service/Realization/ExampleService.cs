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
            return new MessageToSend("������ �����. �� ����� " + info.Request + ", � �������� /start");
        }
        else
        {
            info.Data.State = State.OutOfRoom.InputName;

            return new MessageToSend("������� ���� ���", false);
        }
    }


    [BotMethod(State.OutOfRoom.InputName, BotMethodType.Message)]
    public MessageToSend ProcessInputName(TransmittedInfo info)
    {
        var name = info.Request;

        info.Data.Storage.Add(ConstantsStorage.AccountName, name);

        info.Data.State = State.OutOfRoom.InputtedNameCorrect;

        var keyboard = BotKeyboardCreator.Instance.GetKeyboardMarkup(
            InlineKeyboardButton.WithCallbackData("��", "Yes"),
            InlineKeyboardButton.WithCallbackData("���", "No"));

        return new MessageToSend($"{name}. ���������?", keyboard, false);
    }


    [BotMethod(State.OutOfRoom.InputtedNameCorrect, BotMethodType.Callback)]
    public MessageToSend ProcessInputNameCorrect(TransmittedInfo info)
    {
        if (info.Request == "Yes")
        {
            info.Data.State = State.InRoom.MainMenu;

            var keyboard = BotKeyboardCreator.Instance.GetKeyboardMarkup(
                InlineKeyboardButton.WithCallbackData("�������", "Profile"),
                InlineKeyboardButton.WithCallbackData("�����", "Exit"));

            return new MessageToSend("��� ������� ������", keyboard, false);
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
            return new MessageToSend($"��� �������:\n��� {name}");
        }

        if (info.Request == "Exit")
        {
            return ProcessStart(info.GetNewWithRequest("/start"));
        }

        throw new NotSupportedException(info.Request);
    }
}