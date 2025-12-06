using UnityEngine;

public class BossCameraScript : MonoBehaviour
{
    public Transform lookAtPoint;

    private void Update()
    {
        transform.LookAt(lookAtPoint);
    }
}
