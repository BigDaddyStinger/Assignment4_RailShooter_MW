using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public float killTime = 4f;

    public Transform _cam;

    private void Start()
    {
        _cam = Camera.main.transform;
        Destroy(gameObject, killTime);
    }

    private void Update()
    {
        transform.LookAt(_cam);
    }
}
