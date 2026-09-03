using UnityEngine;

[System.Serializable]
public class QuestData : MonoBehaviour
{
    public SoundEffect sound;
    public Player player;


    public Quest[] BasicquestList;
    public Quest[] SpecialquestList;

    Color greenColor = new Color(63f / 255f, 209f / 255f, 31f / 255f, 208f / 255f);
    Color blackcolor = new Color(0, 0, 0, 183f / 255f);
    public void Awake()
    {
        foreach(Quest quest in BasicquestList) {
            quest.titleText.text = quest.Title;
            quest.goldrewardText.text = string.Format("{0:#,###0}", quest.goldreward) + "G";
            if(quest.diamonrewardText != null) {
                quest.diamonrewardText.text = string.Format("{0:#,###0}", quest.diamondreward);
            }
        }
        for(int i = 1; i < SpecialquestList.Length; i++) {
            SpecialquestList[i].titleText.text = SpecialquestList[i].Title;
            if(SpecialquestList[i].goldrewardText != null) {
                SpecialquestList[i].goldrewardText.text = string.Format("{0:#,###0}", SpecialquestList[i].goldreward) + "G";
            }
            if(SpecialquestList[i].diamonrewardText != null) {
                SpecialquestList[i].diamonrewardText.text = string.Format("{0:#,###0}", SpecialquestList[i].diamondreward);
            }
        }
         
        //노 6개
        SpecialquestList[1].progressbar.maximum = 6;
        SpecialquestList[1].progressbar.current = player.setNumber[0];

        //프로펠러 6개
        SpecialquestList[2].progressbar.maximum = 6;
        SpecialquestList[2].progressbar.current = player.setNumber[1];

        SpecialquestList[3].progressbar.maximum = 6;
        SpecialquestList[3].progressbar.current = player.setNumber[2];

        SpecialquestList[4].progressbar.maximum = 6;
        SpecialquestList[4].progressbar.current = player.setNumber[3];

        SpecialquestList[5].progressbar.maximum = 20;
        SpecialquestList[5].progressbar.current = player.collect.gettotalCollect();

        SpecialquestList[6].progressbar.maximum = 50;
        SpecialquestList[6].progressbar.current = player.collect.gettotalCollect();

        SpecialquestList[7].progressbar.maximum = 130;
        SpecialquestList[7].progressbar.current = player.collect.gettotalCollect();
    }

    void Update()
    {
        foreach(Quest quest in BasicquestList) {
            if(!quest.isActive && !quest.isCompleted) {
                quest.completedQuest.color = Color.white;
                quest.checkIcon.SetActive(false);
            }
        }

        foreach(Quest quest in BasicquestList) {
            if(quest.isActive && !quest.isCompleted) {
                quest.completedQuest.color = greenColor;
                quest.checkIcon.SetActive(false);
            }
        }
        foreach(Quest quest in BasicquestList) {
            if(quest.isActive && quest.isCompleted) {
                quest.completedQuest.color = blackcolor;
                quest.checkIcon.SetActive(true);
            }
        }
        for(int i = 1; i < SpecialquestList.Length; i++) {
            if(!SpecialquestList[i].isActive && !SpecialquestList[i].isCompleted) {
                SpecialquestList[i].completedQuest.color = Color.white;
                SpecialquestList[i].checkIcon.SetActive(false);
            }
        }
        for(int i = 1; i < SpecialquestList.Length; i++) {
            if(SpecialquestList[i].isActive && !SpecialquestList[i].isCompleted) {
                SpecialquestList[i].completedQuest.color = greenColor;
                SpecialquestList[i].checkIcon.SetActive(false);
            }
        }
        for(int i = 1; i < SpecialquestList.Length; i++) {
            if(SpecialquestList[i].isActive && SpecialquestList[i].isCompleted) {
                SpecialquestList[i].completedQuest.color = blackcolor;
                SpecialquestList[i].checkIcon.SetActive(true);
            }
        }
        questMission();


        for(int i = 1; i < SpecialquestList.Length-2; i++) {
            SpecialquestList[i].progressNumber.text = player.setNumber[i - 1] + "/" + SpecialquestList[i].progressbar.maximum;
        }

        //수집품
        SpecialquestList[5].progressNumber.text = player.collect.gettotalCollect() + "/" + SpecialquestList[5].progressbar.maximum;
        SpecialquestList[6].progressNumber.text = player.collect.gettotalCollect() + "/" + SpecialquestList[6].progressbar.maximum;
        SpecialquestList[7].progressNumber.text = player.collect.gettotalCollect() + "/" + SpecialquestList[7].progressbar.maximum;

    }

