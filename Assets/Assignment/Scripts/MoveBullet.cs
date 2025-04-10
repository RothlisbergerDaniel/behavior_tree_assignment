using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{

    public float speed;
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy"); //populate list with enemies to be respawned
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
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

        Destroy(gameObject); //remove bullet on ANY collision
    }
}
