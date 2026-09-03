using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestAnimation : MonoBehaviour
{
    public SoundEffect sound;
    public float _Speed = 1f;
    public int _FrameRate = 30;
    public bool _Loop = false;
    private Image mImage = null;

    public int type;

    private Sprite[] mSprites = null;
    private float mTimePerFrame = 0f;
    private float mElapsedTime = 0f;
    public int mCurrentFrame = 0;
     
    public float delayTime;

    public void starting()
    {
        mImage = GetComponent<Image>();
        enabled = false;
        if(type == 1)
            mImage.sprite = Resources.Load<Sprite>("Wooden_Chest");
        else if(type == 2)
            mImage.sprite = Resources.Load<Sprite>("Silver_Chest");
        else if(type == 3)
            mImage.sprite = Resources.Load<Sprite>("Epic_Chest");
        else if(type == 4)
            mImage.sprite = Resources.Load<Sprite>("Golden_Chest");

        Invoke("LoadSpriteSheet", delayTime);
    }
    private void LoadSpriteSheet()
    {
        if(type == 1) {
            mSprites = Resources.LoadAll<Sprite>("Wooden_Chest");

        } else if (type == 2) {
            mSprites = Resources.LoadAll<Sprite>("Silver_Chest");
        } else if (type == 3) {
            mSprites = Resources.LoadAll<Sprite>("Epic_Chest");
        } else if (type == 4) {
            mSprites = Resources.LoadAll<Sprite>("Golden_Chest");
        }
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
                if(_Loop) {
                    mCurrentFrame = 0;
                } else {
                    enabled = false;
                }
            }
            if(mCurrentFrame == mSprites.Length - 3) {
                sound.PlaySound("chestOpenSound");
            }
        }
    }
    private void SetSprite()
    {
        if(mCurrentFrame >= 0 && mCurrentFrame < mSprites.Length) {
            mImage.sprite = mSprites[mCurrentFrame];
        }
    }
}