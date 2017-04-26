using UnityEngine;
using System.Collections;

public class _TSizeChange : MonoBehaviour
{
    public bool startSmall;
    Vector3 initialScale;
    private bool isShrinking;
    private bool isGrowing;
    public float targetScale = 0;
    public float shrinkSpeed;
    private float curShrink;

    bool smallSizeChange;

    void Start()
    {
        initialScale = transform.localScale;
        if (startSmall)
            ResetToSmall();
        else
            ResetToNaturalSize();
        smallSizeChange = false;
        isShrinking = false;
        isGrowing = false;
    //    Invoke("StartShrink", 3);
    }

    // Update is called once per frame

    void Update()
    {
        if (isShrinking)
            Shrink();
        else if (isGrowing)
            Grow();
    }
    public void StartShrink()
    {
        isGrowing = false;
        isShrinking = true;
        smallSizeChange = true;
    }
    public void StartGrow()
    {
        isShrinking = false;
        isGrowing = true;
        smallSizeChange = true;
    }
    void ResetToSmall()
    {
        isShrinking = false;
        isGrowing = false;
        smallSizeChange = false;
        transform.localScale = Vector3.zero;
        curShrink = 0;
    }
    void ResetToNaturalSize()
    {
        isShrinking = false;
        isGrowing = false;
        smallSizeChange = false;
        transform.localScale = initialScale;
        curShrink = shrinkSpeed;
    }

    void Shrink()
    {
        float curValue = curShrink / shrinkSpeed;
        if (smallSizeChange)
        {
            curShrink += Time.deltaTime;
            if (curValue >= 1.2)
                smallSizeChange = false;
        }
        else
            curShrink -= Time.deltaTime;
        transform.localScale = curValue * initialScale;
        if (curShrink <= 0)
            ResetToSmall();
    }
    void Grow()
    {
        float curValue = curShrink / shrinkSpeed;
        if (smallSizeChange)
        {
            curShrink += Time.deltaTime;
            if (curValue >= 1.2)
                smallSizeChange = false;
        }
        else
            curShrink -= Time.deltaTime;

        transform.localScale = curValue * initialScale;
        if (curShrink <= shrinkSpeed && !smallSizeChange)
            ResetToNaturalSize();
    }
    public void StartDestroy(float time)
    {
        Destroy(this, time);
    } 
}