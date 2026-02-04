using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitSplashUI : MonoBehaviour
{
    [SerializeField] private Color hitColor;
    [SerializeField] private float fadeInTime = 0.05f;
    [SerializeField] private float fadeOutTime = 0.3f;
    
    private Image _image;
    private Coroutine _splashCoroutine;

    private void Start()
    {
        _image = GetComponent<Image>();
        GameEvents.OnPlayerDamaged += HitSplash;
        ResetSplash();
    }

    private void OnDestroy()
    {
        GameEvents.OnPlayerDamaged -= HitSplash;
    }

    private void HitSplash(float damage)
    {
        if (_splashCoroutine != null)
            StopCoroutine(_splashCoroutine);

        _splashCoroutine = StartCoroutine(SplashEffectCoroutine());
    }

    private void ResetSplash()
    {
        _image.color = new Color(hitColor.r, hitColor.g, hitColor.b, 0f);
    }


    private IEnumerator SplashEffectCoroutine()
    {
        // Fade in
        float t = 0f;
        while (t < fadeInTime)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, hitColor.a, t / fadeInTime);
            _image.color = new Color(hitColor.r, hitColor.g, hitColor.b, alpha);
            yield return null;
        }

        // Fade out
        t = 0f;
        while (t < fadeOutTime)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(hitColor.a, 0f, t / fadeOutTime);
            _image.color = new Color(hitColor.r, hitColor.g, hitColor.b, alpha);
            yield return null;
        }

        ResetSplash();
        _splashCoroutine = null;
    }
}
