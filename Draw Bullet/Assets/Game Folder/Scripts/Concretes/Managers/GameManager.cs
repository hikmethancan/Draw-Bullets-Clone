
using System;
using Game_Folder.Scripts.Abstracts.Utilities;

namespace Game_Folder.Scripts.Concretes.Managers
{
    public class GameManager : SingletonObjects<GameManager>
    {
        public int bulletCount;
        public bool isGameStarted;
        private void Awake()
        {
            SingletonObject(this);
        }

       
        
        
    }
}
