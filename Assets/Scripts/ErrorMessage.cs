using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorMessage : MonoBehaviour
{
    float time;

    public float SetTime;

    private void Start()
    {
        if(SetTime == 0)
            SetTime = 1.5f;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if(time > SetTime) {
            this.gameObject.SetActive(false);
            time = 0;
        }
    }
}
