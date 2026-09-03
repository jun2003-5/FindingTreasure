using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData1
{
    public bool NewDataSaveWayLoaded;

    //Player
    public float gametimePlayed;
    public float speed;
    public float distance;
    public long money;
    public int diamond;
    public int minerNumber;
    public SerializableDictionary<string, int> setNumber;
    public long totalMaterial;
    public SerializableDictionary<string, long> materialNumber;
    public bool is100Over;
    public int boughtPaddleNumber;
    public bool legendaryPaddle;
    public long maxMoney;
    public bool sawEnding;

    //paddleShop
    public SerializableDictionary<string, bool> paddleisBought;
    public SerializableDictionary<string, bool> paddlebuyable;

    //miner
    public long minerCost;
    public int _NumberOfMine;

    //CargoShop
    public SerializableDictionary<string, bool> CargoisBought;
    public SerializableDictionary<string, bool> Cargobuyable;
    public int highestCargo;
    public int boughtCargoNumber;
    public bool legendaryBought;
    public bool legendaryCargo;
    public long chestLimitNumber;

    //Quest
    public SerializableDictionary<string, bool> BasicmissionIsActive;
    public SerializableDictionary<string, bool> BasicmissionIsCompleted;
    public SerializableDictionary<string, bool> SpecialmissionIsActive;
    public SerializableDictionary<string, bool> SpecialmissionIsCompleted;

    //Pickaxe
    public SerializableDictionary<string, bool> PickaxeisBought;
    public SerializableDictionary<string, bool> Pickaxebuyable;
    public SerializableDictionary<string, int> upgradeLevel;
    public SerializableDictionary<string, float> MoneycostNumber;
    public SerializableDictionary<string, float> MatcostNumber;

    //Item
    public SerializableDictionary<string, float> increasingSpeed;
    public SerializableDictionary<string, int> increasingAmount;
    public int coinageincreasingAmount;
    public SerializableDictionary<string, long> itemNumber;
    public SerializableDictionary<string, float> addCount;
    public SerializableDictionary<string, float> itemCost;

    //바다 주화
    public long coinage;

    //MiniGame
    public SerializableDictionary<string, bool> isGameBuyable;
    public SerializableDictionary<string, bool> isBought;
    public SerializableDictionary<string, bool> isSelected;
    public int scoreMul;
    public int timeManger;
    public SerializableDictionary<string, int> timesPlayed;
    public int numberOfMinigame;

    //background
    public string xxxTime;
    public float timer;

    //Collection
    public SerializableDictionary<string, bool> isFound;

    //SpecialStore
    public SerializableDictionary<string, bool> specialisBought;
    public SerializableDictionary<string, bool> specialisbuyable;
    public int frogCount;
    public int koalaCount;
    public bool catBought;

    //buff
    public SerializableDictionary<string, bool> isActivate;
    public SerializableDictionary<string, float> count;

    //tutorial
    public bool isTutorialPlayed;

    //자동저장
    public bool autoSave;

    //버프
    public bool isCoinageBuffon;
    public bool isMatBuffOn;
    public bool isAutoMinerBought;
    public bool autoBought;


    //Default values
    public GameData1()
    {
        NewDataSaveWayLoaded = false;

        gametimePlayed = 0;
        speed = 1;
        distance = 0;
        money = 0;
        diamond = 0;
        minerNumber = 1;
        setNumber = new SerializableDictionary<string, int>();
        totalMaterial = 0;
        materialNumber = new SerializableDictionary<string, long>();
        is100Over = false;
        boughtPaddleNumber = 0;
        legendaryPaddle = false;
        maxMoney = 0;
        sawEnding = false;

        paddleisBought = new SerializableDictionary<string, bool>();
        paddlebuyable = new SerializableDictionary<string, bool>();

        minerCost = 5000;
        _NumberOfMine = 1;

        CargoisBought = new SerializableDictionary<string, bool>();
        Cargobuyable = new SerializableDictionary<string, bool>();

        highestCargo = 0;
        boughtCargoNumber = 0;
        legendaryBought = false;
        legendaryCargo = false;

        chestLimitNumber = 10000;

        BasicmissionIsActive = new SerializableDictionary<string, bool>();
        BasicmissionIsCompleted = new SerializableDictionary<string, bool>();
        SpecialmissionIsActive = new SerializableDictionary<string, bool>();
        SpecialmissionIsCompleted = new SerializableDictionary<string, bool>();

        PickaxeisBought = new SerializableDictionary<string, bool>();
        Pickaxebuyable = new SerializableDictionary<string, bool>();
        upgradeLevel = new SerializableDictionary<string, int>();
        MoneycostNumber = new SerializableDictionary<string, float>();
        MatcostNumber = new SerializableDictionary<string, float>();

        increasingSpeed = new SerializableDictionary<string, float>();
        increasingAmount = new SerializableDictionary<string, int>();
        coinageincreasingAmount = 0;
        itemNumber = new SerializableDictionary<string, long>();
        addCount = new SerializableDictionary<string, float>();
        itemCost = new SerializableDictionary<string, float>();

        coinage = 0;

        isGameBuyable = new SerializableDictionary<string, bool>();
        isBought = new SerializableDictionary<string, bool>();
        isSelected = new SerializableDictionary<string, bool>();
        scoreMul = 1;
        timeManger = 10;
        timesPlayed = new SerializableDictionary<string, int>();
        numberOfMinigame = 0;

        xxxTime = "morning";
        timer = 0;
        isFound = new SerializableDictionary<string, bool>();

        specialisBought = new SerializableDictionary<string, bool>();
        specialisbuyable = new SerializableDictionary<string, bool>();
        frogCount = 0;
        koalaCount = 0;
        catBought = false;

        isActivate = new SerializableDictionary<string, bool>();
        count = new SerializableDictionary<string, float>();

        isTutorialPlayed = false;

        autoSave = true;

        isCoinageBuffon = false;
        isMatBuffOn = false;
        isAutoMinerBought = false;
        autoBought = false;
    }
}
