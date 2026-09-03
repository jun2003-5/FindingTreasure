using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChange : MonoBehaviour
{
    Image img;
    float time;
    void Awake()
    {
        img = GetComponent<Image>();
    }
    void Update()
    {
        time += Time.deltaTime;
        if(time > 0.1) {
            changeColor();
            time = 0;
        }
    }
    void changeColor()
    {
        StartCoroutine(changeColorRoutine());
    }
    IEnumerator changeColorRoutine()
    {
        if(img != null) {
            Color newColor = new Color(Random.value, Random.value, Random.value);

            img.color = newColor;
        }

        yield return new WaitForSeconds(0.2f);
    }
}
