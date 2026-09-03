using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

[System.Serializable]
public class Player : MonoBehaviour, IDataPersistence
{
    public SoundEffect sound;

    [Header("--------총 플레이 시간")]
    public float gamePlayedTime;

    [Header("--------속도와 거리")]
    public float speed;
    public float distance;

    [Header("--------게임 돈")]
    public long money;
    public int diamond;

    [Header("--------상속")]
    public GameManager manager;
    public BackgroundLoop backgroundSpeed;
    public QuestData questdata;
    public MiniGameManager minigame;
    public CollectionManager collect;
    public SpecialStore special;
    public GatchaManager gatcha;
    public Buff buff;
    public StartScreen startScreen;
    public Ending ending;
    public TouchScreen touchscreen;
    public CouponManager couponManager;
    public RewardAd reward;

    [Header("--------재료 모음")]
    public Item[] item;
    //item[0] = stone
    //item[1] = wood
    //item[2] = glass
    //item[3] = bronze
    //item[4] = silver
    //item[5] = gold

    [Header("--------바다 주화")]
    public Item coinage;
    public int coinageMul;

    [Header("--------기타 텍스트")]
    public TextMeshProUGUI moneyText;
    public Text diamondText;
    public Text speedText;
    public Text distanceText;

    [Header("--------노 상점 모음")]
    public Shop[] paddelShop;
    public GameObject legendaryPaddle;

    [Header("--------저장소 상점 모음")]
    public Shop[] cargoShop;
    public GameObject legendaryCargo;
    public Image legendsImage;
    public bool legendaryBought;
    public Image CargoImage;
    public Sprite BasicBag;

    [Header("--------곡괭이 상점 모음")]
    public Shop[] pickaxeShop;

    [Header("--------곡괭이 상점 모음")]
    public Shop minerShop;
    public MinerClone minerclone;

    [Header("--------세트 효과")]
    public int[] setNumber;

    [Header("--------아이템 총 갯수")]
    public long totalMaterial;

    public bool catBought;
    public long[] materialNumber;

    [Header("--------퀘스트")]
    public bool is100Over;

    public int boughtPaddleNumber;
    public int boughtCargoNumber;
    public long maxMoney;

    public bool isTutorialPlayed;
    public Tutorial tutorial;

    public bool autoSave;
    public GameObject autoCheckMark;

    public bool sawEnding;

    public GameObject legendaryPickaxeCover;
    public GameObject legendaryPickaxeCover2;
    public GameObject onOFFMiner;

    float timer;
    float time = 10;
    float totalPlayedTimer;

    public bool isMatBuffOn;

    void Awake()
    {
#if UNITY_IOS
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
#endif
    }

    public void Start()
    {
        if (reward != null)
            reward.gameObject.SetActive(true);

        if (special != null && special.specialItem != null && special.specialItem.Length > 6 && special.specialItem[6].isBought) {
            for (int i = 0; i < item.Length; i++) {
                item[i].itemCost *= 2;
            }
        }

        if (special != null && special.specialItem != null && special.specialItem.Length > 3 && special.specialItem[3].isBought) {
            for (int i = 0; i < item.Length; i++) {
                item[i].coinagePrice /= 2;
            }
        }

    }

    public void CheckTutorial()
    {
        if (!isTutorialPlayed && tutorial != null) {
            tutorial.gameObject.SetActive(true);
            isTutorialPlayed = true;
        }
    }

    void Update()
    {
        if (special != null && !special.gameObject.activeSelf && special.lackDiamond != null) {
            special.lackDiamond.SetActive(false);
        }

        if (speed <= 300) {
            backgroundSpeed.movingSpeed = (speed * 0.02f) + 0.5f;
        } else if (speed > 300) {
            backgroundSpeed.movingSpeed = (299 * 0.02f + 0.5f) + (speed * 0.002f);
        }

        if (speedText != null)
            speedText.text = "속도: " + string.Format("{0:#,###0.0#}", speed) + "km/h";
        if (distanceText != null)
            distanceText.text = "거리: " + string.Format("{0:#,##0.00}", Mathf.Round(distance * 100.0f) * 0.01f) + "km";
        if (moneyText != null)
            moneyText.text = string.Format("{0:#,###0}", money) + "G";
        if (diamondText != null)
            diamondText.text = string.Format("{0:#,###0}", diamond);

        UpdateShopMaterialInfo(paddelShop);
        UpdateShopMaterialInfo(cargoShop);
        UpdateShopMaterialInfo(pickaxeShop);

        if (minerShop != null)
            minerShop.getMoney(money);

        if (CargoImage != null) {
            if (boughtCargoNumber < 1) {
                CargoImage.sprite = BasicBag;
                CargoImage.color = Color.white;
            } else if (cargoShop != null && boughtCargoNumber - 1 >= 0 && boughtCargoNumber - 1 < cargoShop.Length && cargoShop[boughtCargoNumber - 1] != null) {
                CargoImage.sprite = cargoShop[boughtCargoNumber - 1].cargoImage;
                CargoImage.color = Color.white;
            }
        }

        if (legendaryBought && legendsImage != null && CargoImage != null) {
            legendsImage.gameObject.SetActive(true);
            CargoImage.color = legendsImage.color;
        }

        if (maxMoney <= money)
            maxMoney = money;

        if (!autoSave) {
            if (autoCheckMark != null) {
                Image autoImage = autoCheckMark.GetComponent<Image>();
                if (autoImage != null)
                    autoImage.color = new Color(0, 0, 0, 1);
            }

            timer += Time.deltaTime;
            if (timer > time && minigame != null && !minigame.inGame && gatcha != null && !gatcha.isGatching) {
                timer = 0f;
            }
        } else {
            if (autoCheckMark != null) {
                Image autoImage = autoCheckMark.GetComponent<Image>();
                if (autoImage != null)
                    autoImage.color = new Color(0, 0, 0, 0);
            }
        }

        totalPlayedTimer += Time.deltaTime;
        if (totalPlayedTimer >= 1) {
            gamePlayedTime++;
            totalPlayedTimer = 0f;
        }

        if (distance >= 40075 && !sawEnding && ending != null) {
            ending.gameObject.SetActive(true);
            ending.startEndingScene();
            sawEnding = true;
        }

        if (legendaryPaddle != null && paddelShop != null && boughtPaddleNumber < paddelShop.Length - 1) {
            legendaryPaddle.SetActive(false);
        }

        if (legendaryCargo != null && cargoShop != null && boughtCargoNumber < cargoShop.Length - 1) {
            legendaryCargo.SetActive(false);
        }

        int pickaxeApplyLength = Mathf.Min(item != null ? item.Length - 2 : 0, pickaxeShop != null ? pickaxeShop.Length : 0);
        for (int i = 0; i < pickaxeApplyLength; i++) {
            if (sawEnding) {
                item[i].increasingAmount = pickaxeShop[i].upgradeLevel + 1 + 100;
            } else {
                item[i].increasingAmount = pickaxeShop[i].upgradeLevel + 1;
            }
        }

        if (cargoShop != null && cargoShop.Length > 5 && cargoShop[4] != null && cargoShop[5] != null) {
            if (cargoShop[4].isBought && !cargoShop[5].isBought) {
                manager.chestLimitNumber = 555560000;
            }
        }
    }

