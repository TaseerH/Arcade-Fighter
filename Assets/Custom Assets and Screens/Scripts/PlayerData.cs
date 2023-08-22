using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string name;
    public int lockStatus;
    public int price;

    public PlayerData(string name, int lockStatus, int price)
    {
        this.name = name;
        this.lockStatus = lockStatus;
        this.price = price;
    }

    public override string ToString()
    {
        return $"{name} has {lockStatus} lock status and his price is {price}";
    }
}
