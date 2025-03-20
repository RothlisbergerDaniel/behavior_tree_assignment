using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSearchSphere : MonoBehaviour
{
    private bool draw = false;
    private float radius;
    private Color sc = new Color(0.7f, 0.8f, 1f, 0.5f); //make the sphere semitransparent blue
    public void doSphereGizmo(float rad, bool enable)
    {
        radius = rad;
        draw = enable;
    }

    private void OnDrawGizmos()
    {
        if (draw)
        {
            Gizmos.color = sc;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
