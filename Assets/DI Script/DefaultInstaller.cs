using UnityEngine;
using Zenject;

public class DefaultInstaller : MonoInstaller
{
    //Total Car Objects
    public GameObject[] cars;
    public GameObject road;
    public override void InstallBindings()
    {
        //Binding assets
        Container.Bind<GameObject[]>().FromInstance(cars).AsSingle();
        Container.Bind<GameObject>().FromInstance(road).AsSingle();
        //Bindng Manager
        Container.Bind<GameManager>().AsSingle();
        Container.Bind<CarManager>().AsSingle();
    }
}

