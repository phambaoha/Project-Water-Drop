using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour, IStoreListener
{
    // tạo một create empty để chứa Script purchase(bắt buộc)
    public static Purchaser purchase;
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    private string productIDNoAds = "No_Ads_Uno";
    public string productIDUnlockAll = "UnlockAll";
    public string productIDcharacter_ca;
    public string productIDcharacter_caduoi;
    public string productIDcharacter_bachtuoc;

    private void Awake()
    {
        if (purchase == null)
            purchase = this;
    }
    private void Start()
    {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }

    private void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(productIDNoAds, ProductType.NonConsumable);
        builder.AddProduct(productIDcharacter_ca, ProductType.Consumable);
        builder.AddProduct(productIDcharacter_caduoi, ProductType.NonConsumable);
        builder.AddProduct(productIDcharacter_bachtuoc, ProductType.NonConsumable);
        builder.AddProduct(productIDUnlockAll, ProductType.NonConsumable);
        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    public void RemoveNoAds()
    {
        BuyProductID(productIDNoAds);
    }
    public void UnlockAll()
    {
        BuyProductID(productIDUnlockAll);
    }
    public void BuyCharacter_ca()
    {
        BuyProductID(productIDcharacter_ca);
    }

    public void BuyCharacter_caduoi()
    {
        BuyProductID(productIDcharacter_caduoi);
    }

    public void BuyCharacter_bachtuoc()
    {
        BuyProductID(productIDcharacter_bachtuoc);
    }
    private void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }
    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) => {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
           Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);

    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, productIDcharacter_ca, StringComparison.Ordinal))
        {
              SwitchCharacter.Sc.Result_Buycharacter_Ca();
              PlayerPrefs.SetInt("buy_char_ca", 1);
        }
        if (String.Equals(args.purchasedProduct.definition.id, productIDcharacter_caduoi, StringComparison.Ordinal))
        {
            SwitchCharacter.Sc.Result_Buycharacter_Caduoi();
            PlayerPrefs.SetInt("buy_char_caduoi", 1);
        }
        if (String.Equals(args.purchasedProduct.definition.id, productIDcharacter_bachtuoc, StringComparison.Ordinal))
        {
            SwitchCharacter.Sc.Result_Buycharacter_Bachtuoc();
            PlayerPrefs.SetInt("buy_char_bachtuoc", 1);
        }
        if (String.Equals(args.purchasedProduct.definition.id, productIDNoAds, StringComparison.Ordinal))
        {
            SwitchCharacter.Sc.Result_removeAds();
           
        }
        else if (String.Equals(args.purchasedProduct.definition.id, productIDUnlockAll, StringComparison.Ordinal))
        {
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}