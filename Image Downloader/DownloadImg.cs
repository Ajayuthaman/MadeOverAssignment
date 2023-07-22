using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using System;

public class DownloadImg : MonoBehaviour
{
    public static DownloadImg instance;
    static int downloadingCount;

    public delegate void ImageDownloaded();
    public static event ImageDownloaded eventImageDownloaded;


    private void Awake()
    {
        if(instance==null)
        {
            instance=this;
            /*DontDestroyOnLoad(gameObject);*/
        }
       /* else
        {
            Destroy(gameObject);
        }*/
    }
    
    void Start()
    {
        downloadingCount = 0;
    }
    public static IEnumerator DownloadImgFromUrl(string url,GameObject obj)
    {
        if(downloadingCount<=5)
        {
            downloadingCount += 1;

            /*UnityWebRequest request=UnityWebRequest.Get(url);*/
            UnityWebRequest request=UnityWebRequestTexture.GetTexture(url);
            /*Debug.Log(request.url);*/
            request.timeout= 10;
            yield return request.SendWebRequest();
            if(request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
                downloadingCount -= 1;
                obj.GetComponent<Image>().sprite = obj.GetComponent<WebImage>().defaultSprite;
            }
            else
            {
                downloadingCount-= 1;
               /* Debug.Log("Image donwloaded");*/
                /*Texture2D texture = DownloadHandlerTexture.GetContent(request);*/
                Texture2D myTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                /*Debug.Log("Hi"+myTexture);*/
                obj.GetComponent<Image>().sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), Vector2.zero);
                byte[] bytes = myTexture.EncodeToJPG();
                File.WriteAllBytes(Application.dataPath + obj.name + ".jpg", bytes);
                obj.GetComponent<WebImage>().cached = true;
                
                eventImageDownloaded?.Invoke();
                PlayerPrefs.SetString(obj.name, DateTime.Now.ToString());
            }
        }
        else
        {
            yield return null;
        }
    }

    
}
