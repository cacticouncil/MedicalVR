using UnityEngine;
using System.Collections;

[System.Serializable]
public class StrategyItem
{
    public enum StrategyItems
    {
        CDK = 0,
        TGF,
        Fuzeon,
        Antigen,
        Interferon,
        Protein,
        WhiteCell
    }
    public StrategyItems type;
    public Mesh mesh;
    public Material material;
    public int count = 0;
}
