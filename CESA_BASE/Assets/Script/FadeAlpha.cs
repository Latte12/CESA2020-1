﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeAlpha : MonoBehaviour
{
    private Image m_image;

    private Color m_addAlpha = new Color(0, 0, 0, 0.001f);
    private enum _FadeStep
    {
        None = 0,
        FadeOut,
        SceneLoad,
        FadeIn,
        Max
    }
    private _FadeStep m_fadeStep = _FadeStep.None;

    // Start is called before the first frame update
    private void Start()
    {
        m_image = GetComponent<Image>();
        m_image.color = Color.clear;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (m_fadeStep == _FadeStep.None) return;

        // フェードアウト
        if (m_fadeStep == _FadeStep.FadeOut)
        {
            m_image.color += m_addAlpha;

            if (m_image.color.a >= 1)
            {
                m_image.color = Color.black;
                m_fadeStep++;
            }
        }

        // シーン切り替え
        if (m_fadeStep == _FadeStep.SceneLoad)
        {
            SceneManager.LoadScene("SampleScene");
            m_fadeStep++;
        }

        // フェードイン
        if (m_fadeStep == _FadeStep.FadeIn)
        {
            m_image.color -= m_addAlpha;

            if (m_image.color.a <= 0)
            {
                m_image.color = Color.clear;
                Destroy(gameObject);
            }

        }

    }

    public void StartFade()
    {
        m_fadeStep = _FadeStep.FadeOut;
        transform.GetComponent<RectTransform>().sizeDelta = GetComponentInParent<RectTransform>().sizeDelta;
    }
}
