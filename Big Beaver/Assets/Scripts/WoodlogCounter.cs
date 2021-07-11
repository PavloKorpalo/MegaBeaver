using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodlogCounter : MonoBehaviour
{
    public Text numberOfWood;

    public float woodLeft;
    void Start()
    {
        numberOfWood.text = "Wood left:" + woodLeft;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void UpdateScore()
    {

    }
}
