using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntStatus : MonoBehaviour
{
    private AntState currentAntState;

    public float TimeToNextPher = 1f;
    private float currentTime = 0;

    private float timeAlive = 0;

    public string Target
    {
        get { return currentAntState == AntState.SearchingForFood ? "Food" : "Hive"; }
    }

    private GameObject PheromonePrefab
    {
        get { return currentAntState == AntState.SearchingForFood ? FoodPheromonePrefab : HivePheromonePrefab; }
    }

    [SerializeField]
    private GameObject FoodPheromonePrefab;
    [SerializeField]
    private GameObject HivePheromonePrefab;
    
    void Start()
    {
        AntManager.instance.Add(1);
        currentAntState = AntState.SearchingForFood;
    }
    
    void Update()
    {
        timeAlive += Time.deltaTime;
        if(timeAlive > Manager.AntLifetime)
        {
            Destroy(gameObject);
        }

        currentTime += Time.deltaTime;
        if(currentTime >= TimeToNextPher)
        {
            currentTime = 0;
            Destroy(Instantiate(PheromonePrefab, transform.position, Quaternion.identity), Manager.PheromoneLasting);
        }
    }

    public void SwitchState()
    {
        timeAlive = 0;
        currentAntState = currentAntState == AntState.SearchingForFood ? AntState.SearchingForHive : AntState.SearchingForFood;
    }

    private void OnDestroy()
    {
        AntManager.instance.Add(-1);
    }
}

public enum AntState
{
    SearchingForFood,
    SearchingForHive
}
