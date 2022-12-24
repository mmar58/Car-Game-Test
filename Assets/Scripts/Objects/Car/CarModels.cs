using UnityEngine;
namespace CarTest.Objects.Car
{
    public class CarModels:MonoBehaviour
    {
        public GameObject[] carlist;
        private void OnDestroy()
        {
           for (int i = 0; i < carlist.Length; i++)
            {
                GameObject.Destroy(carlist[i]);
            }
           carlist = null;
        }

    }
}
