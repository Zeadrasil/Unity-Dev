using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float pitch = 30;
    [SerializeField] private float yaw = 0;
    [SerializeField] private float distance = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        yaw += Input.GetAxis("Mouse X") * 0.2f;
        Quaternion qpitch = Quaternion.AngleAxis(pitch, Vector3.right);
        Quaternion qyaw = Quaternion.AngleAxis(yaw, Vector3.up);
        Quaternion rotation = qyaw * qpitch;
        transform.position = target.position + rotation * (Vector3.back * distance);
        transform.rotation = qpitch;
    }
}
