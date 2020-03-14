using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AntDetection : MonoBehaviour
{

    public Transform Detector;

    private AntStatus antStatus;
    private AntDirection[] directions = new AntDirection[15];

    private const int range = 240;

    Dictionary<string, int> costs = new Dictionary<string, int>()
    {
        { "Wall", -10 },
        { "Target", 10 },
        { "TargetPheromone", 6 }
    };
    

    public int GetTargetDirection {
        get {
            int max = directions.ToList().Max(x => x.Rating);
            List<AntDirection> bestRatings = directions.ToList().Where(x => x.Rating == max).ToList();
            return bestRatings[(int)(Random.Range(0,bestRatings.Count))].Angle;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        antStatus = GetComponent<AntStatus>();
        CalculateDirections();
    }

    private void CalculateDirections()
    {
        for (int i = 0; i < directions.Length; i++)
            directions[i] = new AntDirection();

        int angle = range / (directions.Length - 1);
        int middleIndex = directions.Length / 2;
        directions[middleIndex].Angle = 0;
        for(int i = 0; i < middleIndex; i++)
        {
            directions[middleIndex - i - 1].Angle = -(i + 1) * angle;
            directions[middleIndex + i + 1].Angle = (i + 1) * angle;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < directions.Length; i++)
        {
            Vector3 v = Quaternion.AngleAxis(directions[i].Angle, Vector3.up) * Vector3.forward;
            
            RaycastHit hit;
            if(Physics.Raycast(Detector.position, v, out hit, 2))
            {
                var tag = hit.collider.tag;
                Debug.DrawRay(Detector.position, v * 2, Color.red, 0.1f);

                if(costs.ContainsKey(tag))
                {
                    directions[i].Rating = costs[tag];
                } else
                {   
                    if(tag.Contains(antStatus.Target))
                    {
                        directions[i].Rating = costs[tag.Replace(antStatus.Target, "Target")];
                    } else
                    {
                        directions[i].Rating = 0;
                    }
                }
            } else
            {
                directions[i].Rating = 0;
            }
        }
    }
}
