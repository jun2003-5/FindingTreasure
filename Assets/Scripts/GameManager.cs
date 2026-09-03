using System.Collections;
using TMPro;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using UnityEngine;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    public SoundEffect sound;

    [Header("--------재료 텍스트")]
    public TextMeshProUGUI[] materialText;
    public TextMeshProUGUI[] materialSellCostText;

    //보트 레벨
    [Header("--------보트 레벨")]
    public TextMeshProUGUI boatSailText;
    public TextMeshProUGUI boatCargoText;

    [Header("--------상점 탭")]
    public GameObject sailtab;
    public GameObject cargotab;

    [Header("--------퀘스트 탭")] 
    public GameObject basictab;
    public GameObject specialtab;

    [Header("--------설정 탭")]
    public GameObject soundTab;
    public GameObject saveTab;
    public GameObject gameInfortab;

    public GameObject chestInformation;
    public GameObject informationSettingButtons;
    public GameObject reviewTab;

    //이스터애그
    [Header("--------이스터애그")]
    public GameObject CatRotate;
    public GameObject catTab;
    [Header("--------세이브 메뉴")]
    public GameObject wannaSaveTab;
    public GameObject wannaLoadTab;

    [Header("--------초기화")]
    public GameObject restartTab;

    [Header("--------상자")]
    //ChestLimitation
    public Text chestLimit;
    public long chestLimitNumber;
    public GameObject sellAllButtonTab;
    public TextMeshProUGUI chestMaxText;

    [Header("--------재료 탭")]
    public GameObject materialTab;
    public TextMeshProUGUI[] itemTabText;
    public TextMeshProUGUI oceanCoinText;
    public TextMeshProUGUI earningCoinText;
    public Text[] increasingSpeedText;

    [Header("--------기타 탭")]
    public GameObject etcTab;
    public GameObject openDropdown;
    public GameObject closeDropdown;
    public GameObject minigameCaution;
    public GameObject sellPortionTab;
    [Header("--------종료탭")]
    public GameObject quitgameTab;
    [Header("--------상속")]
    public Player player;

    public GameObject[] tabs;
    public MiniGameManager minigame;
    public QuestData questdata;
    public TradingCenter trade;

    public GameObject adsSample;
    public Text adsSampelText;

    //부분판매
    public TextMeshProUGUI portionMoney;
    public TextMeshProUGUI portionText;
    public Image portionImage;
    public TextMeshProUGUI sellItemNumber;
    //총 재료 수
    public float addedAmountofItem;

    //버전
    public Text version;

    //상점에게 현재 가진 재료 정보 주기
    Item[] item;

    //파는 재료 부분
    long portion;
    long sellingNumber;
    public Sprite[] itemImage;
    IEnumerator catMove;
    public void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        addDistance();
        version.text = "Version " + Application.version + "v";
    }
    void Update()
    {
        //보트 레벨 업데이트
        boatSailText.text = "속도: " + string.Format("{0:#,###0.##}", player.speed) + "km/h";
        boatCargoText.text = "저장공간: " + string.Format("{0:#,###0}", chestLimitNumber);

        //재료 텍스트 프린트
        item = player.item;
        for(int i = 0; i < item.Length; i++) {
            materialText[i].text = string.Format("{0:#,###0}", item[i].itemNumber);
            item[i].isOverDistance(player.distance);
        }
        //재료 오르는속도 프린트
        for (int i = 0; i < item.Length - 2; i++) {
            increasingSpeedText[i].text = "현재 채굴 속도: " + string.Format("{0:#,###0.#####}", item[i].increasingSpeed) + "/초";
        }

        //재료 판매 가격 프린트
        for(int i = 0; i < item.Length; i++) {
            materialSellCostText[i].text = string.Format("{0:#,###0}", (long)item[i].itemCost * item[i].itemNumber);
        }


        //상자 용량및 통제
        for(int i = 0; i < item.Length; i++) {
            addedAmountofItem += item[i].itemNumber;
        }
        chestLimit.text = ((addedAmountofItem / chestLimitNumber) * 100).ToString("F1") + "%";
        chestMaxText.text = "저장공간: " + string.Format("{0:#,###0}", addedAmountofItem) + "/" + string.Format("{0:#,###0}", chestLimitNumber);
        if(player.getTotalmaterial() >= chestLimitNumber) {
            chestLimit.text = "100%";
            for(int i = 0; i < item.Length; i++) {
                item[i].limitOver = true;
            }
        } else {
            for(int i = 0; i < item.Length; i++) {
                item[i].limitOver = false;
            }
        }
        if(chestLimitNumber > long.MaxValue - 10000000) {
            chestLimit.fontSize = 50;
            chestMaxText.text = "저장공간: " + string.Format("{0:#,###0}", addedAmountofItem) + "/" + "\u221E";
            boatCargoText.text = "\u221E";
            chestLimit.text = "\u221E";
        } else {
            chestLimit.fontSize = 23;
        }
        addedAmountofItem = 0;

        //재료 탭 업데이트
        for(int i = 0; i < player.item.Length; i++) {
            itemTabText[i].text = string.Format("{0:#,###0}", player.item[i].itemNumber);
        }
        oceanCoinText.text = string.Format("{0:#,###0}", player.coinage.itemNumber);
        earningCoinText.text = "+" + string.Format("{0:#,###0}", player.coinageMul * 1);

        //만약 높으면 빼기
        if(player.getTotalmaterial() >= chestLimitNumber) {
            long highestNumber = 0;
            int index = 0;
            for(int i = 0; i < item.Length; i++) {
                if(item[i].itemNumber > highestNumber) {
                    highestNumber = item[i].itemNumber;
                    index = i;
                }
            }
            player.item[index].itemNumber -= (long)(player.getTotalmaterial() - chestLimitNumber);
            player.money += (long)(player.getTotalmaterial() - chestLimitNumber) * (long)player.item[index].itemCost;
        }
        

        //부분판매
        portionMoney.text = string.Format("{0:#,###0}", item[sellingNumber].addMoney((long)(item[sellingNumber].itemNumber * (portion / 10.0f)))) + "G";
        portionText.text = (portion * 10).ToString() + "%";
        sellItemNumber.text = string.Format("{0:#,###0}", (long)(item[sellingNumber].itemNumber * (portion / 10.0f))) + "개";

        //게임종료
        if(Application.platform == RuntimePlatform.Android) {
            // Check if Back was pressed this frame
            if(Input.GetKeyDown(KeyCode.Escape)) {

                quitgameTab.SetActive(true);
            }
        }
    }

    public void quitTabOpen()
    {
        quitgameTab.SetActive(true);
    }

    //거리 지속적으로 추가하기
    public void addDistance()
    {
        StartCoroutine(addDistancecoroutine());
    }
    IEnumerator addDistancecoroutine()
    {
        yield return new WaitForSeconds(1f);
        player.distance += player.speed / 3600;
        addDistance();
    }
    public void OpenedTab(string name)
    {
        if(!minigame.inGame) {
            checkIfotherTabOpened(name);
            sound.PlaySound("normalClick");
            for(int i = 0; i < tabs.Length; i++) {
                if(name.ToLower() == tabs[i].name.ToLower()) {
                    if(name == "MiniGame") {
                        if(player.distance >=100) {
                            tabs[i].SetActive(true);
                        } else{
                            sound.audioSource.Stop();
                            StartCoroutine(minigameCautionRou());
                        }
                    } else {
                        tabs[i].SetActive(true);
                    }
                    if(name == "Trading")
                        trade.exchangingCoinNumber = 10;
                    if(name == "Setting") {
                        openSettingTabs(0);
                    }
                }
            }
        }
    }
    public void closeTab(string name)
    {
        sound.PlaySound("Cancel");
        for(int i = 0; i < tabs.Length; i++) {
            if(name.ToLower() == tabs[i].name.ToLower()) {
                minigame.lackDiamond.SetActive(false);
                tabs[i].SetActive(false);
            }
        }
    }
    public void checkIfotherTabOpened(string name)
    {
        for(int i = 0; i < tabs.Length; i++) {
            if(tabs[i].activeSelf && tabs[i].name != name) {
                tabs[i].SetActive(false);
            }
        }
    }
    public void SoldPortion(int i)
    {
        sound.PlaySound("normalClick");
        portion = 5;
        sellingNumber = i;
        portionImage.sprite = itemImage[i];
        sellPortionTab.SetActive(true);
    }

    public void sold()
    {
        sound.PlaySound("BuyOrSell");
        player.money = (long)(player.money + item[sellingNumber].addMoney(item[sellingNumber].itemNumber));
        item[sellingNumber].itemNumber = 0;
        sellPortionTab.SetActive(false);
    }
    public void sellPortion()
    {
        sound.PlaySound("BuyOrSell");
        player.money = (long)(player.money + (float)(item[sellingNumber].addMoney(item[sellingNumber].itemNumber) * (float)((float)portion / 10.0f)));
        item[sellingNumber].itemNumber -= (long)(item[sellingNumber].itemNumber * (portion / 10.0f));
        sellPortionTab.SetActive(false);
    }
    public void up()
    {
        sound.PlaySound("normalClick");
        if(portion < 10)
            portion++;
    }
    public void down()
    {
        sound.PlaySound("normalClick");
        if(portion > 0)
            portion--;
    }
    public void leavePortionTab()
    {
        sound.PlaySound("Cancel");
        sellPortionTab.SetActive(false);
    }

    //Chest
    public void sellAllButton()
    {
        sound.PlaySound("normalClick");
        sellAllButtonTab.SetActive(true);
    }
    public void sellAll(string s)
    {
        sound.PlaySound("BuyOrSell");
        if(s == "yes") {
            sound.PlaySound("BuyOrSell");
            for(int i = 0; i < item.Length; i++) {
                sellingNumber = i;
                sold();
                sellAllButtonTab.SetActive(false);
            }
        } else if(s == "no") {
            sellAllButtonTab.SetActive(false);
        }
    }

    //상점 탭
    public void openSailTab()
    {
        sound.PlaySound("normalClick");
        sailtab.SetActive(true);
        cargotab.SetActive(false);
    }
    public void openCargoTab()
    {
        sound.PlaySound("normalClick");
        cargotab.SetActive(true);
        sailtab.SetActive(false);
    }

    //퀘스트 탭
    public void openbasicTab()
    {
        sound.PlaySound("normalClick");
        basictab.SetActive(true);
        specialtab.SetActive(false);
    }
    public void openSpecialTab()
    {
        sound.PlaySound("normalClick");
        specialtab.SetActive(true);
        basictab.SetActive(false);
    }

    //설정 탭
    public void openSettingTabs(int i)
    {
        if (i == 0) {
            sound.PlaySound("normalClick");
            soundTab.SetActive(true);
            saveTab.SetActive(false);
            gameInfortab.SetActive(false);

        } else if (i == 1) {
            sound.PlaySound("normalClick");
            soundTab.SetActive(false);
            saveTab.SetActive(true);
            gameInfortab.SetActive(false);

        } else if (i == 2) {
            sound.PlaySound("normalClick");
            soundTab.SetActive(false);
            saveTab.SetActive(false);
            gameInfortab.SetActive(true);
            informationSettingButtons.SetActive(true);
            catTab.SetActive(false);
            chestInformation.SetActive(false);
            if(catMove != null)
                StopCoroutine(catMove);
        }
    }

    public void openChestInformation()
    {
        sound.PlaySound("normalClick");
        chestInformation.SetActive(true);
        informationSettingButtons.SetActive(false);
    }
    public void closeChestInformation()
    {
        sound.PlaySound("normalClick");
        chestInformation.SetActive(false);
        informationSettingButtons.SetActive(true);
    }

    public void openCatTab()
    {
        sound.PlaySound("normalClick");
        catTab.SetActive(true);
        informationSettingButtons.SetActive(false);
        catMove = catSpin();
        StartCoroutine(catMove);
    }

    IEnumerator catSpin()
    {
        CatRotate.transform.position = new Vector3(231.13f, 222.27f, 0);
        CatRotate.transform.GetChild(0).gameObject.SetActive(false);
        CatRotate.transform.GetChild(1).gameObject.SetActive(false);
        CatRotate.transform.GetChild(1).gameObject.GetComponent<Text>().text = "";
        catTab.transform.GetChild(3).gameObject.SetActive(false);
        catTab.transform.GetChild(3).gameObject.GetComponent<Text>().color = new Color(0, 0, 0, 0);
        CatRotate.transform.localScale = new Vector3(-1, 1, 1);
        CatRotate.GetComponent<Image>().color = Color.white;

        while(CatRotate.transform.position.x < 370) {
            CatRotate.transform.Translate(Vector2.right * 1.5f);
            yield return new WaitForSeconds(0.01f);
        }
        while(CatRotate.transform.position.x > 350) {
            CatRotate.transform.Translate(Vector2.left * 1.5f);
            yield return new WaitForSeconds(0.01f);
        }
        while(CatRotate.transform.position.y < 450) {
            CatRotate.transform.Translate(Vector2.up * 1.5f);
            yield return new WaitForSeconds(0.01f);
        }
        CatRotate.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        CatRotate.transform.GetChild(0).gameObject.SetActive(false);
        float count = 0;

        while(count < 160) {
            CatRotate.transform.Rotate(new Vector3(0, 0, 20));
            yield return new WaitForSeconds(0.005f);
            count++;
        }
        CatRotate.transform.rotation = Quaternion.Euler(0,0,0);
        CatRotate.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        CatRotate.transform.GetChild(1).gameObject.GetComponent<Text>().text = ".";
        yield return new WaitForSeconds(0.3f);
        CatRotate.transform.GetChild(1).gameObject.GetComponent<Text>().text = "..";
        yield return new WaitForSeconds(0.3f);
        CatRotate.transform.GetChild(1).gameObject.GetComponent<Text>().text = "...";
        yield return new WaitForSeconds(2f);
        CatRotate.transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        CatRotate.transform.localScale = new Vector3(1, 1, 1);

        while(CatRotate.transform.position.x > 270) {
            CatRotate.transform.Translate(Vector2.left * 1.5f);
            yield return new WaitForSeconds(0.01f);
        }
        for(float i = 1.5f; i >= 0; i -= Time.deltaTime) {
            CatRotate.GetComponent<Image>().color = new Color(1, 1, 1, i / 1.5f);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        CatRotate.transform.localScale = new Vector3(2, 2, 1);
        for(float i = 0; i <= 1.5f; i += Time.deltaTime) {
            CatRotate.GetComponent<Image>().color = new Color(1, 1, 1, i / 1.5f);
            yield return null;
        }
        count = 0;
        while(count <= 100) {
            CatRotate.transform.Translate(new Vector3(0.5f,1f) * 2f);
            CatRotate.transform.localScale = new Vector3(2.0f - (0.02f * count), 2.0f - (0.02f * count),1);
            yield return new WaitForSeconds(0.01f);
            count++;
        }
        catTab.transform.GetChild(3).gameObject.SetActive(true);
        for(float i = 0; i <= 1.5f; i += Time.deltaTime) {
            catTab.transform.GetChild(3).gameObject.GetComponent<Text>().color = new Color(0, 0, 0, i / 1.5f);
            yield return null;
        }
    }
    public void closeCattab()
    {
        sound.PlaySound("Cancel");
        if(catMove != null)
            StopCoroutine(catMove);
        catTab.SetActive(false);
        informationSettingButtons.SetActive(true);
    }
    public void tutorialRestart()
    {
        sound.PlaySound("normalClick");
        player.tutorial.gameObject.SetActive(true);
        tabs[4].SetActive(false);       
    }
    //세이브 메뉴
    public void openWannaSaveTab()
    {
        sound.PlaySound("normalClick");
        wannaSaveTab.SetActive(true);
    }
    public void savedLocalGame()
    {
        sound.PlaySound("Cancel");
        wannaSaveTab.SetActive(false);
    }
    public void openWannaLoadTab()
    {
        sound.PlaySound("normalClick");
        wannaLoadTab.SetActive(true);
    }
    public void loadLocalGame()
    {
        sound.PlaySound("Cancel");
        wannaLoadTab.SetActive(false);
    }
    public void openRestartTab()
    {
        sound.PlaySound("normalClick");
        restartTab.SetActive(true);
    }
    public void closeRestartTab()
    {
        sound.PlaySound("Cancel");
        restartTab.SetActive(false);
    }
    //재료 탭
    public void MaterialTabClicked()
    {
        sound.PlaySound("normalClick");
        if(!materialTab.activeSelf) {
            materialTab.SetActive(true);
        } else {
            materialTab.SetActive(false);
        }
    }
    public void etcTabopen(int i)
    {
        sound.PlaySound("normalClick");
        if(i == 0) {
            etcTab.SetActive(false);
            openDropdown.SetActive(true);
            closeDropdown.SetActive(false);
        } else if(i == 1) {
            checkIfotherTabOpened("d");
            etcTab.SetActive(true);
            openDropdown.SetActive(false);
            closeDropdown.SetActive(true);
        }

    }

    public void OpenReview()
    {
        sound.PlaySound("normalClick");
        reviewTab.SetActive(true);
    }
    public void closeReview()
    {
        sound.PlaySound("Cancel");
        reviewTab.SetActive(false);
    }
    IEnumerator minigameCautionRou()
    {
        sound.PlaySound("Caution");
        minigameCaution.SetActive(true);
        yield return new WaitForSeconds(1);
        minigameCaution.SetActive(false);
    }

    //재시작
    public void restartWholegame()
    {
        SceneManager.LoadScene("Main");
    }

    public void quitgame()
    {
        Application.Quit();
    }
    public void quitTabClose()
    {
        sound.PlaySound("Cancel");
        quitgameTab.SetActive(false);
    }
}
