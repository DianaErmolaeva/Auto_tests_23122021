using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ConsoleApp21
{
    class Program
    {
        public static string processName { get; private set; }
        public static string time { get; private set; }
        public static string range { get; private set; }
        public static string text { get; private set; }
 
        static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                processName = args[0];
                time = args[1];
                range = args[2];
            }

            int rangeInteger;
            //преобразуем строковое значение range в числовое
            bool isCorrectInteger = int.TryParse(range, out rangeInteger);
            if (isCorrectInteger)
            {
                //запускаем таймер с заданной частотой обновления range
                Timer t = new Timer(TimerCallback, null, 0, rangeInteger * 60000);
            }
            else
            {
                Console.WriteLine("Not a correct integer number");
            }
            Console.ReadLine();
        }

        private static void TimerCallback(Object o)
        {

            //открываем файл для записи
            using (StreamWriter text = new StreamWriter("log.txt", true))
            try
            {
                //цикл поиска запущенного процесса по имени
                foreach (var process in System.Diagnostics.Process.GetProcessesByName(processName))
                {
                    if ((DateTime.Now - process.StartTime).TotalMinutes > Convert.ToInt32(time))
                    {
                        //закрываем процесс
                        process.Kill();
                        //записываем лог
                        text.WriteLine("Process {0} found Time: {1}", processName, DateTime.Now);
                    }
                    else
                    {
                        //записываем лог
                        text.WriteLine("Old process {0} not found. Time: {1}", processName, DateTime.Now);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
}
    }
}

