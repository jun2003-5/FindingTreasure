using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SoundManager1 : MonoBehaviour
{
    public SoundEffect sound;
    [SerializeField] Slider volumeSlider2;

    public Text valueText2;

    [Header("이미지")]
    public Sprite[] images;
    public Image effectImage;

    void Start()
    {
        if(!PlayerPrefs.HasKey("eVolume")) {
            PlayerPrefs.SetFloat("eVolume", 0.5f);
            load();
        } else {
            load();
        }

    }
    public void Update()
    { 
        ChangeVolume();
    }
    public void ChangeVolume()
    {
        //Effect
        valueText2.text = Mathf.RoundToInt(volumeSlider2.value * 100).ToString() + "%";
        sound.soundControll = volumeSlider2.value * 200;
        if(volumeSlider2.value > 0) {
            effectImage.sprite = images[0];
        } else if(volumeSlider2.value <= 0) {
            effectImage.sprite = images[1];
        }
        Save();
    }
    public void UP()
    {
        sound.PlaySound("BuyOrSell");
    }

    private void load()
    {
        volumeSlider2.value = PlayerPrefs.GetFloat("eVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("eVolume", volumeSlider2.value);
    }
}
