namespace TelegramFinancialGameBot.Service.Router.Transmitted;

// Uses as enum
public sealed class State
{
    // limit for public const values per one class
    private const int _perLevel = 20;

    private State()
    {

    }

    private const int UnregisteredId = 0;
    public static class OutOfRoom
    {
        private const int parent = UnregisteredId * _perLevel;

        public const int CmdStart = parent + 1;

        public const int InputName = parent + 2;
        public const int InputtedNameCorrect = parent + 3;
    }

    private const int RegisteredId = 1;
    public static class InRoom
    {
        private const int parent = RegisteredId * _perLevel;

        public const int MainMenu = parent + 1;

        public static class SetupCharacter
        {
            private const int parent = InRoom.parent * _perLevel * 1;

            public const int CreateCharacter = parent + 1;
        }
    }
}
