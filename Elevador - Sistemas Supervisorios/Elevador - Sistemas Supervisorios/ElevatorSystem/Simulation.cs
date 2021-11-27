using System;
using System.Diagnostics;
using System.Threading;
using static ElevadorSistemasSupervisorios.ElevatorSystem.ElevatorUtils;

namespace ElevadorSistemasSupervisorios.ElevatorSystem
{
    public class Simulation
    {
        //Tempo para passar gerar um novo andar
        private const int TIME_BETWEEN_GENERATION = 10000;
        private const int MIN_FLOOR = 1;
        private const int MAX_FLOOR = 10;

        private readonly Thread simulationThread;
        private readonly CancellationTokenSource cancellationToken;

        private readonly Elevator currentElevator;
        private bool enable;

        public Simulation(Elevator elevator)
        {
            currentElevator = elevator;

            cancellationToken = new CancellationTokenSource();
            simulationThread = new Thread(SimulationLoop);
            simulationThread.Start();
        }

        public void Enable()
        {
            enable = true;
        }

        public void Disable()
        {
            enable = false;
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
                if (enable)
                {
                    var random = new Random();
                    var floor = random.Next(MIN_FLOOR, MAX_FLOOR + 1);

                    var randomDirection = random.Next(0, 2);

                    ElevatorDirection direction;
                    switch (randomDirection)
                    {
                        case 0:
                            direction = ElevatorDirection.UP;
                            break;
                        case 1:
                            direction = ElevatorDirection.DOWN;
                            break;
                        default:
                            direction = ElevatorDirection.UP;
                            break;
                    }

                    currentElevator.RequestFloor(floor, direction);
                    Thread.Sleep(TIME_BETWEEN_GENERATION);
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}