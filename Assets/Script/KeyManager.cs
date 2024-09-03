using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyManager : MonoBehaviour
{   
    public static KeyManager instance;
    private bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        instance =  this;
    }

    // Update is called once per frame
    void Update()
    {   
        playerInRange = KeyController.instance.GetPlayerInRange();
    }

    public bool GetPlayerInRange()
    {
        return playerInRange;
    }

}
