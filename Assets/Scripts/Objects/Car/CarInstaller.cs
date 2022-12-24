using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CarTest.Objects.Car
{
    public class CarInstaller : MonoInstaller
    {
        // Start is called before the first frame update
        [Inject(Optional = true)]
        protected CarSettings carSettings;
        public override void InstallBindings()
        {
            Container.BindInstance(carSettings).AsSingle();
        }
    }
}
