using UnityEngine;


public class CharacterCoinCollector : MonoBehaviour
{
    public GameManager gameManager;

    void Start()
    {
        if (gameManager == null)
            gameManager = FindFirstObjectByType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Coin"))
            return;

        if (gameManager != null)
            gameManager.CollectCoin(other.gameObject);
    }
}