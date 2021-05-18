using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PhotoItem : MonoBehaviour, IPointerClickHandler
{
    GameObject uiItem;

    void Start() {
        uiItem = GameObject.Find("PhotoItem");
    }

    public void OnPointerClick(PointerEventData data){
        uiItem.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }

    public void Exit(){
        uiItem = GameObject.Find("PhotoItem");
        uiItem.GetComponent<RectTransform>().anchoredPosition = Vector3.left * -2000;
    }
}