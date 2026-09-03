using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatMove : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float y = Mathf.PingPong(Time.time * 1.5f, 1) * 0.1f + 0.6f;
        transform.position = new Vector3(-0.1f, y, 0);
    }
}
