using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(LayoutElement))]
public class ImageSizeController : MonoBehaviour
{
    private Image image;
    private RectTransform parent;
    private RectTransform rectTransform;
    private LayoutElement layout;

    private void Awake()
    {
        layout = GetComponent<LayoutElement>();
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {

    }

    void Update()
    {
        if(parent == null)
        {
            parent = transform.parent.GetComponent<RectTransform>();
        }
        UpdateSize();
    }

    private void UpdateSize()
    {
        if(parent != null && rectTransform != null)
        {
            var imgW = (float)image.sprite.texture.width;
            var imgH = (float)image.sprite.texture.height;
            var aspectRate = imgW / imgH;

            var newW = (parent.sizeDelta.y * imgW) / imgH;
            var newH = parent.sizeDelta.y;
            //rectTransform.sizeDelta = new Vector2(newW, parent.sizeDelta.y);
            layout.minWidth = newW;
            layout.preferredWidth = newW;
            layout.flexibleWidth = newW;

            layout.minHeight = newH;
            layout.preferredHeight = newH;
            layout.flexibleHeight = newH;
        }
    }
}
