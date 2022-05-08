using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game_Folder.Scripts.Concretes.Controllers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game_Folder.Scripts.Concretes.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        public int numOfTotalObjects = 0;
        [FormerlySerializedAs("InstantObjects")] public List<GameObject> instantObjects = new List<GameObject>() ;
        [FormerlySerializedAs("SpawnObjectPrefab")] public GameObject spawnObjectPrefab;
        [FormerlySerializedAs("InstantPoint")] public Transform instantPoint;

        private int _currentNumOfObject;
        private DrawingController _drawingController;

        private void Awake()
        {
            _drawingController = FindObjectOfType<DrawingController>();

        }

        private void Start()
        {
            _currentNumOfObject = 0;
            SpawnStartGame();
            StartCoroutine(SpawnObject());
        }

        private void SpawnStartGame()
        {
            for (int i = 0; i < numOfTotalObjects; i++)
            {
                var newObject =  Instantiate(spawnObjectPrefab, instantPoint);
                instantObjects.Add(newObject);
                instantObjects[i].SetActive(false);
            }
        }

        IEnumerator SpawnObject()
        {
            yield return new WaitForSeconds(1f);

            while (instantObjects != null)
            {
                if (_currentNumOfObject < instantObjects.Count &&_currentNumOfObject <= Player.Instance.Gun.MaxBulletCount && _drawingController.isBulletSpawning)
                {
                    instantObjects[_currentNumOfObject].SetActive(true);
                    instantObjects[_currentNumOfObject].transform.position = instantPoint.transform.position;
                    _currentNumOfObject++;
                }
                else
                {
                    _drawingController.isBulletSpawning = false;
                    _currentNumOfObject = 0;
                    instantObjects[_currentNumOfObject].SetActive(false);
                    Debug.Log("İlk instant pozisyonuna girdik");
                    instantObjects[_currentNumOfObject].transform.position = instantPoint.transform.position;
                }
                yield return new WaitForSeconds(.2f);
            }
        }
    }
}
