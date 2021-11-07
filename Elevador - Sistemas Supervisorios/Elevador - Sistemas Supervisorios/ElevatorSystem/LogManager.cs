using System;
using System.IO;

namespace ElevadorSistemasSupervisorios.ElevatorSystem
{
    public class LogManager
    {
        public void Log(int floor)
        {
            var logFile = $"Log-{DateTime.Now:dd-MM-yyyy}.txt";

            if (!File.Exists(logFile))
            {
                using (var sw = File.CreateText(logFile))
                {
                    sw.WriteLine($"{DateTime.Now:HH:mm:ss}: {floor}");
                }
            }
            else
            {
                using (var sw = File.AppendText(logFile))
                {
                    sw.WriteLine($"{DateTime.Now:HH:mm:ss}: {floor}");
                }
            }
        }
    }
}
