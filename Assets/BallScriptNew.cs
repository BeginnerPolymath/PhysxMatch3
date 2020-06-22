using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScriptNew : MonoBehaviour
{
    public int ID;
    public Image spriteRenderer;

    public List<BallScriptNew> BallsContant = new List<BallScriptNew>();

    public MainScript main;

    public RectTransform _rect;
    public CircleCollider2D _collider;

    void Start ()
    {
        _collider.radius = _rect.rect.size.x / 2;
    }

    void OnMouseDown()
    {
        FindBalls(transform);
        
        for (int i = 0; i < BallsContant.Count; i++)
        {
            FindBalls(BallsContant[i].transform);
        }

        if(BallsContant.Count >= 3)
        {
            foreach (var balls in BallsContant)
            {
                Destroy(balls.gameObject);
            }

            main.Score += BallsContant.Count;
            main.UpdateTextScore();
            PlayerPrefs.SetInt("Score", main.Score);

            main.BallCount += BallsContant.Count;
            main.Start();
        }
        else
        {
            BallsContant.Clear();
        }
        
        
    }


    void FindBalls (Transform trans)
    {
        Collider2D[] contacts = Physics2D.OverlapCircleAll(trans.position, 0.32f);

        foreach (var otherBall in contacts)
            if(otherBall.gameObject.tag == "Ball")
            {
                BallScriptNew ballz = otherBall.gameObject.GetComponent<BallScriptNew>();
                
                if(ballz.ID == ID)
                    if(!BallsContant.Contains(ballz))
                        BallsContant.Add(ballz);
            }
    }
}
