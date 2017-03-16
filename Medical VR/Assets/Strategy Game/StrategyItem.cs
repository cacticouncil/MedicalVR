using UnityEngine;
using System.Collections;

[System.Serializable]
public class StrategyItem
{
    public enum StrategyItems
    {
        R = 0,
        R2,
        D,
        I,
        I2,
        P,
        V
    }
    public StrategyItems type;
    public Sprite image;
    public int count = 0;
}
