using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSimulation : MonoBehaviour
{
    public float waterDensity = 20f;

    private Rigidbody woodRb;

    private void Awake()
    {
        woodRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        WaterResistance();
    }

    private void WaterResistance()
    {
        float divePercent = -transform.position.y + transform.localScale.x * 0.5f;
        divePercent = Mathf.Clamp(divePercent, 0f, 1f);

        woodRb.AddForce(Vector3.up * divePercent * waterDensity);
        woodRb.drag = divePercent * 2f;
        woodRb.angularDrag = divePercent * 2f;
    }
}
