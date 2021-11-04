using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Scripts")]
    ShopManager shopManager;
    [SerializeField] CoinManager coinManager;
    [SerializeField] UiManager uiManager;
    [SerializeField] UIChangeColor uIChangeColor;

    [Header("Object")]
    [SerializeField] GameObject Player;
    public Text priceText;
    public GameObject cantBuyText;
    public ParticleSystem feverPs, hpPs;

    [Header("Button")]
    public Button nextBtn , perviousBtn;
    public Button purchasedBtn, selectedBtn;

    [Header("Variable")]
    public int currentCharacterIndex;
    [SerializeField] float timeToshowNoti = 1.5f;
    private void Start()
    {
        cantBuyText.SetActive(false);
        shopManager = ShopManager.shopInstance;
        setBtn();
        setDefaultSkin();
        currentCharacterIndex = PlayerPrefs.GetInt("CharacterSelected");
        Player.GetComponent<Renderer>().material = shopManager.shopdata[currentCharacterIndex].materialCharacter;
    }
    private void Update()
    {
        updateUIShop();
    }
    void setBtn() 
    {
        nextBtn.onClick.AddListener(() => OnNextBtn());
        perviousBtn.onClick.AddListener(() => OnPerviousBtn());
        purchasedBtn.onClick.AddListener(() => purchaseSkin());
        selectedBtn.onClick.AddListener(() => selectedSkin());
    }
    
    void OnNextBtn() 
    {
        currentCharacterIndex++;
        if (currentCharacterIndex == shopManager.shopdata.Length)
            currentCharacterIndex = 0;
        Player.GetComponent<Renderer>().material = shopManager.shopdata[currentCharacterIndex].materialCharacter;
    }
    void OnPerviousBtn() 
    {
        currentCharacterIndex--;
        if (currentCharacterIndex == -1)
            currentCharacterIndex = shopManager.shopdata.Length - 1;
        Player.GetComponent<Renderer>().material = shopManager.shopdata[currentCharacterIndex].materialCharacter;
    }
    void purchaseSkin() 
    {
        int coin = PlayerPrefs.GetInt("Coin");
        if (coin >= shopManager.shopdata[currentCharacterIndex].unlockCost)
        {
            coinManager.useCoin(shopManager.shopdata[currentCharacterIndex].unlockCost);
            uiManager.updateCoinInShop();
            shopManager.shopdata[currentCharacterIndex].purchased = true;
            string keyname = shopManager.shopdata[currentCharacterIndex].characterName + " Purchased";
            PlayerPrefs.SetInt(keyname, shopManager.shopdata[currentCharacterIndex].purchased ? 1 : 0);
            PlayerPrefs.Save();
            
        }
        else
        {
            purchasedBtn.enabled = false;
            StartCoroutine(showNoti());
        }

    }
    void selectedSkin() 
    {
        if (shopManager.shopdata[currentCharacterIndex].purchased)
        {
            PlayerPrefs.SetInt("CharacterSelected", currentCharacterIndex);
            PlayerPrefs.Save();
            shopManager.indexCurrentCharacter = currentCharacterIndex;
            Player.GetComponent<Renderer>().material = shopManager.shopdata[currentCharacterIndex].materialCharacter;
            changeColorPs(shopManager.shopdata[currentCharacterIndex].materialCharacter.color);
            uIChangeColor.enabled = true;
        }

    }
    void setDefaultSkin() 
    {
        shopManager.shopdata[0].purchased = true;
        string keyname = shopManager.shopdata[0].characterName + " Purchased";
        PlayerPrefs.SetInt(keyname, shopManager.shopdata[0].purchased ? 1 : 0);
        PlayerPrefs.Save();
    }
    IEnumerator showNoti()
    {
        
        cantBuyText.SetActive(true);
        yield return new WaitForSeconds(timeToshowNoti);
        cantBuyText.SetActive(false);
        purchasedBtn.enabled = true;
    }
    void updateUIShop()
    {
        if (shopManager.shopdata[currentCharacterIndex].purchased)
        {
            purchasedBtn.gameObject.SetActive(false);
        }
        else
        {
            priceText.text = shopManager.shopdata[currentCharacterIndex].unlockCost.ToString();
            purchasedBtn.gameObject.SetActive(true);
        }

        if (shopManager.indexCurrentCharacter == currentCharacterIndex)
        {
            selectedBtn.gameObject.SetActive(false);
        }
        else
        {
            selectedBtn.gameObject.SetActive(true);
        }
    }
    void changeColorPs(Color color) 
    {
        ParticleSystem playerPs = Player.transform.GetChild(2).GetComponent<ParticleSystem>();
        playerPs.startColor = color;
        feverPs.startColor = color;
        hpPs.startColor = color;
    }
}

