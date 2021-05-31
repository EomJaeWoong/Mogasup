using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserController : MonoBehaviour
{
    public string user_id;
    public string id ;
    public string nickname;
    public string family_id;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
