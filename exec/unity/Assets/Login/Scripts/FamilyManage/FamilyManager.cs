using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class FamilyManager : MonoBehaviour
{
    //���� �г�
    [Header("DefaultPanel")]
    public GameObject FamilyNotExistPanelObj;
    public GameObject FamilyExistPanelObj;
    public Text OpenFamilyText;
    public GameObject DefaultPanelObj;

    //���� ����
    [Header("CreatePanel")]
    public GameObject CreatePanelObj;
    public InputField Create_FamilyNameText;

    //���� �˻� �� ����
    [Header("JoinPanel")]
    public GameObject JoinPanelObj;
    public InputField FamilyNoText;
    public GameObject FamilyNameObj;
    public Text FindResultText;

    //����â ������
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

    //���� â ����
    public void OpenDefaultBtn()
    {
        ClearText();

        string family_id = userController.family_id;
        //���� ���� ��
        if (family_id == "0")
        {
            FamilyNotExistPanelObj.SetActive(true);
            FamilyExistPanelObj.SetActive(false);
        }
        //���� ���� ��
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

    //�������� â ����
    public void OpenCreateFamilyBtn()
    {
        ClearText();
        DefaultPanelObj.SetActive(false);
        CreatePanelObj.SetActive(true);
        JoinPanelObj.SetActive(false);
    }

    //�������� â ����
    public void OpenJoinFamilyBtn()
    {
        ClearText();
        DefaultPanelObj.SetActive(false);
        CreatePanelObj.SetActive(false);
        JoinPanelObj.SetActive(true);
    }

    //��� �޼��� �ۼ�
    public void OpenAlert(string text)
    {
        AlertText.text = text;
        AlertPanelObj.SetActive(true);
    }

    //��� �޼��� �ݱ�
    public void CloseAlert()
    {
        AlertPanelObj.SetActive(false);
    }

    //���� ���� ��ư
    public void CreateFamilyBtn()
    {
        if(Create_FamilyNameText.text == "")
        {
            OpenAlert("�������� �Է����ּ���!");
            return;
        }

        StartCoroutine(CreateFamilyCo());
    }

    //���� ���� �Լ�
    IEnumerator CreateFamilyCo()
    {
        //string url = "http://localhost:8080/family";
        string url = "http://k4a102.p.ssafy.io:8080/family";

        // formData�� �۽�
        WWWForm frm = new WWWForm();
        
        frm.AddField("user_id", userController.user_id);
        frm.AddField("name",Create_FamilyNameText.text);
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
                Debug.Log("���̵� ��й�ȣ ����");

            }

            else if (r.message == "success")
            {
                userController.family_id = r.result;
                OpenAlert(Create_FamilyNameText.text + "\n���� ����!");
                OpenDefaultBtn();

            }

        }
        else
        {
            OpenAlert("���ͳ� ������ Ȯ���� �ּ���");
            Debug.Log("error: " + www.error);
        }
    }

    //���� �˻� ��ư
    public void FindFamilyByNoBtn()
    {
        if (FamilyNoText.text == "")
        {
            OpenAlert("������ȣ�� �Է� �� �ּ���");
            return;
        }
           

        StartCoroutine(FindFamilyCo());
    }

    //���� �˻� �Լ�
    IEnumerator FindFamilyCo()
    {
        //string url = "http://localhost:8080/family/name/" + FamilyNoText.text;
        string url = "http://k4a102.p.ssafy.io:8080/family/name/" + FamilyNoText.text;

        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            Debug.Log(www.downloadHandler.text);
            //jsontype�� �����ͷ� ��ȯ
            Response r = JsonUtility.FromJson<Response>(www.downloadHandler.text);
            Debug.Log(www.downloadHandler.text);
            if (r.message == "success")
            {
                if(r.result == "")
                {
                    OpenAlert("���� ��ȣ�� �ٽ� Ȯ���� ������!");
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
                OpenAlert("���� ��ȣ�� �ٽ� Ȯ���� ������!");
                FamilyNameObj.SetActive(false);


            }

        }
        else
        {
            OpenAlert("���ͳ� ������ Ȯ���� �ּ���");
            Debug.Log("error: " + www.error);
        }
    }

    //���� ���� ��ư
    public void JoinFamilyBtn()
    {
        StartCoroutine(JoinFamilyCo());
    }

    //���� ���� �Լ�
    IEnumerator JoinFamilyCo()
    {
        //string url = "http://localhost:8080/family/user";
        string url = "http://k4a102.p.ssafy.io:8080/family/user";

        // formData�� �۽�
        WWWForm frm = new WWWForm();
        frm.AddField("user_id", userController.user_id);
        frm.AddField("family_id", FamilyNoText.text);
        UnityWebRequest www = UnityWebRequest.Post(url, frm);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            Debug.Log(www.downloadHandler.text);
            //jsontype�� �����ͷ� ��ȯ
            Response r = JsonUtility.FromJson<Response>(www.downloadHandler.text);

            if (r.message == "success")
            {
                userController.family_id = FamilyNoText.text;
                OpenAlert(FindResultText.text + "\n���� �Ϸ�!");
                OpenDefaultBtn();

            }
            else
            {
                OpenAlert("���̵� Ȥ�� ��й�ȣ�� Ʋ�Ƚ��ϴ�!");
                Debug.Log("���̵� ��й�ȣ ����");
            }
        }

        else
        {
            OpenAlert("���ͳ� ������ Ȯ���� �ּ���");
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
