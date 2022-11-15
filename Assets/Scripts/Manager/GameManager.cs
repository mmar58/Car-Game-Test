using UnityEngine;

public class GameManager
{
    //It uses for starting car and bridge movement after setupig all, so that looks like player car is moving
    public static bool RunCar=false;
    //It uses for indicating gameover or not
    public static bool GameOver = false;
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
    readonly private float[] moveSpeed = { .033f, .05f, .07f };
    readonly private float[] runningSpeed = { 8, 12, 15 };
    readonly private string[] info = { "Slow running and control speed", "Normal running and control speed", "Fast running and control speed" };

    //lane or road properties
    readonly private float lanLeftLimit = -3.04f;
    readonly private float lanRightLimit = 3.04f;
    //It will be calculated from upper two at start
    private float lanLenght;
    private float roadCreationDelay = 1f;//after this time(in seconds) road will be created
    private readonly float roadCreationTimeDelay = 1f;//this is the interval time(in seconds) between road creation

    //control other car spawning time
    private float otherCarSpawningTime = 1;
    readonly private float otherCarSpawnTimeDifference = 1;

    //Car Movement Boolean
    private bool carMoveLeft = false,
                 carMoveRight = false;

    //Game time
    private float currentTIme = 0;

    //UI
    private bool showRestartUi = false;
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
    public float GetPlayerCarMoveSpeed
    {
        get { return moveSpeed[playerCarNumber]; }
    }
    public float GetPlayerCarRunSpeed
    {
        get { return runningSpeed[playerCarNumber]; }
    }
    public string GetPlayerCarInfo
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
    public float GetLanLength
    {
        get { return lanLenght; }
    }
    public float GetOtherCarSpawningTime
    {
        get { return otherCarSpawningTime; }
        set { otherCarSpawningTime = value; }
    }
    public float GetOtherCarSpawnTimeDifference
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
    public bool CarMoveLeft
    {
        get { return carMoveLeft; }
        set { carMoveLeft = value; }
    }
    public bool CarMoveRight
    {
        get { return carMoveRight; }
        set { carMoveRight = value; }
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
    public bool ShowRestartUi
    {
        get { return showRestartUi; }
        set { showRestartUi = value; }
    }
    public void ResetGameControlVariables()
    {
        GameOver = false;
        RunCar = false;
        showRestartUi = false;
    }
}
