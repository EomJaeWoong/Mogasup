using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Windows.Forms;
using Ookii.Dialogs;
using UnityEngine.Networking;

public class PhotoUpload : MonoBehaviour
{
    VistaOpenFileDialog OpenDialog;
    Stream openStream = null;

    private void Start()
    {
        OpenDialog = new VistaOpenFileDialog();
        OpenDialog.Filter = "jpg files (*.jpg) |*.jpg|png files (*.png) |*.jpg|All files  (*.*)|*.*";
        OpenDialog.FilterIndex = 3;
        OpenDialog.Title = "Image Dialog";
    }
    
    public string FileOpen()
    {
        if (OpenDialog.ShowDialog() == DialogResult.OK)
        {
            if ((openStream = OpenDialog.OpenFile()) != null)
            {
                return OpenDialog.FileName;
            }
        }
        return null;
    }

    public void Upload()
    {
        string fileName =  FileOpen();

        if(!string.IsNullOrEmpty(fileName)){
            Debug.Log(fileName);
            StartCoroutine(Send(fileName));
        }
    }

    IEnumerator Send(string filename)
    {
        WWWForm form = new WWWForm();
        form.AddField("family_id", 1);
        WWW localfile = new WWW(filename);
        string url = "http://k4a102.p.ssafy.io:8080/picture";
        form.AddBinaryData("file", localfile.bytes);
        // Upload via post request
        var www = UnityWebRequest.Post(url, form);
        // change the method name
        www.method = "POST";
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Finished Uploading Screenshot");
        }

    }
}