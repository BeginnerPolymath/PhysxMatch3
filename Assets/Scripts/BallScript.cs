using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public int ID;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D _rigidbody;

    public List<BallScript> BallsContant = new List<BallScript>();

    public ContactPoint2D[] conatscs = new ContactPoint2D[6];


    public Collider2D[] a;

    public MainScript main;

    void OnMouseDown()
    {
        FindBalls(this, conatscs);
        
        for (int i = 0; i < BallsContant.Count; i++)
        {
            FindBalls(BallsContant[i], conatscs);
        }

        if(BallsContant.Count >= 3)
        {
            foreach (var balls in BallsContant)
            {
                Destroy(balls.gameObject);
            }

            main.BallCount = 0;
            //main.AddBalls(BallsContant.Count);
            
            Destroy(gameObject);
        }
        else
        {
            BallsContant.Clear();
        }
        
        
    }


    void FindBalls (BallScript ball, ContactPoint2D[] contacts)
    {
        ball._rigidbody.GetContacts(contacts);

        foreach (var otherBall in contacts)
        {
            if(otherBall.collider != null && otherBall.collider.gameObject.tag == "Ball")
            {
                BallScript ballz = otherBall.collider.gameObject.GetComponent<BallScript>();
                
                if(ballz.ID == ID)
                {
                    if(!BallsContant.Contains(ballz))
                        BallsContant.Add(ballz);
                }
            }
        }
    }
}
