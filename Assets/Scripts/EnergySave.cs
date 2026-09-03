using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnergySave : MonoBehaviour
{

    float Timer;
    int Times;
    [SerializeField] private GameObject batterySaveTab;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        // When the Menu starts, set the rendering to target 20fps
        OnDemandRendering.renderFrameInterval = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;

        if(Timer > 300) {
            OnDemandRendering.renderFrameInterval = 4;
            batterySaveTab.gameObject.SetActive(true);
        } else {
            OnDemandRendering.renderFrameInterval = 1;
            batterySaveTab.gameObject.SetActive(false);
        }


        if (Input.GetMouseButton(0) || (Input.touchCount > 0)) {
            Timer = 0;
        } 
    }
}