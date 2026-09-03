using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    [SerializeField] public string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    [Header("--------1초마다 올라가는 갯수")]
    public float[] settingSpeed;
    public long coinagePrice;

    public float increasingSpeed;
    public int increasingAmount;
    [Header("--------이름")]
    public string itemName;
    [Header("--------가격")]
    public float itemCost;
    [Header("--------재료 숫자")]
    public long itemNumber;
    [Header("--------한도 초과")]
    public bool limitOver;
    [Header("--------필요한 속도")]
    public float requiredDistance;
    public bool isOverTheDistance;

    public float addCount;
    float countMagic;
    float countMagic2;

    public void isOverDistance(float distance) 
    {
        if(distance >= requiredDistance)
            isOverTheDistance = true;
        else {
            isOverTheDistance = false;
        }
    }
    void Update()
    {
        if (itemName != "magicstone" && itemName != "coinage" && itemName != "CreatingPaper") {
            if (!limitOver && isOverTheDistance) {
                addCount += Time.deltaTime;

                if (increasingSpeed >= 0.01f) {
                    if (addCount >= increasingSpeed) {
                        addCount = 0f;
                        itemNumber += increasingAmount;
                    }
                } else {
                    if (addCount >= increasingSpeed) {
                        addCount = 0f;
                        itemNumber += (long)(increasingAmount * (0.0005f/increasingSpeed));
                    }
                }
            } else if (limitOver) {
                addCount = 0f;
            }
        } else if (itemName == "magicstone" && !limitOver && isOverTheDistance) {
            countMagic += Time.deltaTime;

            if (countMagic >= 1) {
                float randomNumber = Random.Range(0.0f, 101.0f);
                if (randomNumber <= 0.6f) {
                    itemNumber++;
                }
                countMagic = 0f;
            }
        } else if (itemName == "coinage") {
            addCount += Time.deltaTime;

            if (addCount >= 1) {
                itemNumber += increasingAmount;
                addCount = 0f;
            }
        } else if (itemName == "CreatingPaper") {
            countMagic2 += Time.deltaTime;

            if (countMagic2 >= 2) {
                float randomNumber = Random.Range(0.0f, 101.0f);
                if (randomNumber <= 1f) {
                    itemNumber++;
                }
                countMagic2 = 0f;
            }
        }

        if(itemNumber < 0) {
            itemNumber = 0;
        }
        if(increasingSpeed < 0.000000000001 || increasingSpeed > 90) {
            increasingSpeed = 1;
        }
       

    }
    public long addMoney(long numberofItem)
    {
        return (long)itemCost * numberofItem;
    }

    //GET AND SET
    public void setIncreasingSpeed(float speed)
    {
        increasingSpeed = speed;
    }
}