    public void buyPaddel(int i)
    {
        int index = i - 1;
        if (!IsShopIndexValid(paddelShop, index))
            return;

        if (paddelShop[index].buyable && !paddelShop[index].isBought) {
            sound.PlaySound("BuyOrSell");

            boughtPaddleNumber++;
            if (legendaryPaddle != null && boughtPaddleNumber == paddelShop.Length - 1) {
                legendaryPaddle.SetActive(true);
            }

            if (catBought) {
                speed /= 1.25f;
            }

            speed += paddelShop[index].increaseSpeedNumber;

            if (catBought) {
                speed *= 1.25f;
            }

            paddelShop[index].isItemBought();

            if (HasCostIndex(paddelShop[index], 0))
                money -= (long)paddelShop[index].costNumber[0];

            addSetNumber(paddelShop[index].setName);

            SpendShopMaterials(paddelShop[index]);
        }
    }

    public void buyCargo(int i)
    {
        int index = i - 1;
        if (!IsShopIndexValid(cargoShop, index))
            return;

        if (cargoShop[index].buyable && !cargoShop[index].isBought) {
            sound.PlaySound("BuyOrSell");

            boughtCargoNumber++;
            if (legendaryCargo != null && boughtCargoNumber == cargoShop.Length - 1) {
                legendaryCargo.SetActive(true);
            }

            if (i == cargoShop.Length) {
                legendaryBought = true;
            }

            manager.chestLimitNumber += (long)cargoShop[index].increaseCargoSizeNumber;

            cargoShop[index].isItemBought();

            if (HasCostIndex(cargoShop[index], 0))
                money -= (long)cargoShop[index].costNumber[0];

            SpendShopMaterials(cargoShop[index]);
        }
    }

    public void buyPickaxe(int i)
    {
        int index = i - 1;
        if (!IsShopIndexValid(pickaxeShop, index))
            return;

        if (pickaxeShop[index].buyable && !pickaxeShop[index].isBought && pickaxeShop[index].upgradeLevel < 10) {
            sound.PlaySound("mine");

            pickaxeShop[index].upgradeLevel++;

            if (HasCostIndex(pickaxeShop[index], 0))
                money -= (long)pickaxeShop[index].costNumber[0];

            if (pickaxeShop[index].upgradeLevel == 10)
                addSetNumber(pickaxeShop[index].setName);

            SpendShopMaterials(pickaxeShop[index]);

            if (item != null && index >= 0 && index < item.Length)
                item[index].increasingAmount += 1;

            if (pickaxeShop[index].upgradeLevel <= 3 || pickaxeShop[index].upgradeLevel == 9) {
                for (int j = 0; j < pickaxeShop[index].costNumber.Length; j++) {
                    pickaxeShop[index].costNumber[j] *= Mathf.Pow(5, 1);
                }
            } else if (pickaxeShop[index].upgradeLevel >= 5 && pickaxeShop[index].upgradeLevel <= 8) {
                for (int j = 0; j < pickaxeShop[index].costNumber.Length; j++) {
                    pickaxeShop[index].costNumber[j] *= 4;
                }
            } else if (pickaxeShop[index].upgradeLevel > 3 && pickaxeShop[index].upgradeLevel < 5) {
                for (int j = 0; j < pickaxeShop[index].costNumber.Length; j++) {
                    pickaxeShop[index].costNumber[j] *= Mathf.Pow(10, 1);
                }
            }

            int textLen = Mathf.Min(pickaxeShop[index].costText != null ? pickaxeShop[index].costText.Length : 0, pickaxeShop[index].costNumber != null ? pickaxeShop[index].costNumber.Length : 0);
            for (int z = 0; z < textLen; z++) {
                if (pickaxeShop[index].costText[z] != null)
                    pickaxeShop[index].costText[z].text = string.Format("{0:#,###0}", pickaxeShop[index].costNumber[z]);
            }

            if (pickaxeShop[index].upgradeLevel == 9) {
                pickaxeShop[index].isItemBought();
            }
        }
    }

