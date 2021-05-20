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

    public class Result
    {
        public string message = "";
        public string result = "";
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
}