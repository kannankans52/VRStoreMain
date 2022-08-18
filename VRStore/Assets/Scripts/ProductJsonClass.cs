using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class ListName
{
    public string Table { get; set; }
    public string IsActive { get; set; }
    public List<Product> Products { get; set; }
}

public class Product
{
    public string ItemID { get; set; }
    public string ItemName { get; set; }
    public string IteamPrice { get; set; }
    public string IteamDetails { get; set; }
    public string IteamColor { get; set; }
    public string IteamX { get; set; }
    public string IteamY { get; set; }
    public string IteamZ { get; set; }
}

public class RootProductList
{
    public List<ListName> ListName { get; set; }
}




public class ProductJsonClass : MonoBehaviour
{

}
