using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowerScript : MonoBehaviour
{
    public RectTransform SelfRect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SelfRect.anchoredPosition = Input.mousePosition;
    }
}
