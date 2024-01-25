using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image uiFillImage;

    public float Duration { get; private set; }

    private float remainingDuration;

    private void Awake()
    {
        ResetTimer();
    }

    private void ResetTimer()
    {
        uiFillImage.fillAmount = 0f;

        Duration = remainingDuration = 0;
    }

    public Timer SetDuration(float seconds)
    {
        Duration = remainingDuration = seconds;
        return this;
    }

    public void Begin()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration > 0)
        {
            UpdateUI(remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        End();
    }

    private void UpdateUI(float seconds)
    {
        uiFillImage.fillAmount = Mathf.InverseLerp(0, Duration, seconds);
    }

    public void End()
    {
        ResetTimer();
    }

    /*private void OnDestroy()
    {
        StopAllCoroutines();
    }*/
}
