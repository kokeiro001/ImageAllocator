using UnityEngine;
using System.Collections;
using System;

public class AllcoateManager : MonoBehaviour {

    [SerializeField]
    private AllocateObjectQueue stack = null;

    public void AddObj(RectTransform obj)
    {
        if(stack == null)
        {
            throw new InvalidOperationException();
        }
        stack.EnqueueObj(obj);
    }
}
