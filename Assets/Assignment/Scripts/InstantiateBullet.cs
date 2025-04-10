using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject spawn(GameObject b)
    {
        GameObject bullet = Instantiate(b, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        bullet.transform.Translate(Vector3.forward * 2);
        return bullet; //return instantiated bullet
    }
}
