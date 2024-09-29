using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class Data
{
    public static Color[] Colors =
    {
        new Color(1f, 0, 0, 1),
        new Color(0, 1f, 0, 1),
        new Color(0.188f, 0.627f, 0.940f, 1),

        new Color(1f, 1f, 0, 1),
        new Color(1f, 0, 1f, 1),
        new Color(0.980f, 0.541f, 0.0392f, 1),


    };
}

public class MainScript : MonoBehaviour
{
    public GameObject BallPrefab;
    public GameObject PopCounterPrefab;


    public RectTransform UIRect;
    public RectTransform DestroyRect;

    public CanvasGroup MainMenuGroup;

    public Transform Balls;

    public int BallCount;

    public bool Adds;

    public RectTransform Sizer;

    public TextMeshProUGUI ScoreText;
    public int Score;

    public int ScorePlus;

    public float time;
    public float Timer = 0.1f;


    public float SpeedBubbleUp;
    public float SpeedBubbleDestroy;
    

    public float timeScale = 1;


    public void CloseMenu ()
    {
        MainMenuGroup.alpha = 0;
        MainMenuGroup.blocksRaycasts = false;
        MainMenuGroup.interactable = false;
    }


    public void UpdateTextScore()
    {
        time = 0;
        Score++;
        ScoreText.text = Score.ToString();
    }

    public void Start()
    {
        Application.targetFrameRate = 120;

        Time.timeScale = timeScale;

        //float siez = Vector2.Distance(new Vector2(LeftWall.position.x, 0), new Vector2(RightWall.position.x, 0)) / 10;
        BallPrefab.GetComponent<RectTransform>().sizeDelta = new Vector2(Sizer.rect.size.x / 7, Sizer.rect.size.x / 7);

        Load ();
        UpdateBalls ();
    }

    public void Load ()
    {
        Score = PlayerPrefs.GetInt("Score");
        ScoreText.text = Score.ToString();
    }

    public void UpdateBalls ()
    {
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
        int randomind = Random.Range(0, Data.Colors.Length-1);
        float shift = Random.Range(-2f, 2f);

        Bubble ball = Instantiate(BallPrefab, new Vector3(shift, 8, 0), Quaternion.identity, Balls).GetComponent<Bubble>();
        ball.MainScript = this;
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
