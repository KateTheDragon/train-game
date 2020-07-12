using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainMovement : MonoBehaviour
{
    float speed = 0.003f;
    enum Direction {
        Up,
        Down,
        Left,
        Right,
        Stop
    }
    Direction going = Direction.Right;
    Direction toGo = Direction.Right;
    public Sprite crashSprite;

    Vector3 prevPos = new Vector3(-10, 0.5f, -1);
    Vector3 tile = new Vector3(0, 0, 0);

    private List<GameObject> onRails = new List<GameObject>();
    private bool gotOn = false;

    // Start is called before the first frame update
    void Start()
    {
        going = Direction.Right;
        transform.position = new Vector3(-10, 0.5f, -1);

    }

    // Update is called once per frame
    void Update()
    {
        if(gotOn)
        {
            bool derailed = true;
            foreach (GameObject go in onRails)
            {
                if (go.name.Contains("Station"))
                {
                    win();
                }
                if (go.name.Contains("Track"))
                {
                    derailed = false;
                    break;
                }
            }
            if (derailed)
            {
                Debug.Log("Derailed");
                StartCoroutine(crash());
            }
        } else
        {
            if (onRails.Count > 0)
            {
                foreach (GameObject go in onRails)
                {
                    if (go.name.Contains("Track"))
                    {
                        gotOn = true;
                        break;
                    }
                }
            }
        }
        

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
            case Direction.Stop:
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

    void OnTriggerExit2D(Collider2D col)
    {
        //To keep track of whether the train is in contact with any rails
        GameObject go = col.gameObject;
        if (onRails.Contains(go))
        {
            onRails.Remove(go);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //To keep track of whether the train is in contact with any rails
        GameObject go = col.gameObject;
        if (!onRails.Contains(go))
        {
            onRails.Add(go);
        }

        string name = col.name;
        tile = col.gameObject.transform.position;
        Direction a = going;
        Direction b = inverse(going);
        if (name.Contains("Obstacle"))
        {
            Debug.Log("Obstacle");
            StartCoroutine(crash());
        }
        else if (name.Contains("Station"))
        {
            win();
        } else
        {
            if (name.Contains("LR"))
            {
                a = Direction.Left;
                b = Direction.Right;
            }
            else if (name.Contains("LT"))
            {
                a = Direction.Left;
                b = Direction.Up;
            }
            else if (name.Contains("LB"))
            {
                a = Direction.Left;
                b = Direction.Down;
            }
            else if (name.Contains("RT"))
            {
                a = Direction.Right;
                b = Direction.Up;
            }
            else if (name.Contains("RB"))
            {
                a = Direction.Right;
                b = Direction.Down;
            }
            else if (name.Contains("TB"))
            {
                a = Direction.Up;
                b = Direction.Down;
            }
            if (going == inverse(a))
            {
                toGo = b;
            }
            else if (going == inverse(b))
            {
                toGo = a;
            }
            else
            {
                Debug.Log("Wrong direction at " + name);
                StartCoroutine(crash());
            }
        }
    }

    Direction inverse (Direction d) {
        switch (d) {
            case Direction.Up:
                return Direction.Down;
            case Direction.Down:
                return Direction.Up;
            case Direction.Left:
                return Direction.Right;
            case Direction.Right:
                return Direction.Left;
            }
        return Direction.Right;
    }         
    
    IEnumerator crash()
    {
        going = Direction.Stop;
        toGo = Direction.Stop;

        gameObject.GetComponent<Animator>().Play("Crash");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
    void win()
    {
        going = Direction.Stop;
        toGo = Direction.Stop;

        SceneManager.LoadScene("Win", LoadSceneMode.Single);
    }
}
