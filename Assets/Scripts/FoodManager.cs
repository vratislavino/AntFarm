using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    Vector2 randX = new Vector2(-45, 45);
    Vector2 randZ = new Vector2(-20, 20);

    public GameObject FoodPrefab;
    
    void Start()
    {
        CreateFoods();
    }

    void CreateFoods()
    {
        for(int i = 0; i < Manager.FoodCount; i++)
        {
            Food f = Instantiate(FoodPrefab, transform).GetComponent<Food>();
            ReplaceFood(f);
            f.Refill();
            f.AllFoodTaken += OnAllFoodTaken;
        }
    }

    private void ReplaceFood(Food f)
    {
        f.Replace(new Vector3(UnityEngine.Random.Range(randX.x, randX.y), 0, UnityEngine.Random.Range(randZ.x, randZ.y)));
    }

    private void OnAllFoodTaken(Food f)
    {
        ReplaceFood(f);
        f.Refill();
    }
}
