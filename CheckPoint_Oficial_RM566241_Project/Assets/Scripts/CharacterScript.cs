using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public CanvasManager canvasManager;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            canvasManager.coins++;

        }
    }
}
