using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    float speed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-10, 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
    }
}
