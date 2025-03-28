using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_BuffSignifier : MonoBehaviour
{
    // Start is called before the first frame update
    //private bool draw = false;
    public float radius;
    private Color sc = new Color(1f, 0.5f, 1f, 1f); //make the sphere semitransparent purple

    //public void doSphereGizmo(float rad, bool enable)
    //{
    //    radius = rad;
    //    draw = enable;
    //}

    private void OnDrawGizmos()
    {
        //if (draw)
        //{
            Gizmos.color = sc;
            Gizmos.DrawWireSphere(transform.position - new Vector3(0, 0.9f, 0), radius);
        //}
    }
}
