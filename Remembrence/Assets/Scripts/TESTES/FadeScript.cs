using System;
using System.Collections;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] public float fadeDuration ;
    [SerializeField] private bool IN;


    private void OnEnable()
    {
        canvasGroup = GameObject.Find("FadeCanvas").GetComponent<CanvasGroup>();
        if (IN)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }

    private void Start()
    {
#if UNITY_EDITOR
       // canvasGroup.gameObject.SetActive(false);
#endif
        
    }

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0, fadeDuration));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1, fadeDuration));
    }
    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start , float end , float duration)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            yield return null;
        }

        cg.alpha = end;
    }
}
