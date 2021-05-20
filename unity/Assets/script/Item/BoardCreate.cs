using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class BoardCreate : MonoBehaviour
{
    [Header("CreateBoard")]
    public InputField BoardTitle;
    public InputField BoardContent;

    [Header("AlertBoard")]
    public Text AlertText;
    public GameObject AlertPanelObj;

    public RectTransform uiGroup;
    GameObject uiItem;
    UserController userController;
    void Start()
    {
        uiItem = GameObject.Find("BoardCreate");
        userController = GameObject.Find("UserInfo").GetComponent<UserController>();
    }

    public void OpenCreateBoardGroup()
    {
        uiGroup.anchoredPosition = Vector3.zero;
    }

    public void Exit()
    {
        BoardContent.text = "";
        BoardTitle.text = "";
        uiGroup.anchoredPosition = Vector3.left * -4000 + Vector3.down * 2000;
    }

    public void SubmitBtn()
    {
        if (BoardTitle.text == "" || BoardContent.text == "")
        {
            OpenAlert("공백이 존재합니다!");
            return;
        }
        StartCoroutine(CreateBoardCo());
    }

    IEnumerator CreateBoardCo()
    {
        //string url = "http://localhost:8080/notice";
        string url = "http://k4a102.p.ssafy.io:8080/notice";

        // formData로 송신
        WWWForm frm = new WWWForm();
        frm.AddField("content", BoardContent.text);
        frm.AddField("date", System.DateTime.Now.ToString("yyyy-MM-dd"));
        frm.AddField("family_id", userController.family_id);
        frm.AddField("name", BoardTitle.text);
        frm.AddField("user_id", userController.user_id);
        UnityWebRequest www = UnityWebRequest.Post(url, frm);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            Debug.Log(www.downloadHandler.text);
            //jsontype을 데이터로 변환
            Response r = JsonUtility.FromJson<Response>(www.downloadHandler.text);

            if (r.message == "fail")
            {
                OpenAlert("다시 접속 후 작성해주세요!");

            }

            else if (r.message == "success")
            {
                GameObject boards = GameObject.Find("BoardGroup");
                var temp = boards.GetComponentsInChildren<Transform>();

                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i].name.Contains("BoardNone"))
                    {
                        Destroy(temp[i].gameObject);
                    }
                }


                ScrollRect scrollRect = GameObject.Find("BoardScroll2").GetComponent<ScrollRect>();
                GameObject boardPrefab = Resources.Load("Prefabs/boardPrefab") as GameObject;
                GameObject board = Instantiate(boardPrefab) as GameObject;
                board.GetComponentInChildren<Text>().text = BoardTitle.text;
                board.GetComponentInChildren<NoticeInfo>().notice_id = r.result;
                board.GetComponentInChildren<NoticeInfo>().title = BoardTitle.text;
                board.GetComponentInChildren<NoticeInfo>().content = BoardContent.text;
                board.GetComponentInChildren<NoticeInfo>().date = System.DateTime.Now.ToString("yyyy-MM-dd");
                board.GetComponentInChildren<NoticeInfo>().user_id = userController.user_id;
                board.GetComponentInChildren<NoticeInfo>().nickname = userController.nickname;


                // 스크롤 뷰에 추가 
                board.transform.SetParent(scrollRect.content.transform, false);


                Exit();
            }

        }
        else
        {
            OpenAlert("인터넷 연결을 확인해 주세요");
            Debug.Log("error: " + www.error);
        }
    }



    public void OpenAlert(string text)
    {
        AlertText.text = text;
        AlertPanelObj.SetActive(true);
    }

    public void CloseAlert()
    {
        AlertPanelObj.SetActive(false);
    }

    [System.Serializable]
    class Response
    {
        public string message;
        public string result;
    }
}

