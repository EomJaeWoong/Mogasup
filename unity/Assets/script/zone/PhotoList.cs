using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoList : MonoBehaviour
{
    public RectTransform uiGroup;
    PlayerMovement player;


    public void Enter(PlayerMovement p)
    {
        player = p;
        uiGroup.anchoredPosition = Vector3.zero;
    }

    public void Exit()
    {
        uiGroup.anchoredPosition = Vector3.down * 1000;
    }
}
