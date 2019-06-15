using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    string moveAxisName = "Vertical";
    string turnAxisName = "Horizontal";

    float moveAxis;
    float turnAxis;

    float moveSpeed = 10;
    float turnSpeed = 240;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveAxis = Input.GetAxis(moveAxisName);//1 ve -1 değerlerini atıyor
        turnAxis = Input.GetAxis(turnAxisName);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            Shoot();
    }

    private void Shoot()
    {
        GetComponent<ShootBehaviour>().Shoot();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * moveAxis * moveSpeed * Time.deltaTime);
        Quaternion rotation = Quaternion.Euler(0, turnAxis * turnSpeed * Time.deltaTime, 0);
        rb.MoveRotation(transform.rotation * rotation);
    }


}
