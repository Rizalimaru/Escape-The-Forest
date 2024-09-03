using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI interactText;
    public GameObject GameOvertext;
    public GameObject TextEscape;
    public GameObject restratButton;
    public GameObject MainMenuButton;
    public GameObject key;
    public string tagToCount = "Key";
    private int jumlahKunci;
    
    void Start()
    {
        instance = this;
        interactText.gameObject.SetActive(false);
        EnemyAI.OnPlayerBitten += HandlePlayerBitten;
    }

    void OnDestroy()
    {
        EnemyAI.OnPlayerBitten -= HandlePlayerBitten;
    }

    void Update()
    {
        int count = CountObjectsWithTag(tagToCount);
        jumlahKunci = count;
        Debug.Log("Number of objects with tag " + tagToCount + ": " + count);

        if (DoorManager.instance.GetPlayerInRange() == true && count == 1)
        {
            interactText.gameObject.SetActive(true);
            interactText.color = Color.red;
            interactText.text = "Find Key To Open Door";
        }
        else if (DoorManager.instance.GetPlayerInRange() == true || DoorInteractionFunction.instance.GetPlayerInRange() == true)
        {
            interactText.gameObject.SetActive(true);
            interactText.color = Color.white;
            interactText.text = "Press F to open door";
        }
        else if (DoorCantGetIn.instance.GetPlayerInRange() == true)
        {
            interactText.gameObject.SetActive(true);
            interactText.color = Color.red;
            interactText.text = "Door is locked";
        }
        else if (KeyManager.instance.GetPlayerInRange() == true && key != null)
        {
            interactText.gameObject.SetActive(true);
            interactText.color = Color.white;
            interactText.text = "Press F to collect key";
        }
        else
        {
            interactText.gameObject.SetActive(false);
        }
    }

    int CountObjectsWithTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        return objectsWithTag.Length;
    }

    public int GetJumlahKunci()
    {
        return jumlahKunci;
    }

    public void InteractiontextVisible(String text)
    {
        interactText.gameObject.SetActive(true);
        interactText.color = Color.white;
        interactText.text = text;
    }

    public void InteractiontextInvisible()
    {
        interactText.gameObject.SetActive(false);
    }

    private void HandlePlayerBitten()
    {
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        GameOvertext.SetActive(true);
        restratButton.SetActive(true);
        MainMenuButton.SetActive(true);
    }
}
