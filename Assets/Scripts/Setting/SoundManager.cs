using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SoundManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    [SerializeField] Slider volumeSlider;

    public Text valueText;

    [Header("이미지")]
    public Sprite[] images;
    public Image backImage;

    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume")) {
            PlayerPrefs.SetFloat("musicVolume", 0.5f);
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
        //Background
        valueText.text = Mathf.RoundToInt(volumeSlider.value * 100).ToString() + "%";
        backgroundMusic.volume = volumeSlider.value;
        if(volumeSlider.value > 0) {
            backImage.sprite = images[0];
        } else if(volumeSlider.value <= 0) {
            backImage.sprite = images[1];
        }
        Save();
    }
    private void load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
