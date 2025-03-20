using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGarbage : MonoBehaviour
{
    public void destroyGarbage()
    {
        Destroy(gameObject); //you can't call Destroy() from a NodeCanvas script, so we've gotta call it here.
    }
}
