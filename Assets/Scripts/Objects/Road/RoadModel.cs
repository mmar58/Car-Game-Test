using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTest.Objects.Road
{
    public class RoadModel : MonoBehaviour
    {
        public GameObject road;
        private void OnDestroy()
        {
            Object.Destroy(road);
        }
    }
}
    
