using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinFading : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(ClickedFading());
    }
     
    IEnumerator ClickedFading()
    {
        if(this.transform.name != "Coin") {
            for(float f = 1.5f; f >= 0; f -= 0.1f) {
                transform.position = new Vector3(transform.position.x, transform.position.y + 3, Input.mousePosition.z);
                Color c = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b);
                Color c2 = new Color(this.transform.GetComponentInChildren<TextMeshProUGUI>().color.r, this.transform.GetComponentInChildren<TextMeshProUGUI>().color.g, this.transform.GetComponentInChildren<TextMeshProUGUI>().color.b);

                c.a = f;
                c2.a = f;
                GetComponent<Image>().color = c;

                this.transform.GetComponentInChildren<TextMeshProUGUI>().color = c2;
                yield return new WaitForSeconds(0.05f);
            }
        } else {
            GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }

        if(this.transform.name == "Clone") {
            GameObject.Destroy(this.gameObject);
        }
    }
}
