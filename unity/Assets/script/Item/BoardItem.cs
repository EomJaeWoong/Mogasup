using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoardItem : MonoBehaviour, IPointerClickHandler
{
    GameObject uiItem;

    void Start() {
        uiItem = GameObject.Find("BoardItem");
    }

    public void OnPointerClick(PointerEventData data){
        NoticeInfo noticeinfo = data.pointerPress.GetComponentInChildren<NoticeInfo>();
        Text[] t = uiItem.GetComponentsInChildren<Text>();
        t[0].text = noticeinfo.title;
        t[1].text = noticeinfo.content;
        t[2].text = noticeinfo.nickname;
        t[3].text = noticeinfo.date;


        uiItem.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }

    public void Exit(){

        uiItem.GetComponent<RectTransform>().anchoredPosition = Vector3.left * -2000 + Vector3.down * 2000;
    }
}