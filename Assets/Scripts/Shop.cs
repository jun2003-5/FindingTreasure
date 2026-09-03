using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Shop : MonoBehaviour
{
    [SerializeField] public string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = Guid.NewGuid().ToString();
    }

    public bool isBought;

    [Header("--------정보")]
    public float[] costNumber;
    public Text[] costText;
    public string[] NAME_OF_ITEM;
    public string itemType;

    [Header("--------재료 갯수")]
    public int numberOfMaterials;

    [Header("--------세트 효과")]
    public string setName;

    [Header("--------구매 가능 여부")]
    public bool buyable;

    [Header("--------속도")]
    public float increaseSpeedNumber;
    public Text increaseSpeedText;

    [Header("--------저장 공간")]
    public float increaseCargoSizeNumber;
    public Text increaseCargoSizeText;
    public Sprite cargoImage;

    [Header("--------광산")]
    public float increaseOfPickaxe;
    public TextMeshProUGUI currentPower;
    public int upgradeLevel;

    [Header("--------광부")]
    public TextMeshProUGUI MinerAmount;
    public TextMeshProUGUI MinerMoneyText;
    public long minerCost;
    public int minerNumber;
    public float[] minerLevel;

    [Header("--------가리개")]
    public GameObject boughtImage;

    private long currentMoney;
    private long[] currentMaterials = Array.Empty<long>();
    private Image boughtImageComponent;

    private void Awake()
    {
        minerNumber = Mathf.Max(1, minerNumber);

        if (boughtImage != null)
            boughtImageComponent = boughtImage.GetComponent<Image>();

        if (costNumber == null)
            costNumber = Array.Empty<float>();

        if (NAME_OF_ITEM == null)
            NAME_OF_ITEM = Array.Empty<string>();

        if (costText == null)
            costText = Array.Empty<Text>();
    }

    private void Update()
    {
        UpdateBuyable();
        UpdateBoughtImage();
        UpdateTexts();
    }

    private void UpdateBuyable()
    {
        if (itemType == "miner") {
            buyable = currentMoney >= minerCost;
            return;
        }

        if (costNumber == null || costNumber.Length == 0) {
            buyable = false;
            return;
        }

        if (currentMoney < costNumber[0]) {
            buyable = false;
            return;
        }

        if (numberOfMaterials <= 0) {
            buyable = true;
            return;
        }

        if (currentMaterials == null) {
            buyable = false;
            return;
        }

        for (int i = 0; i < numberOfMaterials; i++) {
            int costIndex = i + 1;
            if (costIndex >= costNumber.Length || i >= currentMaterials.Length) {
                buyable = false;
                return;
            }

            if (currentMaterials[i] < costNumber[costIndex]) {
                buyable = false;
                return;
            }
        }

        buyable = true;
    }

    private void UpdateBoughtImage()
    {
        if (boughtImage == null || boughtImageComponent == null)
            return;

        if (!isBought) {
            if (buyable) {
                boughtImage.SetActive(false);
            } else {
                boughtImage.SetActive(true);

                if (itemType == "miner")
                    boughtImageComponent.color = new Color(0, 0, 0, 0.81f);
                else
                    boughtImageComponent.color = new Color(0, 0, 0, 0.51f);
            }
        } else {
            boughtImage.SetActive(true);
            boughtImageComponent.color = new Color(0, 0, 0, 0.95f);
        }
    }

    private void UpdateTexts()
    {
        if (itemType == "paddel" && increaseSpeedText != null) {
            increaseSpeedText.text = "+" + string.Format("{0:#,###0}", increaseSpeedNumber) + "km/h";
        } else if (itemType == "cargo" && increaseCargoSizeText != null) {
            increaseCargoSizeText.text = "+" + string.Format("{0:#,###0}", increaseCargoSizeNumber) + "\n저장공간";
        }

        if (itemType != "miner") {
            int len = Mathf.Min(costText != null ? costText.Length : 0, costNumber != null ? costNumber.Length : 0);
            for (int i = 0; i < len; i++) {
                if (costText[i] != null)
                    costText[i].text = string.Format("{0:#,###0}", costNumber[i]);
            }
        } else {
            if (MinerMoneyText != null)
                MinerMoneyText.text = string.Format("{0:#,###0}", minerCost);

            if (MinerAmount != null)
                MinerAmount.text = "현재 광부 수: " + minerNumber;
        }

        if (currentPower != null) {
            if (upgradeLevel == 9)
                currentPower.text = "MAX " + string.Format("{0:#,###0}", (upgradeLevel + 1)) + "개";
            else
                currentPower.text = (upgradeLevel + 1) + "개 => " + (upgradeLevel + 2) + "개";
        }
    }

    public void isItemBought()
    {
        isBought = true;
        if (boughtImage != null)
            boughtImage.SetActive(true);
    }

    public void getMoney(long value)
    {
        currentMoney = value;
    }

    public void getMaterialNumber(long[] materials)
    {
        currentMaterials = materials ?? Array.Empty<long>();
    }
}