using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using SomeBot.Models;

namespace ConsoleApp1
{
    class Program
    {
         public static ITelegramBotClient botClient;
        static void Main(string[] args)
        {
            botClient = new TelegramBotClient(SomeBot.Config.telegramToken) { Timeout = TimeSpan.FromSeconds(20) };

          
             var bot = botClient.GetMeAsync().Result;
           

            Console.WriteLine($"Бот работает. \nBot:{bot.Id} \nИмя:{bot.FirstName}");
            botClient.OnMessage += MessageFromBot;
              botClient.StartReceiving();
        
            Console.ReadKey();
             botClient.StopReceiving();
        }


        
         public async static void MessageFromBot(object sender, MessageEventArgs e)
        {
            var text = e?.Message?.Text;
            if (text == null)
                return;
            await botClient.SendTextMessageAsync(
                         chatId: e.Message.Chat,
                         text: $" Узнаем погоду..."
                         ).ConfigureAwait(false);
            try
            {
                var city = SomeBot.Api.apiLogic(text);
                 await botClient.SendTextMessageAsync(
                         chatId: e.Message.Chat,
                         text: $" Погода {text} \n {city}"
                         ).ConfigureAwait(false);

                if (text == "СПБ" || text == "Спб") //почему не работает?
                {
                    text = "Санкт-Петербург";
                }


            }
            catch (Exception)
            {
                await botClient.SendTextMessageAsync(
            chatId: e.Message.Chat,
            text: "Город не найден "
            ).ConfigureAwait(false);
            }
        }
    }

        

    }

       





