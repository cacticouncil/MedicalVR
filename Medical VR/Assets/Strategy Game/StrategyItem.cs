using UnityEngine;
using System.Collections;

[System.Serializable]
public class StrategyItem
{
    public enum StrategyItems
    {
        R,
        R2,
        D,
        I,
        I2,
        V
    }
    public StrategyItems type;
    public Sprite image;
    public int count = 0;
}
