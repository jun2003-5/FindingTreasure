using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

[System.Serializable]
public class CollectionManager : MonoBehaviour
{

    [SerializeField] GameObject gridPrefab;

    public Collection[] collect;
    public Sprite[] sprites;
    public Player player;

    public Sprite coinageImage;

    [SerializeField] float number_Hor; 
    [SerializeField] float number_Ver;

    int count;
    int numberOfLegend;
    int numberOfUnique;
    int numberOfEpic;
    int numberOfRare;
    int numberOfNormal;

    int totalNumberofCollection;

    public Text totalCollection;
    public TextMeshProUGUI currentCollectionSpeed;

    public Transform Collectionparet;
    void Start()
    {
        SetUpSprites();
        changeImage();
        for(int i = 0; i < number_Ver; i++) {
            for(int r = 0; r < number_Hor; r++) {
                count++;
                GameObject grid = Instantiate(gridPrefab) as GameObject;
                grid.transform.position = new Vector3(gridPrefab.transform.position.x + (r * (gridPrefab.GetComponent<RectTransform>().rect.width + 13)), gridPrefab.transform.position.y - (i * (gridPrefab.GetComponent<RectTransform>().rect.height + 10)), 0f);
                grid.transform.SetParent(Collectionparet);
                grid.transform.localScale = gridPrefab.transform.localScale;
                grid.transform.name = (gridPrefab.transform.name + count);
                grid.transform.GetChild(0).GetComponent<Image>().sprite = collect[count - 1].itemImage;

                if(i < 2) {
                    grid.GetComponent<Image>().color = Color.red;
                    collect[count - 1]._class = "legend";
                } else if(i >= 2 && i < 5) {
                    grid.GetComponent<Image>().color = Color.yellow;
                    collect[count - 1]._class = "unique";
                } else if(i >= 5 && i < 10) {
                    grid.GetComponent<Image>().color = Color.magenta;
                    collect[count - 1]._class = "epic";
                } else if(i >= 10 && i < 18) {
                    grid.GetComponent<Image>().color = new Color(0.1830223f, 0.6792453f, 0.0480598f);
                    collect[count - 1]._class = "rare";
                } else {
                    grid.GetComponent<Image>().color = Color.white;
                    collect[count - 1]._class = "normal";
                }
            }
        }
    }

    void SetUpSprites()
    {
        sprites = Resources.LoadAll("Necklace", typeof(Sprite)).Cast<Sprite>().ToArray();
    }

    void changeImage()
    {
        for(int i = 0; i < collect.Length; i++) {
            collect[i].itemImage = sprites[i];
        }
    }
    void Update()
    {
        for(int i = 0; i < collect.Length; i++) {
            if(!collect[i].isFound) {
                Collectionparet.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0,0.3f);
            } else {
                Collectionparet.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1,1);
            }
        }
        int num = 0;
        for(int i = 0; i < collect.Length; i++) {
            if(collect[i].isFound) {
                num++;
            }
        }

        numberOfLegend = 0;
        numberOfUnique = 0;
        numberOfEpic = 0;
        numberOfRare = 0;
        numberOfNormal = 0;
        for(int i = 0; i < collect.Length; i++) {
            if(collect[i]._class == "legend" &&collect[i].isFound) {
                numberOfLegend++;
            } else if(collect[i]._class == "unique" && collect[i].isFound) {
                numberOfUnique++;
            } else if(collect[i]._class == "epic" && collect[i].isFound) {
                numberOfEpic++;
            } else if(collect[i]._class == "rare" && collect[i].isFound) {
                numberOfRare ++;
            } else if(collect[i]._class == "normal" && collect[i].isFound) {
                numberOfNormal++;
            }
        }
        if (player.sawEnding) {
            player.coinageMul = (150 * numberOfLegend) + (75 * numberOfUnique) + (30 * numberOfEpic) + (5 * numberOfRare) + (numberOfNormal * 3) + 1 + 100000;
            player.coinage.increasingAmount = (300 * numberOfLegend) + (100 * numberOfUnique) + (50 * numberOfEpic) + 1000000;
        } else {
            player.coinageMul = (150 * numberOfLegend) + (75 * numberOfUnique) + (30 * numberOfEpic) + (5 * numberOfRare) + (numberOfNormal * 3) + 1;
            player.coinage.increasingAmount = (300 * numberOfLegend) + (100 * numberOfUnique) + (50 * numberOfEpic);
        }
      

        currentCollectionSpeed.text = "현재: 클릭당 " + player.coinageMul + "개, " + "초당 " + player.coinage.increasingAmount + "개";

        totalCollection.text = "수집품: " + num + "/150";
    }

    public int gettotalCollect()
    {
        totalNumberofCollection = 0;
        for(int i = 0; i < collect.Length; i++) {
            if(collect[i].isFound) {
                totalNumberofCollection++;
            }
        }
        return totalNumberofCollection;
    }
}