    public void buyMiner()
    {
        if (minerShop != null && minerShop.buyable) {
            sound.PlaySound("mine");

            money -= (long)minerShop.minerCost;

            for (int i = 0; i < item.Length - 2; i++) {
                if (minerShop.minerNumber < 11)
                    item[i].increasingSpeed = item[i].settingSpeed[minerShop.minerNumber - 1];
                else if (minerShop.minerNumber >= 11 && minerShop.minerNumber < 16) {
                    item[i].increasingSpeed /= 1.5f;
                }
            }

            minerclone.minerbought();

            if (minerShop.minerNumber - 1 < minerShop.minerLevel.Length)
                minerShop.minerCost = (long)minerShop.minerLevel[minerShop.minerNumber - 1];
            else {
                minerShop.minerCost += (long)(Mathf.Pow(10, 9) * 1);
            }

            minerShop.minerNumber++;
        }
    }

    public void addSetNumber(string set)
    {
        switch (set) {
            case "paddle":
                setNumber[0]++;
                break;
            case "propeller":
                setNumber[1]++;
                break;
            case "engine":
                setNumber[2]++;
                break;
            case "pet":
                setNumber[3]++;
                break;
            case "pickaxe":
                setNumber[4]++;
                break;
        }
    }

    void UpdateMinerUI()
    {
        if (minerclone.autoBought) {
            legendaryPickaxeCover2.SetActive(true);
            legendaryPickaxeCover.SetActive(true);
            onOFFMiner.SetActive(true);
        } else {
            legendaryPickaxeCover2.SetActive(false);
            legendaryPickaxeCover.SetActive(false);
            onOFFMiner.SetActive(false);
        }
    }

    public long getTotalmaterial()
    {
        totalMaterial = 0;
        for (int i = 0; i < item.Length; i++) {
            totalMaterial += item[i].itemNumber;
        }
        return totalMaterial;
    }

    public void checkAutoSave()
    {
        if (autoSave)
            autoSave = false;
        else
            autoSave = true;
    }

    public void LoadJson()
    {
        GameData data = SaveSystem.Load();
        if (data == null)
            return;

        LoadOldGameData(data);
    }

    public void Save()
    {
        SaveSystem.Save(this);
    }

