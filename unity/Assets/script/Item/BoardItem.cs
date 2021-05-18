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
        uiItem.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }

    public void Exit(){
        uiItem = GameObject.Find("BoardItem");
        uiItem.GetComponent<RectTransform>().anchoredPosition = Vector3.left * -2000;
    }
}