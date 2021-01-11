using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nodes;
using MapTiles;

public class MapGrid : MonoBehaviour
{

    [Header("Map Information")]
    public int width;
    public int height;
    [Range(0.0f, 1.0f)]
    public float density;
    public int cellularAutomataIterations;

    public GameObject tilePrefab;
    public Node[,] nodes;
    public GameObject[,] tiles;
    public NodeType[] automataRules;

    private void CreateRandomFill()
    {
        nodes = new Node[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var newNode = new Node();

                // chooses a random NodeType between Hole and Empty
                
                newNode.Type = Random.Range(0.0f, 1.0f) < density
                    ? NodeType.Empty
                    : NodeType.Hole;

                nodes[x, y] = newNode;
            }
        }
    }

    private Node GetNode(int x, int y)
    {
        if (x < 0 || y < 0 || x >= width || y >= height)
        {
            return null;
        }

        return nodes[x, y];
    }

    private int GetNeighbourCount(int x, int y)
    {
        var count = 0;

        for (var yOffset = -1; yOffset <= 1; yOffset++)
        {
            for (var xOffset = -1; xOffset <= 1; xOffset++)
            {
                if (yOffset == 0 && xOffset == 0)
                {
                    continue;
                }

                var neighbour = GetNode(x + xOffset, y + yOffset);

                if (neighbour == null)
                {
                    continue;
                }

                switch (neighbour.Type)
                {
                    case NodeType.Hole:
                        break;
                    default: 
                        count += 1;
                        break;
                }
            }
        }

        return count;
    }

    private void CellularAutomataIter()
    {
        var refined = new Node[width, height];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var neighbourCount = GetNeighbourCount(x, y);
                var newNode = new Node();

                newNode.Type = automataRules[neighbourCount];

                refined[x, y] = newNode;
            }
        }

        nodes = refined;
    }

    private void RunAutomata(int iterations)
    {
        for (; iterations > 0; iterations--)
        {
            CellularAutomataIter();
        }
    }

    private void CreateGameObjects()
    {
        tiles = new GameObject[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var node = GetNode(x, y);

                if (node == null || node.Type == NodeType.Hole)
                {
                    continue;
                }

                var mapTile = Instantiate(tilePrefab, transform);
                var mapTileInfo = mapTile.GetComponent<MapTile>();

                mapTile.transform.position = new Vector3(
                    x,
                    -y,
                    0.0f
                );

                var nodeLeft = GetNode(x - 1, y);
                var nodeUp = GetNode(x, y - 1);

                if (nodeLeft != null && nodeLeft.Type != NodeType.Hole)
                {
                    var neighbourLeft = tiles[x - 1, y].GetComponent<MapTile>();
                    
                    mapTileInfo.SetNeighbour(Neighbour.Left, neighbourLeft);
                    neighbourLeft.SetNeighbour(Neighbour.Right, mapTileInfo);
                }

                if (nodeUp != null && nodeUp.Type != NodeType.Hole)
                {
                    var neighbourUp = tiles[x, y - 1].GetComponent<MapTile>();

                    mapTileInfo.SetNeighbour(Neighbour.Up, neighbourUp);
                    neighbourUp.SetNeighbour(Neighbour.Down, mapTileInfo);
                }

                tiles[x, y] = mapTile;
            }
        }
    }

    public void GenerateMap()
    {
        DestroyMap();
        CreateRandomFill();
        RunAutomata(cellularAutomataIterations);
        CreateGameObjects();
    }

    public void OnButtonPress()
    {
        GenerateMap();
    }

    void DestroyMap()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    void Awake()
    {
        Random.InitState((int) System.DateTimeOffset.Now.ToUnixTimeSeconds());
    }

}
