using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTac : MonoBehaviour
{
    Rigidbody rb;
    const float TimeToSettle = 5f;
    float time = 0;

    public Hive hive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(Random.Range(-10, 10), 10, Random.Range(-10, 10)), ForceMode.Impulse);
        rb.AddTorque(new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), Random.Range(-30, 30)));
    }

    // Update is called once per frame
    void Update()
    {
        if(time < TimeToSettle)
        {
            time += Time.deltaTime;
            if(rb.velocity.magnitude < 0.5)
            {
                Instantiate(hive.AntPrefab, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);

                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
