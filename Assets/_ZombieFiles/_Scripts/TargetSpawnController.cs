using System.Collections;
using UnityEngine;

public class TargetSpawnController : MonoBehaviour
{
    public int numberOfTargets = 5;

    public GameObject _target;

    public void SpawnTargets()
    {
        StartCoroutine(Targets());
    }

    IEnumerator Targets()
    {
        for (int i = 0; i < numberOfTargets; i++)
        {
            float randomX = Random.Range(-0.25f, 0.25f);
            float randomY = Random.Range(-0.25f, 0.25f);

            Instantiate(_target, transform.position + new Vector3(randomX, randomY, 0), Quaternion.identity);

            yield return new WaitForSeconds(0.25f);
        }
    }
}
