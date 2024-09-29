using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.EventSystems;


public class Bubble : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public Color bubbleColor; // Исходный цвет шарика

    public List<Bubble> connectedBubbles = new List<Bubble>();
    private bool isSelected = false;

    public MainScript MainScript;

    public Rigidbody2D Rigidbody;


    public RectTransform _rect;
    public CircleCollider2D _collider;

    public Image Image;

    public float Radius;

    public int ColorID;

    public Vector2 ClickPosition;

    public bool DestroyBubble;

    private float Times;


    void Start ()
    {
        Radius = _rect.rect.size.x / 2;

        _collider.radius = Radius;

        Image.color = bubbleColor;

        Times = MainScript.SpeedBubbleDestroy;
    }

    public void SetColor (int ID)
    {
        ColorID = ID;
        bubbleColor = Data.Colors[ID];
    }

    private void Update()
    {
        if(DestroyBubble)
        {
            Rigidbody.bodyType = RigidbodyType2D.Static;
            _collider.enabled = false;

            _rect.anchoredPosition += new Vector2(0, MainScript.SpeedBubbleUp * Time.deltaTime);

            Times -= Time.deltaTime;

            Image.color = new Color(bubbleColor.r, bubbleColor.g, bubbleColor.b, Times / MainScript.SpeedBubbleDestroy);
            _rect.localScale = Vector2.Lerp(Vector2.zero, Vector2.one, Times / MainScript.SpeedBubbleDestroy);

            if(Times <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Image.color = Data.Colors[ColorID];
        }
    }

    private void LateUpdate()
    {
        // Если зажата кнопка мыши и шарик выбран
        if (isSelected)
        {
            connectedBubbles.Clear();
            FindConnectedBubbles(this);
            UpdateBubbleTransparency(true);

            if(connectedBubbles.Count >= 3)
            {
                Time.timeScale = 0.5f;
            }
            else
            {
                Time.timeScale = MainScript.timeScale;
            }
        }
    }

    // Поиск всех соседних шариков того же цвета
    private void FindConnectedBubbles(Bubble bubble)
    {
        if (connectedBubbles.Contains(bubble))
            return;

        connectedBubbles.Add(bubble);

        // Проверяем соседние шарики в заданном радиусе
        Collider2D[] nearbyBubbles = Physics2D.OverlapCircleAll(bubble.transform.position, 0.45f);
        foreach (Collider2D collider in nearbyBubbles)
        {
            Bubble nearbyBubble = collider.GetComponent<Bubble>();
            if (nearbyBubble != null && nearbyBubble.bubbleColor == bubble.bubbleColor)
            {
                FindConnectedBubbles(nearbyBubble);
            }
        }
    }

    // Обновление полупрозрачности шариков
    private void UpdateBubbleTransparency(bool makeTransparent)
    {
        if(connectedBubbles.Count < 3)
            return;

        foreach (Bubble bubble in connectedBubbles)
        {
            if (makeTransparent)
            {
                // Применяем полупрозрачный цвет
                bubble.Image.color = new Color(bubble.bubbleColor.r, bubble.bubbleColor.g, bubble.bubbleColor.b, 0.5f); // Полупрозрачность
            }
            else
            {
                // Восстанавливаем исходный цвет
                bubble.Image.color = bubble.bubbleColor;
            }
        }
    }

    // Удаление всех найденных одинаковых шариков
    private void PopBubbles()
    {
        foreach (Bubble bubble in connectedBubbles)
        {
            //Destroy(bubble.gameObject);
            bubble._rect.SetParent(MainScript.DestroyRect);
            bubble.DestroyBubble = true;
        }

        MainScript.ScorePlus += connectedBubbles.Count;

        PopCounterScript popCounter = Instantiate(MainScript.PopCounterPrefab, MainScript.UIRect).GetComponent<PopCounterScript>();
        popCounter.TextCounter.text = $"+{connectedBubbles.Count}";
        popCounter.SelfRect.anchoredPosition = ClickPosition;
            
        PlayerPrefs.SetInt("Score", MainScript.Score);

        MainScript.BallCount += connectedBubbles.Count;
        MainScript.UpdateBalls();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isSelected = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ClickPosition = _rect.anchoredPosition + new Vector2(0, 150f);
        //ClickPosition = eventData.pressPosition;

        // Проверяем, если 3 или больше шариков, то лопаем их
        if (connectedBubbles.Count >= 3)
        {
            PopBubbles();
        }

        isSelected = false;

        Time.timeScale = MainScript.timeScale;
    }


}