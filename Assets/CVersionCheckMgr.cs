using System.Collections;
using UnityEngine; 
using UnityEngine.UI;

public class CVersionCheckMgr : MonoBehaviour
{
    private static CVersionCheckMgr instance;
    public static CVersionCheckMgr GetInstance()
    {
        if (!instance) {
            instance = GameObject.FindObjectOfType<CVersionCheckMgr>();
            if (!instance)
                Debug.LogWarning("There needs to be one active CVersionCheckMgr script on a VersionCheckManager in your scene.");
        }
        return instance;
    }


    public string url;              //데이터를 가져올 URL -> https://play.google.com/store/apps/details?id=com.brinicle.ourhomefairy

    //유니티 자체에서 bundleIdentifier를 읽을수도 있지만, 이렇게 읽을 수 도 있다.
    public string _bundleIdentifier
    {
        get
        {
            return url.Substring(url.IndexOf("details"), url.LastIndexOf("details") + 1);
        }
    }


    [HideInInspector]
    public bool isSamePlayStoreVersion = false;

    public bool isTestMode = false;        //테스트 모드 여부


    private void Start()
    {
        if (isTestMode == false)
            StartCoroutine(PlayStoreVersionCheck());
        else
            isSamePlayStoreVersion = true;
    }

    /// <summary>
    /// 버전체크를 하여, 강제업데이트를 체크한다.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayStoreVersionCheck()
    {
        WWW www = new WWW(url);
        yield return www;

        //인터넷 연결 에러가 없다면, 
        if (www.error == null) {
            int index = www.text.IndexOf("softwareVersion");
            string versionText = www.text.Substring(index, 30);

            //플레이스토어에 올라간 APK의 버전을 가져온다.
            int softwareVersion = versionText.IndexOf(">");
            string playStoreVersion = versionText.Substring(softwareVersion + 1, Application.version.Length + 1);

            //버전이 같다면,
            if (playStoreVersion.Trim().Equals(Application.version)) {
                //게임 씬으로 넘어간다.
                Debug.LogWarning("true : " + playStoreVersion + " : " + Application.version);

                //버전이 같다면, 앱을 넘어가도록 한다.
                isSamePlayStoreVersion = true;
            } else {
                //버전이 다르므로, 마켓으로 보낸다.
                Debug.LogWarning("false : " + playStoreVersion + " : " + Application.version);

                //업데이트 팝업을 연결한다.
                Debug.Log("Moving to app");
            }
        } else {
            //인터넷 연결 에러시
            Debug.LogWarning(www.error);
        }
    }

    /// <summary>
    /// 업데이트 팝업에서 업데이트 여부를 체크한다.
    /// </summary>
    public void Call_PlayStoreVersionCheck()
    {
        StartCoroutine(PlayStoreVersionCheck());
    }
}
