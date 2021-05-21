using UnityEngine;
using System.Collections;

public class SpawnFood : MonoBehaviour
{
    // Food Prefab
    public GameObject foodPrefab;

    // Borders
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    SnakeUIManager uimanager;

    // Use this for initialization
    void Start()
    {
        uimanager = GameObject.Find("SnakeUIManager").GetComponent<SnakeUIManager>();
        // Spawn food every 4 seconds, starting in 3
        InvokeRepeating("Spawn", 1, 2);
    }

    void Update()
    {
        if (uimanager.status != null)
        {
            if (uimanager.status == "pause" || uimanager.status == "finish")
            {
                CancelInvoke("Spawn");
            }
            else if (uimanager.status == "resume")
            {
                InvokeRepeating("Spawn",1, 2);
                uimanager.status = "start";
            }

        }
    }

    // Spawn one piece of food
    void Spawn()
    {
        // x position between left & right border
        int x = (int)Random.Range(borderLeft.position.x,borderRight.position.x);

        // y position between top & bottom border
        int y = (int)Random.Range(borderBottom.position.y,borderTop.position.y);

        // Instantiate the food at (x, y)
        Instantiate(foodPrefab,new Vector2(x, y),Quaternion.identity); // default rotation
    }
}