using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TradingCenter : MonoBehaviour
{
    public SoundEffect sound;
    public GameObject _exItems;
    //가진 주화
    public TextMeshProUGUI titleCoinage;

    //주화 패널 display
    public TextMeshProUGUI panelCoinage;
    public TextMeshProUGUI panelMat;

    //바꾸는 주화 갯수 
    public long exchangingCoinNumber;
    public long exchangingMatNumber;

    //버튼 누른지 확인
    public bool isHeldDown;

    //경고 표시
    public GameObject caution;
    public GameObject safeCaution;
    public TextMeshProUGUI currentsafe;

    public Player player;

    int frames;
    int frameCount;
    public int _addOrMinus;
    public int changingItemNum;

    float timer;
    bool isCaution;
    bool isSafeCaution;
    int cautionInt;
    public void Awake()
    {
        exchangingCoinNumber = 10;
    }
    public void Update()
    {

        titleCoinage.text = "주화 " + string.Format("{0:#,###0}", player.coinage.itemNumber) + "개 보유";
        panelCoinage.text = string.Format("{0:#,###0}", exchangingCoinNumber);
        panelMat.text = string.Format("{0:#,###0}", exchangingMatNumber);

        exchangingMatNumber = (exchangingCoinNumber / player.item[changingItemNum].coinagePrice);

        //매초
        if(isHeldDown) {
            frames++;
            if(frames % frameCount == 0) {
                if(exchangingCoinNumber + ((_addOrMinus / _addOrMinus) / _addOrMinus * player.item[changingItemNum].coinagePrice) <= player.coinage.itemNumber && exchangingCoinNumber + ((_addOrMinus / _addOrMinus) / _addOrMinus) > player.item[changingItemNum].coinagePrice) {
                    exchangingCoinNumber += (_addOrMinus * player.item[changingItemNum].coinagePrice);
                }
                if(frames % (frameCount * 3) == 0 && frameCount > 1) {
                    frameCount--;
                    if(Mathf.Abs(_addOrMinus) < 100)
                        _addOrMinus *= 2;
                }
            }
        } else {
            frameCount = 10;
        }

        if(isCaution) {
            timer += Time.deltaTime;
            caution.GetComponentInChildren<Text>().text = "거리가 부족합니다 (" + player.item[cautionInt].requiredDistance + "km)";
            caution.SetActive(true);
            for(int i = 0; i < _exItems.transform.childCount; i++) {
                if(i == 0) {
                    _exItems.transform.GetChild(i).gameObject.SetActive(true);
                } else
                    _exItems.transform.GetChild(i).gameObject.SetActive(false);
            }
            if(timer > 1) {
                caution.SetActive(false);
                isCaution = false;
                exchangingMaterial("stone");
            }
        }
        if(isSafeCaution) {
            timer += Time.deltaTime;
            safeCaution.SetActive(true);
            if(timer > 1) {
                safeCaution.SetActive(false);
                isSafeCaution = false;
            }
        }
        currentsafe.text = "현재 공간: " + string.Format("{0:#,###0}", player.getTotalmaterial()) + "/" + string.Format("{0:#,###0}", player.manager.chestLimitNumber);
    }

    public void exchangingMaterial(string name)
    {
        for(int i = 0; i < _exItems.transform.childCount; i++) {
            if(_exItems.transform.GetChild(i).name == name) {
                if(player.distance >= player.item[i].requiredDistance) {
                    sound.PlaySound("normalClick");
                    _exItems.transform.GetChild(i).gameObject.SetActive(true);
                    changingItemNum = i;
                    exchangingCoinNumber = player.item[i].coinagePrice;
                    timer = 1.99f;
                } else {
                    sound.PlaySound("Caution");
                    for(int z = 0; z < _exItems.transform.childCount; z++) {
                        if(player.distance >= player.item[z].requiredDistance) {
                            _exItems.transform.GetChild(z).gameObject.SetActive(true);
                        } else {
                            _exItems.transform.GetChild(i).gameObject.SetActive(false);
                        }
                    }
                    cautionInt = i;
                    isCaution = true;
                    timer = 0;
                }
            } else {
                _exItems.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void Up()
    {
        frames = frameCount - 1;
        _addOrMinus = 1;
    }
    public void down()
    {
        frames = frameCount - 1;
        _addOrMinus = -1;
    }
    public void PointerDown()
    {
        sound.audioSource.loop = true;
        sound.PlaySound("normalClick");
        isHeldDown = true;
    }
    public void PointerUp()
    {
        sound.audioSource.loop = false;
        isHeldDown = false;
    }

    public void setToMax()
    {
        sound.PlaySound("normalClick");
        long count = player.coinage.itemNumber;
        while(count % player.item[changingItemNum].coinagePrice != 0) {
            count--;
        }
        exchangingCoinNumber = count;
        if(exchangingCoinNumber == 0) {
            exchangingCoinNumber = player.item[changingItemNum].coinagePrice;
        }
    }
    public void SetToMin()
    {
        sound.PlaySound("normalClick");
        exchangingCoinNumber = player.item[changingItemNum].coinagePrice;
    }
    public void PlusTenPercent()
    {
        long count = player.coinage.itemNumber;
        while(count % player.item[changingItemNum].coinagePrice != 0) {
            count--;
        }
        if(exchangingCoinNumber + (count/10) <= player.coinage.itemNumber)
            exchangingCoinNumber += count/10;
        
        if(exchangingCoinNumber == 0) {
            exchangingCoinNumber = player.item[changingItemNum].coinagePrice;
        }
    }
    public void MinusTenPercent()
    {
        long count = player.coinage.itemNumber;
        while(count % player.item[changingItemNum].coinagePrice != 0) {
            count--;
        }
        if(exchangingCoinNumber - (count / 10) >= 0 )
            exchangingCoinNumber -= count / 10;

        if(exchangingCoinNumber == 0) {
            exchangingCoinNumber = player.item[changingItemNum].coinagePrice;
        }
    }
    public void exchange()
    {
        timer = 1.99f;
        if(player.coinage.itemNumber - exchangingCoinNumber >= 0) {
            if(exchangingMatNumber + player.getTotalmaterial() <= player.manager.chestLimitNumber) {
                sound.PlaySound("tradeExchange");
                player.coinage.itemNumber -= exchangingCoinNumber;
                player.item[changingItemNum].itemNumber += exchangingMatNumber;

                if(player.coinage.itemNumber - (exchangingCoinNumber) <= 0) {
                    setToMax();
                }
            } else {
                sound.PlaySound("Caution");
                isSafeCaution = true;
                timer = 0;
            }
        } else {
            sound.PlaySound("Denied");
        }
    }
}
