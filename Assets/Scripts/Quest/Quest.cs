using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public bool isCompleted;
    public bool isSpecial;

    public string Title;
    public long goldreward;
    public int diamondreward;

    public Text titleText;
    public Text goldrewardText;
    public Text diamonrewardText;
    public Image completedQuest;

    public GameObject checkIcon;
    public ProgressBar progressbar;

    public Text progressNumber;
} 
