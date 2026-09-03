using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameData
{
    //Player
    public float gametimePlayed;
    public float speed;
    public float distance;
    public long money;
    public int diamond;
    public int minerNumber;
    public int[] setNumber;
    public long totalMaterial;
    public long[] materialNumber;
    public bool is100Over;
    public int boughtPaddleNumber;
    public bool legendaryPaddle;
    public long maxMoney;
    public bool sawEnding;
     
    //paddleShop
    public bool[] paddleisBought;
    public bool[] paddlebuyable;

    //miner
    public long minerCost;
    public int _NumberOfMine;

    //CargoShop
    public bool[] CargoisBought;
    public bool[] Cargobuyable;
    public int highestCargo;
    public int boughtCargoNumber;
    public bool legendaryBought;
    public bool legendaryCargo;
    public long chestLimitNumber;

    //Quest
    public bool[] BasicmissionIsActive;
    public bool[] BasicmissionIsCompleted;
    public bool[] SpecialmissionIsActive;
    public bool[] SpecialmissionIsCompleted;

    //Pickaxe
    public bool[] PickaxeisBought;
    public bool[] Pickaxebuyable;
    public int[] upgradeLevel;
    public float[] MoneycostNumber;
    public float[] MatcostNumber;

    //Item
    public float[] increasingSpeed;
    public int[] increasingAmount;
    public int coinageincreasingAmount;
    public long[] itemNumber;
    public float[] addCount;
    public float[] itemCost;

    //바다 주화
    public long coinage;

    //MiniGame
    public bool[] isGameBuyable;
    public bool[] isBought;
    public bool[] isSelected;
    public int scoreMul;
    public int timeManger;
    public int[] timesPlayed;
    public int numberOfMinigame;

    //background
    public string xxxTime;
    public float timer;

    //Collection
    public bool[] isFound;

    //SpecialStore
    public bool[] specialisBought;
    public bool[] specialisbuyable;
    public int frogCount;
    public int koalaCount;
    public bool catBought;

    //buff
    public bool[] isActivate;
    public float[] count;

    //tutorial
    public bool isTutorialPlayed;

    //자동저장
    public bool autoSave;

    //버프
    public bool isCoinageBuffon;
    public bool isMatBuffOn;
    public bool isAutoMinerBought;
    public bool autoBought;

    public GameData (Player player)
    {
        gametimePlayed = player.gamePlayedTime;

        speed = player.speed;
        distance = player.distance;

        money = player.money;
        diamond = player.diamond;

        legendaryBought = player.legendaryBought;
        minerNumber = player.minerShop.minerNumber;
        setNumber = player.setNumber;
        totalMaterial = player.totalMaterial;
        materialNumber = player.materialNumber;
        is100Over = player.is100Over;
        boughtPaddleNumber = player.boughtPaddleNumber;
        boughtCargoNumber = player.boughtCargoNumber;
        legendaryPaddle = player.legendaryPaddle.activeSelf;
        maxMoney = player.maxMoney;

        //paddleShop
        paddleisBought = new bool[player.paddelShop.Length];
        for(int i = 0; i < paddleisBought.Length; i++) {
            paddleisBought[i] = player.paddelShop[i].isBought;
        }

        paddlebuyable = new bool[player.paddelShop.Length];
        for(int i = 0; i < paddlebuyable.Length; i++) {
            paddlebuyable[i] = player.paddelShop[i].buyable;
        }

        //Miner
        minerCost = player.minerShop.minerCost;

        _NumberOfMine = player.minerclone._NumberOfMine;

        //Cargo
        CargoisBought = new bool[player.cargoShop.Length];
        for(int i = 0; i < CargoisBought.Length; i++) {
            CargoisBought[i] = player.cargoShop[i].isBought;
        }
        Cargobuyable = new bool[player.cargoShop.Length];
        for(int i = 0; i < Cargobuyable.Length; i++) {
            Cargobuyable[i] = player.cargoShop[i].buyable;
        }
        legendaryCargo = player.legendaryCargo.activeSelf;
        chestLimitNumber = player.manager.chestLimitNumber;

        //Quest
        BasicmissionIsActive = new bool[player.questdata.BasicquestList.Length];
        for(int i = 0; i < BasicmissionIsActive.Length; i++) {
            BasicmissionIsActive[i] = player.questdata.BasicquestList[i].isActive;
        }

        BasicmissionIsCompleted = new bool[player.questdata.BasicquestList.Length];
        for(int i = 0; i < BasicmissionIsCompleted.Length; i++) {
            BasicmissionIsCompleted[i] = player.questdata.BasicquestList[i].isCompleted;
        }

        SpecialmissionIsActive = new bool[player.questdata.SpecialquestList.Length];
        for(int i = 0; i < SpecialmissionIsActive.Length; i++) {
            SpecialmissionIsActive[i] = player.questdata.SpecialquestList[i].isActive;
        }

        SpecialmissionIsCompleted = new bool[player.questdata.SpecialquestList.Length];
        for(int i = 0; i < SpecialmissionIsCompleted.Length; i++) {
            SpecialmissionIsCompleted[i] = player.questdata.SpecialquestList[i].isCompleted;
        }

        //Pickaxe
        PickaxeisBought = new bool[player.pickaxeShop.Length];
        for(int i = 0; i < PickaxeisBought.Length; i++) {
            PickaxeisBought[i] = player.pickaxeShop[i].isBought;
        }
        Pickaxebuyable = new bool[player.pickaxeShop.Length];
        for(int i = 0; i < Pickaxebuyable.Length; i++) {
            Pickaxebuyable[i] = player.pickaxeShop[i].buyable;
        }
        upgradeLevel = new int[player.pickaxeShop.Length];
        for(int i = 0; i < upgradeLevel.Length; i++) {
            upgradeLevel[i] = player.pickaxeShop[i].upgradeLevel;
        }
        MoneycostNumber = new float[player.pickaxeShop.Length];
        for(int i = 0; i < MoneycostNumber.Length; i++) {
            MoneycostNumber[i] = player.pickaxeShop[i].costNumber[0];
        }
        MatcostNumber = new float[player.pickaxeShop.Length];
        for(int i = 0; i < MatcostNumber.Length; i++) {
            MatcostNumber[i] = player.pickaxeShop[i].costNumber[1];
        }

        //Item
        increasingSpeed = new float[player.item.Length];
        for(int i = 0; i < increasingSpeed.Length; i++) {
            increasingSpeed[i] = player.item[i].increasingSpeed;
        }
        increasingAmount = new int[player.item.Length];
        for (int i = 0; i < increasingAmount.Length; i++) {
            increasingAmount[i] = player.item[i].increasingAmount;
        }
        itemNumber = new long[player.item.Length];
        for(int i = 0; i < itemNumber.Length; i++) {
            itemNumber[i] = player.item[i].itemNumber;
        }
        addCount = new float[player.item.Length];
        for(int i = 0; i < addCount.Length; i++) {
            addCount[i] = player.item[i].addCount;
        }

        coinageincreasingAmount = player.coinage.increasingAmount;

        //바다 주화
        coinage = player.coinage.itemNumber;

        //MiniGame
        isGameBuyable = new bool[player.minigame.minigames.Length];
        for(int i = 0; i < isGameBuyable.Length;i++) {
            isGameBuyable[i] = player.minigame.minigames[i].isGameBuyable;
        }
        isBought = new bool[player.minigame.minigames.Length];
        for(int i = 0; i < isBought.Length; i++) {
            isBought[i] = player.minigame.minigames[i].isBought;
        }
        isSelected = new bool[player.minigame.minigames.Length];
        for(int i = 0; i < isSelected.Length; i++) {
            isSelected[i] = player.minigame.minigames[i].isSelected;
        }

        scoreMul = player.minigame.scoreMul;
        timeManger = player.minigame.timeManager;

        numberOfMinigame = player.minigame.numberOfMiniGame;

        timesPlayed = new int[player.minigame.minigames.Length];
        for(int i = 0; i< timesPlayed.Length; i++) {
            timesPlayed[i] = player.minigame.minigames[i].timesPlayed;
        }

        //Background
        xxxTime = player.backgroundSpeed.xxxtime;
        timer = player.backgroundSpeed.timer;

        //콜렉션
        isFound = new bool[player.collect.collect.Length];
        for(int i = 0; i < isFound.Length; i++) {
            isFound[i] = player.collect.collect[i].isFound;
        }

        //special
        specialisBought = new bool[player.special.specialItem.Length];
        for(int i = 0; i < specialisBought.Length; i++) {
            specialisBought[i] = player.special.specialItem[i].isBought;
        }
        specialisbuyable = new bool[player.special.specialItem.Length];
        for(int i = 0; i < specialisbuyable.Length; i++) {
            specialisbuyable[i] = player.special.specialItem[i].isBuyable;
        }
        catBought = player.catBought;

        isActivate = new bool[player.buff.buffs.Length];
        for(int i = 0; i < isActivate.Length; i++) {
            isActivate[i] = player.buff.buffs[i].isActivate;
        }

        count = new float[player.buff.buffs.Length];
        for(int i = 0; i < count.Length; i++) {
            count[i] = player.buff.buffs[i].count;
        }

        //tutorial
        isTutorialPlayed = player.isTutorialPlayed;

        //자동저장
        autoSave = player.autoSave;

        //엔딩
        sawEnding = player.sawEnding;

        //버프
        isCoinageBuffon = player.touchscreen.coinageBuffOn;
        isMatBuffOn = player.isMatBuffOn;
        isAutoMinerBought = player.minerclone.isAutomatic;
        autoBought = player.minerclone.autoBought;
    }
}

