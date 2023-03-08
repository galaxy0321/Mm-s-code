using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageShake : MonoBehaviour
{
    public Image img;
    public float showTime = 0.2f;

    private float time = 0;
    private bool isStart = false;
    //public void OnClick()
    //{
    //    index++;
    //    if (index > maxLevel)
    //        index = 1;
    //    UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    //}
    private void Update()
    {
        if (!isStart)
            return;

        time += Time.unscaledDeltaTime;
        if (time >= showTime)
        {
            isStart = false;
            //img.CrossFadeAlpha(0.0f, 0.3f, true);
            DOTween.ToAlpha(() => img.color, x => img.color = x, 0.0f, 0.3f);
        }

    }

    public void Show()
    {
        isStart = true;            
        time = 0.0f;
        DOTween.ToAlpha(() => img.color, x => img.color = x, 0.4f, 0.1f);
        //img.DOFade(0.5f, 0.1f, true);
        gameObject.SetActive(true);
    }
}