    public void restartGame()
    {
        if (this.GetComponent<AudioSource>() != null)
            this.GetComponent<AudioSource>().Stop();

        if (startScreen != null) {
            startScreen.gameObject.SetActive(true);
            if (startScreen.transform.childCount > 0)
                startScreen.transform.GetChild(0).gameObject.SetActive(true);
            startScreen.loading();
        }

        gamePlayedTime = 0;
        money = 0;
        speed = 1;
        diamond = 0;
        distance = 0;
        legendaryBought = false;
        if (minerShop != null)
            minerShop.minerNumber = 1;

        for (int i = 0; i < setNumber.Length; i++) {
            setNumber[i] = 0;
        }

        totalMaterial = 0;
        is100Over = false;
        boughtPaddleNumber = 0;
        boughtCargoNumber = 0;
        if (legendaryPaddle != null)
            legendaryPaddle.SetActive(false);
        maxMoney = 0;

        for (int i = 0; i < paddelShop.Length; i++) {
            if (paddelShop[i] != null) {
                paddelShop[i].isBought = false;
                paddelShop[i].buyable = false;
            }
        }

        if (minerShop != null)
            minerShop.minerCost = 5000;

        if (minerclone != null)
            minerclone._NumberOfMine = 1;

        for (int i = 0; i < cargoShop.Length; i++) {
            if (cargoShop[i] != null) {
                cargoShop[i].isBought = false;
                cargoShop[i].buyable = false;
            }
        }

        if (legendaryCargo != null)
            legendaryCargo.SetActive(false);

        if (manager != null)
            manager.chestLimitNumber = 10000;

        for (int i = 0; i < questdata.BasicquestList.Length; i++) {
            questdata.BasicquestList[i].isActive = false;
            questdata.BasicquestList[i].isCompleted = false;
        }

        for (int i = 0; i < questdata.SpecialquestList.Length; i++) {
            questdata.SpecialquestList[i].isActive = false;
            questdata.SpecialquestList[i].isCompleted = false;
        }

        for (int i = 0; i < pickaxeShop.Length; i++) {
            if (pickaxeShop[i] != null) {
                pickaxeShop[i].isBought = false;
                pickaxeShop[i].buyable = false;
                pickaxeShop[i].upgradeLevel = 0;

                if (HasCostIndex(pickaxeShop[i], 0))
                    pickaxeShop[i].costNumber[0] = 5000;
            }
        }

        SetPickaxeMaterialCost(0, 1000);
        SetPickaxeMaterialCost(1, 800);
        SetPickaxeMaterialCost(2, 600);
        SetPickaxeMaterialCost(3, 500);
        SetPickaxeMaterialCost(4, 450);
        SetPickaxeMaterialCost(5, 350);
        SetPickaxeMaterialCost(6, 200);
        SetPickaxeMaterialCost(7, 100);
        SetPickaxeMaterialCost(8, 50);

        coinage.itemNumber = 0;
        coinageMul = 1;
        coinage.increasingAmount = 0;

        item[0].increasingSpeed = 0.5f;
        item[1].increasingSpeed = 1;
        item[2].increasingSpeed = 2;
        item[3].increasingSpeed = 3;
        item[4].increasingSpeed = 5;
        item[5].increasingSpeed = 10;
        item[6].increasingSpeed = 30;
        item[7].increasingSpeed = 45;
        item[8].increasingSpeed = 60;
        item[9].increasingSpeed = 0;

        for (int i = 0; i < item.Length; i++) {
            item[i].increasingAmount = 1;
        }

        for (int i = 0; i < item.Length; i++) {
            item[i].itemNumber = 0;
            item[i].addCount = 0;
        }

        item[0].itemCost = 1;
        item[1].itemCost = 10;
        item[2].itemCost = 20;
        item[3].itemCost = 50;
        item[4].itemCost = 100;
        item[5].itemCost = 500;
        item[6].itemCost = 3000;
        item[7].itemCost = 5000;
        item[8].itemCost = 10000;
        item[9].itemCost = 1000000;

        item[0].coinagePrice = 5;
        item[1].coinagePrice = 15;
        item[2].coinagePrice = 30;
        item[3].coinagePrice = 75;
        item[4].coinagePrice = 100;
        item[5].coinagePrice = 500;
        item[6].coinagePrice = 1500;
        item[7].coinagePrice = 5000;
        item[8].coinagePrice = 7500;
        item[9].coinagePrice = 100000000;

        for (int i = 0; i < minigame.minigames.Length; i++) {
            minigame.minigames[i].isGameBuyable = false;
            minigame.minigames[i].isBought = false;
            minigame.minigames[i].isSelected = false;
            minigame.minigames[i].timesPlayed = 2;
        }

        minigame.scoreMul = 1;
        minigame.timeManager = 10;
        minigame.numberOfMiniGame = 0;

        backgroundSpeed.xxxtime = "morning";
        backgroundSpeed.timer = 0;

        for (int i = 0; i < collect.collect.Length; i++) {
            collect.collect[i].isFound = false;
        }

        for (int i = 0; i < special.specialItem.Length; i++) {
            special.specialItem[i].isBought = false;
            special.specialItem[i].isBuyable = false;
        }

        catBought = false;

        for (int i = 0; i < buff.buffs.Length; i++) {
            buff.buffs[i].isActivate = false;
        }

        buff.StopAllCoroutines();

        if (buff.buffs[0].count > buff.buffs[0].cooldown) {
            for (int z = 0; z < item.Length; z++) {
                item[z].increasingSpeed *= 3;
            }
        }

        if (buff.buffs[1].count > buff.buffs[1].cooldown) {
            speed /= 3;
        }

        for (int i = 0; i < buff.buffs.Length; i++) {
            buff.buffs[i].isCoroutine = false;
            buff.buffs[i].count = 0;
        }

        autoSave = false;
        sawEnding = false;
        touchscreen.coinageBuffOn = false;
        isMatBuffOn = false;
    }

    public void LoadData(GameData1 data1)
    {
        if (data1 == null)
            return;

        if (!data1.NewDataSaveWayLoaded) {
            if (PlayerPrefs.HasKey("dataVersion"))
                data1.NewDataSaveWayLoaded = false;
            else
                data1.NewDataSaveWayLoaded = true;
        }

        if (!data1.NewDataSaveWayLoaded) {
            GameData data = SaveSystem.Load();
            if (data == null)
                return;

            LoadOldGameData(data);
            data1.NewDataSaveWayLoaded = true;
            SaveData(data1);
        } else {
            LoadNewGameData(data1);
            CheckTutorial();
        }

        UpdateMinerUI();
    }

