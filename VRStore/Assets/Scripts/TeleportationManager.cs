using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TeleportationManager : MonoBehaviour
{
    [SerializeField]
    private  InputActionAsset actionAsset;

    public InputAction InputAction { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
       // var activate:InputAction = actionAsset.FindActionMap(nameOrld: "XRI LeftHand").FindAction(nameOrld:"");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
