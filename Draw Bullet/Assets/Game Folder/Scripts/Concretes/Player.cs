
using Game_Folder.Scripts.Abstracts.Utilities;
using UnityEngine;

public class Player : SingletonObjects<Player>
{
    private void Awake()
    {
        SingletonObject(this);
    }
    
    
    
}
