
using Game_Folder.Scripts.Abstracts.Utilities;
using Game_Folder.Scripts.Concretes.UI;

public class UIManager : SingletonObjects<UIManager>
{
    public TapToPlayButton TapToPlayButton { get; private set; }
    public BulletFillBar BulletFillBar { get; private set; }
    private void Awake()
    {
        SingletonObject(this);
        BulletFillBar = GetComponent<BulletFillBar>();
        TapToPlayButton = GetComponentInChildren<TapToPlayButton>();
    }
}
