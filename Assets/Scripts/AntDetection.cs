using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AntDetection : MonoBehaviour
{

    public Transform Detector;

    private AntStatus antStatus;
    private AntDirection[] directions = new AntDirection[5];

    private const float neededDistance = 1f;

    private const int range = 180;

    Dictionary<string, int> costs = new Dictionary<string, int>()
    {
        { "Wall", -100 },
        { "Target", 100 },
        { "TargetPheromone", 1 }
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
            Vector3 v = (Quaternion.AngleAxis(directions[i].Angle, Vector3.up)*transform.rotation) * Vector3.forward;
            
            var all = Physics.RaycastAll(Detector.position, v, 8);

            if(all != null && all.Length > 0)
            {
                int cost = 0;
                foreach (RaycastHit hit in all)
                {
                    var tag = hit.collider.tag;

                    if (costs.ContainsKey(tag))
                    {
                        cost += costs[tag];
                        //directions[i].Rating = costs[tag];
                        //Debug.DrawRay(Detector.position, v * 8, Color.red, 0.1f);
                    }
                    else
                    {
                        if (tag.Contains(antStatus.Target))
                        {
                            // directions[i].Rating = costs[tag.Replace(antStatus.Target, "Target")];
                            cost += costs[tag.Replace(antStatus.Target, "Target")];
                            if (costs[tag.Replace(antStatus.Target, "Target")] == 100)
                            {
                                if (Vector3.Distance(transform.position, hit.point) < neededDistance)
                                {
                                    antStatus.SwitchState();
                                    hit.collider.GetComponentInParent<Target>().Use();
                                }
                            }
                            //Debug.DrawRay(Detector.position, v * 8, Color.green, 0.1f);
                        }
                        else
                        {
                            cost += 0;
                            //directions[i].Rating = 0;
                        }
                    }
                }
                directions[i].Rating = cost;
            } else
            {
                directions[i].Rating = 0;
            }
        }
    }
}
