using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class StartScreen : MonoBehaviour
{
    public Player player;
    public GameManager game;

    public Text progressNumber;
    public float _Speed = 1f;
    public int _FrameRate = 30;
    public bool _Loop = false;

    public Slider loadingSlider;

    void Start()
    {
        player.GetComponent<AudioSource>().Stop();
        loading();
    }
    public void loading() 
    {
        StartCoroutine(loadingScreen());
    }
    IEnumerator loadingScreen()
    {
        for(int i = 0; i < game.tabs.Length; i++) {
            loadingSlider.value = (float)((float)i / ((float)game.tabs.Length - 1.0f));
            progressNumber.text = ((float)((float)i / ((float)game.tabs.Length - 1.0f)) * 100.0f).ToString("F1") + "%";
            if(game.tabs[i].name.ToLower() == "setting") {
                game.tabs[i].SetActive(true);
                game.soundTab.SetActive(true);
            } else
                game.tabs[i].SetActive(true);
            yield return new WaitForSeconds(0.25f);
            game.tabs[i].SetActive(false);
        }
        game.soundTab.SetActive(false);
        this.transform.GetChild(11).gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        player.GetComponent<AudioSource>().Play();
    }
}