    public void SaveData(GameData1 data)
    {
        if (data == null)
            return;

        data.gametimePlayed = gamePlayedTime;
        data.money = money;
        data.speed = speed;
        data.diamond = diamond;
        data.distance = distance;
        data.legendaryBought = legendaryBought;
        data.minerNumber = minerShop != null ? minerShop.minerNumber : data.minerNumber;

        for (int i = 0; i < setNumber.Length; i++) {
            SetDictionaryValue(data.setNumber, "setNumber" + i, setNumber[i]);
        }

        data.totalMaterial = totalMaterial;
        data.is100Over = is100Over;
        data.boughtPaddleNumber = boughtPaddleNumber;
        data.boughtCargoNumber = boughtCargoNumber;
        data.legendaryPaddle = legendaryPaddle != null && legendaryPaddle.activeSelf;
        data.maxMoney = maxMoney;
        data.coinage = coinage.itemNumber;
        data.isAutoMinerBought = minerclone != null && minerclone.isAutomatic;
        data.autoBought = minerclone != null && minerclone.autoBought;

        for (int i = 0; i < collect.collect.Length; i++) {
            SetDictionaryValue(data.isFound, "Collection" + i, collect.collect[i].isFound);
        }

        for (int i = 0; i < paddelShop.Length; i++) {
            if (paddelShop[i] == null || string.IsNullOrWhiteSpace(paddelShop[i].id))
                continue;

            SetDictionaryValue(data.paddleisBought, paddelShop[i].id, paddelShop[i].isBought);
            SetDictionaryValue(data.paddlebuyable, paddelShop[i].id, paddelShop[i].buyable);
        }

        if (minerShop != null)
            data.minerCost = minerShop.minerCost;
        if (minerclone != null)
            data._NumberOfMine = minerclone._NumberOfMine;

        for (int i = 0; i < cargoShop.Length; i++) {
            if (cargoShop[i] == null || string.IsNullOrWhiteSpace(cargoShop[i].id))
                continue;

            SetDictionaryValue(data.CargoisBought, cargoShop[i].id, cargoShop[i].isBought);
            SetDictionaryValue(data.Cargobuyable, cargoShop[i].id, cargoShop[i].buyable);
        }

        data.legendaryCargo = legendaryCargo != null && legendaryCargo.activeSelf;
        data.chestLimitNumber = manager != null ? manager.chestLimitNumber : data.chestLimitNumber;

        for (int i = 0; i < questdata.BasicquestList.Length; i++) {
            SetDictionaryValue(data.BasicmissionIsActive, questdata.BasicquestList[i].Title, questdata.BasicquestList[i].isActive);
            SetDictionaryValue(data.BasicmissionIsCompleted, questdata.BasicquestList[i].Title, questdata.BasicquestList[i].isCompleted);
        }

        for (int i = 0; i < questdata.SpecialquestList.Length; i++) {
            SetDictionaryValue(data.SpecialmissionIsActive, questdata.SpecialquestList[i].Title, questdata.SpecialquestList[i].isActive);
            SetDictionaryValue(data.SpecialmissionIsCompleted, questdata.SpecialquestList[i].Title, questdata.SpecialquestList[i].isCompleted);
        }

        data.coinageincreasingAmount = coinage.increasingAmount;

        for (int i = 0; i < pickaxeShop.Length; i++) {
            if (pickaxeShop[i] == null || string.IsNullOrWhiteSpace(pickaxeShop[i].id))
                continue;

            SetDictionaryValue(data.PickaxeisBought, pickaxeShop[i].id, pickaxeShop[i].isBought);
            SetDictionaryValue(data.Pickaxebuyable, pickaxeShop[i].id, pickaxeShop[i].buyable);
            SetDictionaryValue(data.upgradeLevel, pickaxeShop[i].id, pickaxeShop[i].upgradeLevel);

            if (HasCostIndex(pickaxeShop[i], 0))
                SetDictionaryValue(data.MoneycostNumber, pickaxeShop[i].id, pickaxeShop[i].costNumber[0]);

            if (HasCostIndex(pickaxeShop[i], 1))
                SetDictionaryValue(data.MatcostNumber, pickaxeShop[i].id, pickaxeShop[i].costNumber[1]);
        }

        for (int i = 0; i < minigame.minigames.Length; i++) {
            string key = "MiniGame_" + i;

            SetDictionaryValue(data.isGameBuyable, key, minigame.minigames[i].isGameBuyable);
            SetDictionaryValue(data.isBought, key, minigame.minigames[i].isBought);
            SetDictionaryValue(data.isSelected, key, minigame.minigames[i].isSelected);
            SetDictionaryValue(data.timesPlayed, key, minigame.minigames[i].timesPlayed);
        }

        data.scoreMul = minigame.scoreMul;
        data.timeManger = minigame.timeManager;
        data.numberOfMinigame = minigame.numberOfMiniGame;
        data.xxxTime = backgroundSpeed.xxxtime;
        data.timer = backgroundSpeed.timer;

        for (int i = 0; i < special.specialItem.Length; i++) {
            SetDictionaryValue(data.specialisBought, special.specialItem[i].abilityName, special.specialItem[i].isBought);
            SetDictionaryValue(data.specialisbuyable, special.specialItem[i].abilityName, special.specialItem[i].isBuyable);
        }

        data.catBought = catBought;

        for (int i = 0; i < buff.buffs.Length; i++) {
            SetDictionaryValue(data.isActivate, buff.buffs[i].buffImage.name, buff.buffs[i].isActivate);
            SetDictionaryValue(data.count, buff.buffs[i].buffImage.name, buff.buffs[i].count);
        }

        data.isTutorialPlayed = isTutorialPlayed;
        data.autoSave = autoSave;
        data.sawEnding = sawEnding;
        data.isMatBuffOn = isMatBuffOn;

        for (int i = 0; i < item.Length; i++) {
            if (!string.IsNullOrWhiteSpace(item[i].id)) {
                SetDictionaryValue(data.increasingSpeed, item[i].id, item[i].increasingSpeed);
                SetDictionaryValue(data.itemNumber, item[i].id, item[i].itemNumber);
                SetDictionaryValue(data.addCount, item[i].id, item[i].addCount);
            }
        }
    }

    void UpdateShopMaterialInfo(Shop[] shops)
    {
        if (shops == null || item == null)
            return;

        for (int i = 0; i < shops.Length; i++) {
            if (shops[i] == null)
                continue;

            shops[i].getMoney(money);

            int neededCount = Mathf.Max(0, shops[i].numberOfMaterials);
            long[] localMaterials = new long[neededCount];

            for (int v = 0; v < neededCount; v++) {
                string targetName = "";
                if (shops[i].NAME_OF_ITEM != null && v < shops[i].NAME_OF_ITEM.Length)
                    targetName = shops[i].NAME_OF_ITEM[v];

                for (int j = 0; j < item.Length; j++) {
                    if (item[j] != null && item[j].itemName == targetName) {
                        localMaterials[v] = item[j].itemNumber;
                        break;
                    }
                }
            }

            shops[i].getMaterialNumber(localMaterials);
        }
    }

