using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public int value;

    public Coin()
    {
        this.value = 1;
    }

    void OnTriggerEnter(Collider other)
    {
        GlobalVariables.totalCoins += this.value;
        //Debug.Log("Total Coins: " + GlobalVariables.totalCoins);
        GameObject.FindGameObjectWithTag("BasicUI").GetComponent<BasicUI>().UpdateCoinText();
        Destroy(this.gameObject);
    }
}
