using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class PhotoItem : MonoBehaviour, IPointerClickHandler
{
    public string picName, path;
    GameObject uiItem;
    GameObject reply,replyPrefab;
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
    //comment 받는 dto
    class Response
    {
        public string message;
        public result[] result; //여기를 배열로


    }

    void Start() {
        uiItem = GameObject.Find("PhotoItem");
    }

    public void OnPointerClick(PointerEventData data){
        string[] token = data.pointerPress.name.Split('_');
        picName = token[2];
        GameObject.Find("picName").GetComponent<Text>().text = token[2];
        uiItem.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        StartCoroutine(open());
    }

    public void Exit(){

        RawImage item = GameObject.Find("img").GetComponent<RawImage>();
        item.texture = Texture2D.whiteTexture;

        GameObject playbtn = GameObject.Find("RecordPlayButton");
        if(playbtn != null)
        {
            if (playbtn.activeSelf)
            {
                playbtn.SetActive(false);
            }
        }

        GameObject reply = GameObject.Find("PhotoItem");
        var temp = reply.GetComponentsInChildren<Transform>();

        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].name.Contains("Clone"))
            {
                Destroy(temp[i].gameObject);
            }
        }


        uiItem = GameObject.Find("PhotoItem");
        uiItem.GetComponent<RectTransform>().anchoredPosition = Vector3.left * -2000;
    }

    IEnumerator open()
    {
        string jsonResult;
        bool isOnLoading = true;
        string GetDataUrl = "http://k4a102.p.ssafy.io:8080/picture/" + picName;
        // string GetDataUrl = "http://localhost:8080//picture/" + 4;
        using (UnityWebRequest www = UnityWebRequest.Get(GetDataUrl))
        {
            //www.chunkedTransfer = false;
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    isOnLoading = false;
                    jsonResult =
                        System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Result r = JsonUtility.FromJson<Result>(www.downloadHandler.text);
                    path = r.result;
                    Debug.Log(path);
                    StartCoroutine(openImage());
                    StartCoroutine(openCommentCo());
                }
            }
        }
    }

    IEnumerator openImage()
    {
        UnityWebRequest www = null;
            
        www = UnityWebRequestTexture.GetTexture("http://"+path);
        // www = UnityWebRequestTexture.GetTexture("http://k4a102.p.ssafy.io/home/ubuntu/backend/picture/20210518154252avatar.png");

        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }

        else {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            RawImage item = GameObject.Find("img").GetComponent<RawImage>();
            item.texture = myTexture;
        }
    }

    //댓글 추가
    IEnumerator openCommentCo()
    {
        // 스크롤 뷰 불러오기
        ScrollRect scrollRect = GameObject.Find("PhotoScroll").GetComponent<ScrollRect>();
        replyPrefab = Resources.Load("Prefabs/Reply") as GameObject;

        string GetDataUrl = "http://k4a102.p.ssafy.io:8080/comment/list/" + picName;
        using (UnityWebRequest www = UnityWebRequest.Get(GetDataUrl))
        {
            //www.chunkedTransfer = false;
            yield return www.SendWebRequest();
            if (www.error != null) //불러오기 실패 시
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    Response r = JsonUtility.FromJson<Response>(www.downloadHandler.text);
                    if (r.result.Length > 0)
                    {
                        for (int i = 0; i < r.result.Length; i++)
                        {
                            reply = Instantiate(replyPrefab) as GameObject;
                            WWW localfile = new WWW(r.result[i].voice_path);
                            while (!localfile.isDone)
                            {
                                yield return null;
                            }

                            Debug.Log(r.result[i].voice_path);
                            reply.GetComponentInChildren<AudioSource>().clip = localfile.GetAudioClip(false);
                            Debug.Log(reply.GetComponentInChildren<AudioSource>().name);

                            reply.transform.SetParent(scrollRect.content.transform, false);
                        }
                    }
                }
            }
        }
    }
}