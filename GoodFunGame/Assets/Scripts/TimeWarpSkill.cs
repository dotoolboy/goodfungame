using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarpSkill : MonoBehaviour
{
    private bool isActive = false;
    private float duration = 1f;

    public void Activate()
    {
        if (!isActive)
        {
            StartCoroutine(TimeWarpCoroutine());
        }
    }

    private IEnumerator TimeWarpCoroutine()
    {
        isActive = true;
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(duration);

        Time.timeScale = 1.0f;
        isActive = false;
    }
}
