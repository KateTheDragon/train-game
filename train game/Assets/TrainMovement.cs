using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    float speed = 0.02f;
    enum Direction {
        Up,
        Down,
        Left,
        Right
    }
    Direction going = Direction.Right;
    Direction toGo = Direction.Right;

    Vector3 prevPos = new Vector3(-10, 0.5f, 0);
    Vector3 tile = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        going = Direction.Right;
        transform.position = new Vector3(-10, 0.5f, 0);

    }

    // Update is called once per frame
    void Update()
    {
        switch (going) {
            case Direction.Up:
                transform.eulerAngles = new Vector3 (0, 0, 90);
                transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
                break;
            case Direction.Down:
                transform.eulerAngles = new Vector3 (0, 0, -90);
                transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
                break;
            case Direction.Left:
                transform.eulerAngles = new Vector3 (0, 0, 180);
                transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
                break;
            case Direction.Right:
                transform.eulerAngles = new Vector3 (0, 0, 0);
                transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
                break;
        }
        
        if (toGo != going) {
            if ((prevPos.x < tile.x && transform.position.x >= tile.x) || (prevPos.x > tile.x && transform.position.x <= tile.x)) {
                going = toGo;
            } else if ((prevPos.y < tile.y && transform.position.y >= tile.y) || (prevPos.y > tile.y && transform.position.y <= tile.y)) {
                going = toGo;
            }
        }
        prevPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        string name = col.name;
        tile = col.gameObject.transform.position;
        Debug.Log(tile);
        Direction a = going;
        Direction b = going;
        if (name.Contains("LR")) {
            a = Direction.Left;
            b = Direction.Right;
        } else if (name.Contains("LT")) {
            a = Direction.Left;
            b = Direction.Up;
        } else if (name.Contains("LB")) {
            a = Direction.Left;
            b = Direction.Down;
        } else if (name.Contains("RT")) {
            a = Direction.Right;
            b = Direction.Up;
        } else if (name.Contains("RB")) {
            a = Direction.Right;
            b = Direction.Down;
        } else if (name.Contains("TB")) {
            a = Direction.Up;
            b = Direction.Down;
        }
        //TODO: crash if obstacle
        if (going == inverse(a)) {
            toGo = b;
        } else {
            toGo = a;
            //TODO: crash if track is wrong direction
            //TODO: crash if train derails
        }
    }

    Direction inverse (Direction d) {
        switch (d) {
            case Direction.Up:
                return Direction.Down;
                break;
            case Direction.Down:
                return Direction.Up;
                break;
            case Direction.Left:
                return Direction.Right;
                break;
            case Direction.Right:
                return Direction.Left;
                break;
            }
        return Direction.Right;
    }                                   
}
