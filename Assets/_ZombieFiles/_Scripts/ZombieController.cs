using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum STATES
{
    Moving,
    Attack,
    Hurt
}

public class ZombieController : MonoBehaviour
{
    public STATES _state;

    [SerializeField] ZombieHealthScript zHealth;

    [SerializeField] Animator _anim;



    [Header("Zombie Position Variables")]

    public Transform stopPoint;

    public float moveSpeed = 2f;
    public float attackDistance = 0.25f;



    [Header("Zombie Attack Variables")]

    public float attackTimer;



    [Header("Bools")]

    public bool inAction;
    public bool isHurt;
    public bool isMoving;
    public bool isDead;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        zHealth = GetComponent<ZombieHealthScript>();
    }

    private void Update()
    {
        if (!isDead && !isHurt)
        {
            float _distance = Vector3.Distance(transform.position, stopPoint.position);

            if (_distance > attackDistance)
            {
                _state = STATES.Moving;

                isMoving = true;

                inAction = false;

                transform.position = Vector3.MoveTowards(transform.position, stopPoint.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                isMoving = false;

                if (!isHurt)
                {
                    _state = STATES.Attack;
                }
                else
                {
                    _state = STATES.Hurt;
                }

            }

            if(_distance <= attackDistance && !inAction)
            {
                inAction = true;

                StartCoroutine(AttackPlayer());

            }

        }

            //===== ANIMATIONS =====

            _anim.SetBool("Active", isMoving);

        

    }

    IEnumerator AttackPlayer()
    {
        while (!isDead)
        {
            if (!isHurt)
            {
                _anim.SetTrigger("Attack");
                AttackTrigger();
            }

            yield return new WaitForSeconds(attackTimer);

        }
    }

    public void AttackTrigger()
    {
        Debug.Log("Got Hurt");
        GameManager.Instance.HurtPlayer();
    }
}
