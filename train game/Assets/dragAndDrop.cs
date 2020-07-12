using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class dragAndDrop : MonoBehaviour
{
    private bool isDragging;
    Tilemap map;
    TileBase tile;
    private Vector3Int previous;

    //TODO: figure out how not to overwrite things

    private void Start()
    {
        map = gameObject.GetComponentInParent<Tilemap>();
        previous = map.WorldToCell(transform.position);
        tile = map.GetTile(previous);
        if (map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)) == map.WorldToCell(transform.position) && Input.GetMouseButton(0))
        {
            isDragging = true;
        }
    }

    public void OnMouseDown()
    {
        isDragging = true;
    }

    public void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging && !Input.GetMouseButton(0))
        {
            isDragging = false;
        }
        if (isDragging && Input.GetMouseButton(0)) {
            // get current grid location
            Vector3Int currentCell = map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            // if the position has changed
            if (currentCell != previous)
            {
                // set the new tile
                map.SetTile(currentCell, tile);

                // erase previous
                map.SetTile(previous, null);
            }
            // save the new position for next frame
            previous = currentCell;
        }
    }
}
