using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoUpload : MonoBehaviour
{
    public void OnSelectImageButtonClicked()
    {
        // #if UNITY_EDITOR

        // #else
        //     NativeGallery.GetImageFromGallery(callbackForGallery);
        // #endif
    }

    private void callbackForGallery(string path)
    {
    //       UpdateImage(path);
    }
}