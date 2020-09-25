using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int coinsCount;

    public Text coinsCountText;

    public static Inventory instance;

    /* Mettre en place le design pattern singleton */
    public void Awake()
    {
        if (instance)
            Debug.LogWarning("Déjà un inventaire");

        instance = this;
    }

    public void AddCoins(int count)
    {
        coinsCount += count;
        UpdateTextUI();
    }

    public void RemoveCoints(int count)
    {
        coinsCount -= count;
        UpdateTextUI();
    }

    public void UpdateTextUI()
    {
        coinsCountText.text = coinsCount.ToString();
    }
}
