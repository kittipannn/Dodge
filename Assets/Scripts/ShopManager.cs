using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager shopInstance;
    public Shopdata[] shopdata;
    public int indexCurrentCharacter;
    private void Awake()
    {
        if (shopInstance == null)
            shopInstance = this;
        indexCurrentCharacter = PlayerPrefs.GetInt("CharacterSelected");
        for (int i = 0; i < shopdata.Length; i++)
        {
            string keyname = shopdata[i].characterName + " Purchased";
            shopdata[i].purchased = PlayerPrefs.GetInt(keyname) == 1 ? true : false;
        }
    }
}
[System.Serializable]
public class Shopdata 
{
    public string characterName;
    public Material materialCharacter;
    public int unlockCost;
    public bool purchased;
}

