using UnityEngine;
using System.Collections;

public class CellScript : MonoBehaviour
{

    public int reproduction = 1;
    public int defense = 1;
    public int immunity = 1;

    public Vector2 key;

    private int Treproduction = 10;

    // Use this for initialization
    void Start()
    {
        //gameObject.GetComponentInParent<CellManagerScript>().AddToDictionary(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnUpdate()
    {
        Treproduction -= reproduction;
        if (Treproduction <= 0)
        {
            //reproduce
            gameObject.GetComponentInParent<CellManagerScript>().SpawnCell(key);
            Treproduction = 10 + Treproduction;
        }

        if (immunity >= 10)
        {
            //spread immunity
        }
    }

    public void Infect()
    {

    }
}