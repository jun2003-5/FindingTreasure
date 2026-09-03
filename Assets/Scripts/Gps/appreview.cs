using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appreview : MonoBehaviour
{
    public void Rate()
    {
        Application.OpenURL("market://details?id=com.junsgame.pirateship");
    }

    public void FeedBack()
    {
        Application.OpenURL("mailto:jungames0519@gmail.com");
    }
}
 