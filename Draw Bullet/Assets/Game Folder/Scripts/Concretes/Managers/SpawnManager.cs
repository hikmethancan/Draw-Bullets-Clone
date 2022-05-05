using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnManager : MonoBehaviour
{
    public int numOfTotalObjects = 0;
    [FormerlySerializedAs("InstantObjects")] public List<GameObject> instantObjects = new List<GameObject>() ;
    [FormerlySerializedAs("SpawnObjectPrefab")] public GameObject spawnObjectPrefab;
    [FormerlySerializedAs("InstantPoint")] public Transform instantPoint;

    private int _currentNumOfObject;

    private void Start()
    {
        Debug.Log("Merhaba..");
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
                Debug.Log("Spawn 1 ba�lad�");
                instantObjects[_currentNumOfObject].SetActive(true);
                instantObjects[_currentNumOfObject].transform.position = instantPoint.transform.position;
                _currentNumOfObject++;
                Debug.Log("Spawn 1 tane ettik");
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
