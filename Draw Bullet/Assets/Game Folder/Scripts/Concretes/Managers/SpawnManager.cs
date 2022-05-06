using System.Collections;
using System.Collections.Generic;
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
                GameObject newObject =  Instantiate(spawnObjectPrefab, transform);
                instantObjects.Add(newObject);
                instantObjects[i].SetActive(false);
            }
        }

        IEnumerator SpawnObject()
        {
            yield return new WaitForSeconds(3f);

            while (instantObjects != null)
            {
                if (_currentNumOfObject < instantObjects.Count)
                {
                    instantObjects[_currentNumOfObject].SetActive(true);
                    instantObjects[_currentNumOfObject].transform.position = instantPoint.transform.position;
                    _currentNumOfObject++;
                }
                else
                {
                    _currentNumOfObject = 0;
                    instantObjects[_currentNumOfObject].SetActive(true);
                    instantObjects[_currentNumOfObject].transform.position = instantPoint.transform.position;
                }
                yield return new WaitForSeconds(5f);
            }
        }
    }
}
