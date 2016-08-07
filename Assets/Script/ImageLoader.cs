using UnityEngine;
using System.Collections;
using System;
using System.IO;

public static class ImageLoader
{
    public static Texture2D LoadTexture2D(string imageFullPath)
    {
        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(LoadBin(imageFullPath));
        return tex;
    }

    public static Sprite LoadSprite(string imageFullPath, Rect rect, Vector2 pivot)
    {
        Sprite sprite = new Sprite();
        var texture = LoadTexture2D(imageFullPath);
        Sprite.Create(texture, rect, pivot);
        return sprite;
    }

    private static byte[] LoadBin(string path)
    {
        using(FileStream fs = new FileStream(path, FileMode.Open))
        using(BinaryReader br = new BinaryReader(fs))
        {
            byte[] buf = br.ReadBytes((int)br.BaseStream.Length);
            return buf;
        }
    }
}
