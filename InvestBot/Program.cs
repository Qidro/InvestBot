using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.IO;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;
using System;
using static System.Net.Mime.MediaTypeNames;

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
            var callback = update.CallbackQuery;
           
            //обработка inline собыйтий
            if (callback != null && callback.Data == "1.1")
            {
                var buttons = new InlineKeyboardButton[]
                        {
                          InlineKeyboardButton.WithCallbackData("Давай перейдем на урок 2", "1.2")
                        };
                await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, System.IO.File.ReadAllText("Архив\\Уроки инвестирования\\Урок1.txt"),
                   replyMarkup: new InlineKeyboardMarkup(buttons));
                return;
              
            }
            else if (callback != null && callback.Data == "1.2")
            {
                var buttons = new InlineKeyboardButton[]
                        {
                          InlineKeyboardButton.WithCallbackData("Давай перейдем на урок 1", "1.1")
                        };
                await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, System.IO.File.ReadAllText("Архив\\Уроки инвестирования\\Урокы2.txt"),
                   replyMarkup: new InlineKeyboardMarkup(buttons));
                return;

            }

            //смотрим какой тип сообщения отправил пользователь, если тип не доступный для обработки - бот это уведомит
            switch (message.Type)
            {
                case MessageType.Photo:
                    {
                        

                            //@"F:\Проекты\photo\aa.jpg"
                   
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
                default:
                    break;
            }

            //если было отправленно текстовое сообщение, то бот начнет обработку тектсовых команд
            if (message.Text != null)
            {
                //приветствие
                if (message.Text == "/start")
                {
                    //создание клавиатурных кнопок
                    var buttons = new KeyboardButton[][]
                    {
                        new KeyboardButton[] { "Главная страница","О пет-проекте" },
                        new KeyboardButton[] { "Уроки инвестирования", "Глоссарий инвестора" },
                    };
                    //Приветсвие 
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Привет, я бот РБО! Я начну тебя базовым навыкам инвестирования.\n" +
                        "\nТы познакомишься с фондовым рынком и узнаешь как правильного подбирать компании для своего портфеля, что поможет тебе получить новый заработок.");
                    //Вывод на экран клавиатуры
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Теперь начнем тебя обучать!",
                       replyMarkup:  new ReplyKeyboardMarkup(buttons) { ResizeKeyboard = true });

                    return;
                }

                //обработка клавиатурных унопок
                switch (message.Text)
                {
                    case "Главная страница":
                       
                        
                        await botClient.SendTextMessageAsync(message.Chat.Id, System.IO.File.ReadAllText("Архив\\Главная страница\\Главная страница.txt"));
                        return;

                    case "Уроки инвестирования":
                        //создание кнопок сообщения
                        var buttons = new InlineKeyboardButton[]
                        {
                          InlineKeyboardButton.WithCallbackData("1 урок", "1.1"),
                          InlineKeyboardButton.WithCallbackData("2 урок", "1.2"),
                                    
                        };
                        await botClient.SendTextMessageAsync(message.Chat.Id, System.IO.File.ReadAllText("Архив\\Уроки инвестирования\\Уроки.txt"),
                           replyMarkup: new InlineKeyboardMarkup(buttons) );
                        return;

                    case "О пет-проекте":
                        await botClient.SendTextMessageAsync(message.Chat.Id, System.IO.File.ReadAllText("Архив\\О пет проекте\\О проекте.txt"));
                        
                        return;

                    case "Глоссарий инвестора":
                        await botClient.SendTextMessageAsync(message.Chat.Id, System.IO.File.ReadAllText("Архив\\Глоссарий инвестора\\Глоссарий1.txt"));
                        await botClient.SendTextMessageAsync(message.Chat.Id, System.IO.File.ReadAllText("Архив\\Глоссарий инвестора\\Глоссарий2.txt"));
                        return;
                }
            }
            
            //switch (update.Type)
            //{
            //    case UpdateType.CallbackQuery:
            //        {
            //            switch (update.CallbackQuery.Data)
            //            {
            //                case "1.1":
            //                    {


            //                        await botClient.SendTextMessageAsync(
            //                           message.Chat.Id,
            //                            "aaaaa");
            //                        return;
            //                    }
            //                case "1.2":
            //                    {


            //                        await botClient.SendTextMessageAsync(
            //                           message.Chat.Id,
            //                            "aaaaa");
            //                        return;
            //                    }
            //            }
            //            return;
            //        }

            //}

        }



        
        //метод обработки ошибок
        private static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            Console.WriteLine("Произошла ошибка: "+ exception.ToString() + "\n Пользователь: "+ client.ToString());
            return Task.CompletedTask;

        }
    }
}
