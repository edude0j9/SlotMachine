using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Text balanceText;

    private int balance = 5000;

    private void SetTextValue()
    {
        balanceText.text = balance.ToString();
    }

    public void ReduceBalance(int value)
    {
        balance -= value;
        SetTextValue();
    }

    public void AddBalance(int value, int multiplier)
    {
        balance += (value * multiplier);
        SetTextValue();
    }

    public int CheckBalance()
    {
        return balance;
    }

    void Start()
    {
        SetTextValue();
    }
}
