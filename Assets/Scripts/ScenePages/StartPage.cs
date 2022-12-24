using UnityEngine;
using Zenject;
using CarTest.Objects.Car;
using CarTest.Signals;
using CarTest.Values;
namespace CarTest.ScenePages
{
    public class StartPage : MonoBehaviour
    {
        private Car playerCar;
        [Inject]
        private GameManager gameManager;
        [Inject]
        private Car.CarFactory carFactory;
        [Inject]
        SignalBus signalBus;

        private void Start()
        {
            
            gameManager.CurrentScene = SceneType.Start_Scene;
            var playerCarSettings = new CarSettings
            {
                PlayerCar = true,
                CarNumber = gameManager.PlayerCarNumber,
                CarPosition = gameManager.PlayerCarPosition_StartPage,
                CarRotation = 1
            };
            playerCar = carFactory.Create(playerCarSettings);
            //Registering signals
            signalBus.Subscribe<UIControlSignal>(SelectCar);
            signalBus.Subscribe<SceneEndSignal>(SceneEnd);

        }
        private void SelectCar(UIControlSignal uiControlSignal)
        {
            if (uiControlSignal.value == 1)
            {
                playerCar.NextCar();
            }
            else if (uiControlSignal.value == -1)
            {
                playerCar.PreviusCar();
            }
        }
        private void SceneEnd()
        {
            //Unsubscribing
            signalBus.Unsubscribe<UIControlSignal>(SelectCar);
            signalBus.Unsubscribe<SceneEndSignal>(SceneEnd);
            //Destroy the scene car object
            Object.Destroy(playerCar);
            Debug.Log("Scene ended");
        }
    }
}

