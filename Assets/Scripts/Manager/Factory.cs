using UnityEngine;

public class Factory : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject Create(GameObject carObject)
    {
        return Instantiate(carObject);
    }
    public static GameObject Create(GameObject road, Vector3 position)
    {
        return Instantiate(road, position, Quaternion.identity);
    }
    public static GameObject Create(GameObject carObject, Vector3 postion, Quaternion rotation)
    {
        return Instantiate(carObject, postion, rotation);
    }
    public static void CreateStartRoad(GameObject road)
    {
        Instantiate(road, new Vector3(12, 0, 0), Quaternion.identity).SetActive(true);
        Instantiate(road, new Vector3(6, 0, 0), Quaternion.identity).SetActive(true);
        Instantiate(road, new Vector3(0, 0, 0), Quaternion.identity).SetActive(true);
        Instantiate(road, new Vector3(-6, 0, 0), Quaternion.identity).SetActive(true);
        Instantiate(road, new Vector3(-12, 0, 0), Quaternion.identity).SetActive(true);
        Instantiate(road, new Vector3(-18, 0, 0), Quaternion.identity).SetActive(true);

        //starting road movement
        GameManager.RunCar = true;
    }

}
