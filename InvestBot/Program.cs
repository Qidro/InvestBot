using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace InvestBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //отправляем в наш конструктор наш токен
            var client = new TelegramBotClient("5889212515:AAEcH4MhXVs7osVVlr7h5vgK4IacOEaKoAI");
            //задаем методы обработки
            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }

        

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {

            var message = update.Message;
            
            switch (message.Type)
            {
                case MessageType.Photo:
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Я не воспринимаю фотографии");
                        return;
                    }
                case MessageType.Video: 
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Я не воспринимаю видео");
                        return; 
                    }
                
                case MessageType.Audio:
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Я не воспринимаю аудиосообщения");
                        return;
                    }

                case MessageType.Sticker: 
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Я не воспринимаю стикеры");
                        return;
                    }
            }

            if (message.Text != null)
            {
                if (message.Text == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Привет");
                    return;
                }
            }
        }

        private static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
