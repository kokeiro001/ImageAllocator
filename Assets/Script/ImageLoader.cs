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

    public static Sprite LoadSprite(string imageFullPath)
    {
        var texture = LoadTexture2D(imageFullPath);
        var rect = new Rect(0, 0, texture.width, texture.height);
        return Sprite.Create(texture, rect, Vector2.zero);
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

