using System;
using Game_Folder.Scripts.Abstracts.Utilities;


namespace Game_Folder.Scripts.Concretes.Managers
{
    public class GameManager : SingletonObjects<GameManager>
    {
        public int bulletCount = 0;
        public bool isGameStarted;
        public bool isGameFinished;
        public GunType gunType;

        public static event Action OnGunChanged;

        
        private void Awake()
        {
            SingletonObject(this);
            gunType = GunType.Revolver;
        }

        public void GunChanged()
        {
            OnGunChanged?.Invoke();
        }

        public void GameOver()
        {
            if(isGameFinished) return;
            isGameStarted = false;
            UIManager.Instance.gameOverPanel.SetActive(true);
        }

        public void GameFinished()
        {
            isGameFinished = true;
            isGameStarted = false;
            UIManager.Instance.winPanel.SetActive(true);
        }


    }
}