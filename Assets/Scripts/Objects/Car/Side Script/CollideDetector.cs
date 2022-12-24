using UnityEngine;
using Zenject;
using CarTest.Signals;
namespace CarTest.Objects.Car.SideScript
{
    public class CollideDetector : MonoBehaviour
    {
        [Inject]
        SignalBus signalBus;
        //For detecting car collisons
        private void OnCollisionEnter(Collision collision)
        {
            signalBus.Fire(new GameOverSignal());

        }
    }
}
