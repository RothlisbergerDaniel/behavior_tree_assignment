using NodeCanvas.Tasks.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    private Vector3 vel;
    private Rigidbody rb;
    public float speed;
    public float friction;

    // Start is called before the first frame update
    void Start()
    {
        vel = new Vector3();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        vel += Vector3.Normalize(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))) * speed * Time.deltaTime; //adjust player velocity
        vel *= friction; //reduce velocity based on specified friction
        rb.velocity = vel; //update player velocity
    }
}
