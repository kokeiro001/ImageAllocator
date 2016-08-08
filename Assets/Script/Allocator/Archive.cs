using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
[RequireComponent(typeof(RectTransform))]
public class Archive : MonoBehaviour
{
    [SerializeField]
    private GameObject imageObjectPrefab = null;

    private void Awake()
    {
    }

    public void LoadImageDirectory(string dir)
    {
        var files = Directory.GetFiles(dir);
        foreach(var imageFilePath in files.Take(5))
        {
            var obj = Instantiate(imageObjectPrefab);
            var img = obj.GetComponent<Image>();
            img.sprite = ImageLoader.LoadSprite(imageFilePath);
            obj.transform.SetParent(transform);
        }
    }

}
