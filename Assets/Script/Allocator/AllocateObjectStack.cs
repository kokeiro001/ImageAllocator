using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class AllocateObjectStack : MonoBehaviour
{
    private List<AllocateObject> objectList = new List<AllocateObject>();

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void EnqueueObj(AllocateObject item)
    {
        if(item == null)
        {
            throw new ArgumentNullException();
        }

        objectList.Add(item);
        item.transform.SetParent(transform);

        if(objectList.Count == 1)
        {
            // 最初のやつは最上部に配置する
            float x = transform.position.x;
            float y = transform.position.y;

            item.transform.position = new Vector3(x, y);
        }
        else
        {
            var obj = objectList.Last();
            // 最後のやつよりも下に配置
            float x = obj.transform.position.x;
            float y = obj.transform.position.y;
            y += rectTransform.sizeDelta.y;

            item.transform.position = new Vector3(x, y);

            // 最後のやつに向けて移動開始する
        }

    }

    public void DequeueItem()
    {
    }
}
