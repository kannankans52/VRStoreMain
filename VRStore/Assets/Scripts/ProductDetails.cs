using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProductDetails : MonoBehaviour
{
    public TextMeshProUGUI getProductDetails;
    public GameObject Container, AddCartButton,GetCanvas,AreYouSureContainer, AddtoCartContainer;
    
    public Camera GetCamera;


    private void Update()
    {
        GetCanvas.transform.LookAt(GetCamera.transform);
    }
}
