using UnityEngine;

public class BossController : MonoBehaviour
{
    Animator _anim;
    public float moveSpeed = 0.5f;

    public bool walkingForward;
    public bool isAttacking;
    public bool inAction;
    public bool isDead;

    // Move Points

    public Transform PointA;
    public Transform PointB;

    // Get Targets
    public int numberOfTargets;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        walkingForward = true;
    }

    private void Update()
    {
        if(!isDead && !inAction)
        {
            if(walkingForward)
            {
                transform.position = Vector3.MoveTowards(transform.position, PointB.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, PointA.position, moveSpeed * Time.deltaTime);
            }
        }

        float _distance = Vector3.Distance(transform.position, PointB.position);

        if(_distance < 0.5f && walkingForward && !isAttacking)
        {
            TargetSpawnController _spawnTargets = GameObject.FindFirstObjectByType<TargetSpawnController>();
            _spawnTargets.SpawnTargets();
            isAttacking = true;
            numberOfTargets = _spawnTargets.numberOfTargets;
        }

        _anim.SetBool("Forward", walkingForward);
        _anim.SetBool("Attacking", isAttacking);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Point")
        {
            if(!walkingForward)
            {
                walkingForward = true;
            }
            else 
            {
                inAction = true;
                _anim.SetTrigger("Attack");
            }
        }
    }

    public void StunBoss()
    {
        _anim.SetTrigger("Stun");

        numberOfTargets--;

        if(numberOfTargets <= 0)
        {
            ResetBoss();
        }
    }

    public void HurtPlayer()
    {
        GameManager.Instance.HurtPlayer();
    }

    public void ResetBoss()
    {
        isAttacking = false;
        inAction = false;
        walkingForward = false;
    }

    public void BossDeath()
    {
        Debug.Log("Boss Dead");
        isDead = true;
        _anim.SetTrigger("Death");
        Invoke("GameManager.Instance.LevelComplete", 5f);
    }
}
