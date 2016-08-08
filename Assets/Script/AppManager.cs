using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using System.IO;

public class AppManager : SingletonMonoBehaviour<AppManager>
{
    //[SerializeField]
    //private AllcoateManager allocateManager = null;

    [SerializeField]
    private AllocateObjectQueue allocateQueue = null;

    [SerializeField]
    private GameObject spawnArchivePrefab = null;

    private List<string> imageDirectories = null;

    private void Start()
    {
        imageDirectories = FileUtilitiy.GetImageDirectories(Config.ImagePath);

        this.ObserveEveryValueChanged(_ => Input.GetKeyDown(KeyCode.Q))
            .Where(isDown => isDown)
            .Subscribe(_ => {
                AddArchive();
            });

        // 左右キーで振り分けるテストコード
        this.ObserveEveryValueChanged(_ => Input.GetKeyDown(KeyCode.RightArrow))
            .Where(isDown => isDown)
            .Subscribe(_ => allocateQueue.RemoveToRight());

        this.ObserveEveryValueChanged(_ => Input.GetKeyDown(KeyCode.LeftArrow))
            .Where(isDown => isDown)
            .Subscribe(_ => allocateQueue.RemoveToLeft());

        allocateQueue.OnRemovedToRight += rect => {
            Destroy(rect.gameObject);
            AddArchive();
        };
        allocateQueue.OnRemovedToLeft += rect => {
            Destroy(rect.gameObject);
            AddArchive();
        };

        Observable
            .ReturnUnit()
            .DelayFrame(1)
            .Subscribe(StartCoroutine_Auto => {
                for(int i = 0; i < Config.LoadFirstArchiveCount; i++)
                {
                    AddArchive();
                }
            });
    }

    private void AddArchive()
    {
        if(allocateQueue != null && spawnArchivePrefab != null)
        {
            if(imageDirectories.Count > 0)
            {
                var archiveObj = Instantiate(spawnArchivePrefab);
                Observable
                    .ReturnUnit()
                    .DelayFrame(1)
                    .Subscribe(_ => {
                        var archive = archiveObj.GetComponent<Archive>();
                        archive.LoadImageDirectory(imageDirectories.First());
                        allocateQueue.EnqueueObj(archiveObj.GetComponent<RectTransform>());
                        imageDirectories.RemoveAt(0);
                    });
            }
        }
    }
}
