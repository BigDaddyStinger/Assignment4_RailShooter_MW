using System.Collections;
using UnityEngine;

public class ZombieHealthScript: MonoBehaviour
{
    public PathFollowScript _follow;
    public int zombieHealth = 3;

    public float deleteTime = 3f;

    Animator _anim;

    private void Awake()
    {
        _follow = GameObject.FindFirstObjectByType<PathFollowScript>();
    }

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void TakeDamage(bool _head)
    {
        zombieHealth--;

        if (zombieHealth > 0)
        {
            if (_head)
            {
                _anim.SetTrigger("Head1");
            }
            else
            {
                _anim.SetTrigger("Body1");
            }
        }
        else
        {
            StartCoroutine(ZombieDead());
        }
    }

    IEnumerator ZombieDead()
    {
        ZombieController controller = GetComponent<ZombieController>();
        controller.isDead = true;
        _anim.SetTrigger("Dead1");
        yield return new WaitForSeconds(deleteTime);
        _follow.SubtractEnemy();
        Destroy(gameObject);
    }
}
