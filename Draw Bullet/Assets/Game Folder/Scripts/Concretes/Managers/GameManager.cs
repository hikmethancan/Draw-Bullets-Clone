
using Game_Folder.Scripts.Abstracts.Utilities;

namespace Game_Folder.Scripts.Concretes.Managers
{
    public class GameManager : SingletonObjects<GameManager>
    {
        public int bulletCount;
        private void Awake()
        {
            SingletonObject(this);
        }
        
        
    }
}
