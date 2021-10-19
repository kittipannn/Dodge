using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChangeColor : MonoBehaviour
{

    [SerializeField] Image[] imageUI;
    [SerializeField] Text[] textUI;
    [SerializeField] Shop shop;
    [SerializeField] ShopManager shopManager;
    int currentIndex;
    void Start()
    {
        currentIndex = PlayerPrefs.GetInt("CharacterSelected");

        
        for (int i = 0; i < imageUI.Length; i++)
        {
            
            if (imageUI[i].color != shopManager.shopdata[currentIndex].materialCharacter.color)
            {
                imageUI[i].color = shopManager.shopdata[currentIndex].materialCharacter.color;
            }
        }
        for (int i = 0; i < textUI.Length; i++)
        {
            if (textUI[i].color != shopManager.shopdata[currentIndex].materialCharacter.color)
            {
                textUI[i].color = shopManager.shopdata[currentIndex].materialCharacter.color;
            }
        }

        Invoke("disableScript", 1);
    }

    private void OnEnable()
    {
        currentIndex = PlayerPrefs.GetInt("CharacterSelected");
        for (int i = 0; i < imageUI.Length; i++)
        {

            if (imageUI[i].color != shopManager.shopdata[currentIndex].materialCharacter.color)
            {
                imageUI[i].color = shopManager.shopdata[currentIndex].materialCharacter.color;
            }
        }
        for (int i = 0; i < textUI.Length; i++)
        {
            if (textUI[i].color != shopManager.shopdata[currentIndex].materialCharacter.color)
            {
                textUI[i].color = shopManager.shopdata[currentIndex].materialCharacter.color;
            }
        }
        Invoke("disableScript", 1);
    }
    void disableScript()
    {
        this.enabled = false;
    }


}