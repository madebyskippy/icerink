using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skater : MonoBehaviour
{
    [SerializeField] Text label;
    [SerializeField] GameObject radius;

    [Space(10)]
    [SerializeField] Vector2 speedRange;

    float speed;

    float currentArcAngle;
    float currentArcRadius;

    // in polar coordinates, so (r, theta)
    Vector2 currentArcStartPolar;
    Vector3 currentArcCenter;
    Vector2 currentArcPolar;

    // Start is called before the first frame update
    void Start()
    {
        newStroke();
        Debug.Log(currentArcPolar.y+" "+currentArcCenter);
        float x = currentArcCenter.x + currentArcRadius * Mathf.Cos(Mathf.Deg2Rad * currentArcPolar.y);
        float y = currentArcCenter.y + currentArcRadius * Mathf.Sin(Mathf.Deg2Rad * currentArcPolar.y);
        transform.position = new Vector3(x, y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float speedRate = ((currentArcPolar.y - currentArcStartPolar.y) / currentArcAngle);
        float fraction = speedRate;
        if (speedRate > 0.5f) {
            speedRate = Mathf.Abs(speedRate - 1);
        }
        speedRate *= 2;
        speed = Mathf.Lerp(speedRange.x, speedRange.y, speedRate);
        label.text = currentArcPolar.y+" - "+currentArcStartPolar.y+" / "+currentArcAngle+" = "+fraction+"\n"+speedRate + "\n" + speed;

        currentArcPolar.y += Time.deltaTime * speed;
        currentArcPolar.y %= 360.0f;

        float x = currentArcCenter.x + currentArcRadius * Mathf.Cos(Mathf.Deg2Rad * currentArcPolar.y);
        float y = currentArcCenter.y + currentArcRadius * Mathf.Sin(Mathf.Deg2Rad * currentArcPolar.y);
        transform.position = new Vector3(x, y, 0);

        if ((currentArcPolar.y- currentArcStartPolar.y) > currentArcAngle)
        {
            //this equation isn't working cus im making everything go 0-360 
            //so if start is at 355 and current is at 20
            //even if the goal is 25, it won't register trigger this
            newStroke();
        }

    }

    void newStroke()
    {
        currentArcAngle = Random.Range(10f, 120f);
        currentArcRadius = Random.Range(0.25f, 4f);
        currentArcCenter = transform.position + (Vector3)(currentArcRadius * Random.insideUnitCircle.normalized);
        //how to make it wrap????

        float currentAngle = Mathf.Rad2Deg * Mathf.Atan2(transform.position.y - currentArcCenter.y, transform.position.x - currentArcCenter.x);
        if (currentAngle < 0f)
        {
            currentAngle += 360;
        }
        currentArcPolar = new Vector2(currentArcRadius, currentAngle);
        currentArcStartPolar = new Vector2(currentArcRadius, currentAngle);
        Debug.Log("new stroke "+currentArcAngle+" "+currentArcRadius+" "+currentArcCenter);

        radius.transform.position = currentArcCenter;
        radius.transform.localScale = Vector3.one * currentArcRadius*2;
    }
}
