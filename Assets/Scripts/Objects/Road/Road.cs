using Zenject;
using UnityEngine;

namespace CarTest.Objects.Road
{
    public class Road : MonoBehaviour
    {
        private bool modelDestroyed = false;
        private RoadModel roadModel;
        private GameManager gameManager;

        [Inject]
        public void Initialize(RoadModel _roadModel,RoadSetting setting,GameManager _ganeManager)
        {
            roadModel = _roadModel;
            roadModel.road.transform.position = setting.position;
            roadModel.road.SetActive(true);

            gameManager = _ganeManager;
        }
        private void FixedUpdate()
        {
            if (!modelDestroyed)
            {
                if (!gameManager.GameOver)
                {
                    if (gameManager.RunCar)
                    {
                        roadModel.road.transform.position = new(roadModel.road.transform.position.x + gameManager.RoadMoveSpeed * Time.deltaTime, 0, 0);
                    }
                }
                if(roadModel.road.transform.position.x > 18)
                {
                    Object.Destroy(roadModel);
                    modelDestroyed = true;
                }
            }
        }
        private void OnDestroy()
        {
            if (!modelDestroyed)
            {
                Object.Destroy(roadModel);
            }
        }
        public class RoadFactory : PlaceholderFactory<RoadSetting, Road>
        {

        }
    }
}
