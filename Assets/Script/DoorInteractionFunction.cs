using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractionFunction : MonoBehaviour
{   
    public static DoorInteractionFunction instance;
    bool playerInRange = false;
    bool playerIsTeleported = false;
    public GameObject player;
    public Vector3 teleportPosition =new Vector3(-38.9f,0,-97.06f);
    public GameObject DoorSound;
    
    void Start()
    {
        instance = this;
        DoorSound.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.F))
        {   
            Debug.Log("Teleporting Player");
            StartCoroutine(TeleportPlayer());
            StartCoroutine(doorSound());
        }
    }

    public bool GetPlayerInRange()
    {
        return playerInRange;
    }

    public bool GetPlayerIsTeleported()
    {
        return playerIsTeleported;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {   
            Debug.Log("Player Entered");
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    IEnumerator TeleportPlayer()
    {   
        StartCoroutine(EventManager.instance.Transition(0.2f));
        yield return new WaitForSeconds(0.5f);
        playerIsTeleported =  true;
        player.transform.position = teleportPosition;
        yield return new WaitForSeconds(0.5f);
        playerIsTeleported = false;
    }

    public IEnumerator doorSound()
    {
        DoorSound.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        DoorSound.SetActive(false);
    }
}
