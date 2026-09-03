using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MiniGameData
{
    public string gameName;

    public string displayingGameName;

    public float multiplier;
    public int costDiamond;
    public bool isGameBuyable;
    public bool isBought;
    public bool isSelected;

    public GameObject diamondCostText;
    public GameObject selectText;
    public Text Gameinformation;
    public Image selectButton;

    public int timesPlayed;
    public Text timesPlayedText;
     
    public int MaximumGame;
}
