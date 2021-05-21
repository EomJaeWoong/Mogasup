using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //�� �гο� ���� ������
    [Header("LoginPanel")]
    public InputField IDInputField;
    public InputField PWInputField;
    public GameObject LoginPanelObj;

    //�������� ������
    [Header("CreateAccountPanel")]
    public InputField New_IDInputField;
    public InputField New_PWInputField;
    public InputField Check_PWInputField;
    public InputField NicknameInputField;
    public GameObject CreateAccountPanelObj;


    //����â ������
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

    //�α��� ��ư Ŭ��
    public void LoginBtn()
    {
        if(IDInputField.text =="" || PWInputField.text == "")
        {
            OpenAlert("������ �����մϴ�!");
            return;
        }

        StartCoroutine(LoginCo());
    }

    //�α��� �Լ�
    IEnumerator LoginCo()
    {
        //Debug.Log("���̵� : "+ IDInputField.text);
        //Debug.Log("��й�ȣ : " + PWInputField.text);

        //string url = "http://localhost:8080/user/login";
        string url = "http://k4a102.p.ssafy.io:8080/user/login";

        // formData�� �۽�
        WWWForm frm = new WWWForm();
        frm.AddField("email", IDInputField.text);
        frm.AddField("password", PWInputField.text);
        UnityWebRequest www = UnityWebRequest.Post(url, frm);
        
        yield return www.SendWebRequest();

        if (www.error == null)
        {
            Debug.Log(www.downloadHandler.text);
            //jsontype�� �����ͷ� ��ȯ
            Response r = JsonUtility.FromJson<Response>(www.downloadHandler.text);
            
            if (r.message == "no exist email")
            {
                OpenAlert("���̵� Ȥ�� ��й�ȣ�� Ʋ�Ƚ��ϴ�!");
                Debug.Log("���̵� ��й�ȣ ����" );

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
            OpenAlert("���ͳ� ������ Ȯ���� �ּ���");
            Debug.Log("error: " + www.error);
        }
    }

    //�ؽ�Ʈ â �ʱ�ȭ
    public void ClearText()
    {
        IDInputField.text = "";
        PWInputField.text = "";
        New_IDInputField.text = "";
        New_PWInputField.text = "";
        NicknameInputField.text = "";
        Check_PWInputField.text = "";
    }

    //ȸ������ â ����
    public void OpenCreateAccountBtn()
    {
        ClearText();
        LoginPanelObj.SetActive(false);
        CreateAccountPanelObj.SetActive(true);
    }

    //�α��� â ����
    public void OpenLoginBtn()
    {
        ClearText();
        CreateAccountPanelObj.SetActive(false);
        LoginPanelObj.SetActive(true);
    }

    //ȸ������ ��ư Ŭ��
    public void CreateAccountBtn()
    {
        if(New_IDInputField.text == "" || New_PWInputField.text == "" || NicknameInputField.text == "" || Check_PWInputField.text == "")
        {
            OpenAlert("������ �����մϴ�!");
                return;
        }

        if (New_PWInputField.text != Check_PWInputField.text)
        {
            OpenAlert("��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            New_PWInputField.text = "";
            Check_PWInputField.text = "";
            return;
        }


        StartCoroutine(CreateAccountCo());
    }

    //ȸ������ �Լ�
    IEnumerator CreateAccountCo()
    {
        

        //Debug.Log("���̵� : "+ IDInputField.text);
        //Debug.Log("��й�ȣ : " + PWInputField.text);

        // formData�� �۽�

        //string url = "http://localhost:8080/user";
        string url = "http://k4a102.p.ssafy.io:8080/user";

        WWWForm frm = new WWWForm();
        frm.AddField("email", New_IDInputField.text);
        frm.AddField("nickname", NicknameInputField.text);
        frm.AddField("password", New_PWInputField.text);
        UnityWebRequest www = UnityWebRequest.Post(url, frm);

        yield return www.SendWebRequest();

        //jsontype�� �����ͷ� ��ȯ

        if (www.error == null)
        {
            Response r = JsonUtility.FromJson<Response>(www.downloadHandler.text);
            
            Debug.Log(www.downloadHandler.text);

            if(r.message == "exist email")
            {
                OpenAlert("�̹� �����ϴ� ���̵� �Դϴ�.");
            }
            else if(r.message == "success")
            {
            OpenLoginBtn();
            OpenAlert("ȸ������ ����!");
            }
        }
        else
        {
            OpenAlert("���ͳ� ������ Ȯ���� �ּ���");
            Debug.Log("error: " + www.error);
        }
    }

    //��� �޼��� �ۼ�
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
