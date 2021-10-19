using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPShop : MonoBehaviour
{
    [SerializeField] AdsManager adsManager;
    private string removeAds = "com.varisoft.dodge.removeads";
    //public GameObject restorePurchaseBtn;
    void Start()
    {
        //disableRestorePurchaseBtn();
    }

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == removeAds)
        {
            Debug.Log("Purchase Complete");
            adsManager.removeAds();
            GameObject.FindObjectOfType<UiManager>().updateUIAds();
            PlayerPrefs.SetInt("ads", 0);
        }
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("Purchase of " + product.definition.id + " failed due to " + reason);
    }
    //private void disableRestorePurchaseBtn()
    //{
    //    if (Application.platform != RuntimePlatform.IPhonePlayer)
    //    {
    //        restorePurchaseBtn.SetActive(false);
    //    }
    //}
}
