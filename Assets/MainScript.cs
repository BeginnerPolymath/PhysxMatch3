﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class Data
{
    public static Color[] Colors =
    {
        new Color(0.8f, 0, 0, 1),
        new Color(0, 0.8f, 0, 1),
        new Color(0, 0, 0.8f, 1),

        new Color(0.8f, 0.8f, 0, 1),
        new Color(0.8f, 0, 0.8f, 1),
    };
}

public class MainScript : MonoBehaviour
{
    public GameObject BallPrefab;

    public Transform Balls;

    public int BallCount;

    public bool Adds;

    public RectTransform Sizer;

    public TextMeshProUGUI ScoreText;
    public int Score;

    public int ScorePlus;

    public float time;
    public float Timer = 0.1f;


    public void UpdateTextScore()
    {
        time = 0;
        Score++;
        ScoreText.text = Score.ToString();
    }

    public void Start()
    {
        Application.targetFrameRate = 60;

        //float siez = Vector2.Distance(new Vector2(LeftWall.position.x, 0), new Vector2(RightWall.position.x, 0)) / 10;
        BallPrefab.GetComponent<RectTransform>().sizeDelta = new Vector2(Sizer.rect.size.x / 7, Sizer.rect.size.x / 7);


        Score = PlayerPrefs.GetInt("Score");
        ScoreText.text = Score.ToString();

        if(!Adds)
            AddBalls ();
    }

    void Update()
    {
        if(ScorePlus > 0)
        {
            time += Time.deltaTime;

            if(time >= Timer)
            {
                ScorePlus--;
                UpdateTextScore();
            }
        }
        else
        {
            time = 0;
        }


        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetInt("Score", Score);
            Application.Quit();
        }
    }

    public void AddBalls ()
    {
        int randomind = Random.Range(0, 5);
        float shift = Random.Range(-2f, 2f);

        BallScriptNew ball = Instantiate(BallPrefab, new Vector3(shift, 8, 0), Quaternion.identity, Balls).GetComponent<BallScriptNew>();
        ball.main = this;
        ball.gameObject.name = $"{BallCount}";
        
        ball.SetColor(randomind);


        Adds = true;

        BallCount--;

        if(BallCount > 0)
        {
            StartCoroutine (AddsBalls());
        }
        else
        {
            Adds = false;
        }
    }

    IEnumerator AddsBalls()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        AddBalls ();
    }

}
