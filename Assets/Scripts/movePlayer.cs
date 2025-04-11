using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public class playerMovement : MonoBehaviour
{

    Rigidbody rb;
    float speed = 5f;
    Vector3 direction;
    public Transform orientation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MyInput();
    }
    void MyInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = orientation.forward * vertical + orientation.right * horizontal;

    }
    private void FixedUpdate()
    {
        Debug.Log(direction.normalized * speed);
        rb.linearVelocity = direction.normalized * speed;
    }
}