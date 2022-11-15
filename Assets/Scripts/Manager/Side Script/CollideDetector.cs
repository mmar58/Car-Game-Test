using UnityEngine;

public class CollideDetector : MonoBehaviour
{
    //For detecting car collisons
    private void OnCollisionEnter(Collision collision)
    {
        //Setting game over
        GameManager.GameOver = true;
        GameManager.RunCar = false;
    }
}
