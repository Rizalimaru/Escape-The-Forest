using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{   
    public static EventManager instance;
    public GameObject DialogText;
    public GameObject Indicator;
    private Animator anim;
    public GameObject player;
    public Image imageTransition;
    [HideInInspector] public bool isWakeDone = false;

    void Start()
    {   
        instance = this;
        DialogText.SetActive(false);
        Indicator.SetActive(false);
        anim = player.GetComponent<Animator>();
        StartCoroutine(FadeOutImage(5f)); // Call the coroutine to fade out the image
    }

    void Update()
    {   
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Getting Up"))
        {
            if (stateInfo.normalizedTime >= 0.9f)
            {   
                Debug.Log("Wake Up Done");
                DialogText.SetActive(true);
                isWakeDone = true;
            }   
        }

        if (Dialog.instance.GetDialogDone() == true)
        {   
            Indicator.SetActive(true);
        }
    }

    public bool GetWakeDone()
    {
        return isWakeDone;
    }

    IEnumerator FadeOutImage(float duration)
    {   
        Color color = imageTransition.color;
        float startAlpha = color.a;

        for (float t = 0.0f; t < duration; t += Time.deltaTime)
        {
            float blend = t / duration;
            color.a = Mathf.Lerp(startAlpha, 0f, blend);
            imageTransition.color = color;
            yield return null;
        }

        color.a = 0f; // Ensure the image is completely invisible at the end
        imageTransition.color = color;
    }


    public IEnumerator Transition(float duration)
    {
        Color color = imageTransition.color;

        // Fade in
        float startAlpha = 0f;
        float endAlpha = 1f;
        for (float t = 0.0f; t < duration; t += Time.deltaTime)
        {
            float blend = t / duration;
            color.a = Mathf.Lerp(startAlpha, endAlpha, blend);
            imageTransition.color = color;
            yield return null;
        }

        color.a = endAlpha; // Ensure the image is completely visible at the end of fade-in
        imageTransition.color = color;

        yield return new WaitForSeconds(2f); 

        // Fade out
        startAlpha = 1f;
        endAlpha = 0f;
        for (float t = 0.0f; t < duration; t += Time.deltaTime)
        {
            float blend = t / duration;
            color.a = Mathf.Lerp(startAlpha, endAlpha, blend);
            imageTransition.color = color;
            yield return null;
        }

        color.a = endAlpha; // Ensure the image is completely invisible at the end of fade-out
        imageTransition.color = color;
    }
}
