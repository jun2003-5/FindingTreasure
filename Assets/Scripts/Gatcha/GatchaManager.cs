using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GatchaManager : MonoBehaviour
{
    public SoundEffect sound;
    public GameObject GatchaScreen;
    public Player player;
    public CollectionManager collection;
    public Image[] hider;
    public GameObject diaCaution;
    public GameObject moneycaution;
    public GameObject leaveButton;


    public Sprite[] ItemImage; 
    public Sprite coinage;

    public Gatcha[] gatcha;

    public bool isGatching = false;
    public string randomItem;
    int typeofItem;

    int numberOfItem;
    int random;
    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < gatcha.Length; i++) {
            gatcha[i].chestCost.text = string.Format("{0:#,###0}", gatcha[i].Cost);
        }
    }

    public void chestBought(int i)
    {
        if(i <= 2) {
            if(player.money >= gatcha[i-1].Cost) {
                sound.PlaySound("BuyOrSell");
                player.money -= gatcha[i - 1].Cost;
                GatchaScreen.SetActive(true);
                fadeAway();
                setItem(i);
            } else {
                StartCoroutine(revealCaution());
            }
        } else {
            if(player.diamond >= gatcha[i-1].Cost) {
                sound.PlaySound("BuyOrSell");
                player.diamond -= gatcha[i - 1].Cost;
                GatchaScreen.SetActive(true);
                fadeAway();
                setItem(i);
            } else {
                StartCoroutine(revealCaution2());
            }
        }
       
    }
    IEnumerator revealCaution()
    {
        sound.PlaySound("Caution");
        moneycaution.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        moneycaution.SetActive(false);
    }
    IEnumerator revealCaution2()
    {
        sound.PlaySound("Caution");
        diaCaution.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        diaCaution.SetActive(false);
    }
    public void setItem(int type)
    {
        isGatching = true;
        for(int i=0;i<GatchaScreen.transform.childCount - 1; i++) {
            GatchaScreen.transform.GetChild(i).GetComponent<ChestAnimation>().type = type;
            GatchaScreen.transform.GetChild(i).GetComponent<ChestAnimation>().starting();

            //랜덤확률 주기
            if(type == 1) {
                //재료55% 바다주화40% 수집품5%
                int chanceOFItem = Random.Range(0, 101);
                if(chanceOFItem <= 5) {
                    typeofItem = 3;
                } else if (chanceOFItem > 5 && chanceOFItem <= 45) {
                    typeofItem = 1;
                } else if (chanceOFItem > 45 && chanceOFItem <= 100) {
                    typeofItem = 2;
                }

                if(typeofItem == 1) {
                    int count = 0;
                    bool quitWhile = false;
                    while(player.item[count].isOverTheDistance && !quitWhile) {
                        count++;
                        if(count == 2) {
                            quitWhile = true;
                        }
                    }

                    int random = Random.Range(0, count + 1);

                    GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
                    GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = ItemImage[random];
                    numberOfItem = (int)((float)player.pickaxeShop[count].costNumber[1] * 0.01f);
                    GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().text = string.Format("{0:#,###0}", numberOfItem);

                    if((player.getTotalmaterial() + numberOfItem < player.manager.chestLimitNumber)) {
                        player.item[random].itemNumber += numberOfItem;
                    } else if(player.getTotalmaterial() + numberOfItem >= player.manager.chestLimitNumber) {
                        float rest = numberOfItem - (player.manager.chestLimitNumber - player.getTotalmaterial());
                        player.item[random].itemNumber += (int)(numberOfItem - rest);
                        player.money += (long)rest * (long)player.item[random].itemCost;
                    }
                //바다 주화
                } else if (typeofItem == 2) {
                    int percentage = Random.Range(0, 101);
                    if(percentage <= 20) {
                        numberOfItem = Random.Range((player.coinageMul+2) * 300, (player.coinageMul + 2) * 400);
                    } else if(percentage <= 50 && percentage > 20) {
                        numberOfItem = Random.Range((player.coinageMul + 2) * 50, (player.coinageMul + 2) * 100);
                    } else if(percentage > 50 && percentage <= 100) {
                        numberOfItem = Random.Range((player.coinageMul + 2) * 10, (player.coinageMul + 2) * 50);
                    }
                    GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                    GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = coinage;
                    GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().text = string.Format("{0:#,###0}", numberOfItem);

                    player.coinage.itemNumber += numberOfItem;

                } else if (typeofItem == 3) {
                    getCollection(i,3,10,20,45);
                }

            } else if (type == 2) {
                int chanceOFItem = Random.Range(0, 101);
                if(chanceOFItem <= 10) {
                    typeofItem = 3;
                } else if(chanceOFItem > 10 && chanceOFItem <= 50) {
                    typeofItem = 1;
                } else if(chanceOFItem > 50 && chanceOFItem <= 100) {
                    typeofItem = 2;
                }

                if(typeofItem == 1) {
                    int count = 0;
                    bool quitWhile = false;
                    while(player.item[count].isOverTheDistance && !quitWhile) {
                        count++;
                        if(count == 5) {
                            quitWhile = true;
                        }
                    }

                    int random = Random.Range(3, count + 1);

                    GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
                    GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = ItemImage[random];
                    numberOfItem = (int)((float)player.pickaxeShop[count].costNumber[1] * 0.05f);
                    GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().text = string.Format("{0:#,###0}", numberOfItem);

                    if((player.getTotalmaterial() + numberOfItem < player.manager.chestLimitNumber)) {
                        player.item[random].itemNumber += numberOfItem;
                    } else if(player.getTotalmaterial() + numberOfItem >= player.manager.chestLimitNumber) {
                        float rest = numberOfItem - (player.manager.chestLimitNumber - player.getTotalmaterial());
                        player.item[random].itemNumber += (int)(numberOfItem - rest);
                        player.money += (long)rest * (long)player.item[random].itemCost;
                    }

                    //바다 주화
                } else if(typeofItem == 2) {
                    int percentage = Random.Range(0, 101);
                    if(percentage <= 20) {
                        numberOfItem = Random.Range((player.coinageMul + 2) * 500, (player.coinageMul + 4) * 600);
                    } else if(percentage <= 50 && percentage > 20) {
                        numberOfItem = Random.Range((player.coinageMul + 2) * 100, (player.coinageMul + 4) * 200);
                    } else if(percentage > 50 && percentage <= 100) {
                        numberOfItem = Random.Range((player.coinageMul + 2) * 50, (player.coinageMul + 4) * 100);
                    }
                    GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                    GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = coinage;
                    GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().text = string.Format("{0:#,###0}", numberOfItem);

                    player.coinage.itemNumber += numberOfItem;

                } else if(typeofItem == 3) {
                    getCollection(i, 3, 10, 25, 55);
                }
            } else if (type == 3) {
                int chanceOFItem = Random.Range(0, 101);
                if(chanceOFItem <= 20) {
                    typeofItem = 3;
                } else if(chanceOFItem > 20 && chanceOFItem <= 55) {
                    typeofItem = 1;
                } else if(chanceOFItem > 55 && chanceOFItem <= 100) {
                    typeofItem = 2;
                }

                if(typeofItem == 1) {
                    int count = 3;
                    bool quitWhile = false;
                    while(player.item[count].isOverTheDistance && !quitWhile) {
                        count++;
                        if(count == 8) {
                            quitWhile = true;
                        }
                    }

                    int random = Random.Range(3, count + 1);

                    GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
                    GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = ItemImage[random];
                    numberOfItem = (int)((float)player.pickaxeShop[count].costNumber[1] * 0.4f);
                    GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().text = string.Format("{0:#,###0}", numberOfItem);
                    if((player.getTotalmaterial() + numberOfItem < player.manager.chestLimitNumber)) {
                        player.item[random].itemNumber += numberOfItem;
                    } else if(player.getTotalmaterial() + numberOfItem >= player.manager.chestLimitNumber) {
                        float rest = numberOfItem - (player.manager.chestLimitNumber - player.getTotalmaterial());
                        player.item[random].itemNumber += (int)(numberOfItem - rest);
                        player.money += (long)rest * (long)player.item[random].itemCost;
                    }

                    //바다 주화
                } else if(typeofItem == 2) {
                    int percentage = Random.Range(0, 101);
                    if(percentage <= 20) {
                        numberOfItem = Random.Range((player.coinageMul + 2) * 600, (player.coinageMul + 10) * 700);
                    } else if(percentage <= 50 && percentage > 20) {
                        numberOfItem = Random.Range((player.coinageMul + 2) * 300, (player.coinageMul + 10) * 600);
                    } else if(percentage > 50 && percentage <= 100) {
                        numberOfItem = Random.Range((player.coinageMul + 2) * 200, (player.coinageMul + 10) * 300);
                    }
                    GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                    GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = coinage;
                    GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().text = string.Format("{0:#,###0}", numberOfItem);
                    player.coinage.itemNumber += numberOfItem;


                } else if(typeofItem == 3) {
                    getCollection(i, 4, 12, 25, 55);
                }

            } else if (type == 4) {
                int chanceOFItem = Random.Range(0, 101);
                if(chanceOFItem <= 23 || i == 0) {
                    typeofItem = 3;
                } else if(chanceOFItem > 23 && chanceOFItem <= 55) {
                    typeofItem = 1;
                } else if(chanceOFItem > 55 && chanceOFItem <= 100) {
                    typeofItem = 2;
                }

                
                if(typeofItem == 1) {
                    int count = 3;
                    bool quitWhile = false;
                    while(player.item[count].isOverTheDistance && !quitWhile) {
                        count++;
                        if(count == 8) {
                            quitWhile = true;
                        }
                    }

                    int random = Random.Range(3, count+1);

                    GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
                    GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = ItemImage[random];
                    numberOfItem = (int)((float)player.pickaxeShop[count].costNumber[1] * 0.6f);
                    GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().text = string.Format("{0:#,###0}", numberOfItem);
                    if((player.getTotalmaterial() + numberOfItem < player.manager.chestLimitNumber)) {
                        player.item[random].itemNumber += numberOfItem;
                    } else if(player.getTotalmaterial() + numberOfItem >= player.manager.chestLimitNumber) {
                        float rest = numberOfItem - (player.manager.chestLimitNumber - player.getTotalmaterial());
                        player.item[random].itemNumber += (int)(numberOfItem - rest);
                        player.money += (long)rest * (long)player.item[random].itemCost;
                    }

                    //바다 주화
                } else if(typeofItem == 2) {
                    int percentage = Random.Range(0, 101);
                    if(percentage <= 20) {
                        numberOfItem = Random.Range((player.coinageMul + 2) * 4000, (player.coinageMul + 20) * 10000);
                    } else if(percentage <= 50 && percentage > 20) {
                        numberOfItem = Random.Range((player.coinageMul + 2) * 2500, (player.coinageMul + 20) * 4000);
                    } else if(percentage > 50 && percentage <= 100) {
                        numberOfItem = Random.Range((player.coinageMul + 2) * 2000, (player.coinageMul + 20) * 2500);
                    }
                    GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                    GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = coinage;
                    GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().text = string.Format("{0:#,###0}", numberOfItem);
                    player.coinage.itemNumber += numberOfItem;


                } else if(typeofItem == 3) {
                    if(i == 0) {
                        getCollection(i, 10, 40, 101, 105);
                    } else {
                        getCollection(i, 4, 12, 25, 55);
                    }
                }
            }


        }
    }
    public void getCollection(int i, int percentage1, int percentage2, int percentage3, int percentage4)
    {
        int percentage = Random.Range(0, 101);
        if(percentage <= percentage1) {

            random = Random.Range(0, 10);
            int collectNum = 0;
            while(collection.collect[random].isFound == true && collectNum != 11) {
                collectNum++;
                random = Random.Range(0, 10);
            }
            if(collectNum == 11) {
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = coinage;
                GatchaScreen.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.black;
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().text = "1,000,000";
                player.coinage.itemNumber += 1000000;
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().color = Color.white;
            } else {
                collection.collect[random].isFound = true;
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = collection.collect[random].itemImage;
                GatchaScreen.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.red;
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().text = "레전더리";
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().color = Color.red;
            }
            //유니크
        } else if(percentage <= percentage2 && percentage > percentage1) {
            random = Random.Range(10, 25);
            int collectNum = 0;
            while(collection.collect[random].isFound == true && collectNum != 16) {
                collectNum++;
                random = Random.Range(10, 25);
            }
            if(collectNum == 16) {
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = coinage;
                GatchaScreen.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.black;
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().text = "500,000";
                player.coinage.itemNumber += 500000;
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().color = Color.white;
            } else {
                collection.collect[random].isFound = true;
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = collection.collect[random].itemImage;
                GatchaScreen.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.yellow;
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().text = "유니크";
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().color = Color.yellow;
            }
            //에픽
        } else if(percentage > percentage2 && percentage <= percentage3) {
            random = Random.Range(25, 50);
            int collectNum = 0;
            while(collection.collect[random].isFound == true && collectNum != 26) {
                collectNum++;
                random = Random.Range(25, 50);
            }
            if(collectNum == 26) {
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = coinage;
                GatchaScreen.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.black;
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().text = "100,000";
                player.coinage.itemNumber += 100000;
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().color = Color.white;
            } else {
                collection.collect[random].isFound = true;
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = collection.collect[random].itemImage;
                GatchaScreen.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.magenta;
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().text = "에픽";
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().color = Color.magenta;
            }
        }
        //레어
        else if(percentage > percentage3 && percentage <= percentage4) {
            random = Random.Range(50, 90);
            int collectNum = 0;
            while(collection.collect[random].isFound == true && collectNum != 41) {
                collectNum++;
                random = Random.Range(50, 90);
            }
            if(collectNum == 41) {
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = coinage;
                GatchaScreen.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.black;
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().text = "30,000";
                player.coinage.itemNumber += 30000;
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().color = Color.white;
            } else {
                collection.collect[random].isFound = true;
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = collection.collect[random].itemImage;
                GatchaScreen.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color(0.1830223f, 0.6792453f, 0.0480598f); ;
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().text = "레어";
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().color = new Color(0.1830223f, 0.6792453f, 0.0480598f);
            }
            //노말
        } else if(percentage > percentage4 && percentage <= 100) {
            random = Random.Range(90, 150);
            int collectNum = 0;
            while(collection.collect[random].isFound == true && collectNum != 61) {
                collectNum++;
                random = Random.Range(90, 150);
            }
            if(collectNum == 61) {
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = coinage;
                GatchaScreen.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.black;
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().text = "10000";
                player.coinage.itemNumber += 10000;
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().color = Color.white;
            } else {
                collection.collect[random].isFound = true;
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
                GatchaScreen.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = collection.collect[random].itemImage;
                GatchaScreen.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.white;
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().text = "노말";
                GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().color = Color.white;
            }
        }
    }

    public void fadeAway()
    {
        StartCoroutine(FadeTab());
    }

    IEnumerator FadeTab()
    {
        player.gameObject.GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(3f);
        for(float i = 1.5f; i >= 0; i -= Time.deltaTime) {
            hider[0].color = new Color(0, 0, 0, i / 1.5f);
            yield return null;
        }
        for(float i = 1.5f; i >= 0; i -= Time.deltaTime) {
            hider[1].color = new Color(0, 0, 0, i / 1.5f);
            yield return null;
        }
        for(float i = 1.5f; i >= 0; i -= Time.deltaTime) {
            hider[2].color = new Color(0, 0, 0, i / 1.5f);
            yield return null;
        }

        collection.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        collection.gameObject.SetActive(false);
        leaveButton.SetActive(true);
    }

    public void leaveChest()
    {
        sound.PlaySound("normalClick");
        GatchaScreen.SetActive(false);
        isGatching = false;
        for(int i = 0; i < hider.Length; i++) {
            hider[i].color = new Color(0, 0, 0, 1);
        }
        for(int i = 0; i < GatchaScreen.transform.childCount - 1; i++) {
            GatchaScreen.transform.GetChild(i).GetComponent<ChestAnimation>().mCurrentFrame = 0;
        }
        for(int i = 0; i < GatchaScreen.transform.childCount - 1; i++) {
            GatchaScreen.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.black;
            GatchaScreen.transform.GetChild(i).GetComponentInChildren<Text>().color = Color.white;
        }
        leaveButton.SetActive(false);
        player.gameObject.GetComponent<AudioSource>().Play();
    }
}
