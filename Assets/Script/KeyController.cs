using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    // Start is called before the first frame update
    public static KeyController instance;
    private bool playerInRange = false;
    public GameObject pickKeySound;
    void Start()
    {
        instance = this;
        pickKeySound.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {   
            StartCoroutine(keySound());     
            Destroy(gameObject);
            Debug.Log("Key Collected");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {   
            playerInRange = true;
            Debug.Log("Player Entered");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
            Debug.Log("Player Exited");
        }
    }

    public bool GetPlayerInRange()
    {
        return playerInRange;
    }

    IEnumerator keySound()
    {
        pickKeySound.SetActive(true);
        yield return new WaitForSeconds(1);
        pickKeySound.SetActive(false);
    }
}
