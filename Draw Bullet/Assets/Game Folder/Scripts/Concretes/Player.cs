
using Game_Folder.Scripts.Abstracts.Utilities;
using Game_Folder.Scripts.Concretes.Controllers;


public class Player : SingletonObjects<Player>
{
    public BulletSpawn BulletSpawn { get; private set; }
    public DrawingController DrawingController { get; private set; }
    
    public Gun Gun { get; private set; }
    private void Awake()
    {
        SingletonObject(this);
        Gun = GetComponentInChildren<Gun>();
        DrawingController = GetComponentInChildren<DrawingController>();
        BulletSpawn = GetComponentInChildren<BulletSpawn>();
    }

}
