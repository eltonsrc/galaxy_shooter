using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject[] _powerUpList;

    [SerializeField]
    private GameObject _playerPrefab;

    public void StartCoroutines()
    {
        StartCoroutine(GenerateEnemy());
        StartCoroutine(GeneratePowerUps());
    }

    public void StopCoroutines()
    {
        StopAllCoroutines();
    }

    public void SpawnPlayer()
    {
        Instantiate(_playerPrefab, new Vector3(0, 0), Quaternion.identity);
    }

    private IEnumerator GenerateEnemy()
    {
        while (true)
        {
            Instantiate(_enemyPrefab, RandomPosition(), Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }

    private IEnumerator GeneratePowerUps()
    {
        while (true)
        {
            int random = Random.Range(0, 3);
            Instantiate(_powerUpList[random], RandomPosition(), Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }

    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-8f, 8f), 6.26f);
    }
}
