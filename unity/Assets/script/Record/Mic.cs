using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

using System;
using System.IO;
public class Mic : MonoBehaviour
{
    AudioSource aud;
    AudioClip aud2;
    AudioClip tmp;
    // Start is called before the first frame update
    public float rmsValue;
    public float modulate = 10000;
    public int resultValue;
    public int cutValue = 15;
    private float[] samples;
    int sampleRate = 44100;

    GameObject reply, replyPrefab;

    [Header("Record Button")]
    public GameObject RecordStartbtn;
    public GameObject RecordStopbtn;
    public GameObject RecordPlaybtn;

    UserController userController;

    
    public class Result
    {
        public string message = "";
        public string result = "";
    }

    //comment 받는 dto
    [System.Serializable]
    public class result
    {
        public string voice_path = "";
        public string comment_id = "";
        public string user_id = "";
    }


    class Response
    {
        public string message;
        public result[] result; //여기를 배열로


    }
    public void Start()
    {
        userController = GameObject.Find("UserInfo").GetComponent<UserController>();
        aud = GetComponent<AudioSource>();
        samples = new float[sampleRate];
        aud2 = Microphone.Start(Microphone.devices[0].ToString(), true, 1, sampleRate);

    }
    public void Update()
    {
        aud2.GetData(samples, 0);
        float sum = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += samples[i] * samples[i];
        }
        rmsValue = Mathf.Sqrt(sum / samples.Length);
        rmsValue = rmsValue * modulate;
        rmsValue = Mathf.Clamp(rmsValue, 0, 100);
        resultValue = Mathf.RoundToInt(rmsValue);
        if (resultValue < cutValue)
            resultValue = 0;
    }

    public void PlaySnd()
    {
        aud.Play();
    }
    public void RecSnd()//녹음
    {
        aud.clip = Microphone.Start(Microphone.devices[0].ToString(), false, 12, 44100);// (마이크, ?, 시간, 주파수)
        RecordStartbtn.SetActive(false);
        RecordStopbtn.SetActive(true);
        RecordPlaybtn.SetActive(false);
    }

    public void RecStop()//녹음중단 중단해도 원래정했던 시간 다채워짐..
    {
        Microphone.End(Microphone.devices[0].ToString());
        RecordStartbtn.SetActive(true);
        RecordStopbtn.SetActive(false);
        RecordPlaybtn.SetActive(true);
    }

    public void save()//댓글 저장, 로컬 저장 후 저장된 파일를 서버로 보냄
    {
        DateTime today = DateTime.Now;
        string s = today.ToString("yyyy-MM-dd-HH-mm-ss");//이름
        string path = "C:/SSAFY/" + s + ".wav";//저장위치
        SavWav.Save(path, aud.clip);
        //Thread.Sleep(3000);
        StartCoroutine(Send(path));
    }

    IEnumerator Send(string filename)
    {
        Debug.Log(filename);
        WWWForm form = new WWWForm();
        form.AddField("user_id",userController.user_id);
        form.AddField("picture_id", GameObject.Find("picName").gameObject.GetComponent<Text>().text);
        WWW localfile = new WWW(filename);
        string url = "http://k4a102.p.ssafy.io:8080/comment";
        form.AddBinaryData("file", localfile.bytes);
        // Upload via post request
        var www = UnityWebRequest.Post(url, form);
        // change the method name
        www.method = "POST";
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            // 스크롤 뷰 불러오기
            ScrollRect scrollRect = GameObject.Find("PhotoScroll").GetComponent<ScrollRect>();
            replyPrefab = Resources.Load("Prefabs/Reply") as GameObject;
            reply = Instantiate(replyPrefab) as GameObject;
            reply.GetComponentInChildren<AudioSource>().clip = aud.clip;
            reply.transform.SetParent(scrollRect.content.transform, false);
        }

    }

}
