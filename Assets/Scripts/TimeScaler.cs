using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float f = Input.GetAxisRaw("Vertical");
        if(f < 0)
        {
            Time.timeScale -= 0.05f;
        }
        if(f > 0)
        {
            Time.timeScale += 0.05f;
        }
    }
}
