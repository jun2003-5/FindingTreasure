using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    [Header("--------소리")]
    public AudioSource audioSource;
    public AudioClip[] audioclips;

    public float soundControll;
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(string action)
    {
        switch(action) {
            case "BuyOrSell": 
                audioSource.volume = 0.4f * (soundControll / 100.0f);
                audioSource.clip = audioclips[0];
                break;
            case "miniGameStartCount":
                audioSource.volume = 0.3f * (soundControll / 100.0f);
                audioSource.clip = audioclips[1];
                break;
            case "miniGameIngameCount":
                audioSource.volume = 0.6f * (soundControll / 100.0f);
                audioSource.clip = audioclips[2];
                break;
            case "miniGameScoreShow":
                audioSource.volume = 0.3f * (soundControll / 100.0f);
                audioSource.clip = audioclips[3];
                break;
            case "UIPOP":
                audioSource.volume = 0.3f * (soundControll / 100.0f);
                audioSource.clip = audioclips[4];
                break;
            case "Caution":
                audioSource.volume = 0.4f * (soundControll / 100.0f);
                audioSource.clip = audioclips[5];
                break;
            case "Cancel":
                audioSource.volume = 0.3f * (soundControll / 100.0f);
                audioSource.clip = audioclips[6];
                break;
            case "normalClick":
                audioSource.volume = 2f * (soundControll / 100.0f);
                audioSource.clip = audioclips[7];
                break;
            case "tradeExchange":
                audioSource.volume = 1.1f * (soundControll / 100.0f);
                audioSource.clip = audioclips[8];
                break;
            case "Denied":
                audioSource.volume = 0.7f * (soundControll / 100.0f);
                audioSource.clip = audioclips[9];
                break;
            case "mine":
                audioSource.volume = 0.7f * (soundControll / 100.0f);
                audioSource.clip = audioclips[10];
                break;
            case "minerFound":
                audioSource.volume = 0.3f * (soundControll / 100.0f);
                audioSource.clip = audioclips[11];
                break;
            case "minerFoundButtonClick":
                audioSource.volume = 0.2f * (soundControll / 100.0f);
                audioSource.clip = audioclips[12];
                break;
            case "chestOpenSound":
                audioSource.volume = 0.7f * (soundControll / 100.0f);
                audioSource.clip = audioclips[13];
                break;
            case "QuestComplete":
                audioSource.volume = 0.7f * (soundControll / 100.0f);
                audioSource.clip = audioclips[14];
                break;
        }
        audioSource.Play();
    }
}
