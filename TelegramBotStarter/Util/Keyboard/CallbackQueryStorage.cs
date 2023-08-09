using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramFinancialGameBot.Util.Keyboard;

internal class CallbackQueryStorage
{
    public static readonly ExampleCallbackQueryStorage Example = new ExampleCallbackQueryStorage();
}

internal class ExampleCallbackQueryStorage
{
    public readonly string YesForInputName = "yes_callback";
    public readonly string NoForInputName = "no_callback";

    public readonly string CreateRoom = "create_room_callback";
    public readonly string JoinInRoom = "join_room_callback";

    public readonly string BackInInputName = "back_name_callback";
}
