using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntManager : MonoBehaviour
{
    public static AntManager instance;

    public Text countText;

    private int AntCount;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void Add(int count)  
    {
        AntCount += count;
        countText.text = AntCount + " mravenců";
    }
}
