using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public Player player;
    public Text chatText;

    int chatNum;

    public void Update()
    {
        if(chatNum == 0) {
            chatText.text = "드디어 찾았다냥!!";
        } else if (chatNum == 1) {
            chatText.text = "선장을 찾는동안 우리의 선장이 되주어서 고마웠다냥..";
        } else if(chatNum == 2) {
            chatText.text = "우리는 이제 더 많은 보물을 찾으로 다른 곳으로 간다냥";
        } else if(chatNum == 3) {
            chatText.text = "그때도(?) 잘 부탁한다냥";
        } else if (chatNum == 4) {
            this.gameObject.SetActive(false);
        }
    }
    public void startEndingScene() 
    {           
        player.coinageMul += 100000;
        player.coinage.increasingAmount += 1000000;
        player.speed += 10000;
        for(int i = 0; i < player.item.Length - 2; i++) {
            player.item[i].increasingAmount += 100;
        }
        StartCoroutine(Endgame());
    }

    IEnumerator Endgame()
    {
        yield return new WaitForSeconds(1f);

        for (float i = 1.5f; i >= 0; i -= Time.deltaTime) {
            this.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, i / 1.5f);
            yield return null;
        }
        this.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void earnPirateTitle()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(true);
    }
    public void chat()
    {
        chatNum++;
    }
}
