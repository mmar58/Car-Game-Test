using CarTest.Values;
using UnityEngine;

public class GameManager
{
    //It uses for starting car and bridge movement after setupig all, so that looks like player car is moving
    public bool RunCar=false;
    //It uses for indicating gameover or not
    public bool GameOver = false;
    //Stores current scene number
    private SceneType sceneType;
    //Player Car Positions
    private Vector3 playerCarPosition_StartPage = new(0f, 0.08100128f, 0f),
                    playerCarPosition_GamePage = new(12.5f, 0.08100128f, 0f),
    //Other Starting Positions
                    otherCarsStartingPoint = new(-15, 0.115f, -3.04f),
                    roadInitialPosition = new(-18, 0, 0);
    private int playerCarNumber = 0;
    readonly private float playerCarRotationSpeed = 50;
    //Car rotation
    private readonly float carLeftRotation = -1.2f;//Rotation while car is moving
    private readonly float carRightRotation = -0.8f;
    private readonly float carStraightRotation = -1f;
    //Increase below properties for giving player more car choice but don't increase more than total cars in Resources/ProjectContext
    readonly private int playerCarChoiceLimit = 3;
    readonly private float[] moveSpeed = { 3, 5, 7 };
    readonly private float[] runningSpeed = { 8, 12, 15 };
    readonly private string[] info = { "Slow running and control speed", "Normal running and control speed", "Fast running and control speed" };

    //lane or road properties
    readonly private float lanLeftLimit = -3.04f;
    readonly private float lanRightLimit = 3.04f;
    readonly private float roadMoveSpeed = 6;
    //It will be calculated from upper two at start
    private float lanLenght;
    private float roadCreationDelay = 1f;//after this time(in seconds) road will be created
    private readonly float roadCreationTimeDelay = 1f;//this is the interval time(in seconds) between road creation

    //control other car spawning time
    private float otherCarSpawningTime = 1;
    readonly private float otherCarSpawnTimeDifference = 1;

    //Game time
    private float currentTIme = 0;

    public GameManager()
    {
        lanLenght=lanRightLimit-lanLeftLimit;
        
    }
    
    public System.Random RandomNumberProvider = new();

    public SceneType CurrentScene
    {
        get { return sceneType; }
        set { sceneType = value; }
    }

    public Vector3 PlayerCarPosition_StartPage
    {
        get { return playerCarPosition_StartPage; }
    }
    public Vector3 PlayerCarPosition_GamePage
    {
        get { return playerCarPosition_GamePage; }
    }
    public Vector3 OtherCarStarttingPoint
    {
        get { return otherCarsStartingPoint; }
    }
    public Vector3 RoadInitialPosition
    {
        get { return roadInitialPosition; }
    }
    public int PlayerCarNumber
    {
        get { return playerCarNumber; }
        set { playerCarNumber = value; }
    }
    public int PlayerCarChoiceLimit
    {
        get { return playerCarChoiceLimit; }
    }
    public float PlayerCarMoveSpeed
    {
        get { return moveSpeed[playerCarNumber]; }
    }
    public float PlayerCarRunSpeed
    {
        get { return runningSpeed[playerCarNumber]; }
    }
    public string PlayerCarInfo
    {
        get { return info[playerCarNumber]; }
    }
    public float PlayerCarRotationSpeed
    {
        get { return playerCarRotationSpeed; }
    }
    public float LanLeftLimit
    {
        get { return lanLeftLimit; }
    }
    public float LanRightLimit
    {
        get { return lanRightLimit; }
    }
    public float LanLength
    {
        get { return lanLenght; }
    }
    public float OtherCarSpawningTime
    {
        get { return otherCarSpawningTime; }
        set { otherCarSpawningTime = value; }
    }
    public float OtherCarSpawnTimeDifference
    {
        get { return otherCarSpawnTimeDifference; }
    }
    public float CurrentTime
    {
        get { return currentTIme; }
        set { currentTIme = value; }
    }
    public float CarStraightRotation
    {
        get { return carStraightRotation; }
    }
    public float CarLeftRotation
    {
        get {return carLeftRotation; }
    }
    public float CarRightRotation
    {
        get { return carRightRotation; }
    }

    public float RoadCreationDelay
    {
        get { return roadCreationDelay; }
        set { roadCreationDelay = value; }  
    }
    public float RoadCreationTimeDelay
    {
        get { return roadCreationTimeDelay; }
    }
    public float RoadMoveSpeed
    {
        get { return roadMoveSpeed; }
    }

    public void ResetGameControlVariables()
    {
        GameOver = false;
        RunCar = false;
        otherCarSpawningTime = 1;
        roadCreationDelay = 1;
    }
}
