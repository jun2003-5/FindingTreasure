using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class SpecialStoreData
{
    public string abilityName;
    public int diamondCost;

    public bool isBought;

    public bool isBuyable;

    public Text diamondText;
    public Text abilityText; 
    public TextMeshProUGUI costText;

    public GameObject itemCover;
}
