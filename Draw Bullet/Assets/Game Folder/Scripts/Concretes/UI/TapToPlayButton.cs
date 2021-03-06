using System;
using System.Collections;
using DG.Tweening;
using Game_Folder.Scripts.Concretes.Managers;
using UnityEngine;

namespace Game_Folder.Scripts.Concretes.UI
{
    public class TapToPlayButton : MonoBehaviour
    {
        [SerializeField] private GameObject tapToPlayObject;

        public event Action OnGameStart;

        public bool IsGameStarted = false;

        private void Start()
        {
            StartCoroutine(TapToPlayMovement());
        }

        private void OnEnable()
        {
            OnGameStart += HandleGameStart;
        }

        private void OnDisable()
        {
            OnGameStart -= HandleGameStart;
        }

        private void HandleGameStart()
        {
            tapToPlayObject.SetActive(false);
            IsGameStarted = true;
        }

        public void StartGame()
        {
            OnGameStart?.Invoke();
            GameManager.Instance.isGameStarted = true;
        }

        private IEnumerator TapToPlayMovement()
        {
            while (!IsGameStarted)
            {
                tapToPlayObject.transform.DOPunchScale(Vector3.one * .1f, .8f, 3).SetEase(Ease.Linear);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}