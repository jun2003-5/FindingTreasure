using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Buff : MonoBehaviour
{
    public BuffData[] buffs;
    public Player player;
    public GameObject lackdiamond;

    public void Update() 
    {
        for(int i =0; i < buffs.Length; i++) {
            if(buffs[i].isActivate) {
                buffs[i].buffImage.SetActive(true);
                if(!buffs[i].isCoroutine) {
                    buffActivate(i);
                    buffs[i].isCoroutine = true;
                }
            } else {
                buffs[i].buffImage.SetActive(false);
            }
        }
    }

    public void buffActivate(int i)
    {
        //1 frog
        if (i == 0) {
            StartCoroutine(frogability(i));
        } else if (i == 1) {
            StartCoroutine(koalaAbility(i));
        } else if (i == 2 && !player.touchscreen.coinageBuffOn) {
            if(player.diamond >= 30) {
                player.diamond -= 30;
                StartCoroutine(coinageBuff());
            } else {
                StartCoroutine(lackdiamoncaution());
            }

        } else if (i == 3 && !player.isMatBuffOn) {
            if(player.diamond >= 50) {
                player.diamond -= 50;
                StartCoroutine(MatBuff());

            } else
                StartCoroutine(lackdiamoncaution());
        }

    }
    IEnumerator frogability(int i)
    {
        buffs[i].buffImage.GetComponentInChildren<Image>().color = Color.white;
        while(buffs[i].count < buffs[i].cooldown) {
            buffs[i].buffTime.text = buffs[i].cooldown - buffs[i].count + "초";
            buffs[i].count++;
            yield return new WaitForSeconds(1f);
        }
        for(int z = 0; z < player.item.Length; z++) {
            player.item[z].increasingSpeed /= 3;
        }
        while(buffs[i].count < buffs[i].duration + buffs[i].cooldown) {
            float r = Random.Range(0.0f, 1.0f);
            float g = Random.Range(0.0f, 1.0f);
            float b = Random.Range(0.0f, 1.0f);

            buffs[i].buffImage.GetComponentInChildren<Image>().color = new Color(r, g, b);
            buffs[i].buffTime.text = (buffs[i].duration + buffs[i].cooldown) - buffs[i].count + "초";
            buffs[i].count++;
            yield return new WaitForSeconds(1f);
        }
        for(int z = 0; z < player.item.Length; z++) {
            player.item[z].increasingSpeed *= 3;
        }

        buffs[i].count = 0;
        buffActivate(i);
    }
    IEnumerator koalaAbility(int i)
    {
        buffs[i].buffImage.GetComponentInChildren<Image>().color = Color.white;
        while(buffs[i].count < buffs[i].cooldown) {
            buffs[i].buffTime.text = buffs[i].cooldown - buffs[i].count + "초";
            buffs[i].count++;
            yield return new WaitForSeconds(1f);
        }
        player.speed *= 3;
        while(buffs[i].count < buffs[i].duration + buffs[i].cooldown) {
            float r = Random.Range(0.0f, 1.0f);
            float g = Random.Range(0.0f, 1.0f);
            float b = Random.Range(0.0f, 1.0f);

            buffs[i].buffImage.GetComponentInChildren<Image>().color = new Color(r, g, b);
            buffs[i].buffTime.text = (buffs[i].duration + buffs[i].cooldown) - buffs[i].count + "초";
            buffs[i].count++;
            yield return new WaitForSeconds(1f);
        }
        player.speed /= 3;

        buffs[i].count = 0;

        buffActivate(i);
    }

    IEnumerator coinageBuff()
    {
        player.sound.PlaySound("BuyOrSell");
        player.coinageMul *= 2;
        player.touchscreen.coinageBuffOn = true;
        yield return new WaitForSeconds(20f);
        player.coinageMul /= 2;
        player.touchscreen.coinageBuffOn = false;
    }

    IEnumerator MatBuff()
    {
        player.sound.PlaySound("BuyOrSell");
        for(int i = 0; i < player.item.Length; i++) {
            player.item[i].increasingAmount *= 10;
            player.item[i].increasingSpeed /= 10;
        }
        player.isMatBuffOn = true;
        yield return new WaitForSeconds(20f);
        for(int i = 0; i < player.item.Length; i++) {
            player.item[i].increasingAmount /= 10;
            player.item[i].increasingSpeed *= 10;
        }
        player.isMatBuffOn = false;
    }

    IEnumerator lackdiamoncaution()
    {
        player.sound.PlaySound("Caution");
        lackdiamond.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        lackdiamond.SetActive(false);
    }
}
