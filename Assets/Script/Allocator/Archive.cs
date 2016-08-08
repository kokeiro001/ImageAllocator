using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;

[RequireComponent(typeof(RectTransform))]
public class Archive : MonoBehaviour
{
    [SerializeField]
    private GameObject imageObjectPrefab = null;

    public string ImageDirectory { get; private set; }

    public void LoadImageDirectory(string dir)
    {
        ImageDirectory = dir;
        StartCoroutine(LoadCoroutine());
    }

    private IEnumerator LoadCoroutine()
    {
        var files = Directory.GetFiles(ImageDirectory);
        foreach(var imageFilePath in files.Take(Config.LoadImageCountByArchive))
        {
            var obj = Instantiate(imageObjectPrefab);
            var img = obj.GetComponent<Image>();
            img.sprite = ImageLoader.LoadSprite(imageFilePath);
            obj.transform.SetParent(transform);
            yield return null;
        }
    }
}
