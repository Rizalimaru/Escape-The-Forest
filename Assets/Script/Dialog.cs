using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{   
    public static Dialog instance;
    public TextMeshProUGUI textDisplay;
    public string[] barisKata;
    private int index;
    private bool isDialogDone = false;
    // Start is called before the first frame update
    void Start()
    {   
        instance = this;
        StartDialog();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextSentence();
        }
    }

    void StartDialog()
    {
        index = 0;
        DisplayText();
    }

    void DisplayText()
    {
        if (index < barisKata.Length)
        {
            textDisplay.text = barisKata[index];
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void NextSentence()
    {
        if (index < barisKata.Length - 1)
        {
            index++;
            DisplayText();
        }
        else
        {
            gameObject.SetActive(false);
            isDialogDone = true;
        }
    }

    public bool GetDialogDone()
    {
        return isDialogDone;
    }


}
