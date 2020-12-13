using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLimitChecker : MonoBehaviour
{
    private Vector2 screenStartPoint, screenEndPoint;
    private int screenWidth, screenHeight;
    // Start is called before the first frame update
    void Start()
    {
        CalculateStartAndEndPoints();
    }

    void CalculateStartAndEndPoints()
    {
        screenStartPoint = Camera.main.ScreenToWorldPoint(Vector2.zero);
        screenEndPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        //Save width and height
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    void CheckPosition()
    {
        if (transform.position.x < screenStartPoint.x)
        {
            Vector2 tempPos = transform.position;
            tempPos.x = screenEndPoint.x;
            transform.position = tempPos;
        }
        else if (transform.position.x > screenEndPoint.x)
        {
            Vector2 tempPos = transform.position;
            tempPos.x = screenStartPoint.x;
            transform.position = tempPos;
        }

        if (transform.position.y < screenStartPoint.y)
        {
            Vector2 tempPos = transform.position;
            tempPos.y = screenEndPoint.y;
            transform.position = tempPos;
        }
        else if (transform.position.y > screenEndPoint.y)
        {
            Vector2 tempPos = transform.position;
            tempPos.y = screenStartPoint.y;
            transform.position = tempPos;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Check if the saved widht and height match with the current width and height
        if(Screen.width != screenWidth || Screen.height != screenHeight)
        {
            CalculateStartAndEndPoints();
        }
        CheckPosition();
    }
}
