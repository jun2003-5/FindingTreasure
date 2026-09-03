using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gongji : MonoBehaviour
{
    [SerializeField] GameObject thisObject;
    public string KeyString;

    void Start()
    {
        if (PlayerPrefs.HasKey(KeyString)) {
            if (PlayerPrefs.GetInt(KeyString) == 2) {
                thisObject.SetActive(true);
            } else if ((PlayerPrefs.GetInt(KeyString) == 1))
                thisObject.SetActive(false);
        } else {
            thisObject.SetActive(true);
            PlayerPrefs.SetInt(KeyString, 1);
        }
    }
    public void dontShowAgain()
    {
        if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color.a == 0) {
            PlayerPrefs.SetInt(KeyString, 1);
            UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        } else if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color.a == 1) {
            PlayerPrefs.SetInt(KeyString, 2);
            UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }
    public void CloseTab()
    {
        thisObject.SetActive(false);
    }
}
