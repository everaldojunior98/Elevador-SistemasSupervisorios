using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using static ElevadorSistemasSupervisorios.ElevatorSystem.ElevatorUtils;

namespace ElevadorSistemasSupervisorios.ElevatorSystem
{
    public class Elevator
    {
        //Tempo para passar de um andar para o outro em ms
        private const int TIME_BETWEEN_FLOORS = 1000;
        //Tempo parado no andar em ms
        private const int TIME_STOPPED_ON_FLOOR = 5000;

        public delegate void OnFloorChangedEventHandler(object source, OnFloorChangedEventArgs args);
        public event OnFloorChangedEventHandler OnFloorChanged;

        private readonly Thread elevatorThread;
        private readonly CancellationTokenSource cancellationToken;

        private ElevatorDirection currentDirection;
        private int currentFloor;

        private bool changedRoute;

        private readonly Queue<int> route;
        private readonly List<int> internalFloors;
        private readonly Dictionary<int, ElevatorDirection> externalFloors;

        public Elevator()
        {
            route = new Queue<int>();
            internalFloors = new List<int>();

            externalFloors = new Dictionary<int, ElevatorDirection>();
            
            currentDirection = ElevatorDirection.UP;
            currentFloor = 1;

            cancellationToken = new CancellationTokenSource();
            elevatorThread = new Thread(ElevatorLoop);
            elevatorThread.Start();
        }

        public void RequestFloor(int floor, ElevatorDirection direction)
        {
            if (!externalFloors.ContainsKey(floor) && floor != currentFloor)
                externalFloors.Add(floor, direction);
        }

        public void RequestFloor(int floor)
        {
            if (!internalFloors.Contains(floor) && floor != currentFloor)
            {
                internalFloors.Add(floor);
                changedRoute = true;
            }
        }

        public void Dispose()
        {
            cancellationToken.Cancel();
            elevatorThread.Abort();
            elevatorThread.Join();
        }

        //Calculo de rota baseado no algoritmo de escalonamento de disco SCAN
        private void CalculateRoute()
        {
            route.Clear();
            var below = new List<int>();
            var above = new List<int>();
            var direction = currentDirection;

            foreach (var floor in internalFloors)
                if (floor < currentFloor)
                    below.Add(floor);
                else if (floor > currentFloor)
                    above.Add(floor);

            below.Sort();
            above.Sort();

            //Roda o loop 2 vezes, uma para os items acima e outro para os items abaixo
            for (var i = 0; i < 2; i++)
            {
                if (direction == ElevatorDirection.DOWN)
                {
                    for (var j = below.Count - 1; j >= 0; j--)
                        route.Enqueue(below[j]);
                    direction = ElevatorDirection.UP;
                }
                else if (direction == ElevatorDirection.UP)
                {
                    for (var j = 0; j < above.Count; j++)
                        route.Enqueue(above[j]);
                    direction = ElevatorDirection.DOWN;
                }
            }
        }

        private void ElevatorLoop()
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Thread.Sleep(10);

                if (changedRoute)
                {
                    changedRoute = false;
                    CalculateRoute();
                }

                if(route.Count == 0)
                {
                    foreach (var pair in externalFloors)
                        RequestFloor(pair.Key);
                    externalFloors.Clear();

                    continue;
                }

                var floor = route.Dequeue();
                var increase = floor > currentFloor;
                currentDirection = increase ? ElevatorDirection.UP : ElevatorDirection.DOWN;

                while (currentFloor != floor)
                {
                    if (increase)
                        currentFloor++;
                    else
                        currentFloor--;
                    
                    Thread.Sleep(TIME_BETWEEN_FLOORS);

                    OnFloorChanged?.Invoke(this,
                        new OnFloorChangedEventArgs
                        {
                            Floor = currentFloor, Direction = currentDirection
                        });

                    if (externalFloors.ContainsKey(currentFloor) && externalFloors[currentFloor] == currentDirection)
                    {
                        externalFloors.Remove(currentFloor);
                        Thread.Sleep(TIME_STOPPED_ON_FLOOR);
                    }

                    if (changedRoute)
                        break;
                }

                if (changedRoute)
                    continue;
                
                internalFloors.Remove(floor);
                Thread.Sleep(TIME_STOPPED_ON_FLOOR);
            }
        }
    }
}