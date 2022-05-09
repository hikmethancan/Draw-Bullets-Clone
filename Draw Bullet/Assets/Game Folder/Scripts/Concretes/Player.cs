
using Game_Folder.Scripts.Abstracts.Utilities;
using Game_Folder.Scripts.Concretes.Controllers;


public class Player : SingletonObjects<Player>
{
    public BulletSpawnController BulletSpawnController { get; private set; }
    public DrawingController DrawingController { get; private set; }
    
    public GunController GunController { get; private set; }
    private void Awake()
    {
        SingletonObject(this);
        GunController = GetComponentInChildren<GunController>();
        DrawingController = GetComponentInChildren<DrawingController>();
        BulletSpawnController = GetComponentInChildren<BulletSpawnController>();
    }

}
