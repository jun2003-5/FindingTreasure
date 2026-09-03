using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class BackgroundLoop : MonoBehaviour
{

    [Header("--------아침 점심 저녘 배경 설정")]
    public GameObject morning;

    public string xxxtime;
    public Image fadeAwayTab;
     
    [Header("--------아침 점심 저녘 배경")]
    public GameObject[] backgroundlevels;
    public Sprite[] backgroundsky;

    [Header("--------배")]
    public SpriteRenderer Ship;
    public SpriteRenderer trail;

    [Header("--------텍스트 색깔 바꾸기")]
    public GameObject[] textColor;

    private Camera mainCamera;
    private Vector2 screenBounds;
    public float choke;

    public float movingSpeed;

    public float timer;

    bool lsFadingAway;

    void Awake()
    {
        xxxtime = "morning";
    }
    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        makeBackground();
    }
    void makeBackground()
    {
        foreach(GameObject obj in backgroundlevels) {
            loadChildObjects(obj);
        }
    }
    void loadChildObjects(GameObject obj)
    {
        float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - choke;
        int childsNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objectWidth);
        GameObject clone = Instantiate(obj) as GameObject;
        for(int i = 0; i <= childsNeeded; i++) {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position
                .z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }
    void repositionChildObjects(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if(children.Length > 1) {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x - choke;
            if(transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth) {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);
            } else if(transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth) {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth * 2, firstChild.transform.position.y, firstChild.transform.position.z);
            }
        }
    }
    void Update()
    {
        mainCamera.transform.position += Vector3.right * Time.deltaTime * movingSpeed;

        float y = Mathf.PingPong(Time.time * 0.25f, 0.3f) * 0.2f + 0.7f;
        float y2 = Mathf.PingPong(Time.time * 0.25f, 0.3f) * 0.2f - 0.53f;
        Ship.transform.position = new Vector3(mainCamera.transform.position.x, y, Ship.transform.position.z);
        trail.transform.position = new Vector3(mainCamera.transform.position.x - 2.45f, y2, trail.transform.position.z); ;

        if(xxxtime == "morning") {

            for(int i = 0; i < backgroundlevels[0].transform.childCount; i++) {
                backgroundlevels[0].transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = backgroundsky[0];
            }
            for(int i = 0; i < backgroundlevels[1].transform.childCount; i++) {
                backgroundlevels[1].transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            }
            trail.color = Color.white;
            foreach(GameObject a in textColor) {
                if(a.GetComponent<Text>() != null)
                    a.GetComponent<Text>().color = new Color(0.1f, 0.1f, 0.1f);
                else
                    a.GetComponent<TextMeshProUGUI>().color = new Color(0.1f, 0.1f, 0.1f);
            }
        } else if(xxxtime == "evening") {
             
            for(int i = 0; i < backgroundlevels[0].transform.childCount; i++) {
                backgroundlevels[0].transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = backgroundsky[1];
            }
            for(int i = 0; i < backgroundlevels[1].transform.childCount; i++) {
                backgroundlevels[1].transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 0.7688679f, 0.7688679f);
            }
            trail.color = new Color(1, 0.807f, 0.807f);
            foreach(GameObject a in textColor) {
                if(a.GetComponent<Text>() != null)
                    a.GetComponent<Text>().color = new Color(0.1f, 0.1f, 0.1f);
                else
                    a.GetComponent<TextMeshProUGUI>().color = new Color(0.1f, 0.1f, 0.1f);
            }
        } else if(xxxtime == "night") {

            for(int i = 0; i < backgroundlevels[0].transform.childCount; i++) {
                backgroundlevels[0].transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = backgroundsky[2];
            }
            for(int i = 0; i < backgroundlevels[1].transform.childCount; i++) {
                backgroundlevels[1].transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(0.6127626f, 0.6489328f, 0.9622642f);
            }
            trail.color = new Color(0.6127626f, 0.6489328f, 0.9622642f);
            foreach(GameObject a in textColor) {
                if(a.GetComponent<Text>() != null)
                    a.GetComponent<Text>().color = Color.white;
                else
                    a.GetComponent<TextMeshProUGUI>().color = Color.white;
            }
        }

        if(xxxtime == "morning") {
            timer += Time.deltaTime;
            if(timer > 15 * 60 ) { //15
                if(!lsFadingAway)
                    fadeAway();
                if(timer > (15 *60) + 1.5) {
                    xxxtime = "evening";
                    timer = 0;
                }
            }
        } else if(xxxtime == "evening") {
            timer += Time.deltaTime;
            if(timer > 5 * 60) { //5
                if(!lsFadingAway)
                    fadeAway();
                if(timer > (5 * 60) + 1.5) {
                    xxxtime = "night";
                    timer = 0;
                }
            }
        } else if(xxxtime == "night") {
            timer += Time.deltaTime;
            if(timer > 10 * 60) { //10
                if(!lsFadingAway)
                    fadeAway();
                if(timer > (10*60) + 1.5) {
                    xxxtime = "morning";
                    timer = 0;
                }
            }
        }
    }
    void LateUpdate()
    {
        for(int i = 0; i < backgroundlevels.Length; i++) {

            repositionChildObjects(backgroundlevels[i]);
        }
    }

    //서서히 시간이 바뀌는 효과
    public void fadeAway()
    {
        StartCoroutine(FadeTab());
    }

    IEnumerator FadeTab()
    {
        lsFadingAway = true;
        for(float i = 0; i <= 1.5f; i += Time.deltaTime) {
            fadeAwayTab.color = new Color(0, 0, 0, i / 1.3f);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        for(float i = 1.5f; i >= 0; i -= Time.deltaTime) {
            fadeAwayTab.color = new Color(0, 0, 0, i / 1.3f);
            yield return null;
        }
        lsFadingAway = false;
    }
}
