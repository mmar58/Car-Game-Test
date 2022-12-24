
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using CarTest.Signals;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace CarTest.Ui
{
    public class GamePageUI : MonoBehaviour
    {
        private float timeScore = 0;
        [Inject]
        private SignalBus signalBus;
        [Inject]
        GameManager gameManager;

        [SerializeField]
        private Text scoreText;
        [SerializeField]
        private GameObject restartDialog;
        [SerializeField]
        private Text restartDialogScoreText;

        private void FixedUpdate()
        {
            if (!gameManager.GameOver)
            {
                    timeScore+=Time.deltaTime;
                    scoreText.text = timeScore.ToString("0.00") + " s";
                    signalBus.Fire(new TimeSignal() { time= timeScore });
                    
            }
            else
            {
                restartDialogScoreText.text= timeScore.ToString("0.00") + " s";
                restartDialog.SetActive(true);
            }
        }
        public void Move(InputAction.CallbackContext input)
        {
            signalBus.Fire(new UIControlSignal()
            {
                value = input.ReadValue<float>(),
            });
        }
        public void Restart()
        {
            signalBus.Fire(new SceneEndSignal());
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
