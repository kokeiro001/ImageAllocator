using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UniRx;
using UniRx.Triggers;



public class AllocateObjectStack : MonoBehaviour
{
    private List<AllocateObject> objectList = new List<AllocateObject>();

    private RectTransform rectTransform;

    private int margine = 10;
    private int startHaba = 50;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // 左右キーで振り分けるテストコード
        this.ObserveEveryValueChanged(_ => Input.GetKeyDown(KeyCode.RightArrow))
            .Where(isDown => isDown)
            .Subscribe(_ => RemoveToRight());

        this.ObserveEveryValueChanged(_ => Input.GetKeyDown(KeyCode.LeftArrow))
            .Where(isDown => isDown)
            .Subscribe(_ => RemoveToLeft());
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

    public void DequeueItem()
    {
    }

    public void Reposition()
    {
        // すべての大きさ、現在座標を取得して、いい具合に動かす

        float x = transform.position.x;
        float y = transform.position.y;
        y = rectTransform.sizeDelta.y;

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
        iTween.MoveTo(top.gameObject, iTween.Hash("x", w));
        Reposition();
    }

    public void RemoveToLeft()
    {
        if(objectList.Count == 0)
            return;

        var top = objectList[0];
        objectList.Remove(top);

        iTween.MoveTo(top.gameObject, iTween.Hash("x", 0));
        Reposition();
    }
}
