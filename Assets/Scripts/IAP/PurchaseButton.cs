using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseButton : MonoBehaviour
{
    public enum PurchaseType { Diamond100,Diamond300,Diamond500,Diamond1050,Diamond1500,Diamond5000,Diamond10000,Diamond50000,AutoMiner,DiamondSale };
    public PurchaseType purchaseType;

    public void ClickPurchaseButton()
    {
        switch (purchaseType) {
            case PurchaseType.Diamond100:
                IAPManager.instance.buyDiamond100();
                break;
            case PurchaseType.Diamond300:
                IAPManager.instance.buyDiamond300();
                break;
            case PurchaseType.Diamond500:
                IAPManager.instance.buyDiamond500();
                break;
            case PurchaseType.Diamond1050:
                IAPManager.instance.buyDiamond1050();
                break;
            case PurchaseType.Diamond1500:
                IAPManager.instance.buyDiamond1500();
                break;
            case PurchaseType.Diamond5000:
                IAPManager.instance.buyDiamond5000();
                break;
            case PurchaseType.Diamond10000:
                IAPManager.instance.buyDiamond10000();
                break;
            case PurchaseType.Diamond50000:
                IAPManager.instance.buyDiamond50000();
                break;
            case PurchaseType.AutoMiner:
                IAPManager.instance.buyAutominer();
                break;
            case PurchaseType.DiamondSale:
                IAPManager.instance.diamondSale();
                break;

        }
    }
}
