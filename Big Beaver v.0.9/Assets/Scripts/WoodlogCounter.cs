using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodlogCounter : MonoBehaviour
{
    public Text numberOfWood;
    public GameObject gameWin;

    public float woodLeft;
    void Start()
    {
        numberOfWood.text = "Wood left:" + woodLeft;
    }

    private void Update()
    {
        if(woodLeft == 0)
        {
            GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Woodlog"))
        {
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        woodLeft--;
        numberOfWood.text = "Wood left:" + woodLeft;
    }

    private void GameOver()
    {
        gameWin.SetActive(true);
    }
}
