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
    bool isInit = false;

    void Start()
    {
        Inititalize();
    }

    public void Inititalize()
    {
        if (isInit)
            return;
        isInit = true;
        initialScale = transform.localScale;
        if (startSmall)
            ResetToSmall();
        else
            ResetToNaturalSize();
        smallSizeChange = false;
        isShrinking = false;
        isGrowing = false;
    }
    
    void FixedUpdate()
    {
        if (isShrinking)
            Shrink();
        else if (isGrowing)
            Grow();
    }
    public void StartShrink()
    {
        if (transform.localScale == Vector3.zero)
            return;
        isGrowing = false;
        isShrinking = true;
        smallSizeChange = true;
    }
    public void StartGrow()
    {
        if (transform.localScale == initialScale)
            return;
        isShrinking = false;
        isGrowing = true;
        smallSizeChange = true;
    }
    public void ResetToSmall()
    {
        isShrinking = false;
        isGrowing = false;
        smallSizeChange = false;
        transform.localScale = Vector3.zero;
        curShrink = 0;
    }
    public void ResetToNaturalSize()
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
            curShrink += Time.fixedDeltaTime;
            if (curValue >= 1.2)
                smallSizeChange = false;
        }
        else
            curShrink -= Time.fixedDeltaTime;
        transform.localScale = curValue * initialScale;
        if (curShrink <= 0)
            ResetToSmall();
    }
    void Grow()
    {
        float curValue = curShrink / shrinkSpeed;
        if (smallSizeChange)
        {
            curShrink += Time.fixedDeltaTime;
            if (curValue >= 1.2)
                smallSizeChange = false;
        }
        else
            curShrink -= Time.fixedDeltaTime;

        transform.localScale = curValue * initialScale;
        if (curShrink <= shrinkSpeed && !smallSizeChange)
            ResetToNaturalSize();
    }
    public void StartDestroy(float time)
    {
        Destroy(this, time);
    } 
}