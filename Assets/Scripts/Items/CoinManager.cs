﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public Text CoinNum;
    public int Coins = 0;
    void Start()
    {
        Coins = 0;
    }
    public void AddCoin(int num)
    {
        Coins += num;
    }
    void Update()
    {
        CoinNum.text = "Coins: " + Coins;
    }
}