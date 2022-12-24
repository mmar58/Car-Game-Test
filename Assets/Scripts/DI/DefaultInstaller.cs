using UnityEngine;
using Zenject;
using CarTest.Objects.Car;
using CarTest.Signals;
using CarTest.Objects.Road;

namespace CarTest.DI
{
    public class DefaultInstaller : MonoInstaller
    {
        //Total Car Objects
        //public GameObject[] cars;
        //public GameObject road;
        [SerializeField]
        private Car car;
        [SerializeField]
        private Road road;
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            //Signals
            Container.DeclareSignal<UIControlSignal>();
            Container.DeclareSignal<SceneEndSignal>();
            Container.DeclareSignal<GameOverSignal>();
            Container.DeclareSignal<TimeSignal>();
            //Binding Managers and Factories
            Container.Bind<GameManager>().AsSingle();
            Container.BindFactory<CarSettings, Car, Car.CarFactory>().FromSubContainerResolve().ByNewContextPrefab<CarInstaller>(car.gameObject);
            Container.BindFactory<RoadSetting, Road, Road.RoadFactory>().FromSubContainerResolve().ByNewContextPrefab<RoadInstaller>(road.gameObject);
        }
    }
}

