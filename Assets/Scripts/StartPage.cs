using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class StartPage : MonoBehaviour
{

    //UI
    public Text description;//Description Text

	[Inject]
	CarManager carManager;
	[Inject]
	GameManager gameManager;

    private void Start()
	{
		gameManager.CurrentScene = SceneType.Start_Scene;
		carManager.ShowPlayerCar();
		
	}
	public void NextCar()
	{
		carManager.NextCar();
		SetCarDescription();
	}

	public void PreviousCar()
	{
        carManager.PreviusCar();
		SetCarDescription();
    }
	private void SetCarDescription()
	{
        //Setting new car description
        description.text = gameManager.GetPlayerCarInfo;
    }
	public void StartGame()
	{
		//Loading game scene
		SceneManager.LoadScene(1, LoadSceneMode.Single);
	}
	public void FixedUpdate()
	{
		carManager.Update();
	}
	public void Exit()
	{
		Application.Quit();
	}
}
