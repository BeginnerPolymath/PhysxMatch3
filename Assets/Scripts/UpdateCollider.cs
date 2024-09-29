using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCollider : MonoBehaviour
{
    public RectTransform _rect;
    public BoxCollider2D _collider;


    // Start is called before the first frame update
    void Start()
    {
        print(_rect.rect.size);
        _collider.size = _rect.rect.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
