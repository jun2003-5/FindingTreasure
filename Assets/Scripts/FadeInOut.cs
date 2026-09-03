using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public Image rend;
    // Start is called before the first frame update
    void Start()
    {
        rend.color = new Color(1, 1, 1, 0f);
    }
 
    async void FadeInandOut()
    {
        for(float f = 0.05f; f <= 1.3; f += 0.025f) {
            rend.color = new Color(1, 1, 1, f);
            await Task.Delay(15);
        }
        await Task.Delay(1000);
        for(float f = 1f; f >= -0.1f; f -= 0.025f) {
            rend.color = new Color(1, 1, 1, f);
            await Task.Delay(15);
        }
    }

    public void startFading()
    {
        FadeInandOut();
    }
}
