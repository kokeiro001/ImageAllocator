using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using System.IO;

public class AppManager : SingletonMonoBehaviour<AppManager>
{
    [SerializeField]
    private AllcoateManager allocateManager = null;

    [SerializeField]
    private GameObject spawnImagePrefab = null;

    [SerializeField]
    private GameObject spawnArchivePrefab = null;

    private List<string> imageDirectories = null;

    private void Start()
    {
        imageDirectories = FileUtilitiy.GetImageDirectories(Config.ImagePath);

        // spawn adon
        //this.ObserveEveryValueChanged(_ => Input.GetKeyDown(KeyCode.Q))
        //    .Where(isDown => isDown)
        //    .Subscribe(_ => {
        //        if(allocateManager != null && spawnImagePrefab != null)
        //        {
        //            var obj = Instantiate(spawnImagePrefab);
        //            string path = @"C:\workspace\32310635_2180519179_62large.jpg";
        //            Observable
        //                .ReturnUnit()
        //                .DelayFrame(1)
        //                .Subscribe(a => {
        //                    obj.GetComponent<UnityEngine.UI.Image>().sprite = ImageLoader.LoadSprite(path);
        //                });
        //            allocateManager.AddObj(obj.GetComponent<RectTransform>());
        //        }
        //    });
        this.ObserveEveryValueChanged(_ => Input.GetKeyDown(KeyCode.Q))
            .Where(isDown => isDown)
            .Subscribe(_ => {
                if(allocateManager != null && spawnArchivePrefab != null)
                {
                    if(imageDirectories.Count > 0)
                    {
                        var archiveObj = Instantiate(spawnArchivePrefab);
                        Observable
                            .ReturnUnit()
                            .DelayFrame(1)
                            .Subscribe(a => {
                                var archive = archiveObj.GetComponent<Archive>();
                                archive.LoadImageDirectory(imageDirectories.First());
                            });
                        allocateManager.AddObj(archiveObj.GetComponent<RectTransform>());

                        imageDirectories.RemoveAt(0);
                    }
                }
            });
    }

}
