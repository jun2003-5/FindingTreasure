using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BuffData
{
    public float duration;
    public float cooldown;
    public float count;
    public Text buffTime;
    public GameObject buffImage;
    public bool isActivate = false;
    public bool isCoroutine = false;
}
