using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargerBuffHandler : MonoBehaviour
{

    public float maxBuffDuration;
    [NonSerialized]
    public float buffTimer;
    public NavMeshAgent nma;
    public float buffSpeed;
    public float defaultSpeed;
    public float buffAccel;
    public float defaultAccel;

    // Start is called before the first frame update
    void Start()
    {
        buffTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (buffTimer > 0)
        {
            buffTimer -= Time.deltaTime;
            if (buffTimer <= 0)
            {
                buffTimer = 0;
                nma.speed = defaultSpeed;
                nma.acceleration = defaultAccel;
            }
        }
    }

    public void buff()
    {
        buffTimer = maxBuffDuration;
        nma.speed = buffSpeed;
        nma.acceleration = buffAccel;
    }
}
