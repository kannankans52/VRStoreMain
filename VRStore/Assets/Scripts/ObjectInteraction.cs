using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public void HoverObjectDetails()
    {
        Debug.Log(this.transform.GetChild(0).gameObject.name);
        this.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void HoverEndObjectDetails()
    {
        Debug.Log(this.transform.GetChild(0).gameObject.name);
        this.transform.GetChild(0).gameObject.SetActive(false);
    }
}
