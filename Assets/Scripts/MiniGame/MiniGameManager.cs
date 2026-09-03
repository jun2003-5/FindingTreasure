using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    public SoundEffect sound;


    public Player player;
    public Sprite[] mateiralItemImages;

    [Header("--------게임종료 UI")]
    public MiniGameData[] minigames;

    [Header("--------미니게임 준비 UI")]
    public GameObject[] buttons;
    public Text _CountNum;


    [Header("--------미니게임Tab")]
    public GameObject gameUIS;
    public GameObject clickButton;
    public GameObject itemMenu;
    public GameObject lackDiamond;

    [Header("--------미니게임 UI")]
    public RectTransform miningObject;
    public Text ScoreText;
    public Text timeText;
    public Image materialImage;

    [Header("--------게임종료 UI")]
    public GameObject endingScreen;
    public Text finalScoreText;
    public Image finalmaterial;
    public GameObject finalButtons;

    [Header("--------외부 클라스에서 접근")]
    public int timeManager;
    public int scoreMul;

    int randomNumber;
    int score;
    int timeLeft;
    bool isBig;
    public bool inGame = false;

    public int numberOfMiniGame;

    public bool gamePlayable;

    public void Start()
    {
        timeManager = 10;
        scoreMul = 1;
        score = 0;
        ScoreText.text = string.Format("{0:#,###0}", score);

        for (int i = 1; i < minigames.Length; i++) {
            if (minigames[i].MaximumGame <= 0)
                minigames[i].MaximumGame = 2;

            if (minigames[i].timesPlayed < 0)
                minigames[i].timesPlayed = 0;

            if (minigames[i].timesPlayed > minigames[i].MaximumGame)
                minigames[i].timesPlayed = minigames[i].MaximumGame;
        }
    }

    void Update()
    {
        timeText.text = timeLeft.ToString() + "초";
        for (int i = 1; i < minigames.Length; i++) {
            minigames[i].timesPlayedText.text = "선택  " + minigames[i].timesPlayed + "/2";
            minigames[i].diamondCostText.GetComponentInChildren<Text>().text = minigames[i].costDiamond + "개";
            minigames[i].Gameinformation.text = "클릭당 " + minigames[i].displayingGameName + " " + minigames[i].multiplier + "개 " + "\n" + "획득합니다.";

            if (!minigames[i].isBought || minigames[i].timesPlayed == 0) {
                minigames[i].diamondCostText.SetActive(true);
                minigames[i].selectText.SetActive(false);
                minigames[i].selectButton.color = Color.white;
                minigames[i].isBought = false;
                minigames[i].isSelected = false;
            } else if (minigames[i].isBought) {
                minigames[i].diamondCostText.SetActive(false);
                minigames[i].selectText.SetActive(true);
                gamePlayable = true;
                if (!minigames[i].isSelected) {
                    minigames[i].selectButton.color = Color.white;
                } else if (minigames[i].isSelected) {
                    minigames[i].selectButton.color = Color.green;
                }
            }
            if (minigames[i].costDiamond <= player.diamond) {
                minigames[i].isGameBuyable = true;
            } else {
                minigames[i].isGameBuyable = false;
            }
        }

        gamePlayable = false;
        for (int i = 0; i < minigames.Length; i++) {
            if (minigames[i].isBought) {
                gamePlayable = true;
                break;
            }
        }

        if (player.special.specialItem[2].isBought) {
            timeManager = 20;
        } else {
            timeManager = 10;
        }
        if (player.special.specialItem[3].isBought) {
            scoreMul = 2;
        } else {
            scoreMul = 1;
        }
    }

    public void boughtClicked(int z)
    {
        if (!minigames[z].isBought) {
            if (player.diamond >= minigames[z].costDiamond) {
                numberOfMiniGame = z;
                player.diamond -= minigames[z].costDiamond;
                minigames[z].MaximumGame = 2;
                minigames[z].timesPlayed = 2;
                minigames[z].isBought = true;
                for (int i = 0; i < minigames.Length; i++) {
                    if (i == z) {
                        minigames[i].isSelected = true;
                    } else {
                        minigames[i].isSelected = false;
                    }
                }
                minigames[z].isGameBuyable = false;
            } else {
                StartCoroutine(revealLackDiamond());
            }
        } else {
            numberOfMiniGame = z;
            for (int i = 0; i < minigames.Length; i++) {
                if (i == z) {
                    minigames[i].isSelected = true;
                } else {
                    minigames[i].isSelected = false;
                }
            }
        }
    }

    public void startMinigame()
    {
        if (numberOfMiniGame <= 3 && gamePlayable) {
            materialImage.sprite = mateiralItemImages[numberOfMiniGame - 1];
        }
        if (minigames[numberOfMiniGame].gameName == "gold" || minigames[numberOfMiniGame].gameName == "silver" || minigames[numberOfMiniGame].gameName == "bronze") {
            minigames[numberOfMiniGame].gameName = "bar";
        }
        if (minigames[numberOfMiniGame].gameName == "emerald" || minigames[numberOfMiniGame].gameName == "ruby" || minigames[numberOfMiniGame].gameName == "sapphire") {
            minigames[numberOfMiniGame].gameName = "gem";
        }
        if (minigames[numberOfMiniGame].gameName == "bar") {
            randomNumber = Random.Range(0, 101);

            if (randomNumber <= 15) {
                minigames[numberOfMiniGame].gameName = "gold";
                minigames[numberOfMiniGame].displayingGameName = "금";
                materialImage.sprite = mateiralItemImages[5];
            } else if (randomNumber > 15 && randomNumber <= 40) {
                minigames[numberOfMiniGame].gameName = "silver";
                minigames[numberOfMiniGame].displayingGameName = "은";
                materialImage.sprite = mateiralItemImages[4];
            } else if (randomNumber > 40 && randomNumber <= 100) {
                minigames[numberOfMiniGame].gameName = "bronze";
                minigames[numberOfMiniGame].displayingGameName = "동";
                materialImage.sprite = mateiralItemImages[3];
            }
        }
        if (minigames[numberOfMiniGame].gameName == "gem") {
            randomNumber = Random.Range(0, 101);

            if (randomNumber <= 15) {
                minigames[numberOfMiniGame].gameName = "emerald";
                minigames[numberOfMiniGame].displayingGameName = "에메랄드";
                materialImage.sprite = mateiralItemImages[8];
            } else if (randomNumber > 15 && randomNumber <= 40) {
                minigames[numberOfMiniGame].gameName = "sapphire";
                minigames[numberOfMiniGame].displayingGameName = "사파이어";
                materialImage.sprite = mateiralItemImages[7];
            } else if (randomNumber > 40 && randomNumber <= 100) {
                minigames[numberOfMiniGame].gameName = "ruby";
                minigames[numberOfMiniGame].displayingGameName = "루비";
                materialImage.sprite = mateiralItemImages[6];
            }
        }
        timeLeft = timeManager;

        score = 0;
        ScoreText.text = string.Format("{0:#,###0}", score);

        if (minigames[numberOfMiniGame].isBought && minigames[numberOfMiniGame].timesPlayed > 0) {
            countNumber();
        }
    }

    public void countNumber()
    {
        foreach (GameObject a in buttons) {
            a.SetActive(false);
        }
        itemMenu.SetActive(false);
        inGame = true;
        StartCoroutine(countNumberRoutine());
    }

    IEnumerator countNumberRoutine()
    {
        clickButton.SetActive(true);
        _CountNum.gameObject.SetActive(true);
        sound.PlaySound("miniGameStartCount");
        _CountNum.text = "3";
        yield return new WaitForSeconds(1f);
        _CountNum.text = "2";
        yield return new WaitForSeconds(1f);
        _CountNum.text = "1";
        yield return new WaitForSeconds(1f);
        _CountNum.text = "시작!";
        yield return new WaitForSeconds(1f);
        _CountNum.gameObject.SetActive(false);

        gameUIS.SetActive(true);
        startCounting();
    }

    public void startCounting()
    {
        clickButton.GetComponent<Button>().interactable = true;
        StartCoroutine(countingroutine());
    }

    IEnumerator countingroutine()
    {
        sound.PlaySound("miniGameIngameCount");
        sound.audioSource.loop = true;
        while (timeLeft > 0) {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
        if (timeLeft == 0) {
            sound.audioSource.Stop();
            sound.audioSource.loop = false;
            clickButton.GetComponent<Button>().interactable = false;
            yield return new WaitForSeconds(0.1f);
            gameEnd();
        }
    }

    public void gameEnd()
    {
        StartCoroutine(gameEndroutine());
    }

    IEnumerator gameEndroutine()
    {
        sound.PlaySound("miniGameScoreShow");
        finalmaterial.sprite = materialImage.sprite;

        int rewardAmount = (int)(score * minigames[numberOfMiniGame].multiplier);
        string rewardItemName = minigames[numberOfMiniGame].gameName.ToLower();

        finalScoreText.text = "점수: " + string.Format("{0:#,###0}", score) + "\n" + minigames[numberOfMiniGame].displayingGameName + ": " + string.Format("{0:#,###0}", rewardAmount) + "개 획득";

        finalButtons.SetActive(false);
        gameUIS.SetActive(false);
        endingScreen.SetActive(true);
        clickButton.SetActive(false);

        if (score > 400) {
            player.is100Over = true;
        }

        if (minigames[numberOfMiniGame].timesPlayed > 0)
            minigames[numberOfMiniGame].timesPlayed--;

        yield return new WaitForSeconds(1.5f);

        inGame = false;

        sound.PlaySound("UIPOP");
        finalButtons.SetActive(true);

        bool rewarded = false;

        for (int i = 0; i < player.item.Length; i++) {
            string playerItemName = player.item[i].itemName.ToLower();

            Debug.Log($"[MiniGame Reward Check] rewardItemName={rewardItemName}, playerItemName={playerItemName}");

            if (playerItemName == rewardItemName) {
                rewarded = true;

                long totalMaterial = player.getTotalmaterial();
                long chestLimit = player.manager.chestLimitNumber;

                if (totalMaterial + rewardAmount < chestLimit) {
                    player.item[i].itemNumber += rewardAmount;
                    Debug.Log($"[MiniGame Reward] item 지급 완료: {rewardItemName}, 수량: {rewardAmount}");
                } else {
                    long canAdd = chestLimit - totalMaterial;
                    if (canAdd < 0) canAdd = 0;

                    long rest = rewardAmount - canAdd;

                    player.item[i].itemNumber += canAdd;
                    player.money += (long)rest * (long)player.item[i].itemCost;

                    Debug.Log($"[MiniGame Reward] 일부 아이템 지급: {canAdd}, 나머지 판매: {rest}, 아이템명: {rewardItemName}");
                }

                break;
            }
        }

        if (!rewarded) {
            Debug.LogError($"[MiniGame Reward] 지급 실패 - 일치하는 itemName을 찾지 못함. rewardItemName={rewardItemName}");
        }
    }

    public void gameRestartButton()
    {
        if (minigames[numberOfMiniGame].timesPlayed == 0) {
            minigames[numberOfMiniGame].isBought = false;
            minigames[numberOfMiniGame].diamondCostText.SetActive(true);
            minigames[numberOfMiniGame].selectText.SetActive(false);
            minigames[numberOfMiniGame].selectButton.color = new Color(1, 1, 1);

            int count = 1;
            bool quit = false;
            while (!quit) {
                if (minigames[count].isBought) {
                    quit = true;
                    minigames[count].isSelected = true;
                    boughtClicked(count);
                } else {
                    count++;
                    if (count == minigames.Length - 1) {
                        quit = true;
                    }
                }
            }
        }

        timeLeft = timeManager;
        score = 0;
        endingScreen.SetActive(false);
        buttons[0].SetActive(true);
        buttons[1].SetActive(true);
        itemMenu.SetActive(true);
    }

    public void mining()
    {
        if (gameUIS.activeSelf) {
            score += scoreMul;
            ScoreText.text = string.Format("{0:#,###0}", score);
            if (isBig) {
                miningObject.localScale = new Vector3(1.1f, 1.1f, 1f);
                isBig = false;
            } else {
                miningObject.localScale = new Vector3(1f, 1f, 1f);
                isBig = true;
            }
        }
    }

    IEnumerator revealLackDiamond()
    {
        if (!sound.audioSource.isPlaying)
            sound.PlaySound("Caution");
        lackDiamond.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        lackDiamond.SetActive(false);
    }
}