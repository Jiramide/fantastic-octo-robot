using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour
{

    public GameObject tilePrefab;
    public List<List<MapTile>> nodes;

    public void GenerateMap(int sizeX, int sizeY)
    {
        nodes = new List<List<MapTile>>();

        for (int x = 0; x < sizeX; x++)
        {
            List<MapTile> col = new List<MapTile>();
            nodes.Add(col);

            for (int y = 0; y < sizeY; y++)
            {
                GameObject tile = Instantiate(tilePrefab, transform);
                MapTile tileInfo = tile.GetComponent<MapTile>();

                tile.name = "(" + x + ", " + y + ")";
                tile.transform.position = new Vector3(x, -y, 0.0f);
                
                if (x > 0)
                {
                    MapTile neighbourLeft = nodes[x - 1][y];

                    tileInfo.SetNeighbour(MapTile.Neighbour.Left, neighbourLeft);
                    neighbourLeft.SetNeighbour(MapTile.Neighbour.Right, tileInfo);
                }

                if (y > 0)
                {
                    MapTile neighbourUp = nodes[x][y - 1];

                    tileInfo.SetNeighbour(MapTile.Neighbour.Up, neighbourUp);
                    neighbourUp.SetNeighbour(MapTile.Neighbour.Down, tileInfo);
                }

                /*
                tile.GetComponent<SpriteRenderer>().color = (x + y) % 2 == 0
                    ? new Color(0.7f, 0.7f, 0.7f, 1.0f)
                    : new Color(0.9f, 0.9f, 0.9f, 1.0f); */

                col.Add(tileInfo);
            }
        }
    }

    public void OnButtonPress()
    {
        GenerateMap(20, 10);
    }

    void DestroyMap()
    {
    }

}
