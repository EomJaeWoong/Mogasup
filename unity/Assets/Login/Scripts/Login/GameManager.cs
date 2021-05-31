using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //각 패널에 대한 변수들
    [Header("LoginPanel")]
    public InputField IDInputField;
    public InputField PWInputField;
    public GameObject LoginPanelObj;

    //계정생성 변수들
    [Header("CreateAccountPanel")]
    public InputField New_IDInputField;
    public InputField New_PWInputField;
    public InputField Check_PWInputField;
    public InputField NicknameInputField;
    public GameObject CreateAccountPanelObj;


    //에러창 변수들
    [Header("Alert")]
    public Text AlertText;
    public GameObject AlertPanelObj;

    UserController userController;
    
    void Start()
    {
        userController = GameObject.Find("UserInfo").GetComponent<UserController>();
    }
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (json);
            return wrapper.items;
        }
        
        private class Wrapper<T>
        {
            public T[] items;
        }
        
    }

    //로그인 버튼 클릭
    public void LoginBtn()
    {
        if(IDInputField.text =="" || PWInputField.text == "")
        {
            OpenAlert("공백이 존재합니다!");
            return;
        }

        StartCoroutine(LoginCo());
    }

    //로그인 함수
    IEnumerator LoginCo()
    {
        //Debug.Log("아이디 : "+ IDInputField.text);
        //Debug.Log("비밀번호 : " + PWInputField.text);

        //string url = "http://localhost:8080/user/login";
        string url = "http://k4a102.p.ssafy.io:8080/user/login";

        // formData로 송신
        WWWForm frm = new WWWForm();
        frm.AddField("email", IDInputField.text);
        frm.AddField("password", PWInputField.text);
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
                Debug.Log("아이디 비밀번호 오류" );

            }

            else if(r.message == "success")
            {
                //Debug.Log(r.result.user_id + r.result.nickname + r.result.family_id);
                
                userController.user_id = r.result.user_id;
                userController.id = IDInputField.text;
                userController.family_id = r.result.family_id;
                userController.nickname = r.result.nickname;
                /*
                GetComponent<UserController>().setId(IDInputField.text);
                GetComponent<UserController>().setNickname(r.result.nickname);
                GetComponent<UserController>().setFamilyId(r.result.family_id);
                */
                SceneManager.LoadScene("FamilyScene");

        }

        }
        else
        {
            OpenAlert("인터넷 연결을 확인해 주세요");
            Debug.Log("error: " + www.error);
        }
    }

    //텍스트 창 초기화
    public void ClearText()
    {
        IDInputField.text = "";
        PWInputField.text = "";
        New_IDInputField.text = "";
        New_PWInputField.text = "";
        NicknameInputField.text = "";
        Check_PWInputField.text = "";
    }

    //회원가입 창 열기
    public void OpenCreateAccountBtn()
    {
        ClearText();
        LoginPanelObj.SetActive(false);
        CreateAccountPanelObj.SetActive(true);
    }

    //로그인 창 열기
    public void OpenLoginBtn()
    {
        ClearText();
        CreateAccountPanelObj.SetActive(false);
        LoginPanelObj.SetActive(true);
    }

    //회원가입 버튼 클릭
    public void CreateAccountBtn()
    {
        if(New_IDInputField.text == "" || New_PWInputField.text == "" || NicknameInputField.text == "" || Check_PWInputField.text == "")
        {
            OpenAlert("공백이 존재합니다!");
                return;
        }

        if (New_PWInputField.text != Check_PWInputField.text)
        {
            OpenAlert("비밀번호가 일치하지 않습니다.");
            New_PWInputField.text = "";
            Check_PWInputField.text = "";
            return;
        }


        StartCoroutine(CreateAccountCo());
    }

    //회원가입 함수
    IEnumerator CreateAccountCo()
    {
        

        //Debug.Log("아이디 : "+ IDInputField.text);
        //Debug.Log("비밀번호 : " + PWInputField.text);

        // formData로 송신

        //string url = "http://localhost:8080/user";
        string url = "http://k4a102.p.ssafy.io:8080/user";

        WWWForm frm = new WWWForm();
        frm.AddField("email", New_IDInputField.text);
        frm.AddField("nickname", NicknameInputField.text);
        frm.AddField("password", New_PWInputField.text);
        UnityWebRequest www = UnityWebRequest.Post(url, frm);

        yield return www.SendWebRequest();

        //jsontype을 데이터로 변환

        if (www.error == null)
        {
            Response r = JsonUtility.FromJson<Response>(www.downloadHandler.text);
            
            Debug.Log(www.downloadHandler.text);

            if(r.message == "exist email")
            {
                OpenAlert("이미 존재하는 아이디 입니다.");
            }
            else if(r.message == "success")
            {
            OpenLoginBtn();
            OpenAlert("회원가입 성공!");
            }
        }
        else
        {
            OpenAlert("인터넷 연결을 확인해 주세요");
            Debug.Log("error: " + www.error);
        }
    }

    //경고 메세지 작성
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
        public Result result;
        

    }
    [System.Serializable]
    class Result
    {
        public string family_id;
        public string user_id;
        public string nickname;
    }
}
