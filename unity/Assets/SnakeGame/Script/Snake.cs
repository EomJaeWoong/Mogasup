using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class Snake : MonoBehaviour
{
    // Did the snake eat something?
    bool ate = false;

    //Did user died?
    bool isDied = false;
    float speed;

    // Tail Prefab
    public GameObject tailPrefab;
    [SerializeField] Text tailLength;

    SnakeUIManager uimanager;

    // Current Movement Direction
    // (by default it moves to the right)
    Vector2 dir = Vector2.right;

    // Keep Track of Tail
    List<Transform> tail = new List<Transform>();

    // Use this for initialization
    void Start()
    {
        uimanager = GameObject.Find("SnakeUIManager").GetComponent<SnakeUIManager>();
        tailLength.text = "0";
        speed = 0.3f;
        // Move the Snake every 300ms
        InvokeRepeating("Move", speed, speed);
    }

    // Update is called once per frame
    void Update()
    {

        if (!isDied)
        {
            // Move in a new Direction?
            if (Input.GetKey(KeyCode.RightArrow))
                dir = Vector2.right;
            else if (Input.GetKey(KeyCode.DownArrow))
                dir = -Vector2.up;    // '-up' means 'down'
            else if (Input.GetKey(KeyCode.LeftArrow))
                dir = -Vector2.right; // '-right' means 'left'
            else if (Input.GetKey(KeyCode.UpArrow))
                dir = Vector2.up;
        }
        else
        {
            if (Input.GetKey(KeyCode.R))
            {
                //clear the tail
                tail.Clear();

                //reset to origin
                transform.position = new Vector3(0, 0, 0);

                //make snake alive
                isDied = false;

                uimanager.status = "start";
            }
        }
        if (uimanager.status != null)
        {
            //정지상태일 때
            if (uimanager.status == "pause" || uimanager.status == "finish")
            {
                CancelInvoke("Move");
            }
            else if (uimanager.status == "resume")
            {
                InvokeRepeating("Move", speed, speed);
            }
        }
    }

    void Move()
    {
        if (!isDied)
        {
            // Save current position (gap will be here)
            Vector2 v = transform.position;

            // Move head into new direction (now there is a gap)
            transform.Translate(dir);

            // Ate something? Then insert new Element into gap
            if (ate)
            {
                // Load Prefab into the world
                GameObject g = (GameObject)Instantiate(tailPrefab,
                                  v,
                                  Quaternion.identity);

                // Keep track of it in our tail list
                tail.Insert(0, g.transform);

                tailLength.text = tail.Count().ToString();

                // Reset the flag
                ate = false;
                if(speed >= 0.03f)
                {
                    speed -= 0.01f;
                }
                CancelInvoke("Move");
                InvokeRepeating("Move", speed, speed);

            }
            else if (tail.Count > 0)
            {   // Do we have a Tail?
                // Move last Tail Element to where the Head was
                tail.Last().position = v;

                // Add to front of list, remove from the back
                tail.Insert(0, tail.Last());
                tail.RemoveAt(tail.Count - 1);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // Food?
        if (coll.name.StartsWith("Food"))
        {
            // Get longer in next Move call
            ate = true;

            // Remove the Food
            Destroy(coll.gameObject);
        }
        else
        {   // Collided with Tail or Border
            isDied = true;
            uimanager.status = "finish";
            uimanager.OpenMenuBtn();
        }
    }
}
