using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class trackGenerator : MonoBehaviour
{

    [SerializeField] Tile[] trackTiles = new Tile[6];
    public int size = 10;
    public Vector3Int pos;
    public Tilemap map;
    
    void Start()
    {
        fill();
    }
    //TODO: Reset button
    void Update()
    {
        bool empty = true;
        for (int i = 0; i < size; i++)
        {
            Vector3Int place = pos + new Vector3Int(0, i, 0);
            if (map.GetTile(place) != null)
            {
                empty = false;
                break;
            }
        }
        if (empty)
        {
            fill();
        }
    }

    private void fill()
    {
        Vector3Int[] positions = new Vector3Int[size];
        TileBase[] tracks = new TileBase[size];
        for (int i = 0; i < size; i++)
        {
            positions[i] = pos + new Vector3Int(0, i, 0);
            int rand = Random.Range(0, 6);
            tracks[i] = trackTiles[rand];
        }
        map.SetTiles(positions, tracks);
    }
}
