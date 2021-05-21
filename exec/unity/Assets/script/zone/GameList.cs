using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameList : MonoBehaviour
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
        uiGroup.anchoredPosition = Vector3.down * 3000;
    }

    public void OpenGolfGameBtn()
    {
        SceneManager.LoadScene("GolfGameScene");
    }

    public void OpenSnakeGameBtn()
    {
        SceneManager.LoadScene("SnakeGameScene");
    }

}