    public void basicquestCompleted(int questID)
    {
        if(BasicquestList[questID].isActive && !BasicquestList[questID].isCompleted) {
            sound.PlaySound("miniGameScoreShow");
            //퀘스트 완료!
            BasicquestList[questID].isCompleted = true;
            BasicquestList[questID].completedQuest.color = blackcolor;
            BasicquestList[questID].checkIcon.SetActive(true);
            BasicquestList[questID].isActive = false;
            player.money += BasicquestList[questID].goldreward;
        }
    }
    public void specialquestCompleted(int questID)
    {
        if(SpecialquestList[questID].isActive && !SpecialquestList[questID].isCompleted) {
            sound.PlaySound("miniGameScoreShow");
            //퀘스트 완료!
            SpecialquestList[questID].isCompleted = true;
            SpecialquestList[questID].completedQuest.color = blackcolor;
            SpecialquestList[questID].checkIcon.SetActive(true);

            player.diamond += SpecialquestList[questID].diamondreward;
        }
    }
    public void questMission()
    {
        //1번 퀘스트: 보트 10km/h 이상
        if(player.speed >= 10) {
            BasicquestList[0].isActive = true;
        } else {
            BasicquestList[0].isActive = false;
        }

        //2번 퀘스트: 돌 1000개 넘기기
        if(player.item[0].itemNumber >= 10000) {
            BasicquestList[1].isActive = true;
        } else {
            BasicquestList[1].isActive = false;
        }

        //3번 퀘스트: 거리 1km 이상가기
        if(player.distance >= 5) {
            BasicquestList[2].isActive = true;
        } else {
            BasicquestList[2].isActive = false;
        }

        //4번 퀘스트: 금 5개 모으기
        if(player.item[3].itemNumber >= 100) {
            BasicquestList[3].isActive = true;
        } else {
            BasicquestList[3].isActive = false;
        }

        if(player.minerShop.minerNumber >= 6) {
            BasicquestList[4].isActive = true;
        } else {
            BasicquestList[4].isActive = false;
        }

        if(player.is100Over) {
            BasicquestList[5].isActive = true;
        } else {
            BasicquestList[5].isActive = false;
        }

        if(player.item[6].itemNumber >= 1000) {
            BasicquestList[6].isActive = true;
        } else {
            BasicquestList[6].isActive = false;
        }

         BasicquestList[7].isActive = true;

        if(player.speed >= 100) {
            BasicquestList[8].isActive = true;
        } else {
            BasicquestList[8].isActive = false;
        }

        if(player.gamePlayedTime >= 86400) {
            BasicquestList[9].isActive = true;
        } else {
            BasicquestList[9].isActive = false;
        }
        if (player.distance >= 100) {
            BasicquestList[10].isActive = true;
        } else {
            BasicquestList[10].isActive = false;
        }

        if (player.item[7].itemNumber >= 10000) {
            BasicquestList[11].isActive = true;
        } else {
            BasicquestList[11].isActive = false;
        }
        //12번 퀘스트: 보트속도 300km/h
        if (player.speed >= 300) {
            BasicquestList[12].isActive = true;
        } else {
            BasicquestList[12].isActive = false;
        }
        //13번 퀘스트: 돌 150,000,000개 넘기기
        if (player.item[0].itemNumber >= 150000000) {
            BasicquestList[13].isActive = true;
        } else {
            BasicquestList[13].isActive = false;
        }
        //14번 퀘스트: 광부 20명 고용하기
        if (player.minerShop.minerNumber >= 21) {
            BasicquestList[14].isActive = true;
        } else {
            BasicquestList[14].isActive = false;
        }
        //15번 퀘스트: 플레이 200시간
        if (player.gamePlayedTime >= 720000) {
            BasicquestList[15].isActive = true;
        } else {
            BasicquestList[15].isActive = false;
        }



        //스폐셜 1
        if (player.setNumber[0] < 6) {
            SpecialquestList[1].progressbar.current = player.setNumber[0];

        } else {
            SpecialquestList[1].progressbar.current = player.setNumber[0];
            SpecialquestList[1].isActive = true;
        }


        //스폐셜 2
        if(player.setNumber[1] < 6) {
            SpecialquestList[2].progressbar.current = player.setNumber[1];

        } else {
            SpecialquestList[2].progressbar.current = player.setNumber[1];
            SpecialquestList[2].isActive = true;
        }

        //스폐셜 3
        if(player.setNumber[2] < 6) {
            SpecialquestList[3].progressbar.current = player.setNumber[2];

        } else {
            SpecialquestList[3].progressbar.current = player.setNumber[2];
            SpecialquestList[3].isActive = true;
        }

        //스페셜 4
        if(player.setNumber[3] < 6) {
            SpecialquestList[4].progressbar.current = player.setNumber[3];

        } else {
            SpecialquestList[4].progressbar.current = player.setNumber[3];
            SpecialquestList[4].isActive = true;
        }

        if(player.collect.gettotalCollect() < 20) {
            SpecialquestList[5].progressbar.current = player.collect.gettotalCollect();

        } else {
            SpecialquestList[5].progressbar.current = player.collect.gettotalCollect();
            SpecialquestList[5].isActive = true;
        }

        if (player.collect.gettotalCollect() < 50) {
            SpecialquestList[6].progressbar.current = player.collect.gettotalCollect();

        } else {
            SpecialquestList[6].progressbar.current = player.collect.gettotalCollect();
            SpecialquestList[6].isActive = true;
        }

        if (player.collect.gettotalCollect() < 130) {
            SpecialquestList[7].progressbar.current = player.collect.gettotalCollect();

        } else {
            SpecialquestList[7].progressbar.current = player.collect.gettotalCollect();
            SpecialquestList[7].isActive = true;
        }
    }
}
