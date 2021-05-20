using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class PhotoList : MonoBehaviour
{
    public RectTransform uiGroup;
    PlayerMovement player;
    GameObject photo, photoPrefab;
    Response res;

    [System.Serializable]
    public class Result
    {
        public string image_path = "";
        public string picture_id = "";
    }

    class Response
    {
        public string message;
        public Result[] result;
    }

    public void Enter(PlayerMovement p)
    {
        player = p;
        
        StartCoroutine(openList());
        uiGroup.anchoredPosition = Vector3.zero;
    }

    public void Exit()
    {
        GameObject photos = GameObject.Find("PhotoGroup");
        RawImage[] temp = photos.GetComponentsInChildren<RawImage>();

        for(int i=0; i<temp.Length; i++)
        {
            Destroy(temp[i].gameObject);
        }
        uiGroup.anchoredPosition = Vector3.down * 1000;
    }

    // url 사진 불러오기
    IEnumerator SetTexture() {
        // 스크롤 뷰 불러오기
        ScrollRect scrollRect = GameObject.Find("PhotoListScroll").GetComponent<ScrollRect>();
        photoPrefab = Resources.Load("Prefabs/photoPrefab") as GameObject;

        // 사진 리스트를 받아온다 
        for(int i = 0; i < res.result.Length; i++){
            UnityWebRequest www = null;
            
            www = UnityWebRequestTexture.GetTexture(res.result[i].image_path);

            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
            }

            else {
                photo = Instantiate(photoPrefab) as GameObject;
                Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                RawImage item = photo.GetComponent<RawImage>();
                item.texture = myTexture;

                Debug.Log(res.result[i].picture_id);
                // Object의 이름은 Photh_[가족 id]_[사진_id]
                item.name = "photo_1_" + res.result[i].picture_id;

                photo.transform.SetParent(scrollRect.content.transform, false); 
            }
        }   
    }

    IEnumerator openList()
    {
        string jsonResult;
        bool isOnLoading = true;
        string GetDataUrl = "http://k4a102.p.ssafy.io:8080/picture/list/" + 1;
        // string GetDataUrl = "http://localhost:8080//picture/list/" + 1;
        using (UnityWebRequest www = UnityWebRequest.Get(GetDataUrl))
        {
            //www.chunkedTransfer = false;
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError) //�ҷ����� ���� ��
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
                    res = JsonUtility.FromJson<Response>(www.downloadHandler.text);

                    StartCoroutine(SetTexture());
                }
            }
        }
    }
}
