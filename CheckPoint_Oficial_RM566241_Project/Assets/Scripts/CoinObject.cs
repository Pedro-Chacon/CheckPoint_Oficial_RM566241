using UnityEngine;

public class CoinObject : MonoBehaviour
{
    public int speed = 20;

    void Start()
    {
        
    }


    void Update()
    {
        transform.Rotate(360 * speed, 360 * speed, 0);
    }
}
