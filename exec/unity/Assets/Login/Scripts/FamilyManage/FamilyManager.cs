using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class FamilyManager : MonoBehaviour
{
    //최초 패널
    [Header("DefaultPanel")]
    public GameObject FamilyNotExistPanelObj;
    public GameObject FamilyExistPanelObj;
    public Text OpenFamilyText;
    public GameObject DefaultPanelObj;

    //가족 생성
    [Header("CreatePanel")]
    public GameObject CreatePanelObj;
    public InputField Create_FamilyNameText;

    //가족 검색 및 가입
    [Header("JoinPanel")]
    public GameObject JoinPanelObj;
    public InputField FamilyNoText;
    public GameObject FamilyNameObj;
    public Text FindResultText;

    //에러창 변수들
    [Header("Alert")]
    public Text AlertText;
    public GameObject AlertPanelObj;

    UserController userController;

    void Start()
    {


        userController = GameObject.Find("UserInfo").GetComponent<UserController>();
        OpenDefaultBtn();

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ClearText()
    {
        Create_FamilyNameText.text = "";
        FamilyNoText.text = "";
    }

    //최초 창 열기
    public void OpenDefaultBtn()
    {
        ClearText();

        string family_id = userController.family_id;
        //가족 없을 때
        if (family_id == "0")
        {
            FamilyNotExistPanelObj.SetActive(true);
            FamilyExistPanelObj.SetActive(false);
        }
        //가족 있을 때
        else
        {
            FamilyNotExistPanelObj.SetActive(false);
            FamilyExistPanelObj.SetActive(true);
        }

        DefaultPanelObj.SetActive(true);
        CreatePanelObj.SetActive(false);
        JoinPanelObj.SetActive(false);
        FamilyNameObj.SetActive(false);


    }

    //가족생성 창 열기
    public void OpenCreateFamilyBtn()
    {
        ClearText();
        DefaultPanelObj.SetActive(false);
        CreatePanelObj.SetActive(true);
        JoinPanelObj.SetActive(false);
    }

    //가족가입 창 열기
    public void OpenJoinFamilyBtn()
    {
        ClearText();
        DefaultPanelObj.SetActive(false);
        CreatePanelObj.SetActive(false);
        JoinPanelObj.SetActive(true);
    }

    //경고 메세지 작성
    public void OpenAlert(string text)
    {
        AlertText.text = text;
        AlertPanelObj.SetActive(true);
    }

    //경고 메세지 닫기
    public void CloseAlert()
    {
        AlertPanelObj.SetActive(false);
    }

    //가족 생성 버튼
    public void CreateFamilyBtn()
    {
        if(Create_FamilyNameText.text == "")
        {
            OpenAlert("가족명을 입력해주세요!");
            return;
        }

        StartCoroutine(CreateFamilyCo());
    }

    //가족 생성 함수
    IEnumerator CreateFamilyCo()
    {
        //string url = "http://localhost:8080/family";
        string url = "http://k4a102.p.ssafy.io:8080/family";

        // formData로 송신
        WWWForm frm = new WWWForm();
        
        frm.AddField("user_id", userController.user_id);
        frm.AddField("name",Create_FamilyNameText.text);
        UnityWebRequest www = UnityWebRequest.Post(url, frm);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            Debug.Log(www.downloadHandler.text);
            //jsontype을 데이터로 변환
            Response r = JsonUtility.FromJson<Response>(www.downloadHandler.text);

            if (r.message == "no exist email")
            {
                OpenAlert("아이디 혹은 비밀번호가 틀렸습니다!");
                Debug.Log("아이디 비밀번호 오류");

            }

            else if (r.message == "success")
            {
                userController.family_id = r.result;
                OpenAlert(Create_FamilyNameText.text + "\n생성 성공!");
                OpenDefaultBtn();

            }

        }
        else
        {
            OpenAlert("인터넷 연결을 확인해 주세요");
            Debug.Log("error: " + www.error);
        }
    }

    //가족 검색 버튼
    public void FindFamilyByNoBtn()
    {
        if (FamilyNoText.text == "")
        {
            OpenAlert("가족번호를 입력 해 주세요");
            return;
        }
           

        StartCoroutine(FindFamilyCo());
    }

    //가족 검색 함수
    IEnumerator FindFamilyCo()
    {
        //string url = "http://localhost:8080/family/name/" + FamilyNoText.text;
        string url = "http://k4a102.p.ssafy.io:8080/family/name/" + FamilyNoText.text;

        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            Debug.Log(www.downloadHandler.text);
            //jsontype을 데이터로 변환
            Response r = JsonUtility.FromJson<Response>(www.downloadHandler.text);
            Debug.Log(www.downloadHandler.text);
            if (r.message == "success")
            {
                if(r.result == "")
                {
                    OpenAlert("가족 번호를 다시 확인해 보세요!");
                    FamilyNameObj.SetActive(false);
                }
                else
                {
                    FindResultText.text = r.result;
                    FamilyNameObj.SetActive(true);
                }
               

            }

            else 
            {
                OpenAlert("가족 번호를 다시 확인해 보세요!");
                FamilyNameObj.SetActive(false);


            }

        }
        else
        {
            OpenAlert("인터넷 연결을 확인해 주세요");
            Debug.Log("error: " + www.error);
        }
    }

    //가족 참가 버튼
    public void JoinFamilyBtn()
    {
        StartCoroutine(JoinFamilyCo());
    }

    //가족 참가 함수
    IEnumerator JoinFamilyCo()
    {
        //string url = "http://localhost:8080/family/user";
        string url = "http://k4a102.p.ssafy.io:8080/family/user";

        // formData로 송신
        WWWForm frm = new WWWForm();
        frm.AddField("user_id", userController.user_id);
        frm.AddField("family_id", FamilyNoText.text);
        UnityWebRequest www = UnityWebRequest.Post(url, frm);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            Debug.Log(www.downloadHandler.text);
            //jsontype을 데이터로 변환
            Response r = JsonUtility.FromJson<Response>(www.downloadHandler.text);

            if (r.message == "success")
            {
                userController.family_id = FamilyNoText.text;
                OpenAlert(FindResultText.text + "\n가입 완료!");
                OpenDefaultBtn();

            }
            else
            {
                OpenAlert("아이디 혹은 비밀번호가 틀렸습니다!");
                Debug.Log("아이디 비밀번호 오류");
            }
        }

        else
        {
            OpenAlert("인터넷 연결을 확인해 주세요");
            Debug.Log("error: " + www.error);
        }
    }

    public void StartGameBtn()
    {
        SceneManager.LoadScene("SampleScene");
    }

    [System.Serializable]
    class Response
    {
        public string message;
        public string result;
    }
 

}
