using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class FileUtilitiy
{

    public static List<string> GetImageDirectories(string rootPath)
    {
        List<string> results = new List<string>();
        GetImageDirectoriesSub(rootPath, results);
        return results;
    }
    public static void GetImageDirectoriesSub(string parentPath, List<string> results)
    {
        foreach(var childPath in Directory.GetDirectories(parentPath))
        {
            if(Directory.Exists(childPath))
            {
                if(IsImageDirectory(childPath))
                {
                    results.Add(childPath);
                }
                GetImageDirectoriesSub(childPath, results);
            }
        }
    }

    private static bool IsImageDirectory(string dirPath)
    {
        var dirs = Directory.GetDirectories(dirPath);
        if(dirs.Length > 0)
        {
            return false;
        }

        var files = Directory.GetFiles(dirPath);
        if(files.Length == 0)
        {
            return false;
        }

        return files.All(file => 
            File.Exists(file) && 
            (file.Contains(".jpg") || file.Contains(".jpeg") || file.Contains(".png"))
        );
    }
}