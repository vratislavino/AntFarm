using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Target
{
    private int food = 0;

    public event Action<Food> AllFoodTaken;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    public void Refill()
    {
        food = Manager.FoodCapacity;
    }

    public void Replace(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public void TakeFood()
    {
        food--;
        if(food <= 0)
        {
            if (AllFoodTaken != null)
                AllFoodTaken(this);
            else
                Debug.Log("NIKDO TĚ NEPOSLOUCHÁ, BUZÍČKU!");
        }
    }

    public override void Use()
    {
        TakeFood();
    }
}
