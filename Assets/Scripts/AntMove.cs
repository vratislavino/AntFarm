using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntMove : MonoBehaviour
{
    AntDetection antDetection;
    public float speed = 5f;
    private Quaternion targetRot;

    void Start()
    {
        antDetection = GetComponent<AntDetection>();

        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        InvokeRepeating("ChangeRotation", 0, 0.5f);
    }
    
    void ChangeRotation()
    {
        int angle = antDetection.GetTargetDirection;
        targetRot = transform.rotation * Quaternion.Euler(0, angle, 0);
    }

    void Update()
    {
        // GENERUJE ÚHEL GLOBÁLNĚ, NE LOKÁLNĚ
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 0.1f);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
