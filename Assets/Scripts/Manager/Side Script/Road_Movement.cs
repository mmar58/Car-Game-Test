using UnityEngine;

public class Road_Movement : MonoBehaviour
{
    readonly float roadMoveSpeed = 6;
    public GameObject road;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.RunCar)
        {
            transform.position = new Vector3(transform.position.x + Time.deltaTime * roadMoveSpeed, transform.position.y, transform.position.z);
            if (transform.position.x > 18)
            {
                Destroy(road);
            }
        }
    }
}
