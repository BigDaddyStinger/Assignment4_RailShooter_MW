using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PathFollowScript : MonoBehaviour
{
    public Transform[] _wayPoints;
    public float _moveSpeed = 1.5f;
    [SerializeField] private int currentWaypointIndex = 0;
    public int numberOfEnemies;

    public string bossScene;

    private void Start()
    {
        Invoke("MoveToPoint", 1f);
    }

    public void MoveToPoint()
    {
        if(currentWaypointIndex < _wayPoints.Length)
        {
            transform.DOLookAt(_wayPoints[currentWaypointIndex].position, 0.75f, AxisConstraint.Y);

            transform.DOMove(_wayPoints[currentWaypointIndex].position, _moveSpeed)
                .OnComplete(()=>WaypointCheck());
        }
        else
        {
            SceneManager.LoadSceneAsync(bossScene);
        }
    }

    void WaypointCheck()
    {
        EnemyEncounterScript _encounter = _wayPoints[currentWaypointIndex].GetComponent<EnemyEncounterScript>();

        currentWaypointIndex++;

        if (_encounter != null)
        {
            numberOfEnemies = _encounter.enemyCount;

            transform.DOLookAt(_encounter.lookPoint.position, 0.5f, AxisConstraint.Y).SetEase(Ease.OutBounce);
                
            _encounter.InitiateEncounter();
        }
        else
        {
            MoveToPoint();
        }
    }

    public void SubtractEnemy()
    {
        numberOfEnemies--;
        if(numberOfEnemies <= 0)
        {
            MoveToPoint();
        }
    }
}
