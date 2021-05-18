using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class BoardList : MonoBehaviour
{
    public RectTransform uiGroup;
    PlayerMovement player;
    GameObject board, boardPrefab;

    public void Enter(PlayerMovement p)
    {
        player = p;

        // 스크롤 뷰 불러오기
        ScrollRect scrollRect = GameObject.Find("BoardScroll").GetComponent<ScrollRect>();
        boardPrefab = Resources.Load("Prefabs/boardPrefab") as GameObject;

        // 보드 리스트를 받아온다
        for(int i = 0; i < 5; i++){
            board = Instantiate(boardPrefab) as GameObject;
            board.GetComponentInChildren<Text>().text = "에미야 술을 다오" + (i+1);

            // 스크롤 뷰에 추가 
            board.transform.SetParent(scrollRect.content.transform, false);      
        }
        
        uiGroup.anchoredPosition = Vector3.zero;
    }

    public void Exit()
    {
        uiGroup.anchoredPosition = Vector3.down * 2000;
    }

    
}
