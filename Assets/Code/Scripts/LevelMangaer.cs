using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager main;


    public Transform startPoint;
    public Transform[] path;

    public int currency;



    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 100;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            // buy item
            currency -= amount;
            return true;
        }
        else
        {
            Debug.LogWarning("Not enough currency to spend!");
            // ui로 수정 필요
            return false;
        }
    }


}