    void SpendShopMaterials(Shop shop)
    {
        if (shop == null || item == null || shop.NAME_OF_ITEM == null || shop.costNumber == null)
            return;

        for (int j = 0; j < item.Length; j++) {
            for (int v = 0; v < shop.NAME_OF_ITEM.Length; v++) {
                int costIndex = v + 1;
                if (costIndex >= shop.costNumber.Length)
                    continue;

                if (item[j].itemName == shop.NAME_OF_ITEM[v]) {
                    item[j].itemNumber -= (int)shop.costNumber[costIndex];
                }
            }
        }
    }

    void LoadOldGameData(GameData data)
    {
        gamePlayedTime = data.gametimePlayed;
        money = data.money;
        speed = data.speed;
        diamond = data.diamond;
        distance = data.distance;
        legendaryBought = data.legendaryBought;
        if (minerShop != null)
            minerShop.minerNumber = data.minerNumber;

        for (int i = 0; i < Mathf.Min(setNumber.Length, data.setNumber.Length); i++) {
            setNumber[i] = data.setNumber[i];
        }

        totalMaterial = data.totalMaterial;
        is100Over = data.is100Over;
        boughtPaddleNumber = data.boughtPaddleNumber;
        boughtCargoNumber = data.boughtCargoNumber;

        if (legendaryPaddle != null)
            legendaryPaddle.SetActive(data.legendaryPaddle);

        maxMoney = data.maxMoney;
        coinage.itemNumber = data.coinage;
        if (minerclone != null) {
            minerclone.isAutomatic = data.isAutoMinerBought;
            minerclone.autoBought = data.autoBought;
        }

        for (int i = 0; i < collect.collect.Length; i++) {
            if (i < data.isFound.Length)
                collect.collect[i].isFound = data.isFound[i];
        }

        for (int i = 0; i < paddelShop.Length; i++) {
            if (i < data.paddleisBought.Length)
                paddelShop[i].isBought = data.paddleisBought[i];
            if (i < data.paddlebuyable.Length)
                paddelShop[i].buyable = data.paddlebuyable[i];
        }

        if (minerShop != null)
            minerShop.minerCost = data.minerCost;
        if (minerclone != null)
            minerclone._NumberOfMine = data._NumberOfMine;

        for (int i = 0; i < cargoShop.Length; i++) {
            if (i < data.CargoisBought.Length)
                cargoShop[i].isBought = data.CargoisBought[i];
            if (i < data.Cargobuyable.Length)
                cargoShop[i].buyable = data.Cargobuyable[i];
        }

        if (legendaryCargo != null)
            legendaryCargo.SetActive(data.legendaryCargo);

        if (manager != null)
            manager.chestLimitNumber = data.chestLimitNumber;

        for (int i = 0; i < questdata.BasicquestList.Length; i++) {
            if (i < data.BasicmissionIsActive.Length)
                questdata.BasicquestList[i].isActive = data.BasicmissionIsActive[i];
            if (i < data.BasicmissionIsCompleted.Length)
                questdata.BasicquestList[i].isCompleted = data.BasicmissionIsCompleted[i];
        }

        for (int i = 0; i < questdata.SpecialquestList.Length; i++) {
            if (i < data.SpecialmissionIsActive.Length)
                questdata.SpecialquestList[i].isActive = data.SpecialmissionIsActive[i];
            if (i < data.SpecialmissionIsCompleted.Length)
                questdata.SpecialquestList[i].isCompleted = data.SpecialmissionIsCompleted[i];
        }

        coinage.increasingAmount = data.coinageincreasingAmount;

        for (int i = 0; i < pickaxeShop.Length; i++) {
            if (i < data.PickaxeisBought.Length)
                pickaxeShop[i].isBought = data.PickaxeisBought[i];
            if (i < data.Pickaxebuyable.Length)
                pickaxeShop[i].buyable = data.Pickaxebuyable[i];
            if (i < data.upgradeLevel.Length)
                pickaxeShop[i].upgradeLevel = data.upgradeLevel[i];
            if (i < data.MoneycostNumber.Length && HasCostIndex(pickaxeShop[i], 0))
                pickaxeShop[i].costNumber[0] = data.MoneycostNumber[i];
            if (i < data.MatcostNumber.Length && HasCostIndex(pickaxeShop[i], 1))
                pickaxeShop[i].costNumber[1] = data.MatcostNumber[i];
        }

        for (int i = 0; i < minigame.minigames.Length; i++) {
            if (i < data.isGameBuyable.Length)
                minigame.minigames[i].isGameBuyable = data.isGameBuyable[i];
            if (i < data.isBought.Length)
                minigame.minigames[i].isBought = data.isBought[i];
            if (i < data.isSelected.Length)
                minigame.minigames[i].isSelected = data.isSelected[i];
            if (i < data.timesPlayed.Length)
                minigame.minigames[i].timesPlayed = data.timesPlayed[i];
        }

        minigame.scoreMul = data.scoreMul;
        minigame.timeManager = data.timeManger;
        minigame.numberOfMiniGame = data.numberOfMinigame;
        backgroundSpeed.xxxtime = data.xxxTime;
        backgroundSpeed.timer = data.timer;

        for (int i = 0; i < special.specialItem.Length; i++) {
            if (i < data.specialisBought.Length)
                special.specialItem[i].isBought = data.specialisBought[i];
            if (i < data.specialisbuyable.Length)
                special.specialItem[i].isBuyable = data.specialisbuyable[i];
        }

        catBought = data.catBought;

        for (int i = 0; i < buff.buffs.Length; i++) {
            if (i < data.isActivate.Length)
                buff.buffs[i].isActivate = data.isActivate[i];
        }

        buff.StopAllCoroutines();

        if (buff.buffs[0].count > buff.buffs[0].cooldown) {
            for (int z = 0; z < item.Length; z++) {
                item[z].increasingSpeed *= 3;
            }
        }

        if (buff.buffs[1].count > buff.buffs[1].cooldown) {
            speed /= 3;
        }

        for (int i = 0; i < buff.buffs.Length; i++) {
            buff.buffs[i].isCoroutine = false;
            if (i < data.count.Length)
                buff.buffs[i].count = data.count[i];
        }

        isTutorialPlayed = data.isTutorialPlayed;
        autoSave = data.autoSave;
        sawEnding = data.sawEnding;
        isMatBuffOn = data.isMatBuffOn;

        if (touchscreen.coinageBuffOn) {
            coinageMul /= 2;
            touchscreen.coinageBuffOn = false;
        }

        if (isMatBuffOn) {
            for (int i = 0; i < item.Length; i++) {
                item[i].increasingAmount /= 10;
                item[i].increasingSpeed *= 10;
            }
            isMatBuffOn = false;
        }

        for (int i = 0; i < item.Length; i++) {
            if (i < data.increasingSpeed.Length)
                item[i].increasingSpeed = data.increasingSpeed[i];
        }

        for (int i = 0; i < item.Length - 2 && i < pickaxeShop.Length; i++) {
            item[i].increasingAmount = pickaxeShop[i].upgradeLevel + 1;
        }

        for (int i = 0; i < item.Length; i++) {
            if (i < data.itemNumber.Length)
                item[i].itemNumber = data.itemNumber[i];
            if (i < data.addCount.Length)
                item[i].addCount = data.addCount[i];
        }
    }

