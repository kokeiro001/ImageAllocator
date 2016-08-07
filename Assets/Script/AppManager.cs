using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class AppManager : SingletonMonoBehaviour<AppManager>
{
    [SerializeField]
    private AllcoateManager allocateManager = null;

    [SerializeField]
    private GameObject spawnImagePrefab = null;

    private void Start()
    {
        this.ObserveEveryValueChanged(_ => Input.GetKeyDown(KeyCode.Q))
            .Where(isDown => isDown)
            .Subscribe(_ => {
                if(allocateManager != null && spawnImagePrefab != null)
                {
                    var obj = Instantiate(spawnImagePrefab).GetComponent<AllocateObject>();
                    allocateManager.AddObj(obj);
                }
            });
    }
}
