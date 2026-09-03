using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MinerAnimation : MonoBehaviour
{
    public SoundEffect sound;
    public float _Speed = 1f;
    public int _FrameRate = 30;
    public bool _Loop = false;
    private Image mImage = null;

    private Sprite[] mSprites = null;
    private float mTimePerFrame = 0f;
    private float mElapsedTime = 0f;
    private int mCurrentFrame = 0;

    void Start()
    {
        mImage = GetComponent<Image>(); 
        enabled = false;
        LoadSpriteSheet();
        StartCoroutine(randomNumRou());
        transform.parent.transform.GetChild(1).gameObject.SetActive(false);
    }

    private void LoadSpriteSheet()
    {
        mSprites = Resources.LoadAll<Sprite>("Miner_mining");
        if(mSprites != null && mSprites.Length > 0) {
            mTimePerFrame = 1f / _FrameRate;
            Play();
        } else {
            Debug.Log("error");
        }
    }
    public void Play()
    {
        enabled = true;
    }
    void Update()
    {
        mElapsedTime += Time.deltaTime * _Speed;
        if(mElapsedTime >= mTimePerFrame) {
            mElapsedTime = 0f;
            ++mCurrentFrame;
            SetSprite();
            if(mCurrentFrame >= mSprites.Length) {
                if(_Loop)
                    mCurrentFrame = 0;
                else
                    enabled = false;
            }
        }
    }
    private void SetSprite()
    {
        if(mCurrentFrame >= 0 && mCurrentFrame < mSprites.Length) {
            mImage.sprite = mSprites[mCurrentFrame];
        }
    }

    IEnumerator randomNumRou()
    {
        yield return new WaitForSeconds(1);

        float ran = Random.Range(0.0f, 101.0f);

        if(ran <= 0.6f) {
            sound.PlaySound("minerFound");
            if(this.transform.parent.parent.GetComponent<MinerClone>().isAutomatic) {
                this.transform.parent.parent.GetComponent<MinerClone>().treasureTab();
                yield return new WaitForSeconds(0.6f);
                this.transform.parent.parent.GetComponent<MinerClone>().adsEarn();
            } else {
                transform.parent.transform.GetChild(1).gameObject.SetActive(true);
                yield return new WaitForSeconds(10);
                transform.parent.transform.GetChild(1).gameObject.SetActive(false);
            }
            StartCoroutine(randomNumRou());
        } else {
            StartCoroutine(randomNumRou());
        }
    }

    public void foundTreasure()
    {
          transform.parent.transform.GetChild(1).gameObject.SetActive(false);
    }
}
