using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    [SerializeField][Tooltip("Degrees of rotation per second")] float rate;
    [SerializeField][Tooltip("Axis of rotation")] Vector3 axis;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(rate * Time.deltaTime, axis);
        if(Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.forward * Time.deltaTime;
        }
    }
}
