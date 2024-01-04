using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField][Range(1, 20)][Tooltip("Force to move object")] float force;

    Rigidbody rb;
    void Start()
    {
        print("Start");
    }

    private void Awake()
    {
        print("Awake");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * force, ForceMode.VelocityChange);
        }
    }
}
