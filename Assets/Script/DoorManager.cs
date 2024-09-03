using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static DoorManager instance;
    public GameObject doorClose;
    public GameObject doorOpen;
    bool PlayerInRange = false;
    public GameObject DoorSound;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerInRange && Input.GetKeyDown(KeyCode.F) && UIManager.instance.GetJumlahKunci() == 0)
        {
            doorClose.SetActive(false);
            doorOpen.SetActive(true);
            StartCoroutine(doorSound());
        }   
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInRange = false;
        }
    }

    public bool GetPlayerInRange()
    {
        return PlayerInRange;
    }

    IEnumerator doorSound()
    {
        DoorSound.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        DoorSound.SetActive(false);
    }
}
