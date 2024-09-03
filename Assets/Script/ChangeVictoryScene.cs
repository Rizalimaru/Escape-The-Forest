using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVictoryScene : MonoBehaviour
{   
    public static ChangeVictoryScene instance;
    public GameObject cameraVictory;
    public GameObject EscapeText;
    public GameObject retryButton;
    public GameObject indicator;
    public GameObject MainMenuButton;
    bool isVictory = false;
    // Start is called before the first frame update
    void Start()
    {   
        instance = this;
        cameraVictory.SetActive(false);
        EscapeText.SetActive(false);
        retryButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cameraVictory.SetActive(true);
            EscapeText.SetActive(true);
            isVictory = true;
            retryButton.SetActive(true);
            MainMenuButton.SetActive(true);
            indicator.SetActive(false);
            
        }
    }

    public bool GetIsVictory()
    {
        return isVictory;
    }
}
