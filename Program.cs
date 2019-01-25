using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Suicide_Bot MyBot = new Suicide_Bot();
            MyBot.testApiAsync();
          
            Console.ReadKey();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Udav 2\source\repos\data.txt"))
            {
                file.Write(Suicide_Bot.last_messeg_id.ToString());
            }
            

        }
    }


    class Suicide_Bot
    {
        public static string token = "663983910:AAEmTmXkWpuQMmfwdrF40FI0XEfR9UzpifQ";
        static Telegram.Bot.ITelegramBotClient Bot;
        public static int last_messeg_id;
        static int cur = 0;
        public async void testApiAsync()
        {
            last_messeg_id = Convert.ToInt32(File.ReadAllText(@"C:\Users\Udav 2\source\repos\data.txt"));
            Bot = new Telegram.Bot.TelegramBotClient(token);
            var me = await Bot.GetMeAsync();
            System.Console.WriteLine("Hello, fuck off. My name is {0}, if u interested in", me.FirstName);
            Thread newThread = new Thread(Suicide_Bot.ReciveMassage);
            newThread.Start();
        }

        private static async void ReciveMassage()
        {
           
            
            while (true)
            {
                var messages = await Bot.GetUpdatesAsync(offset: last_messeg_id+1);
                if (messages.Length > 0)
                {
                    
                    if (last_messeg_id != messages[messages.Length -1].Id)
                    {
                        for (cur = 0; cur < messages.Length; cur++)
                        {
                            var cur_messeg = messages[cur];
                            if (cur_messeg.Message != null)
                            {
                                if (cur_messeg.Message.Text != null && !cur_messeg.Message.Text.Contains("/start"))
                                {
                                    Console.WriteLine("{0} from: {1}", cur_messeg.Message.Text, cur_messeg.Message.From.FirstName);
                                    Bot.SendTextMessageAsync(-1001196454585, cur_messeg.Message.Text);
                                    Bot.SendTextMessageAsync(cur_messeg.Message.From.Id, "Твое сообщение уже на Канале!");
                                    
                                }
                                if (cur_messeg.Message.Photo != null)
                                {
                                    Console.WriteLine("Photo from: {0}", cur_messeg.Message.From.FirstName);
                                    Bot.SendPhotoAsync(-1001196454585, cur_messeg.Message.Photo[0].FileId, cur_messeg.Message.Caption);
                                    Bot.SendTextMessageAsync(cur_messeg.Message.From.Id, "Твое картинка уже на Канале!");
                                    
                                }
                                if (cur_messeg.Message.Video != null)
                                {
                                    Console.WriteLine("video from: {0}", cur_messeg.Message.From.FirstName);
                                    Bot.SendVideoAsync(-1001196454585, cur_messeg.Message.Video.FileId);
                                    Bot.SendTextMessageAsync(cur_messeg.Message.From.Id, "Твое видео уже на Канале!");
                                    
                                }
                                if (cur_messeg.Message.Voice != null)
                                {
                                    Console.WriteLine("voice from: {0}", cur_messeg.Message.From.FirstName);
                                    Bot.SendVoiceAsync(-1001196454585, cur_messeg.Message.Voice.FileId);
                                    Bot.SendTextMessageAsync(cur_messeg.Message.From.Id, "Ты пустил гадзу в чат!");
                                }
                                if (cur_messeg.Message.VideoNote != null)
                                {
                                    Console.WriteLine("VideoNote from: {0}", cur_messeg.Message.From.FirstName);
                                    Bot.SendVideoNoteAsync(-1001196454585, cur_messeg.Message.VideoNote.FileId);
                                    Bot.SendTextMessageAsync(cur_messeg.Message.From.Id, "Твой колобок уже всех поразил!");
                                }

                            }
                            last_messeg_id = cur_messeg.Id;
                        }
                       
                       
                        
                       
                        
                    }

                }
                Thread.Sleep(200);
            }
            

        }
        
      
    }
}
