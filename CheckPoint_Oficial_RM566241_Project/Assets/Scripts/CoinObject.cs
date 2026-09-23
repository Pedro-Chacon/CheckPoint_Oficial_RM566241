using UnityEngine;

public class CoinObject : MonoBehaviour
{
    [Tooltip("Velocidade do giro visual da moeda, em graus por segundo")]
    public float rotationSpeed = 90f;



    void Update()
    {

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}