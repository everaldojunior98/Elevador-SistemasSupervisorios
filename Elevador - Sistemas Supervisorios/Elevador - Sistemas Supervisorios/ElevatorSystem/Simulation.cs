using System.Threading;

namespace ElevadorSistemasSupervisorios.ElevatorSystem
{
    public class Simulation
    {
        //Tempo para passar gerar um novo andar
        private const int TIME_BETWEEN_GENERATION = 3000;

        private readonly Thread simulationThread;
        private readonly CancellationTokenSource cancellationToken;

        public Simulation()
        {
            cancellationToken = new CancellationTokenSource();
            simulationThread = new Thread(SimulationLoop);
            simulationThread.Start();
        }

        public void Dispose()
        {
            cancellationToken.Cancel();
            simulationThread.Abort();
            simulationThread.Join();
        }

        private void SimulationLoop()
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Thread.Sleep(TIME_BETWEEN_GENERATION);
            }
        }
    }
}