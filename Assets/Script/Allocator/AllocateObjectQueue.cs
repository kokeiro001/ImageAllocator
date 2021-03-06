﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UniRx;
using UniRx.Triggers;



public class AllocateObjectQueue : MonoBehaviour
{
    private List<RectTransform> objectList = new List<RectTransform>();

    private int margine = 10;
    private int startHaba = 50;

    public event Action<RectTransform> OnRemovedToRight = _ => { };
    public event Action<RectTransform> OnRemovedToLeft = _ => { };

    public void EnqueueObj(RectTransform item)
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

            item.transform.position = new Vector3(x, y - startHaba);
        }
        else
        {
            var obj = objectList[objectList.Count - 2];
            var r = obj.GetComponent<RectTransform>();
            // 最後のやつよりも下に配置
            float x = obj.transform.position.x;
            float y = obj.transform.position.y - r.sizeDelta.y - startHaba;

            item.transform.position = new Vector3(x, y - startHaba);
        }
        Reposition();
    }

    public void Reposition()
    {
        // すべての大きさ、現在座標を取得して、いい具合に動かす

        float x = transform.position.x;
        float y = transform.position.y;
        //y -= rectTransform.sizeDelta.y;

        Vector3 risouPos = new Vector3(x, y);

        foreach(var obj in objectList)
        {
            iTween.MoveTo(obj.gameObject, iTween.Hash("position", risouPos));
            risouPos += new Vector3(0,
                                   -obj.GetComponent<RectTransform>().sizeDelta.y - margine,
                                   0);
        }
    }

    public void RemoveToRight()
    {
        if(objectList.Count == 0)
            return;

        var top = objectList[0];
        objectList.Remove(top);

        var w = Screen.width;
        Hashtable parameters = new Hashtable();
        parameters.Add("x", w);
        parameters.Add("oncomplete", "CompleteRightHandler");
        parameters.Add("oncompletetarget", gameObject);
        parameters.Add("oncompleteparams", top);
        iTween.MoveTo(top.gameObject, parameters);
        Reposition();
    }

    private void CompleteRightHandler(RectTransform rect)
    {
        OnRemovedToRight.Invoke(rect);
    }

    public void RemoveToLeft()
    {
        if(objectList.Count == 0)
            return;

        var top = objectList[0];
        objectList.Remove(top);

        var w = Screen.width;
        Hashtable parameters = new Hashtable();
        parameters.Add("x", -w);
        parameters.Add("oncomplete", "CompleteLeftHandler");
        parameters.Add("oncompletetarget", gameObject);
        parameters.Add("oncompleteparams", top);
        iTween.MoveTo(top.gameObject, parameters);
        Reposition();
    }
    private void CompleteLeftHandler(RectTransform rect)
    {
        OnRemovedToLeft.Invoke(rect);
    }
}
