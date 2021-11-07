using System;
using System.IO;

namespace ElevadorSistemasSupervisorios.ElevatorSystem
{
    public class LogManager
    {
        public void Log(int floor)
        {
            string logFile = $"Log-{DateTime.Now.ToString("dd-MM-yyyy")}.txt";

            if (!File.Exists(logFile))
            {
                using (StreamWriter sw = File.CreateText(logFile))
                {
                    sw.WriteLine($"{DateTime.Now.ToString("hh:mm:ss")}: {floor}");
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(logFile))
                {
                    sw.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}: {floor}");
                }
            }
        }
    }
}
