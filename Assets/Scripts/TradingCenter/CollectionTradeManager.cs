using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using System.Linq;

public class CollectionTradeManager : MonoBehaviour
{
    public Player player;
    public CollectionManager CollectionManager;

    public SoundEffect sound;

    public TextMeshProUGUI CreatingPaperNumberText;

    public TextMeshProUGUI CurrentPaperNumberText;

    public GameObject[] ChestImages;

    [Header("RequireNumber For each Chest")]
    public long[] RequiredPaperNum;
    public int index_RequiredPaperNum;

    [Header("Caution")]
    public GameObject CautionTab;

    [Header("Chest Animation")]
    public GameObject ChestOpenTab;
    public ChestAnimation Chest;

    public FadeInOut FadeTab;

    [Header("ItemImage")]
    public Image ItemImage;
    public Image ItemFrame;

    [Header("Gameobject")]
    public Text GradeText;
    public GameObject Button;

    public TextMeshProUGUI CurrentDqd;

    public List<Collection> list_Collection;

    // Update is called once per frame
    void Update()
    {
        CreatingPaperNumberText.text = RequiredPaperNum[index_RequiredPaperNum].ToString();
        CurrentPaperNumberText.text = "제작서 " + string.Format("{0:#,###0}",player.item[10].itemNumber) + "개 보유중";
    }

    public void SetExchangingChest(int index)
    {
        for (int i = 0; i < ChestImages.Length; i++) {
            ChestImages[i].SetActive(i == index);
        }
        index_RequiredPaperNum = index;
    }

    public void Exchange()
    {
        if (player.item[10].itemNumber >= RequiredPaperNum[index_RequiredPaperNum]) {

            player.item[10].itemNumber -= RequiredPaperNum[index_RequiredPaperNum];
            //Settings
            Chest.gameObject.SetActive(true);
            Chest.mCurrentFrame = 0;
            ItemFrame.gameObject.SetActive(false);
            GradeText.gameObject.SetActive(false);
            Button.SetActive(false);
            list_Collection.Clear();
            ChestOpenTab.SetActive(true);
            GatchaAnime();
        } else {
            CurrentDqd.text = "현재 보유량: " + player.item[10].itemNumber + "개";
            CautionTab.SetActive(true);
        }

    }

    public async void GatchaAnime()
    {
        Chest.type = index_RequiredPaperNum + 1;
        Chest.starting();
        await Task.Delay(2500);
        FadeTab.startFading();
        await Task.Delay(1000);
        Chest.gameObject.SetActive(false);
        GradeText.gameObject.SetActive(true);
        ItemFrame.gameObject.SetActive(true);
        Button.SetActive(true);
        //Item Gatcha
        if (index_RequiredPaperNum == 0)
            SetCollectionRandom(-1, -1, -1, 30);
        else if (index_RequiredPaperNum == 1)
            SetCollectionRandom(-1, -1, 5, 50);
        else if (index_RequiredPaperNum == 2)
            SetCollectionRandom(-1, 10, 100, -1);
    }

    public void SetCollectionRandom(int percentage1, int percentage2, int percentage3, int percentage4)
    {

        int percentage = Random.Range(0, 101);
        //레전더리
        if (percentage <= percentage1) {
            for (int i = 0; i < CollectionManager.collect.Length; i++) {
                if (CollectionManager.collect[i]._class == "legend")
                    list_Collection.Add(CollectionManager.collect[i]);
            }
            int random = Random.Range(0, list_Collection.Count);
            while (list_Collection[random].isFound) {
                random = Random.Range(0, list_Collection.Count);
            }
            ItemImage.sprite = list_Collection[random].itemImage;
            ItemFrame.color = Color.red;
            GradeText.color = Color.red;
            GradeText.text = "레전더리";
            list_Collection[random].isFound = true;
            //유니크
        } else if (percentage <= percentage2 && percentage > percentage1) {
            for (int i = 0; i < CollectionManager.collect.Length; i++) {
                if (CollectionManager.collect[i]._class == "unique")
                    list_Collection.Add(CollectionManager.collect[i]);
            }
            int random = Random.Range(0, list_Collection.Count);
            while (list_Collection[random].isFound) {
                random = Random.Range(0, list_Collection.Count);
            }
            ItemImage.sprite = list_Collection[random].itemImage;
            ItemFrame.color = Color.yellow;
            GradeText.color = Color.yellow;
            GradeText.text = "유니크";
            list_Collection[random].isFound = true;
            //에픽
        } else if (percentage > percentage2 && percentage <= percentage3) {
            for (int i = 0; i < CollectionManager.collect.Length; i++) {
                if (CollectionManager.collect[i]._class == "epic")
                    list_Collection.Add(CollectionManager.collect[i]);
            }
            int random = Random.Range(0, list_Collection.Count);
            while (list_Collection[random].isFound) {
                random = Random.Range(0, list_Collection.Count);
            }
            ItemImage.sprite = list_Collection[random].itemImage;
            ItemFrame.color = Color.magenta;
            GradeText.color = Color.magenta;
            GradeText.text = "에픽";
            list_Collection[random].isFound = true;
        }
        //레어
        else if (percentage > percentage3 && percentage <= percentage4) {
            for (int i = 0; i < CollectionManager.collect.Length; i++) {
                if (CollectionManager.collect[i]._class == "rare")
                    list_Collection.Add(CollectionManager.collect[i]);
            }
            int random = Random.Range(0, list_Collection.Count);
            while (list_Collection[random].isFound) {
                random = Random.Range(0, list_Collection.Count);
            }
            ItemImage.sprite = list_Collection[random].itemImage;
            ItemFrame.color = new Color(0.1830223f, 0.6792453f, 0.0480598f);
            GradeText.color = new Color(0.1830223f, 0.6792453f, 0.0480598f);
            GradeText.text = "레어";
            list_Collection[random].isFound = true;
            //노말
        } else if (percentage > percentage4 && percentage <= 100) {
            for (int i = 0; i < CollectionManager.collect.Length; i++) {
                if (CollectionManager.collect[i]._class == "normal")
                    list_Collection.Add(CollectionManager.collect[i]);
            }
            int random = Random.Range(0, list_Collection.Count);
            while (list_Collection[random].isFound) {
                random = Random.Range(0, list_Collection.Count);
            }
            ItemImage.sprite = list_Collection[random].itemImage;
            ItemFrame.color = Color.white;
            GradeText.color = Color.white;
            GradeText.text = "노말";
            list_Collection[random].isFound = true;
        }
    }
}
