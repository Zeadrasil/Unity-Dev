using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField][Tooltip("Movement speed in m/s")] float movementSpeed;
    [SerializeField][Tooltip("Whether this object can go forwards")] bool canGoForwards = true;
    [SerializeField][Tooltip("Whether this object can go left")] bool canGoLeft = true;
    [SerializeField][Tooltip("Whether this object can go right")] bool canGoRight = true;
    [SerializeField][Tooltip("Whether this object can go backwards")] bool canGoBackwards = true;
    [SerializeField][Tooltip("Rotation speed in degrees/s")] float rotationSpeed;
    [SerializeField][Tooltip("Whether this object can turn without moving")] bool canTurnIndependently = true;
    [SerializeField][Tooltip("Whether this object can go into third person")] bool canThirdPerson;
    [SerializeField][Tooltip("The object upon which rotation is centered upon")] GameObject? center;
    private bool thirdPerson = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool facing = false, side = false;
        Vector3 difference = transform.position;
        if (center != null)
        {
            difference = transform.position - center.transform.position;
            transform.position = center.transform.position;
        }
        bool moving = false;
        if (thirdPerson)
        {
            transform.position += transform.forward * 3;
        }
        if (Input.GetKey(KeyCode.W) && canGoForwards)
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
            moving = true;
            facing = !facing;
        }
        if (Input.GetKey(KeyCode.S) && canGoBackwards)
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime * -1;
            moving = true;
            facing = !facing;
        }
        if (Input.GetKey(KeyCode.D) && canGoRight)
        {
            transform.position += transform.right * movementSpeed * Time.deltaTime;
            moving = true;
            side = !side;
        }
        if (Input.GetKey(KeyCode.A) && canGoLeft)
        {
            transform.position += transform.right * movementSpeed * Time.deltaTime * -1;
            moving = true;
            side = !side;
        }
        if(!(facing || side))
        {
            moving = false;
        }
        bool rotating = false;
        if (Input.GetKey(KeyCode.LeftArrow) && (moving || canTurnIndependently))
        {
            transform.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, new Vector3(0, -1, 0));
            difference = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, new Vector3(0, -1, 0)) * difference;
            rotating = !rotating;
        }
        if (Input.GetKey(KeyCode.RightArrow) && (moving || canTurnIndependently))
        {
            transform.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, new Vector3(0, 1, 0));
            difference = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, new Vector3(0, 1, 0)) * difference;
            rotating = !rotating;
        }
        moving = moving || rotating;
        if(Input.GetKeyDown(KeyCode.F3) && canThirdPerson)
        {
            thirdPerson = !thirdPerson;
        }
        if (thirdPerson)
        {
            transform.position -= transform.forward * 3;
        }
        if(center != null)
        {
            transform.position += difference;
        }
        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            if (moving && !source.isPlaying)
            {
                source.Play();
            }
            else if(!moving && source.isPlaying)
            {
                source.Pause();
            }
        }
    }
}
