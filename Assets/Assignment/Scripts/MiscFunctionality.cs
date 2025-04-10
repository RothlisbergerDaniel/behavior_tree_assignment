using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscFunctionality : MonoBehaviour
{

    public Vector3 velocity;
    public GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) //script to check player collisions, then trigger a respawn
    {
        GameObject c = collision.gameObject;
        if (c.tag == "Player")
        {
            c.GetComponent<MovePlayer>().respawn(); //respawn player
                                                    //search for enemy objects here, then respawn them too
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Respawner>().respawn();
            }
        }

    }
}
