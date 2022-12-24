using System.Numerics;
using Zenject;
namespace CarTest.Objects.Road
{
    public class RoadInstaller : MonoInstaller
    {
        [Inject(Optional = true)]
        private RoadSetting setting;
        public override void InstallBindings()
        {
            Container.BindInstance(setting).AsSingle();
        }
    }
}
