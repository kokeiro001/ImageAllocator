using UnityEngine;
using System.Collections;
using System;

public class AllcoateManager : MonoBehaviour {

    [SerializeField]
    private AllocateObjectStack stack = null;

    public void AddObj(AllocateObject obj)
    {
        if(stack == null)
        {
            throw new InvalidOperationException();
        }
        stack.EnqueueObj(obj);
    }
}
