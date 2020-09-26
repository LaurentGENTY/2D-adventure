using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class Inventory : MonoBehaviour
{
    public int coinsCount;

    public Text coinsCountText;

    /* Stocker les objets dans une liste d'Item (scriptable object) */
    public List<Item> content = new List<Item>();
    private int contentCurrentItem = 0;

    public static Inventory instance;

    /* reference au button */
    public Button consumeItemButton;
    public Button nextItemButton;
    public Button previousItemButton;

    /* Mettre en place le design pattern singleton */
    public void Awake()
    {
        if (instance)
            Debug.LogWarning("Déjà un inventaire");

        instance = this;
    }

    public void Update()
    {
        if (content.Count == 0)
        {
            consumeItemButton.interactable = false;
            nextItemButton.interactable = false;
            previousItemButton.interactable = false;
        }
    }

    public void GetNextItem()
    {
        if (contentCurrentItem + 1 > content.Count - 1)
            contentCurrentItem = 0;
        else
            contentCurrentItem = contentCurrentItem + 1;
    }

    public void GetPreviousItem()
    {
        if (contentCurrentItem - 1 < 0)
            contentCurrentItem = content.Count - 1;
        else
            contentCurrentItem = contentCurrentItem - 1;
    }

    public void ConsumeItem()
    {
        Item currentItem = content[0];
        PlayerHealth.instance.HealPlayer(currentItem.hpGiven);
        PlayerMouvement.instance.moveSpeed += currentItem.speedGiven;

        content.Remove(currentItem);

        GetNextItem();
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
