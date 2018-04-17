using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Helpers;
using Telegram.Bot.Requests;
using Telegram.Bot.Responses;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot
{
    class Program
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient("547077460:AAGmyKd3EGBnWdaz30MUPrKrq01XcmHkGBs");
        static ReplyKeyboardMarkup MarkupYesOrNo = new ReplyKeyboardMarkup();
        static ReplyKeyboardMarkup MarkupCars = new ReplyKeyboardMarkup();
        static ReplyKeyboardMarkup MarkupCar = new ReplyKeyboardMarkup();

        static ReplyKeyboardMarkup number = new ReplyKeyboardMarkup();

        static InlineKeyboardMarkup InlineMarkup = new InlineKeyboardMarkup();
        static bool firstUser = false;


        static void Main(string[] args)
        {
            Bot.OnUpdate += Bot_OnUpdate;
            Bot.OnMessage += BotOnMessage;
            Bot.OnMessageEdited += BotOnMessage;
            Bot.StartReceiving();
            Console.ReadLine();
            Bot.StopReceiving();
        }

        private static void Bot_OnUpdate(object sender, UpdateEventArgs e)
        {

        }


        private static bool CheckMoreOne(long chatId)
        {
            return true;
        }
        private static async void BotOnMessage(object sender, MessageEventArgs e)
        {
            MarkupYesOrNo.ResizeKeyboard = true;
            MarkupYesOrNo.Keyboard = new KeyboardButton[][]
            {
                         new [] // first row
                        {
                           new KeyboardButton("Да"),
                            new KeyboardButton("Нет"),
                        }
            };
            MarkupCars.ResizeKeyboard = true;
            MarkupCars.Keyboard = new KeyboardButton[][]
            {
                        new[]
                        {
                            new KeyboardButton("lexus lx 570(ХВ567У)"),
                            new KeyboardButton("audi tt(ДА325А)"),
                        }
            };
            MarkupCar.ResizeKeyboard = true;
            MarkupCar.Keyboard = new KeyboardButton[][]
            {
                        new[]
                        {
                            new KeyboardButton("ford kuga(ТА531Д)"),
                        }
            };




            if (e.Message.Type == MessageType.TextMessage)
            {
                int[] Arr = new int[5];
                ReShape(Arr, 5);
                if (e.Message.Text == "/start" && firstUser == true)
                {
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, ("Введите номер телефона в формате - \"79993332222\""));

                }
                else if (e.Message.Text == "/start" && firstUser == false)
                {
                    AddButtons("Кнопка - ", 2);
                    if (CheckMoreOne(e.Message.Chat.Id))
                    {
                        await Bot.SendTextMessageAsync(e.Message.Chat.Id, ("Какую машину хотите выбрать?"), ParseMode.Default, false, false, 0, MarkupYesOrNo);
                    }
                }
                else
                {
                    if (e.Message.Text.Length < 11)
                    {
                        await Bot.SendTextMessageAsync(e.Message.Chat.Id, ("У вас цифр Меньше, чем нужно. Попробуйте еще раз. Введите номер телефона в формате - \"79993332222\""));
                    }
                    else if (e.Message.Text.Length > 11)
                    {
                        await Bot.SendTextMessageAsync(e.Message.Chat.Id, ("У вас цифр больше, чем нужно. Попробуйте еще раз. Введите номер телефона в формате - \"79993332222\""));
                    }
                    else if (e.Message.Text.Length == 11 && !(e.Message.Text.Any(c => char.IsLetter(c))))
                    {
                        await Bot.SendTextMessageAsync(e.Message.Chat.Id, ("Ваш номер - " + e.Message.Text));
                        Console.WriteLine(e.Message.Text);
                    }
                    else
                    {
                        await Bot.SendTextMessageAsync(e.Message.Chat.Id, ("У вас не правельный формат. Попробуйте еще раз. Введите номер телефона в формате - \"79993332222\""));
                    }
                }
                //throw new NotImplementedException();
            }
        }





        public static List<KeyboardButton> numbers = new List<KeyboardButton>();
        private static List<KeyboardButton> GenerateButtons(string name, int count)
        {
            List<KeyboardButton> numbers = new List<KeyboardButton>();
            for (int i = 0; i < count + 1; i++)
            {
                numbers.Add(new KeyboardButton(name + Convert.ToString(i))); // добавление элемента
            }
            return numbers;
        }

        private static void ReShape(int[] array, int caunt)
        {
            int[,] a = new int[0, caunt];
            for (int i = 0; i < array.Length; i++)
            {
                a[i, caunt] = array[i];
                Console.WriteLine(a[i, caunt]);
            }
        }
        private static void AddButtons(string name, int count)
        {
            //MarkupYesOrNo.ResizeKeyboard = true;
            //MarkupYesOrNo.Keyboard = new KeyboardButton[][]
            //{
            //             new [] // first row
            //            {
            //               new KeyboardButton("Да"),
            //                new KeyboardButton("Нет"),
            //            }
            //};
            MarkupYesOrNo.ResizeKeyboard = true;
            MarkupYesOrNo.Keyboard = new KeyboardButton[][]
            {
                GenerateButtons(name, count).Select( i => new KeyboardButton(i.Text.ToString())).ToArray()
            };
            //new KeyboardButton("Да"),
            //new KeyboardButton("Нет"),

        }
    }
}
