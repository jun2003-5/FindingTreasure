using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    public static IAPManager instance;

    private StoreController m_StoreController;
    private Dictionary<string, Product> m_ProductMap = new Dictionary<string, Product>();

    [Header("References")]
    public Player player;
    public GameObject loading;

    [Header("Product IDs")]
    public string Product1;
    public string Product2;
    public string Product3;
    public string Product4;
    public string Product5;
    public string Product6;
    public string Product7;
    public string Product8;
    public string Product9;
    public string DiamondSalestring;

    private bool isInitializing = false;
    private bool productsFetched = false;

    private void Awake()
    {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private async void Start()
    {
        await InitializePurchasing();
    }

    public bool IsInitialized()
    {
        return m_StoreController != null && productsFetched;
    }

    public async System.Threading.Tasks.Task InitializePurchasing()
    {
        if (isInitializing || IsInitialized())
            return;

        isInitializing = true;

        try {
            m_StoreController = UnityIAPServices.StoreController();

            // 이벤트 연결
            m_StoreController.OnPurchasePending += OnPurchasePending;
            m_StoreController.OnPurchaseFailed += OnPurchaseFailed;
            m_StoreController.OnPurchasesFetched += OnPurchasesFetched;
            m_StoreController.OnProductsFetched += OnProductsFetched;
            m_StoreController.OnProductsFetchFailed += OnProductsFetchFailed;
            m_StoreController.OnPurchasesFetchFailed += OnPurchasesFetchFailed;
            m_StoreController.OnStoreDisconnected += OnStoreDisconnected;

            Debug.Log("IAP: Connecting to store...");
            await m_StoreController.Connect();
            Debug.Log("IAP: Store connected.");

            var productsToFetch = new List<ProductDefinition>
            {
                new ProductDefinition(Product1, ProductType.Consumable),
                new ProductDefinition(Product2, ProductType.Consumable),
                new ProductDefinition(Product3, ProductType.Consumable),
                new ProductDefinition(Product4, ProductType.Consumable),
                new ProductDefinition(Product5, ProductType.Consumable),
                new ProductDefinition(Product6, ProductType.Consumable),
                new ProductDefinition(Product7, ProductType.Consumable),
                new ProductDefinition(Product8, ProductType.Consumable),
                new ProductDefinition(Product9, ProductType.Consumable),
                new ProductDefinition(DiamondSalestring, ProductType.Consumable)
            };

            Debug.Log("IAP: Fetching products...");
            m_StoreController.FetchProducts(productsToFetch);
        }
        catch (Exception ex) {
            Debug.LogError("IAP Initialize Exception: " + ex);
        }
        finally {
            isInitializing = false;
        }
    }

    private void OnProductsFetched(List<Product> products)
    {
        Debug.Log($"IAP: Products fetched successfully. Count = {products.Count}");

        m_ProductMap.Clear();

        foreach (var product in products) {
            if (product != null && product.definition != null) {
                m_ProductMap[product.definition.id] = product;
                Debug.Log($"IAP Product Loaded: {product.definition.id}");
            }
        }

        productsFetched = true;

        // 기존 구매 내역 조회
        m_StoreController.FetchPurchases();
    }

    private void OnProductsFetchFailed(ProductFetchFailed failure)
    {
        Debug.LogError($"IAP: Products fetch failed. Failure = {failure}");
    }

    private void OnPurchasesFetched(Orders orders)
    {
        int confirmedCount = orders.ConfirmedOrders != null ? orders.ConfirmedOrders.Count : 0;
        int pendingCount = orders.PendingOrders != null ? orders.PendingOrders.Count : 0;
        int deferredCount = orders.DeferredOrders != null ? orders.DeferredOrders.Count : 0;

        Debug.Log($"IAP: Purchases fetched. Confirmed={confirmedCount}, Pending={pendingCount}, Deferred={deferredCount}");
    }

    private void OnPurchasesFetchFailed(PurchasesFetchFailureDescription failure)
    {
        Debug.LogError($"IAP: Purchases fetch failed. Message={failure.Message}");
    }

    private void OnStoreDisconnected(StoreConnectionFailureDescription failure)
    {
        Debug.LogError($"IAP: Store disconnected. Message={failure.message}");
    }

    private void OnPurchasePending(PendingOrder order)
    {
        try {
            if (order == null || order.CartOrdered == null || order.CartOrdered.Items() == null) {
                Debug.LogError("IAP: Pending order is null or invalid.");
                return;
            }

            foreach (var item in order.CartOrdered.Items()) {
                if (item == null || item.Product == null || item.Product.definition == null)
                    continue;

                string productId = item.Product.definition.id;
                Debug.Log("IAP: Purchase pending product = " + productId);

                GrantProduct(productId);
            }

            // 지급 끝났으면 구매 확정
            m_StoreController.ConfirmPurchase(order);

            Debug.Log("IAP: Purchase confirmed.");
        }
        catch (Exception ex) {
            Debug.LogError("IAP: Error while processing pending purchase: " + ex);
        }
        finally {
            if (loading != null)
                loading.SetActive(false);
        }
    }

    private void OnPurchaseFailed(FailedOrder failedOrder)
    {
        string details = failedOrder != null ? failedOrder.Details : "Unknown";
        Debug.LogError("IAP: Purchase failed. Details = " + details);

        if (loading != null)
            loading.SetActive(false);
    }

    private void GrantProduct(string productId)
    {
        if (string.Equals(productId, Product1, StringComparison.Ordinal)) {
            player.diamond += 100;
        } else if (string.Equals(productId, Product2, StringComparison.Ordinal)) {
            player.diamond += 300;
        } else if (string.Equals(productId, Product3, StringComparison.Ordinal)) {
            player.minerclone.isAutomatic = true;
            player.minerclone.autoBought = true;
            player.special.legendaryPickaxed = true;
        } else if (string.Equals(productId, Product4, StringComparison.Ordinal)) {
            player.diamond += 500;
        } else if (string.Equals(productId, Product5, StringComparison.Ordinal)) {
            player.diamond += 1050;
        } else if (string.Equals(productId, Product6, StringComparison.Ordinal)) {
            player.diamond += 1500;
        } else if (string.Equals(productId, Product7, StringComparison.Ordinal)) {
            player.diamond += 5000;
        } else if (string.Equals(productId, Product8, StringComparison.Ordinal)) {
            player.diamond += 10000;
        } else if (string.Equals(productId, Product9, StringComparison.Ordinal)) {
            player.diamond += 50000;
        } else if (string.Equals(productId, DiamondSalestring, StringComparison.Ordinal)) {
            player.diamond += 3000;
            player.minerclone.isAutomatic = true;
            player.minerclone.autoBought = true;
            player.special.legendaryPickaxed = true;
        } else {
            Debug.LogWarning("IAP: Unknown product id received: " + productId);
        }
    }

    private void BuyProductID(string productId)
    {
        if (!IsInitialized()) {
            Debug.LogWarning("IAP: Not initialized yet.");
            return;
        }

        if (!m_ProductMap.TryGetValue(productId, out Product product) || product == null) {
            Debug.LogWarning("IAP: Product not found in fetched products: " + productId);
            return;
        }

        if (loading != null)
            loading.SetActive(true);

        Debug.Log("IAP: Purchasing product = " + productId);
        m_StoreController.PurchaseProduct(product);
    }

    public void RestorePurchases()
    {
        if (m_StoreController == null) {
            Debug.LogWarning("IAP: RestorePurchases failed. StoreController is null.");
            return;
        }

        Debug.Log("IAP: RestorePurchases started...");

        // v5에서는 PurchaseService / StoreController 쪽 restore API 사용
        m_StoreController.RestoreTransactions((success, message) =>
        {
            Debug.Log($"IAP: RestorePurchases result = {success}, message = {message}");
        });
    }

    public void buyDiamond100()
    {
        BuyProductID(Product1);
    }

    public void buyDiamond300()
    {
        BuyProductID(Product2);
    }

    public void buyDiamond500()
    {
        BuyProductID(Product4);
    }

    public void buyDiamond1050()
    {
        BuyProductID(Product5);
    }

    public void buyDiamond1500()
    {
        BuyProductID(Product6);
    }

    public void buyDiamond5000()
    {
        BuyProductID(Product7);
    }

    public void buyDiamond10000()
    {
        BuyProductID(Product8);
    }

    public void buyDiamond50000()
    {
        BuyProductID(Product9);
    }

    public void buyAutominer()
    {
        BuyProductID(Product3);
    }

    public void diamondSale()
    {
        BuyProductID(DiamondSalestring);
    }
}