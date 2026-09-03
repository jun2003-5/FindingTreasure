using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class MinerClone : MonoBehaviour
{
    public SoundEffect sound;
    [Header("화살표")]
    public GameObject rightarrow;
    public GameObject leftArrow;
    [Header("콜렉션")]
    public CollectionManager collection;
    public string _class;
    public GameObject earnButton;

    public GameObject matButton;

    public GameObject minerClone;
    public GameObject autoMinerButton;
    public Player player;
    public GameObject priceTab;
    public GameObject priceTabRandom;
    public Text adsText;
    public Text onOffText;

    public bool isAutomatic;
    public bool autoBought;

    public int matRand;
    public int adsMul;
    public int ran;
    int random;
    public int _NumberOfMine = 1;
    public void minerbought()
    {
        GameObject a = GameObject.Instantiate(minerClone);
        a.transform.position = minerClone.transform.position;
        a.transform.SetParent(transform);
        a.transform.localScale = minerClone.transform.localScale;
        a.name = "Miner" + (_NumberOfMine - 1);
        _NumberOfMine++;
    }
    public void Update()
    {
        if(_NumberOfMine < this.gameObject.transform.childCount) {
            for(int i = 0; i < this.gameObject.transform.childCount - _NumberOfMine; i++) {
                GameObject.Destroy(this.gameObject.transform.GetChild(this.gameObject.transform.childCount - i - 1).gameObject);
            }
        } else if (_NumberOfMine > this.gameObject.transform.childCount) {
            while(this.gameObject.transform.childCount <= _NumberOfMine) {
                GameObject a = GameObject.Instantiate(minerClone);
                a.transform.position = minerClone.transform.position;
                a.transform.SetParent(transform);
                a.transform.localScale = minerClone.transform.localScale;
                a.name = "Miner" + (_NumberOfMine - 1);
            }
        }

        float x = Mathf.PingPong(Time.time * 20f, 5);
        rightarrow.transform.position = new Vector3((Screen.width - 40) + x, rightarrow.transform.position.y, rightarrow.transform.position.z);
        leftArrow.transform.position = new Vector3(40 - x, rightarrow.transform.position.y, rightarrow.transform.position.z);

        for(int i = 0; i < transform.childCount; i++) {
            if(transform.GetChild(i).GetChild(1).GetChild(0).transform.position.x > Screen.width && transform.GetChild(i).GetChild(1).gameObject.activeSelf) {
                rightarrow.SetActive(true);

            } else if(transform.GetChild(i).GetChild(1).GetChild(0).transform.position.x > 0 && transform.GetChild(i).GetChild(1).GetChild(0).transform.position.x < Screen.width) {
                rightarrow.SetActive(false);
            }
        }
        for(int i = transform.childCount - 1; i >= 0; i--) {
            if(transform.GetChild(i).GetChild(1).GetChild(0).transform.position.x < 0 && transform.GetChild(i).GetChild(1).gameObject.activeSelf) {
                leftArrow.SetActive(true);

            } else if(transform.GetChild(i).GetChild(1).GetChild(0).transform.position.x > 0 && transform.GetChild(i).GetChild(1).GetChild(0).transform.position.x < Screen.width) {
                leftArrow.SetActive(false);
            }
        }

        
    }

    public void treasureTab()
    {
        if(!priceTab.activeSelf) {
            sound.PlaySound("Cancel");
            priceTab.SetActive(true);
            ran = Random.Range(0, 101);
            if(ran <= 8 && ran > 3) {
                ran = 1;
            } else if(ran > 8 && ran <= 60) {
                ran = 0;
            } else if(ran > 60 && ran <= 100) {
                ran = 2;
            } else if(ran <= 3) {
                ran = 3;
            }

            int percentage = Random.Range(0, 101);

            //돈 계산
            long moneyRange = 25;
            int count = 0;
            bool quitWhile = false;
            while(player.paddelShop[count].isBought && !quitWhile) {
                count++;
                if(count == player.paddelShop.Length - 1) {
                    quitWhile = true;
                }
            }

            moneyRange = (long)player.paddelShop[count].costNumber[0]/3;
            if(moneyRange < 100) {
                moneyRange = 100;
            }
            if(ran == 0) {
                adsMul = 3;
                earnButton.SetActive(true);
                matButton.SetActive(false);
                if(percentage <= 20) {
                    matRand = Random.Range((int)(moneyRange * 0.12f), (int)(moneyRange * 0.15f));
                } else if(percentage <= 50 && percentage > 20) {
                    matRand = Random.Range((int)(moneyRange * 0.03f), (int)(moneyRange * 0.08f));
                } else if(percentage > 50 && percentage <= 100) {
                    matRand = Random.Range((int)(moneyRange * 0.08f), (int)(moneyRange * 0.12f));
                }

                //다이아 계산
            } else if(ran == 1) {
                adsMul = 4;
                earnButton.SetActive(true);
                matButton.SetActive(false);
                if(percentage <= 30) {
                    matRand = 2;
                } else if(percentage > 50 && percentage <= 100) {
                    matRand = 1;
                }

                //바다주화 계산
            } else if(ran == 2) {
                adsMul = 5;
                earnButton.SetActive(true);
                matButton.SetActive(false);
                if(percentage <= 20) {
                    matRand = Random.Range(player.coinageMul * 400, player.coinageMul * 500);
                } else if(percentage <= 50 && percentage > 20) {
                    matRand = Random.Range(player.coinageMul * 300, player.coinageMul * 400);
                } else if(percentage > 50 && percentage <= 100) {
                    matRand = Random.Range(player.coinageMul * 200, player.coinageMul * 300);
                }
            } else if (ran == 3) {

                //전설
                if(percentage <= 3) {

                    random = Random.Range(0, 10);
                    int collectNum = 0;
                    while(collection.collect[random].isFound == true && collectNum != 11) {
                        collectNum++;
                        random = Random.Range(0, 10);
                    }
                    if(collectNum == 11) {
                        earnButton.SetActive(true);
                        matButton.SetActive(false);
                        priceTabRandom.transform.GetChild(ran).GetComponent<Image>().color = new Color(1, 1, 1, 0f);
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().color = Color.black;
                        priceTabRandom.transform.GetChild(ran).GetChild(1).GetComponent<Image>().sprite = collection.coinageImage;
                        priceTabRandom.transform.GetChild(ran).GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().text = "1,000,000";
                        matRand = 1000000;

                    } else {
                        earnButton.SetActive(false);
                        matButton.SetActive(true);
                        collection.collect[random].isFound = true;
                        _class = collection.collect[random]._class;
                        priceTabRandom.transform.GetChild(ran).GetComponent<Image>().color = Color.red;
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().color = Color.red;
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().text = changeClassName(_class);
                        priceTabRandom.transform.GetChild(ran).GetChild(1).GetComponent<Image>().sprite = collection.collect[random].itemImage;
                        priceTabRandom.transform.GetChild(ran).GetChild(1).GetComponent<Image>().color = Color.white;
                        matRand = 0;
                    }

                    //유니크
                } else if(percentage <= 10 && percentage > 3) {
                    random = Random.Range(10, 25);
                    int collectNum = 0;
                    while(collection.collect[random].isFound == true && collectNum != 16) {
                        collectNum++;
                        random = Random.Range(10, 25);
                    }
                    if(collectNum == 16) {
                        earnButton.SetActive(true);
                        matButton.SetActive(false);
                        priceTabRandom.transform.GetChild(ran).GetComponent<Image>().color = Color.white;
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().color = Color.white;
                        priceTabRandom.transform.GetChild(ran).GetChild(1).GetComponent<Image>().sprite = collection.coinageImage;
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().text = "500,000";
                        matRand = 500000;

                    } else {
                        earnButton.SetActive(false);
                        matButton.SetActive(true);
                        collection.collect[random].isFound = true;
                        _class = collection.collect[random]._class;
                        priceTabRandom.transform.GetChild(ran).GetComponent<Image>().color = Color.yellow;
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().color = Color.yellow;
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().text = changeClassName(_class);
                        priceTabRandom.transform.GetChild(ran).GetChild(1).GetComponent<Image>().sprite = collection.collect[random].itemImage;
                        priceTabRandom.transform.GetChild(ran).GetChild(1).GetComponent<Image>().color = Color.white;
                        matRand = 0;
                    }
                    //에픽
                } else if(percentage > 10 && percentage <= 20) {
                    random = Random.Range(25, 50);
                    int collectNum = 0;
                    while(collection.collect[random].isFound == true && collectNum != 26) {
                        collectNum++;
                        random = Random.Range(25, 50);
                    }
                    if(collectNum == 26) {
                        earnButton.SetActive(true);
                        matButton.SetActive(false);
                        priceTabRandom.transform.GetChild(ran).GetComponent<Image>().color = Color.white;
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().color = Color.white;
                        priceTabRandom.transform.GetChild(ran).GetChild(1).GetComponent<Image>().sprite = collection.coinageImage;
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().text = "100,000";
                        matRand = 100000;

                    } else {
                        earnButton.SetActive(false);
                        matButton.SetActive(true);
                        collection.collect[random].isFound = true;
                        _class = collection.collect[random]._class;
                        priceTabRandom.transform.GetChild(ran).GetComponent<Image>().color = Color.magenta;
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().color = Color.magenta;
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().text = changeClassName(_class);
                        priceTabRandom.transform.GetChild(ran).GetChild(1).GetComponent<Image>().sprite = collection.collect[random].itemImage;
                        priceTabRandom.transform.GetChild(ran).GetChild(1).GetComponent<Image>().color = Color.white;
                        matRand = 0;
                    }
                }
                //레어
                else if(percentage > 20 && percentage <= 45) {
                    random = Random.Range(50, 90);
                    int collectNum = 0;
                    while(collection.collect[random].isFound == true && collectNum != 41) {
                        collectNum++;
                        random = Random.Range(50, 90);
                    }
                    if(collectNum == 41) {
                        earnButton.SetActive(true);
                        matButton.SetActive(false);
                        priceTabRandom.transform.GetChild(ran).GetComponent<Image>().color = Color.white;
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().color = Color.white;
                        priceTabRandom.transform.GetChild(ran).GetChild(1).GetComponent<Image>().sprite = collection.coinageImage;
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().text = "30,000";
                        matRand = 30000;

                    } else {
                        earnButton.SetActive(false);
                        matButton.SetActive(true);
                        collection.collect[random].isFound = true;
                        _class = collection.collect[random]._class;
                        priceTabRandom.transform.GetChild(ran).GetComponent<Image>().color = new Color(0.1830223f, 0.6792453f, 0.0480598f);
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().color = new Color(0.1830223f, 0.6792453f, 0.0480598f);
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().text = changeClassName(_class);
                        priceTabRandom.transform.GetChild(ran).GetChild(1).GetComponent<Image>().sprite = collection.collect[random].itemImage;
                        priceTabRandom.transform.GetChild(ran).GetChild(1).GetComponent<Image>().color = Color.white;
                        matRand = 0;
                    }

                    //노말
                } else if(percentage > 45 && percentage <= 100) {
                    random = Random.Range(90, 150);
                    int collectNum = 0;
                    while(collection.collect[random].isFound == true && collectNum != 61) {
                        collectNum++;
                        random = Random.Range(90, 150);
                    }
                    if(collectNum == 61) {
                        earnButton.SetActive(true);
                        matButton.SetActive(false);
                        priceTabRandom.transform.GetChild(ran).GetComponent<Image>().color = Color.white;
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().color = Color.white;
                        priceTabRandom.transform.GetChild(ran).GetChild(1).GetComponent<Image>().sprite = collection.coinageImage;
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().text = "10,000";
                        matRand = 10000;

                    } else {
                        earnButton.SetActive(false);
                        matButton.SetActive(true);
                        collection.collect[random].isFound = true;
                        _class = collection.collect[random]._class;
                        priceTabRandom.transform.GetChild(ran).GetComponent<Image>().color = Color.white;
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().color = Color.white;
                        priceTabRandom.transform.GetChild(ran).GetChild(0).GetComponent<Text>().text = changeClassName(_class);
                        priceTabRandom.transform.GetChild(ran).GetChild(1).GetComponent<Image>().sprite = collection.collect[random].itemImage;
                        priceTabRandom.transform.GetChild(ran).GetChild(1).GetComponent<Image>().color = Color.white;
                        matRand = 0;
                    }
                }
                StartCoroutine(collectionTab());
            }
            adsText.text = "광고 시청 후 \n" + adsMul + "배 획득";

            for (int i = 0; i < priceTabRandom.transform.childCount; i++) {
                if(i == ran) {
                    priceTabRandom.transform.GetChild(i).gameObject.SetActive(true);
                    if(i != 3)
                        priceTabRandom.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = string.Format("{0:#,###0}", matRand);
                } else {
                    priceTabRandom.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }

    IEnumerator collectionTab()
    {
        collection.gameObject.SetActive(true);
        collection.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        yield return new WaitForSeconds(0.1f);
        collection.gameObject.SetActive(false);
        collection.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public string changeClassName(string name)
    {
        if(name == "legend")
            return "레전더리";
        else if(name == "unique")
            return "유니크";
        else if(name == "epic")
            return "에픽";
        else if(name == "rare")
            return "레어";
        else if(name == "normal")
            return "노말";

        return null;
    }
    public void NormalEarn()
    {
        sound.PlaySound("normalClick");
        priceTab.SetActive(false);
        if(ran == 0) {
            player.money += matRand;
        } else if (ran == 1) {
            player.diamond += matRand;
        } else if (ran == 2) {
            player.coinage.itemNumber += matRand;
        } else if (ran == 3) {
            player.coinage.itemNumber += matRand;
        }
    }
    public void adsEarn()
    {
        sound.PlaySound("normalClick");
        priceTab.SetActive(false);
        if(ran == 0) {
            player.money += matRand * adsMul;
        } else if(ran == 1) {
            player.diamond += matRand * adsMul;
        } else if(ran == 2) {
            player.coinage.itemNumber += matRand * adsMul;
        }
    }

    public void turnOFFONMinerBuff()
    {
        if(isAutomatic) {
            isAutomatic = false;
            onOffText.text = "OFF";
        } else {
            isAutomatic = true;
            onOffText.text = "ON";
        }
    }
}
