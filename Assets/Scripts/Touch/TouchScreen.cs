using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchScreen: MonoBehaviour
{
    public GameObject earnedMoney;

    public Player player;

    public bool coinageBuffOn;
    public void Update() 
    {
        if(coinageBuffOn) {
            clicked(2);
        } 
    }
    public void clicked(int type)
    {
        float _posX = 0;
        float _posY = 0;
        if(type == 1) {
            _posX = Input.mousePosition.x;
            _posY = Input.mousePosition.y;
        } else if (type == 2) {
            _posX = Random.Range(0, Screen.width);
            _posY = Random.Range(0, Screen.height);
        }
        GameObject a = GameObject.Instantiate(earnedMoney);
        a.transform.position = new Vector3(_posX, _posY + 15, Input.mousePosition.z);
        a.transform.SetParent(transform);
        a.transform.name = "Clone";
        player.coinage.itemNumber += (player.coinageMul * 1);
    }
}