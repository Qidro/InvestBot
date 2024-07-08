using Telegram.Bot;
using Telegram.Bot.Types;

namespace InvestBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new TelegramBotClient("5889212515:AAEcH4MhXVs7osVVlr7h5vgK4IacOEaKoAI");
            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }

        

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        private static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
