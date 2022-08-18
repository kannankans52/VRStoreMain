using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using UnityEditor;
using TMPro;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
 
using Newtonsoft.Json.Converters;





public class Products : MonoBehaviour
{
    #region Declaration
    private string getProduct;
    public RootProductList getProductJsonClass;
    public List<ProductList> SelectedProduct = new List<ProductList>();
    public GameObject AddCartGameObject;
    public GameObject CartCube;
    private GameObject getSelectedProductSpwanObj;
    public Camera GetCamera;
    public TextMeshProUGUI getTextDebug;
    #endregion

    private void Update()
    {
       
        Vector3 difference = GetCamera.transform.position - CartCube.transform.position;
        float rotationY = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
        CartCube.transform.rotation = Quaternion.Euler(0.0f, rotationY, 0.0f);
    }
    public struct ProductList
    {
        public string Table { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
    }




    // Start is called before the first frame update
    void Start()
    {
        LoadJson();
    }



    #region  Reading json 
    public void LoadJson()
    {
        
        getProductJsonClass = new RootProductList();
        TextAsset loadedJsonFile = Resources.Load<TextAsset>("Products");
        getProductJsonClass = JsonConvert.DeserializeObject<RootProductList>(loadedJsonFile.ToString());
        SpwanProducts();

    }
    #endregion
    /// <summary>
    /// Instantiate Product list get from json coordinates 
    /// </summary>
    private void SpwanProducts()
    {
        
        foreach (ListName getList in getProductJsonClass.ListName)
        {
            foreach (Product getPro in getList.Products)
            {
                 
                var spawnproduct = Instantiate(Resources.Load("Products/"+ getPro.ItemName) as GameObject);
              
                spawnproduct.transform.GetChild(0).gameObject.SetActive(false);
               
                spawnproduct.transform.position = new Vector3(float.Parse(getPro.IteamX), float.Parse(getPro.IteamY), float.Parse(getPro.IteamZ));
                
                spawnproduct.gameObject.GetComponent<ProductDetails>().GetCamera = GetCamera;
               
                spawnproduct.gameObject.GetComponent<ProductDetails>().getProductDetails.text = "Name : " + getPro.ItemName + "\nPrice : " + getPro.IteamPrice + "\nDetails : " + getPro.IteamDetails + "\nDetails : " + getPro.IteamColor;
                 
                spawnproduct.gameObject.GetComponent<ProductDetails>().AddCartButton.gameObject.GetComponent<InteractableHoverEvents>().onHandHoverBegin.AddListener(() => CallAddToCart(getPro, spawnproduct.gameObject));
                
                spawnproduct.gameObject.GetComponent<ProductDetails>().Container.gameObject.GetComponent<InteractableHoverEvents>().onHandHoverBegin.AddListener(() => HoverProduct(spawnproduct.transform.GetChild(0).gameObject));
                 

            }
        }

    }
    /// <summary>
    /// Controller Hover on the product and deselect the previous one 
    /// </summary>
    /// <param name="getCanvas"></param>
    private void HoverProduct(GameObject getCanvas)
    {
        if (getSelectedProductSpwanObj!=null)
        {
            getSelectedProductSpwanObj.SetActive(false);
        }

        getCanvas.SetActive(true);
        getSelectedProductSpwanObj = getCanvas;
    }
    public void TempAddCart()
    {
         
       // CallAddToCart(getProductJsonClass.List[0].Products[0]);
    }
    /// <summary>
    /// Selected add to cart in cart list and callto CartCustomList() method
    /// </summary>
    /// <param name="getList"></param>
    /// <param name="getCanvas"></param>
    public void CallAddToCart(Product getList, GameObject getCanvas)
    {
        getCanvas.GetComponent<ProductDetails>().AreYouSureContainer.gameObject.SetActive(false);
        getCanvas.GetComponent<ProductDetails>().AddtoCartContainer.gameObject.SetActive(true);

        
        ProductList PName = new ProductList();
        PName.ItemID = getList.ItemID;
        PName.ItemName = getList.ItemName;
        SelectedProduct.Add(PName);
        RemoveCart();
        CartCustomList();
    }



    /// <summary>
    /// Cart list add all selected product in this method
    /// </summary>
    private void CartCustomList()
    {
       
        foreach (ProductList getList in SelectedProduct)  
        {
           var spawnproductList = Instantiate(Resources.Load("Products/ProductListPrefab") as GameObject, AddCartGameObject.transform);
            spawnproductList.gameObject.GetComponent<CartDetails>().getProductImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Products/images/"+getList.ItemName);
            spawnproductList.gameObject.GetComponent<CartDetails>().getProductName.text = getList.ItemName;
            spawnproductList.gameObject.GetComponent<CartDetails>().getRemoveBTN.GetComponent<InteractableHoverEvents>().onHandHoverBegin.AddListener(() => RemoveCartList(getList.ItemID));
        }
    }

    /// <summary>
    /// Remove from cart 
    /// </summary>
    /// <param name="getid"></param>
    public void RemoveCartList(string getid)
    {

        int count = SelectedProduct.Count;
        int getNum = -1;
        for (int i=0;i<count;i++)
        {
            
            if (SelectedProduct[i].ItemID == getid)
            {
                getNum = i;
                
            }
            
           
        }

        if (getNum<0)
        {
            return;
        }
        SelectedProduct.RemoveAt(getNum);

        RemoveCart();
 
       CartCustomList();
    }


    private void RemoveCart()
    {
        foreach (Transform getP in AddCartGameObject.transform)
        {

            Destroy(getP.gameObject);

        }
    }
    
}
