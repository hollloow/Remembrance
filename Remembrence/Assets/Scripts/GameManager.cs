using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float TimerDoJogo;
    private void FixedUpdate() => TimerDoJogo = +Time.unscaledTime;

    public void Freze(float freezeAmount) => StartCoroutine(FreezeTime(freezeAmount));
    IEnumerator FreezeTime(float freezeAmount)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(freezeAmount);
        Time.timeScale = 1;
    }
}
