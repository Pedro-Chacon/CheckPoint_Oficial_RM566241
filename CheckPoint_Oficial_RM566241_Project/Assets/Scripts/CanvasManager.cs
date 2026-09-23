using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI textCoins;
    public int coins;
    public int maxCoins;
 
    void Start()
    {
        coins = 0;
    }

    void Update()
    {
        if (coins >= maxCoins)
        {
            textCoins.text = "Congratulations!";
        }
        else
        {
            textCoins.text = "Total Coins: " + coins;
        }
    }
}
