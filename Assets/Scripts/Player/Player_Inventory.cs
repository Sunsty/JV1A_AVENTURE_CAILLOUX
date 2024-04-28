using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public int coinAmount;

    public TextMeshProUGUI coinDisplay;

    void Update()
    {
        coinDisplay.SetText(string.Format("{0} Coins", coinAmount));
    }

    public void AddCoin(int value)
    {
        coinAmount += value;
    }
}
