using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour
{

    public int SizeX;
    public int SizeY;
    public List<List<Node>> Nodes;

    void Start() 
    {
        Nodes = new List<List<Node>>();

        for (int x = 0; x < SizeX; x++)
        {
            List<Node> row = new List<Node>();

            for (int y = 0; y < SizeY; y++)
            {
                row.Add(new Node());
            }

            Nodes.Add(row);
        }
    }

    public Node GetAt(int x, int y)
    {
        return Nodes[x][y];
    }

}
