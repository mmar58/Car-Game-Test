using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class GamePage : MonoBehaviour
{
    //Ui
    public Text scoreText;
    public GameObject restartUI;
    public Text restartUIScore;
    //Effects
    public GameObject Explotion;
    //Our car collection
    [Inject]
    GameManager gameManager;

    [Inject]
    CarManager carManager;

    private void Start()
    {
        gameManager.CurrentScene = SceneType.Game_Scene;
        carManager.ShowPlayerCar();
    }

    private void FixedUpdate()
    {
        carManager.Update();
        scoreText.text = gameManager.CurrentTime.ToString("0.00");
        if (gameManager.ShowRestartUi)
        {
            //Show Restart UI
            restartUI.SetActive(true);
            restartUIScore.text= scoreText.text = gameManager.CurrentTime.ToString("0.00") + " s";

            //Move the explotion to player car and show
            Explotion.transform.position = carManager.PlayerCarPostion;
            Explotion.SetActive(true);
        }
    }
    public void MovePlayerCar(InputAction.CallbackContext value)
    {
        carManager.controlCar(value.ReadValue<float>());
    }
    
  
    public void LoadStartScene()
    {
        //Loading the start screen
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        gameManager.ResetGameControlVariables();
    }

}
