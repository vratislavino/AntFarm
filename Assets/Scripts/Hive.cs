using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : Target
{
    public GameObject AntPrefab;
    public GameObject TicTacPrefab;
    int food = 0;

    public bool CreateNew = false;

    public override void Use()
    {
        food++;
        TryToCreateNewAnt();
    }

    void TryToCreateNewAnt()
    {
        if(food >= Manager.FoodNeededToBornNewAntWithTicTacCapsuleThrownInTheAir)
        {
            food -= Manager.FoodNeededToBornNewAntWithTicTacCapsuleThrownInTheAir;
            CreateTicTacCapsule();
        }
    }
    
    void CreateTicTacCapsule()
    {
        TicTac tt = Instantiate(TicTacPrefab, transform.position, Quaternion.identity).GetComponent<TicTac>();
        tt.hive = this;
    }

    private void Update()
    {
        if (CreateNew)
        {
            CreateNew = false;
            Start();
        }
    }
    void Start()
    {
        for(int i = 0; i < Manager.AntCount; i++)
        {
            Instantiate(AntPrefab, transform.position, Quaternion.identity);
        }    
    }
}
