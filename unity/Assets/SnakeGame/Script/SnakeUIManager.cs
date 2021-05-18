using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeUIManager : MonoBehaviour
{
    public string status;

    [SerializeField]
    private GameObject menuPanel;

    
    void Awake()
    {
        status = "start";

    }

    //�޴� ����
    public void OpenMenuBtn()
    {
        if (menuPanel.activeSelf == true)
        {
            if(status != "finish")
            {
                status = "resume";
            }
            menuPanel.SetActive(false);
        }
        else
        {
            if (status != "finish")
            {
                status = "pause";
            }
            menuPanel.SetActive(true);
        }
    }
    
    //�ٽý���
    public void RetryGameBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //��������
    public void FinishGameBtn()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
