using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopCounterScript : MonoBehaviour
{
    public TMP_Text TextCounter;
    public RectTransform SelfRect;

    public CanvasGroup CanvasGroup;

    public float Lifetime = 3;
    public float UpSpeed = 5;
    private float Times;

    void Start ()
    {
        Times = Lifetime;
    }


    void Update()
    {
        Times -= Time.deltaTime;
        
        CanvasGroup.alpha = Mathf.Lerp(0, 1, Times / Lifetime );
        SelfRect.anchoredPosition += new Vector2(0, UpSpeed * (Time.deltaTime * 10));

        if(Times <= 0)
        {
            Destroy(gameObject);
        }
    }
}
