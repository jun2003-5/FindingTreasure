using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerPos : MonoBehaviour
{
    public GameObject minePos;


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(minePos.transform.position.x, minePos.transform.position.y- 0.22f, transform.position.z);
        transform.localScale = new Vector3(minePos.transform.localScale.x * 3, transform.localScale.y, transform.localScale.z);
    }
}
 