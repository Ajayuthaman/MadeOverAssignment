using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class WebImage : MonoBehaviour
{
    public string url;

    public Sprite defaultSprite;
    public bool cached;
    TimeSpan timeCover=new TimeSpan(168,0,0);

    private void OnEnable()
    {
        DownloadImg.eventImageDownloaded += GetImage;
    }
    private void OnDisable()
    {
        DownloadImg.eventImageDownloaded-= GetImage;
    }
    private void Start()
    {
        GetImage();

    }
    public void GetImage()
    {
        if(File.Exists(Application.dataPath + gameObject.name+".jpg"))
        {

            cached = true;
            byte[] byteArray = File.ReadAllBytes(Application.dataPath + gameObject.name + ".jpg");
            Texture2D texture = new Texture2D(8, 8);
            texture.LoadImage(byteArray);
            gameObject.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
        else
        {
            StartCoroutine(DownloadImg.DownloadImgFromUrl(url, gameObject));
        }
    }
    private void Update()
    {
        if(cached)
        {
            CalculateTime();
        }
    }
    void CalculateTime()
    {

        DateTime currentTime=DateTime.Now;
        DateTime ImageDownLoadedTime = DateTime.Parse(PlayerPrefs.GetString(gameObject.name));
        TimeSpan coverTime= currentTime.Subtract(ImageDownLoadedTime);

        if(coverTime>=timeCover)
        {
            if(File.Exists(Application.dataPath + gameObject.name+".jpg"))
            {
                File.Delete(Application.dataPath + gameObject.name + ".jpg");
            }
        }
    }
}
