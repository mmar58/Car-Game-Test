using CarTest.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace CarTest.Ui
{
    public class StartPageUI : MonoBehaviour
    {
        [SerializeField]
        private Text Description;
        [Inject]
        private SignalBus signalBus;
        [Inject]
        private GameManager gameManager;
        private void Start()
        {
            SetDescription();
        }
        public void Next()
        {
            signalBus.Fire(new UIControlSignal() { value=1});
            SetDescription();
        }
        public void Previous()
        {
            signalBus.Fire(new UIControlSignal() { value=-1 });
            SetDescription();
        }
        private void SetDescription()
        {
            Description.text = gameManager.PlayerCarInfo;
        }
        public void StartNextScene()
        {
            signalBus.Fire(new SceneEndSignal());
            SceneManager.LoadScene(1,LoadSceneMode.Single);
        }
    }
}
