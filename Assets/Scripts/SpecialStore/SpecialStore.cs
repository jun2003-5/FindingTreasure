using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class SpecialStore : MonoBehaviour
{
    public SoundEffect sound;
    public Player player;

    public SpecialStoreData[] specialItem;

    public SpecialStoreData[] DiatoCOin;

    [Header("--------특별 상점 탭들")]
    public GameObject diamondTab;
    public GameObject specialItemTab;
    public GameObject diaToCoinTab;

    public bool legendaryPickaxed;

    public GameObject lackDiamond;
    int moneyPrice;
    bool quitWhile;
    public float money;
    public void Awake()
    {
        for (int i = 1; i < specialItem.Length; i++) {
            specialItem[i].diamondText.text = string.Format("{0:#,###0}", specialItem[i].diamondCost) + "개";
            specialItem[i].abilityText.text = specialItem[i].abilityName;
        }
        for (int i = 1; i < DiatoCOin.Length; i++) {
            DiatoCOin[i].diamondText.text = string.Format("{0:#,###0}", DiatoCOin[i].diamondCost) + "개";
            DiatoCOin[i].costText.text = DiatoCOin[i].abilityName;
        }
    }
    public void Update()
    {
        for (int i = 1; i < specialItem.Length; i++) {
            if (player.diamond >= specialItem[i].diamondCost && !specialItem[i].isBought) {
                specialItem[i].isBuyable = true;
            } else if (player.diamond < specialItem[i].diamondCost && !specialItem[i].isBought) {
                specialItem[i].isBuyable = false;
            }
        }
        for (int i = 1; i < DiatoCOin.Length; i++) {
            if (player.diamond >= DiatoCOin[i].diamondCost && !DiatoCOin[i].isBought) {
                DiatoCOin[i].isBuyable = true;
            } else if (player.diamond < DiatoCOin[i].diamondCost && !DiatoCOin[i].isBought) {
                DiatoCOin[i].isBuyable = false;
            }
        }

        for (int i = 1; i < specialItem.Length; i++) {
            if (!specialItem[i].isBought) {
                if (specialItem[i].isBuyable) {
                    specialItem[i].itemCover.SetActive(false);
                } else if (!specialItem[i].isBuyable) {
                    specialItem[i].itemCover.SetActive(true);
                    specialItem[i].itemCover.GetComponent<Image>().color = new Color(0, 0, 0, 0.5686275f);
                }
            } else {
                specialItem[i].itemCover.GetComponent<Image>().color = new Color(0, 0, 0, 0.8f);
                specialItem[i].itemCover.SetActive(true);
            }
        }

        while (player.paddelShop[moneyPrice].isBought && !quitWhile) {
            moneyPrice++;
            if (moneyPrice == player.paddelShop.Length - 1) {
                quitWhile = true;
            }
        }
        money = (player.paddelShop[moneyPrice].costNumber[0] / 5);
        if (money <= 5000) {
            money = 5000;
        }
        DiatoCOin[1].costText.text = "다이아 25개를" + "\n" + "골드 " + string.Format("{0:#,###0}", money / 5) + "G로 바꿔줍니다.";
        DiatoCOin[2].costText.text = "다이아 50개를" + "\n" + "골드 " + string.Format("{0:#,###0}", money / 2) + "G로 바꿔줍니다.";
        DiatoCOin[3].costText.text = "다이아 100개를" + "\n" + "골드 " + string.Format("{0:#,###0}", money * 2.5f) + "G로 바꿔줍니다.";
    }

    //특별상점 탭
    public void openSpeicalstoreDiamond()
    {
        sound.PlaySound("normalClick");

        diamondTab.SetActive(true);
        specialItemTab.SetActive(false);
        diaToCoinTab.SetActive(false);
    }

    public void openSpeicalStoreItem()
    {
        sound.PlaySound("normalClick");

        specialItemTab.SetActive(true);
        diamondTab.SetActive(false);
        diaToCoinTab.SetActive(false);
    }
    public void openDiatoCoinItem()
    {
        sound.PlaySound("normalClick");

        specialItemTab.SetActive(false);
        diamondTab.SetActive(false);
        diaToCoinTab.SetActive(true);
    }

    public void itemBought(int i)
    {
        sound.PlaySound("BuyOrSell");
        if (!specialItem[i].isBought && specialItem[i].isBuyable) {

            //다이아 빼기
            player.diamond -= specialItem[i].diamondCost;
            applyAbility(i);
            specialItem[i].isBought = true;
        }
    }
    public void diatoCoinBought(int i)
    {
        if (DiatoCOin[i].isBuyable) {
            sound.PlaySound("BuyOrSell");
            //다이아 빼기
            player.diamond -= DiatoCOin[i].diamondCost;
            if (i == 1) {
                player.money += (long)(money / 5);
            } else if (i == 2) {
                player.money += (long)(money / 2);
            } else if (i == 3) {
                player.money += (long)(money * 2.5f);
            }
        } else {
            lackDiamond.gameObject.GetComponentInChildren<Text>().text = "다이아가 " + (DiatoCOin[i].diamondCost - player.diamond) + "개 부족합니다";
            StartCoroutine(showCaution());
        }
    }

    IEnumerator showCaution()
    {
        if (!sound.audioSource.isPlaying)
            sound.PlaySound("Caution");
        lackDiamond.SetActive(true);
        yield return new WaitForSeconds(1f);
        lackDiamond.SetActive(false);
    }
    public void applyAbility(int index)
    {
        switch (index) {
            case 1:
                player.catBought = true;
                player.speed *= 1.25f;
                break;
            case 2:
                player.minigame.timeManager += 10;
                break;
            case 3:
                for (int i = 0; i < player.item.Length; i++) {
                    player.item[i].coinagePrice /= 2;
                }
                break;
            case 6:
                for (int i = 0; i < player.item.Length; i++) {
                    player.item[i].itemCost *= 2;
                }
                break;
            case 4:
                player.buff.buffs[0].isActivate = true;
                break;
            case 5:
                player.buff.buffs[1].isActivate = true;
                break;
        }

        player.addSetNumber("pet");
    }
}
