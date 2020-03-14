using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntStatus : MonoBehaviour
{
    private AntState currentAntState;

    public string Target
    {
        get
        {
            return currentAntState == AntState.SearchingForFood ? "Food" : "Hive";
        }
    }

    [SerializeField]
    private GameObject FoodPheromonePrefab;
    [SerializeField]
    private GameObject HivePheromonePrefab;

    // Start is called before the first frame update
    void Start()
    {
        currentAntState = AntState.SearchingForFood;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO CREATE PHEROMONES
    }
}

public enum AntState
{
    SearchingForFood,
    SearchingForHive
}
