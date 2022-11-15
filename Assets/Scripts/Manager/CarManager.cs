using System.Collections.Generic;
using UnityEngine;

public class CarManager
{
    private GameObject[] _cars;
    private GameObject _road;
    private List<GameObject> _otherCarsList;
    private GameManager _gameManager;
    private Transform _lastPlayerCarPosition;//saving last car position and rotation
    private Quaternion _carRotation= Quaternion.identity;
    private GameObject _playerCar;
   public CarManager(GameObject[] cars, GameManager gameManager,GameObject road)
    {
        _cars = cars;
        _gameManager = gameManager;
        _road = road;
    }
    public void ShowPlayerCar()
    {
        if (_gameManager.CurrentScene == SceneType.Start_Scene)
        {
            _cars[_gameManager.PlayerCarNumber].transform.position=_gameManager.PlayerCarPosition_StartPage;
            _cars[_gameManager.PlayerCarNumber].SetActive(true);

            //clearing other car list
            _otherCarsList = new();
        }
        else
        {
            _cars [_gameManager.PlayerCarNumber].SetActive(false);
            _playerCar = Factory.Create(_cars[_gameManager.PlayerCarNumber]);
            _playerCar.transform.position = _gameManager.PlayerCarPosition_GamePage;
            _carRotation.y = _gameManager.CarStraightRotation;
            _playerCar.transform.rotation = _carRotation;
            _playerCar.SetActive(true);

            //construct road
            Factory.CreateStartRoad(_road);
        }
    }
    public void NextCar()
    {
        _cars[_gameManager.PlayerCarNumber].SetActive(false);
        _lastPlayerCarPosition = _cars[_gameManager.PlayerCarNumber].transform;
        _gameManager.PlayerCarNumber= (_gameManager.PlayerCarNumber + 1) % _gameManager.PlayerCarChoiceLimit;
        SetCarPositionToNewCar();
    }
    public void PreviusCar()
    {
        _cars[_gameManager.PlayerCarNumber].SetActive(false);
        _lastPlayerCarPosition = _cars[_gameManager.PlayerCarNumber].transform;
        _gameManager.PlayerCarNumber--;
        if (_gameManager.PlayerCarNumber < 0)
        {
            _gameManager.PlayerCarNumber += _gameManager.PlayerCarChoiceLimit;
        }
        SetCarPositionToNewCar();
    }

    private void SetCarPositionToNewCar()
    {
        //Setting position and rotation to the new car
        _cars[_gameManager.PlayerCarNumber].transform.position = _lastPlayerCarPosition.position;
        _cars[_gameManager.PlayerCarNumber].transform.rotation = _lastPlayerCarPosition.rotation;
        _cars[_gameManager.PlayerCarNumber].SetActive(true);
    }
    public void Update()
    {
        if (_gameManager.CurrentScene == SceneType.Start_Scene)
        {
            _cars[_gameManager.PlayerCarNumber].transform.Rotate(0, Time.deltaTime * _gameManager.PlayerCarRotationSpeed, 0);
        }
        else
        {
            if (!GameManager.GameOver)
            {
                _gameManager.CurrentTime += Time.deltaTime;

                //Moving Player Car
                if (_gameManager.CarMoveLeft)
                {
                    Moveleft();
                }
                else if (_gameManager.CarMoveRight)
                {
                    Moveright();
                }

                //Spawning Other Cars
                if (_gameManager.CurrentTime > _gameManager.GetOtherCarSpawningTime)
                {
                    SpawnOtherCars();
                    _gameManager.GetOtherCarSpawningTime += _gameManager.GetOtherCarSpawnTimeDifference;
                }

                //Moving other cars

                for (int i = 0; i < _otherCarsList.Count; i++)
                {
                    if (_otherCarsList[i].transform.position.x > 18)
                    {
                        //The car is out of display
                        Factory.Destroy(_otherCarsList[i]);
                        _otherCarsList.Remove(_otherCarsList[i]);
                    }
                    else
                    {
                        //The car is on track
                        _otherCarsList[i].transform.position = new Vector3(_otherCarsList[i].transform.position.x + _gameManager.GetPlayerCarRunSpeed * Time.deltaTime, _otherCarsList[i].transform.position.y, _otherCarsList[i].transform.position.z);
                    }
                }
                //Creatng roads for showing movement
                if (Time.time > _gameManager.RoadCreationDelay)
                {
                    _gameManager.RoadCreationDelay += _gameManager.RoadCreationTimeDelay;
                    GameObject curRoad = Factory.Create(_road,_gameManager.RoadInitialPosition);
                    curRoad.SetActive(true);
                }
            }
            else
            {
                _gameManager.ShowRestartUi = true;
            }
        }
    }
    public void SpawnOtherCars()
    {
        //Setting properties for curSpawned
        Vector3 curSpawnedPosition = new Vector3(_gameManager.OtherCarStarttingPoint.x, _gameManager.OtherCarStarttingPoint.y, _gameManager.OtherCarStarttingPoint.z + _gameManager.GetLanLength * (float)_gameManager.RandomNumberProvider.NextDouble());
        _carRotation.y = 1;

        //Spawning curSpawned
        GameObject curSpawned = Factory.Create(_cars[_gameManager.RandomNumberProvider.Next(_cars.Length)], curSpawnedPosition, _carRotation);
        curSpawned.SetActive(true);
        _otherCarsList.Add(curSpawned);
    }
    public Vector3 PlayerCarPostion
    {
        get { return _playerCar.transform.position; }
    }
    public void controlCar(float value)
    {
        if (value < 0)
        {
            _gameManager.CarMoveLeft = true;
        }
        else if (value > 0)
        {
            _gameManager.CarMoveRight = true;
        }
        else
        {
            //Clearing player car move bools
            _gameManager.CarMoveLeft = false;
            _gameManager.CarMoveRight = false;
            //making the car body straight again
            _carRotation.y = _gameManager.CarStraightRotation;
            _playerCar.transform.rotation = _carRotation;
        }
    }
    private void Moveleft()
    {
        if (_playerCar.transform.position.z > _gameManager.LanLeftLimit)
        {
            _carRotation.y = _gameManager.CarLeftRotation;
            _playerCar.transform.rotation = _carRotation;
            _playerCar.transform.position = new Vector3(_playerCar.transform.position.x, _playerCar.transform.position.y, _playerCar.transform.position.z - _gameManager.GetPlayerCarMoveSpeed);

        }
    }

    private void Moveright()
    {
        if (_playerCar.transform.position.z < _gameManager.LanRightLimit)
        {
            _carRotation.y = _gameManager.CarRightRotation;
            _playerCar.transform.SetPositionAndRotation(new Vector3(_playerCar.transform.position.x, _playerCar.transform.position.y, _playerCar.transform.position.z + _gameManager.GetPlayerCarMoveSpeed), _carRotation);

        }
    }

}
