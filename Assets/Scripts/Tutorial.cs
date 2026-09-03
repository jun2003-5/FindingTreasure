using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Tutorial : MonoBehaviour
{
    public StartScreen start;
    float chatNum;
    public Text chatText;
    public GameObject[] tabs;
    public GameObject[] arrow;
    public AudioSource first;
    public AudioSource second;

    public float remeberFirst;
    public float remeberSecond;

    public AudioSource audiosource;
    public AudioClip audioclip;

    bool isChecked;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    } 

    // Update is called once per frame
    void Update()
    {
        if(chatNum == 0) {
            if(!isChecked) {
                remeberFirst = first.volume;
                remeberSecond = second.volume;
                isChecked = true;
            }
            first.volume = 0f;
            second.volume = 0f;
            chatText.text = "냐옹. 나는 해적 고양이다냥. 선원이 된걸 축하한다냥.";    
        } else if(chatNum == 1) {
            chatText.text = "선장이 혼자 탐험을 갔다가 연락이 끊겼다냥";
        } else if(chatNum == 2) {
            chatText.text = "그래서 우리는 선장을 찾을 여행을 갈거다냥";
        } else if(chatNum == 3) {
            chatText.text = "무려 40,075km나 향해 해야한다냥.";
        } else if(chatNum == 4) {
            chatText.text = "하지만 40,0075km를 가기에는 우리 배가 너무 안좋다냥.";
        } else if(chatNum == 5) {
            chatText.text = "그래서, 배 업그레이드 방법을 알려주겠다냥.";
        } else if(chatNum == 6) {
            chatText.text = "배를 업그레이드하기 앞서 재료를 먼저 구해야한다냥.";
        } else if(chatNum == 7) {
            chatText.text = "재료는 오른쪽 위에 보이는 버튼을 통해 확인이 가능하다냥.";
        } else if(chatNum == 8) {
            tabs[0].SetActive(true);
            chatText.text = "재료는 한번 팔면 다 팔아야하니 신중하게 파는게 좋다냥.";
        } else if(chatNum == 9) {
            tabs[0].SetActive(false);
            chatText.text = "왼쪽 밑에 업그레이드를 눌러 모은 재료로 배를 업그레이드 할 수 있다냥.";
        } else if(chatNum == 10) {
            tabs[1].SetActive(true);
            chatText.text = "좋은 부품일수록 가격이 높아지니 참고하라냥.";
        } else if(chatNum == 11) {
            tabs[1].SetActive(false);
            chatText.text = "다음은 퀘스트다냥.";
            arrow[0].SetActive(true);
        } else if(chatNum == 12) {
            tabs[2].SetActive(true);
            arrow[0].SetActive(false);
            chatText.text = "미션을 수행하면 다양한 보상을 준다냥.";
        } else if(chatNum == 13) {
            tabs[2].SetActive(false);
            chatText.text = "다음은 광부랑 광질이다냥.";
            arrow[1].SetActive(true);
        } else if(chatNum == 14) {
            tabs[3].SetActive(true);
            chatText.text = "광부는 재료의 속도를 올려주고 광질도구는 채굴 양을 높여준다냥.";
            arrow[1].SetActive(false);
        } else if(chatNum == 15) {
            tabs[3].SetActive(false);
            chatText.text = "이번엔 특별상점이다냥.";
            arrow[2].SetActive(true);
        } else if(chatNum == 16) {
            tabs[4].SetActive(true);
            chatText.text = "다이아로 구매할수있는 상품들이다냥.";
            arrow[2].SetActive(false);
        } else if(chatNum == 17) {
            chatText.text = "선장을 위해 가끔 사달라냥...ㅠ";
        } else if(chatNum == 18) {
            tabs[4].SetActive(false);
            chatText.text = "다음은 오른쪽 밑에있는 미니게임이다냥.";
            arrow[3].SetActive(true);
        } else if(chatNum == 19) {
            tabs[5].SetActive(true);
            chatText.text = "게임방식은 구매한 재료게임을 터치한 만큼 획득하는거다냥. 하지만 거리가 50km를 지나면 플레이가 가능하니 참고하라냥.";
            arrow[3].SetActive(false);
        } else if(chatNum == 20) {
            tabs[5].SetActive(false);
            chatText.text = "재료 얻는 방법은 광부 말고도 있다냥.";
        } else if(chatNum == 21) {
            chatText.text = "화면을 클릭하면 바다주화를 얻는다냥.";
        } else if(chatNum == 22) {
            chatText.text = "바다주화는 오른쪽에있는 교환소를 통해 재료로 교환 할 수 있다냥.";
        } else if(chatNum == 23) {
            chatText.text = "그리고, 다이아나 골드를 모아 뽑기를 통해 수집품, 재료, 바다주화를 대량으로 얻을 수 있다냥.";
        } else if(chatNum == 24) {
            chatText.text = "(뱃고동 소리가 들리며...)";
        } else if(chatNum == 25) {
            chatText.text = "이제 출발 한다냥.";
        } else if(chatNum == 26) {
            chatText.text = "꼭 보물을 찾아달라냥. 행운을 빈다냥.";
        } else if (chatNum == 27) {
            isChecked = false;
            first.volume = remeberFirst;
            second.volume = remeberSecond;
            chatNum = 0;
            this.gameObject.SetActive(false);
        }
    }
    public void chat()
    {
        if(!audiosource.isPlaying)
            chatNum++;
        if(chatNum == 24 && !audiosource.isPlaying) {
            audiosource.clip = audioclip;
            audiosource.Play();
        }
   
    }
    public void skip()
    {
        chatNum = 27;
    }
}
