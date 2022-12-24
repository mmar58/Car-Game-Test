using UnityEngine;
using Zenject;
using CarTest.Values;
namespace CarTest.Objects.Car
{
    public class Car : MonoBehaviour
    {
        private bool modelDestroyed = false,
            //To keep car moving each side
                     moveCarLeft=false,moveCarRight=false;
        private CarSettings _settings;
        private CarModels _models;
        private GameManager _gameManager;
        private GameObject _currentCarModel;
        private Quaternion _rotation=Quaternion.identity;

        [Inject]
        public void Initialize(CarSettings settings, CarModels models,GameManager gameManager)
        {
            _settings = settings;
            _models = models;
            _gameManager = gameManager;
            showCurrentCar();
        }
        private void showCurrentCar()
        {
            //Setting Current Car Model
            _currentCarModel = _models.carlist[_settings.CarNumber];
            //Setting it's position and rotation
            _rotation.y = _settings.CarRotation;
            _currentCarModel.transform.position=_settings.CarPosition;
            _currentCarModel.transform.rotation = _rotation;
            //Let's make the car visible
            _currentCarModel.SetActive(true);
        }
        public void NextCar()
        {
            _gameManager.PlayerCarNumber = (_gameManager.PlayerCarNumber + 1) % _gameManager.PlayerCarChoiceLimit;
            SetNewCar();
            
        }
        public void PreviusCar()
        {
            _gameManager.PlayerCarNumber--;
            if (_gameManager.PlayerCarNumber < 0)
            {
                _gameManager.PlayerCarNumber += _gameManager.PlayerCarChoiceLimit;
            }
            SetNewCar();
        }
        public void MoveCarLeft()
        {
            _rotation.y = _gameManager.CarLeftRotation;
            _currentCarModel.transform.rotation = _rotation;
            moveCarLeft = true;
        }
        public void MoveCarRight()
        {
            _rotation.y = _gameManager.CarRightRotation;
            _currentCarModel.transform.rotation = _rotation;
            moveCarRight = true;
        }
        public void CarStraight()
        {
            _rotation.y = _gameManager.CarStraightRotation;
            _currentCarModel.transform.rotation = _rotation;
            moveCarRight = false;
            moveCarLeft = false;
        }
        public int GetCarModelLength()
        {
            return _models.carlist.Length;
        }
        private void SetNewCar()
        {
            _currentCarModel.SetActive(false);
            var lastPlayerCarPosition = _currentCarModel.transform;
            //Setting new car
            _currentCarModel = _models.carlist[_gameManager.PlayerCarNumber];
            _currentCarModel.transform.position = lastPlayerCarPosition.position;
            _currentCarModel.transform.rotation = lastPlayerCarPosition.rotation;
            _currentCarModel.SetActive(true);
        }
        private void FixedUpdate()
        {
            if (!modelDestroyed)
            {
                if (_gameManager.CurrentScene == SceneType.Start_Scene)
                {
                    if (_settings.PlayerCar)
                    {
                        _currentCarModel.transform.Rotate(0, 1f, 0);
                    }
                }
                else if (_gameManager.CurrentScene == SceneType.Game_Scene)
                {
                    if (!_gameManager.GameOver)
                    {
                        if (!_settings.PlayerCar)
                        {
                            _currentCarModel.transform.position = new(_currentCarModel.transform.position.x + _gameManager.PlayerCarRunSpeed * Time.deltaTime, _currentCarModel.transform.position.y, _currentCarModel.transform.position.z);
                            if (_currentCarModel.transform.position.x > 18)
                            {
                                Object.Destroy(_models);
                                modelDestroyed = true;
                            }
                        }
                        else
                        {
                            if (moveCarLeft)
                            {
                                if (_currentCarModel.transform.position.z > _gameManager.LanLeftLimit)
                                {
                                    _currentCarModel.transform.position = new(_currentCarModel.transform.position.x, _currentCarModel.transform.position.y, _currentCarModel.transform.position.z - _gameManager.PlayerCarMoveSpeed * Time.deltaTime);
                                }
                            }
                            else if (moveCarRight)
                            {
                                if (_currentCarModel.transform.position.z < _gameManager.LanRightLimit)
                                {
                                    _currentCarModel.transform.position = new(_currentCarModel.transform.position.x, _currentCarModel.transform.position.y, _currentCarModel.transform.position.z + _gameManager.PlayerCarMoveSpeed * Time.deltaTime);
                                }
                            }
                        }
                    }
                }
            }
            
        }
        private void OnDestroy()
        {
            if (!modelDestroyed)
            {
                Object.Destroy(_models);
            }
            
        }
        public class CarFactory : PlaceholderFactory<CarSettings, Car>
        {

        }
    }

    
}
