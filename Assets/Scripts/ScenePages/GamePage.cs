using CarTest.Objects.Car;
using CarTest.Objects.Road;
using CarTest.Signals;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using CarTest.Values;

namespace CarTest.ScenePages
{
    public class GamePage : MonoBehaviour
    {
        [Inject]
        private GameManager gameManager;
        [Inject]
        private Car.CarFactory carFactory;
        [Inject]
        private Road.RoadFactory roadFactory;
        [Inject]
        private SignalBus signalBus;
        //Keeping list to destroy at scene end
        private List<Car> otherCars= new();
        private List<Road> roads = new();
        private Car playerCar;
        private void Start()
        {
            gameManager.CurrentScene=SceneType.Game_Scene;
            gameManager.RunCar = true;
            createRoadatStart();
            //Creating Player Car
            var playerCarSettings = new CarSettings
            {
                PlayerCar = true,
                CarNumber = gameManager.PlayerCarNumber,
                CarPosition = gameManager.PlayerCarPosition_GamePage,
                CarRotation = -1
            };
            playerCar = carFactory.Create(playerCarSettings);
            //Subscribing signals
            signalBus.Subscribe<GameOverSignal>(GameIsOver);
            signalBus.Subscribe<TimeSignal>(UpdateTime);
            signalBus.Subscribe<UIControlSignal>(CarControl);
            signalBus.Subscribe<SceneEndSignal>(SceneEnd);
        }
        private void GameIsOver(GameOverSignal gameOverSignal)
        {
            gameManager.RunCar = false;
            gameManager.GameOver = true;
        }
        private void createRoadatStart()
        {
            createRoad(11.9f);
            createRoad(5.9f);
            createRoad(-.1f);
            createRoad(-6.1f);
            createRoad(-12.1f);
            createRoad(-18.1f);
            
        }
        private void createRoad(float x)
        {
            var roadsettings = new RoadSetting
            {
                position = new(x, 0, 0)
            };
            roads.Add(roadFactory.Create(roadsettings));
        }

        private void UpdateTime(TimeSignal timeSignal)
        {
            var CurrentTime = timeSignal.time;
            //Spawning other cars
            if (CurrentTime > gameManager.OtherCarSpawningTime)
            {
                var otherCarSettings = new CarSettings
                {
                    PlayerCar = false,
                    CarNumber = gameManager.RandomNumberProvider.Next(playerCar.GetCarModelLength()),
                    CarPosition = new(gameManager.OtherCarStarttingPoint.x,gameManager.OtherCarStarttingPoint.y,gameManager.OtherCarStarttingPoint.z+gameManager.LanLength*(float)gameManager.RandomNumberProvider.NextDouble()),
                    CarRotation = 1
                };
                otherCars.Add(carFactory.Create(otherCarSettings));
                gameManager.OtherCarSpawningTime += gameManager.OtherCarSpawnTimeDifference;
            }
            
            //Spawning road to look like player car is moving
            if (CurrentTime > gameManager.RoadCreationDelay)
            {
                createRoad(-18);
                gameManager.RoadCreationDelay += gameManager.RoadCreationTimeDelay;
            }
            
        }
        private void CarControl(UIControlSignal signal)
        {
            if (signal.value == 1)
            {
                playerCar.MoveCarRight();
            }else if (signal.value == -1)
            {
                playerCar.MoveCarLeft();
            }else if (signal.value == 0)
            {
                playerCar.CarStraight();
            }
        }
        private void SceneEnd()
        {
            //Unsubscribing
            signalBus.Unsubscribe<GameOverSignal>(GameIsOver);
            signalBus.Unsubscribe<TimeSignal>(UpdateTime);
            signalBus.Unsubscribe<UIControlSignal>(CarControl);
            signalBus.Unsubscribe<SceneEndSignal>(SceneEnd);

            //Destroying all objects
            Object.Destroy(playerCar);
            foreach (var otherCar in otherCars)
            {
                Object.DestroyImmediate(otherCar);
            }
            otherCars.Clear();
            foreach(Road objectRoad in roads)
            {
                Object.DestroyImmediate(objectRoad);
            }
            roads.Clear();
            gameManager.ResetGameControlVariables();
        }
    }
}
