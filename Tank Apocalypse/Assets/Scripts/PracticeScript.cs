using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeScript : BaseController
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        MoveTank();
    }
    void MoveTank()
    {
        rb.MovePosition(rb.position + speed * Time.deltaTime);
    }
}
