using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BoardList : MonoBehaviour
{
    public RectTransform uiGroup;
    PlayerMovement player;
    GameObject board, boardPrefab;


    UserController userController;

    void Start()
    {
        userController = GameObject.Find("UserInfo").GetComponent<UserController>();
    }

    public void Enter(PlayerMovement p)
    {
        player = p;

        StartCoroutine(GetBoardListCo());
    }

    IEnumerator GetBoardListCo()
    {
        // 스크롤 뷰 불러오기
        ScrollRect scrollRect = GameObject.Find("BoardScroll2").GetComponent<ScrollRect>();
        boardPrefab = Resources.Load("Prefabs/boardPrefab") as GameObject;


        //string url = "http://localhost:8080/notice/" + userController.family_id;
        string url = "http://k4a102.p.ssafy.io:8080/notice/"+userController.family_id;

        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();
        

        if (www.error == null)
        {
            Debug.Log(www.downloadHandler.text);
            //jsontype을 데이터로 변환
            Response r = JsonUtility.FromJson<Response>(www.downloadHandler.text);

            if (r.message == "fail")
            {
                Debug.Log("다시 접속 후 작성해주세요!");

            }

            else if (r.message == "success")
            {
                if(r.result.Length > 0)
                {
                    // 보드 리스트를 받아온다
                    for (int i = 0; i < r.result.Length; i++)
                    {
                        board = Instantiate(boardPrefab) as GameObject;
                        board.GetComponentInChildren<Text>().text = r.result[i].name;
                        board.GetComponentInChildren<NoticeInfo>().notice_id = r.result[i].notice_id;
                        board.GetComponentInChildren<NoticeInfo>().title = r.result[i].name;
                        board.GetComponentInChildren<NoticeInfo>().content = r.result[i].content;
                        board.GetComponentInChildren<NoticeInfo>().date = r.result[i].date;
                        board.GetComponentInChildren<NoticeInfo>().user_id = r.result[i].user_id;
                        board.GetComponentInChildren<NoticeInfo>().nickname = r.result[i].nickname;


                        // 스크롤 뷰에 추가 
                        board.transform.SetParent(scrollRect.content.transform, false);
                    }
                }
                else
                {
                    board = Instantiate(boardPrefab) as GameObject;
                    board.GetComponentInChildren<Text>().text = "게시글이 없습니다";
                    

                    // 스크롤 뷰에 추가 
                    board.transform.SetParent(scrollRect.content.transform, false);

                }
            }

        }
        else
        {
            Debug.Log("인터넷 연결을 확인해 주세요");
            Debug.Log("error: " + www.error);
        }


        

        uiGroup.anchoredPosition = Vector3.zero;

    }

    public void Exit()
    {
        GameObject boards = GameObject.Find("BoardGroup");
        var temp = boards.GetComponentsInChildren<Transform>();

        for(int i=0; i<temp.Length; i++)
        {
            if (temp[i].name.Contains("Clone"))
            {
                Destroy(temp[i].gameObject);
            }
        }
        
        uiGroup.anchoredPosition = Vector3.down * 2000;
    }


    [System.Serializable]
    class Response
    {
        public string message;
        public Result[] result;
    }

    [System.Serializable]
    class Result
    {
        public string date;
        public string family_id;
        public string user_id;
        public string name;
        public string nickname;
        public string notice_id;
        public string content;
    }
}
