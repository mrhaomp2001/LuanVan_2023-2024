using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
[ExecuteAlways]
public class CanvasConfig : UIBehaviour
{
    public static float width = 720f;
    public static float height = 1559f;
    private float matchWidthOrHeight_Default = 0.5f;
    private CanvasScaler.ScaleMode ScaleWithScreenSize = CanvasScaler.ScaleMode.ScaleWithScreenSize;
    private CanvasScaler m_CanvasScaler;
    private float ratioScreen = 0;
    protected override void Awake()
    {
        Handle();
    }
    protected override void Start()
    {
        Handle();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        m_CanvasScaler = GetComponent<CanvasScaler>();
        Handle();
    }
    protected override void OnDisable()
    {
        Handle();
        base.OnDisable();
    }
    private void Update()
    {
        if (!Application.isPlaying)
        {
            float ratio = ((float)Screen.width / (float)Screen.height);
            if (ratioScreen != ratio)
            {
                ratioScreen = ratio;
                Handle();
            }
        }
    }
    private void Handle()
    {
        if (m_CanvasScaler != null)
        {
            m_CanvasScaler.uiScaleMode = ScaleWithScreenSize;
            if (Screen.width > Screen.height)
            {
                m_CanvasScaler.referenceResolution = new Vector2(height, width);
                float ratio = ((float)Screen.height / (float)Screen.width);
                float ratio2 = ((float)Screen.height / width);
                if (Screen.height > Screen.width || (ratio > 0.6f && ratio2 >= 2f))
                {
                    ratio = 1 - ratio;
                    if (ratio < 0)
                    {
                        ratio = 0;
                    }
                    m_CanvasScaler.matchWidthOrHeight = ratio;
                }
                else if (ratio > 0.5f || ratio <= 0.4f)
                {
                    m_CanvasScaler.matchWidthOrHeight = 1f;
                }
                else
                {
                    m_CanvasScaler.matchWidthOrHeight = matchWidthOrHeight_Default;
                }
            }
            else
            {
                m_CanvasScaler.referenceResolution = new Vector2(width, height);
                float ratio = ((float)Screen.width / (float)Screen.height);
                if (Screen.width > Screen.height || ratio >= 0.6f)
                {
                    if (ratio > 1)
                    {
                        ratio = 1;
                    }
                    m_CanvasScaler.matchWidthOrHeight = ratio;
                }
                else if (ratio <= 0.4f)
                {
                    m_CanvasScaler.matchWidthOrHeight = 0f;
                }
                else
                {
                    m_CanvasScaler.matchWidthOrHeight = matchWidthOrHeight_Default;
                }
            }

            //Debug.LogFormat("Width: {0} Height: {1} Match: {2} Ratio: {3}", Screen.width, Screen.height, m_CanvasScaler.matchWidthOrHeight, ratio);
        }
    }

}