    void LoadNewGameData(GameData1 data1)
    {
        gamePlayedTime = data1.gametimePlayed;
        money = data1.money;
        speed = data1.speed;
        diamond = data1.diamond;
        distance = data1.distance;
        legendaryBought = data1.legendaryBought;

        if (minerShop != null)
            minerShop.minerNumber = data1.minerNumber;

        for (int i = 0; i < setNumber.Length; i++) {
            setNumber[i] = GetDictionaryValue(data1.setNumber, "setNumber" + i, setNumber[i]);
        }

        totalMaterial = data1.totalMaterial;
        is100Over = data1.is100Over;
        boughtPaddleNumber = data1.boughtPaddleNumber;
        boughtCargoNumber = data1.boughtCargoNumber;

        if (legendaryPaddle != null)
            legendaryPaddle.SetActive(data1.legendaryPaddle);

        maxMoney = data1.maxMoney;
        coinage.itemNumber = data1.coinage;

        if (minerclone != null) {
            minerclone.isAutomatic = data1.isAutoMinerBought;
            minerclone.autoBought = data1.autoBought;

            
        }

        for (int i = 0; i < collect.collect.Length; i++) {
            collect.collect[i].isFound = GetDictionaryValue(data1.isFound, "Collection" + i, collect.collect[i].isFound);
        }

        for (int i = 0; i < paddelShop.Length; i++) {
            string key = paddelShop[i] != null ? paddelShop[i].id : "";
            paddelShop[i].isBought = GetDictionaryValue(data1.paddleisBought, key, paddelShop[i].isBought);
            paddelShop[i].buyable = GetDictionaryValue(data1.paddlebuyable, key, paddelShop[i].buyable);
        }

        if (minerShop != null)
            minerShop.minerCost = data1.minerCost;
        if (minerclone != null)
            minerclone._NumberOfMine = data1._NumberOfMine;

        for (int i = 0; i < cargoShop.Length; i++) {
            string key = cargoShop[i] != null ? cargoShop[i].id : "";
            cargoShop[i].isBought = GetDictionaryValue(data1.CargoisBought, key, cargoShop[i].isBought);
            cargoShop[i].buyable = GetDictionaryValue(data1.Cargobuyable, key, cargoShop[i].buyable);
        }

        if (legendaryCargo != null)
            legendaryCargo.SetActive(data1.legendaryCargo);

        if (manager != null)
            manager.chestLimitNumber = data1.chestLimitNumber;

        for (int i = 0; i < questdata.BasicquestList.Length; i++) {
            string key = questdata.BasicquestList[i].Title;
            questdata.BasicquestList[i].isActive = GetDictionaryValue(data1.BasicmissionIsActive, key, questdata.BasicquestList[i].isActive);
            questdata.BasicquestList[i].isCompleted = GetDictionaryValue(data1.BasicmissionIsCompleted, key, questdata.BasicquestList[i].isCompleted);
        }

        for (int i = 0; i < questdata.SpecialquestList.Length; i++) {
            string key = questdata.SpecialquestList[i].Title;
            questdata.SpecialquestList[i].isActive = GetDictionaryValue(data1.SpecialmissionIsActive, key, questdata.SpecialquestList[i].isActive);
            questdata.SpecialquestList[i].isCompleted = GetDictionaryValue(data1.SpecialmissionIsCompleted, key, questdata.SpecialquestList[i].isCompleted);
        }

        coinage.increasingAmount = data1.coinageincreasingAmount;

        for (int i = 0; i < pickaxeShop.Length; i++) {
            string key = pickaxeShop[i] != null ? pickaxeShop[i].id : "";

            pickaxeShop[i].isBought = GetDictionaryValue(data1.PickaxeisBought, key, pickaxeShop[i].isBought);
            pickaxeShop[i].buyable = GetDictionaryValue(data1.Pickaxebuyable, key, pickaxeShop[i].buyable);
            pickaxeShop[i].upgradeLevel = GetDictionaryValue(data1.upgradeLevel, key, pickaxeShop[i].upgradeLevel);

            if (HasCostIndex(pickaxeShop[i], 0)) {
                float loadedMoneyCost = GetDictionaryValue(data1.MoneycostNumber, key, pickaxeShop[i].costNumber[0]);
                if (loadedMoneyCost > 0)
                    pickaxeShop[i].costNumber[0] = loadedMoneyCost;
            }

            if (HasCostIndex(pickaxeShop[i], 1)) {
                float loadedMatCost = GetDictionaryValue(data1.MatcostNumber, key, pickaxeShop[i].costNumber[1]);
                if (loadedMatCost > 0)
                    pickaxeShop[i].costNumber[1] = loadedMatCost;
            }
        }

        for (int i = 0; i < minigame.minigames.Length; i++) {
            string key = "MiniGame_" + i;

            minigame.minigames[i].isGameBuyable = GetDictionaryValue(data1.isGameBuyable, key, minigame.minigames[i].isGameBuyable);
            minigame.minigames[i].isBought = GetDictionaryValue(data1.isBought, key, minigame.minigames[i].isBought);
            minigame.minigames[i].isSelected = GetDictionaryValue(data1.isSelected, key, minigame.minigames[i].isSelected);
            minigame.minigames[i].timesPlayed = GetDictionaryValue(data1.timesPlayed, key, minigame.minigames[i].timesPlayed);
        }

        minigame.scoreMul = data1.scoreMul;
        minigame.timeManager = data1.timeManger;
        minigame.numberOfMiniGame = data1.numberOfMinigame;
        backgroundSpeed.xxxtime = data1.xxxTime;
        backgroundSpeed.timer = data1.timer;

        for (int i = 0; i < special.specialItem.Length; i++) {
            string key = special.specialItem[i].abilityName;
            special.specialItem[i].isBought = GetDictionaryValue(data1.specialisBought, key, special.specialItem[i].isBought);
            special.specialItem[i].isBuyable = GetDictionaryValue(data1.specialisbuyable, key, special.specialItem[i].isBuyable);
        }

        catBought = data1.catBought;

        for (int i = 0; i < buff.buffs.Length; i++) {
            string key = buff.buffs[i].buffImage.name;
            buff.buffs[i].isActivate = GetDictionaryValue(data1.isActivate, key, buff.buffs[i].isActivate);
        }

        buff.StopAllCoroutines();

        if (buff.buffs[0].count > buff.buffs[0].cooldown) {
            for (int z = 0; z < item.Length; z++) {
                item[z].increasingSpeed *= 3;
            }
        }

        if (buff.buffs[1].count > buff.buffs[1].cooldown) {
            speed /= 3;
        }

        for (int i = 0; i < buff.buffs.Length; i++) {
            buff.buffs[i].isCoroutine = false;
            buff.buffs[i].count = GetDictionaryValue(data1.count, buff.buffs[i].buffImage.name, buff.buffs[i].count);
        }

        isTutorialPlayed = data1.isTutorialPlayed;
        autoSave = data1.autoSave;
        sawEnding = data1.sawEnding;
        isMatBuffOn = data1.isMatBuffOn;

        if (touchscreen.coinageBuffOn) {
            coinageMul /= 2;
            touchscreen.coinageBuffOn = false;
        }

        if (isMatBuffOn) {
            for (int i = 0; i < item.Length; i++) {
                item[i].increasingAmount /= 10;
                item[i].increasingSpeed *= 10;
            }
            isMatBuffOn = false;
        }

        for (int i = 0; i < item.Length; i++) {
            if (!string.IsNullOrWhiteSpace(item[i].id)) {
                item[i].increasingSpeed = GetDictionaryValue(data1.increasingSpeed, item[i].id, item[i].increasingSpeed);
                item[i].itemNumber = GetDictionaryValue(data1.itemNumber, item[i].id, item[i].itemNumber);
                item[i].addCount = GetDictionaryValue(data1.addCount, item[i].id, item[i].addCount);
            }
        }

        for (int i = 0; i < item.Length - 2 && i < pickaxeShop.Length; i++) {
            item[i].increasingAmount = pickaxeShop[i].upgradeLevel + 1;
        }
    }


    bool IsShopIndexValid(Shop[] shops, int index)
    {
        return shops != null && index >= 0 && index < shops.Length && shops[index] != null;
    }

    bool HasCostIndex(Shop shop, int index)
    {
        return shop != null && shop.costNumber != null && index >= 0 && index < shop.costNumber.Length;
    }

    void SetPickaxeMaterialCost(int index, float value)
    {
        if (pickaxeShop == null || index < 0 || index >= pickaxeShop.Length || pickaxeShop[index] == null)
            return;

        if (HasCostIndex(pickaxeShop[index], 1))
            pickaxeShop[index].costNumber[1] = value;
    }

    T GetDictionaryValue<T>(Dictionary<string, T> dictionary, string key, T defaultValue)
    {
        if (dictionary == null || string.IsNullOrWhiteSpace(key))
            return defaultValue;

        if (dictionary.TryGetValue(key, out T value))
            return value;

        return defaultValue;
    }

    void SetDictionaryValue<T>(Dictionary<string, T> dictionary, string key, T value)
    {
        if (dictionary == null || string.IsNullOrWhiteSpace(key))
            return;

        dictionary[key] = value;
    }
}