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
    List<GameObject> photoList;
    GameObject photo, photoPrefab;
    
    public void Enter(PlayerMovement p)
    {
        player = p;
        photoList = new List<GameObject>();
        
        StartCoroutine(SetTexture());

        uiGroup.anchoredPosition = Vector3.zero;
    }

    public void Exit()
    {
        uiGroup.anchoredPosition = Vector3.down * 1000;
    }

    // url 사진 불러오기
    IEnumerator SetTexture() {
        // 스크롤 뷰 불러오기
        ScrollRect scrollRect = GameObject.Find("PhotoScroll").GetComponent<ScrollRect>();
        photoPrefab = Resources.Load("Prefabs/photoPrefab") as GameObject;

        // 사진 리스트를 받아온다 
        for(int i = 0; i < 10; i++){
            UnityWebRequest www = null;
            if(i%2 == 0)
                www = UnityWebRequestTexture.GetTexture("https://cdn.hellodd.com/news/photo/202005/71835_craw1.jpg");
            else
                www = UnityWebRequestTexture.GetTexture("https://mblogthumb-phinf.pstatic.net/MjAxOTEyMTRfMjA0/MDAxNTc2MzI2NDQwODQy.dfAPnaGv28oEtRRtngzaHUxt0_L3K7TuTyI00ThtMuEg.MnZ1FDtj04eapUcQHVEC3NATMi73Coj4ee1YxHCzaAEg.JPEG.parkamsterdam/IMG_3384.JPG?type=w800");

            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
            }

            else {
                photo = Instantiate(photoPrefab) as GameObject;
                Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                RawImage item = photo.GetComponent<RawImage>();
                item.texture = myTexture;

                // Object의 이름은 Photh_[가족 id]_[사진_id]
                item.name = "photo_5_" + i;

                photo.transform.SetParent(scrollRect.content.transform, false); 
            }
        }   
    }
}
