using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skater : MonoBehaviour
{
    [SerializeField] GameObject center;
    [SerializeField] GameObject radius;

    [Space(10)]
    [SerializeField] float speed = 10f;

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
        currentArcPolar.y += Time.deltaTime * speed;

        float x = currentArcCenter.x + currentArcRadius * Mathf.Cos(Mathf.Deg2Rad * currentArcPolar.y);
        float y = currentArcCenter.y + currentArcRadius * Mathf.Sin(Mathf.Deg2Rad * currentArcPolar.y);
        transform.position = new Vector3(x, y, 0);

        if (currentArcPolar.y > currentArcAngle)
        {
            newStroke();
        }

    }

    void newStroke()
    {
        currentArcAngle = Random.Range(10f, 120f);
        currentArcRadius = Random.Range(0.25f, 4f);
        currentArcStartPolar = new Vector2(currentArcRadius, 0);
        currentArcCenter = transform.position + (Vector3)(currentArcRadius * Random.insideUnitCircle);
        if (currentArcCenter.y > 10)
        {
            //this is not rly cutting it
            currentArcCenter.y -= 20;
        }
        currentArcPolar = new Vector2(currentArcRadius, 0);
        Debug.Log("new stroke "+currentArcAngle+" "+currentArcRadius+currentArcCenter);

        center.transform.position = currentArcCenter;
        radius.transform.position = currentArcCenter;
        radius.transform.localScale = Vector3.one * currentArcRadius*2;
    }
}
