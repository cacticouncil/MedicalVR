using System;
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    #region Variablesles
    [SerializeField] private GameObject player;
    [SerializeField] private Camera Camera;
    [SerializeField] private GameObject leftWall, rightWall, UpWall, DownWall;
    [SerializeField] private float verticalDistance;
    [SerializeField] private Shader curShader;
    [SerializeField] private Color VignetteColor;
    [SerializeField] private float VignettePower = 5.0f;
    [SerializeField] private float GreyScalePower = 0.0f;
    [SerializeField] private bool isMenu = false;
    private Color futureColor;
    private float futureVignettePower;
    private float futureGreyScalePower;
    private Material curMaterial;
    private Vector3 speed;
    private Vector3 playerPos;
    private float elapsedTime;
    private float minX, maxX, minY, maxY;
    private bool gotPositions = false;
    private int length;
    private float strength;
    private float shakeCooldown;
    private Vector3 initialPosition;
    private Vector3 shakeAxis;

    #endregion

    #region Properties
    Material material
    {
        get
        {
            if (curMaterial == null)
            {
                curMaterial = new Material(curShader);
                curMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return curMaterial;
        }
    }
    #endregion

    void Start()
    {
        if (leftWall == null)
        {
            leftWall = new GameObject();
        }
        if (rightWall == null)
        {
            rightWall = new GameObject();
        }
        if (UpWall == null)
        {
            UpWall = new GameObject();
        }
        if (DownWall == null)
        {
            DownWall = new GameObject();
        }
        speed = Vector3.zero;
        elapsedTime = 0.0f;

        futureColor = VignetteColor;
        futureVignettePower = VignettePower;
        futureGreyScalePower = GreyScalePower;

        Camera.orthographic = true;
        if (!isMenu)
        {
            var vertExtent = Camera.orthographicSize;
            var horzExtent = vertExtent * Screen.width / Screen.height;
            minX = leftWall.transform.position.x + horzExtent;
            maxX = rightWall.transform.position.x - horzExtent;
            minY = DownWall.transform.position.z + vertExtent;
            maxY = UpWall.transform.position.z - vertExtent;
        }

        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }

        //EventManager.PassStrings += GetString;

        if (!isMenu)
        {
            playerPos = player.transform.position;
            playerPos.y = transform.position.y;
            transform.position = playerPos;
            clampToWorld();
        }
    }
    
    void Update ()
    {
        VignettePower = Mathf.Clamp(VignettePower, 0.0f, 6.0f);
        VignettePower = Mathf.Lerp(VignettePower, futureVignettePower, 0.1f);
        GreyScalePower = Mathf.Lerp(GreyScalePower, futureGreyScalePower, 0.1f);
        VignetteColor = Color.Lerp(VignetteColor, futureColor, .1f);

        if (!isMenu)
        {
            playerPos = player.transform.position;
            playerPos.y = transform.position.y;
            var distance = (playerPos - transform.position).magnitude;

            if (elapsedTime <= 1.0f)
            {
                elapsedTime += Time.deltaTime;
            }

            transform.position = distance <= verticalDistance - .5f ? Vector3.Slerp(transform.position, playerPos, Mathf.Lerp(0, 1 / distance, elapsedTime / 10)) : Vector3.SmoothDamp(transform.position, playerPos, ref speed, .6f);
            if (distance <= .1f)
            {
                elapsedTime = 0.0f;
            }
            doShake();
            clampToWorld();
        }
    }

    /// <summary>
    /// Brings the camera back into it's bounds
    /// </summary>
    void clampToWorld()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minY, maxY);
        transform.position = pos;
    }

    public void ChangetoWater()
    {
        VignetteColor = Color.blue;
        VignettePower = 2;
    }
    void doShake()
    {
        shakeCooldown -= Time.deltaTime;
        if (length > 0 && shakeCooldown <= 0.0f)
        {
            length--;
            if (!gotPositions)
            {
                initialPosition = Camera.transform.position;
            }
            else
            {
                Camera.transform.position = initialPosition;
            }
            float quakeAmount = UnityEngine.Random.value * strength * 2 - strength;
            shakeAxis *= quakeAmount;
            Vector3 pp = Camera.transform.position;
            pp += shakeAxis;
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, pp, .1f);
            shakeAxis /= -quakeAmount;
            shakeCooldown = .25f;
        }
        else
        {
            if (gotPositions)
            {
                Camera.transform.position = initialPosition;
            }
            gotPositions = false;
        }
    }

    /// <summary>
    /// Controls the shaders for the image
    /// </summary>
    /// <param name="sourceTexture"></param>
    /// <param name="destTexture"></param>
    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (curShader != null)
        {
            material.SetFloat("_VignettePower", VignettePower);
            material.SetColor("_Color", VignetteColor);
            material.SetFloat("_GreyScale", GreyScalePower);
            Graphics.Blit(sourceTexture, destTexture, material);
        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }
    }

    void OnDisable()
    {
        if (curMaterial)
        {
            DestroyImmediate(curMaterial);
        }
    }

    /// <summary>
    /// parses input from the PassStrings delegate in EventManager
    /// </summary>
    /// <param name="strings"></param>
    void GetString(String[] strings)
    {
        foreach (string s in strings)
        {
            if (s.StartsWith("InstVPower")) //example input VPower1.4
            {
                string blah = s.Remove(0, 10);
                VignettePower = float.Parse(blah);
                futureVignettePower = VignettePower;
            }
            else if (s.StartsWith("InstGSPower")) //example input GSPower.5
            {
                string blah = s.Remove(0, 11);
                GreyScalePower = float.Parse(blah);
                futureGreyScalePower = GreyScalePower;
            }
            else if (s.StartsWith("InstVColor")) //example input VColor1,.5,.7,1
            {
                string blah = s.Remove(0, 10);
                string[] allTheStrings = blah.Split(',');
                VignetteColor.r = float.Parse(allTheStrings[0]);
                VignetteColor.g = float.Parse(allTheStrings[1]);
                VignetteColor.b = float.Parse(allTheStrings[2]);
                VignetteColor.a = float.Parse(allTheStrings[3]);
                futureColor = VignetteColor;
            }
            else if (s.StartsWith("VPower")) //example input VPower1.4
            {
                string blah = s.Remove(0, 6);
                futureVignettePower = float.Parse(blah);
            }
            else if (s.StartsWith("GSPower")) //example input GSPower.5
            {
                string blah = s.Remove(0, 7);
                futureGreyScalePower = float.Parse(blah);
            }
            else if (s.StartsWith("VColor")) //example input VColor1,.5,.7,1
            {
                string blah = s.Remove(0, 6);
                string[] allTheStrings = blah.Split(',');
                futureColor.r = float.Parse(allTheStrings[0]);
                futureColor.g = float.Parse(allTheStrings[1]);
                futureColor.b = float.Parse(allTheStrings[2]);
                futureColor.a = float.Parse(allTheStrings[3]);
            }
            if (s.StartsWith("camShake")) //camShake10,5
            {
                string temp = s.Remove(0, 8);
                string[] values = temp.Split(',');
                if (!int.TryParse(values[0], out length))
                {
                    Debug.Log("Failed parse: " + values[0]);
                }
                if (!float.TryParse(values[1], out strength))
                {
                    Debug.Log("Failed parse: " + values[1]);
                }
                if (!float.TryParse(values[2], out shakeAxis.x))
                {
                    Debug.Log("Failed Parse: " + values[2]);
                }
                if (!float.TryParse(values[3], out shakeAxis.y))
                {
                    Debug.Log("Failed Parse: " + values[3]);
                }
                if (!float.TryParse(values[4], out shakeAxis.z))
                {
                    Debug.Log("Failed Parse: " + values[4]);
                }
                shakeAxis.Normalize();
            }
        }
    }

    void OnDestroy()
    {
        //EventManager.PassStrings -= GetString;
    }
}
