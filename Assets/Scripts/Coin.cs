using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private int _coinValue;

    public void PickupCoin()
    {
        GameManager.instance.AddCoins(_coinValue);
        //Perhaps spawn some particles and play sound before destroying
        Destroy(gameObject);
    }
}
