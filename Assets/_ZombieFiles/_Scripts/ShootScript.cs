using UnityEngine;

public class ShootScript : MonoBehaviour
{
    Zombie_Input zombieInput;

    public float fireRange = 500f;


    private void Awake()
    {
        zombieInput = new Zombie_Input();
    }

    private void OnEnable()
    {
        zombieInput.Enable();
    }

    private void OnDisable()
    {
        zombieInput.Disable();
    }

    void Update()
    {
        if (zombieInput.Player.Shoot.triggered)
        {
            FireWeapon();
        }

        if (zombieInput.Player.Quit.triggered)
        {
            Application.Quit();
        }
    }

    void FireWeapon()
    {
        SoundManager.Instance.PlaySound2D("pistolShotClip");

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, fireRange))
        {
            //Debug.Log("You Just Shot " + hit.transform.name);

            //Destroy(hit.transform.gameObject);

            if (hit.transform.gameObject.tag == "HeadShot")
            {
                bool _head = true;
                GameObject _zombie = hit.transform.gameObject;
                ZombieHealthScript _health = _zombie.GetComponentInParent<ZombieHealthScript>();
                _health.TakeDamage(_head);
            }

            if (hit.transform.gameObject.tag == "BodyShot")
            {
                bool _head = false;
                GameObject _zombie = hit.transform.gameObject;
                ZombieHealthScript _health = _zombie.GetComponentInParent<ZombieHealthScript>();
                _health.TakeDamage(_head);
            }
            
            if (hit.transform.gameObject.tag == "Shotgun")
            {
                GameManager.Instance.shellCount = 10;
                Destroy(hit.transform.gameObject);
            }

            if (hit.transform.gameObject.tag == "Health")
            {
                GameManager.Instance.playerHealth++;
                Destroy(hit.transform.gameObject);
            }

            if(hit.transform.gameObject.tag == "Target")
            {
                Debug.Log("Target Hit!");
                BossController ctrl = GameObject.FindFirstObjectByType<BossController>();
                ctrl.StunBoss();
                Destroy(hit.transform.gameObject);
            }

            if (hit.transform.gameObject.tag == "WeakPoint")
            {
                Debug.Log("Boss Weak Point Hit!");
                GameManager.Instance.bossHealth -= 2;

                if(GameManager.Instance.bossHealth <= 0f)
                {
                    BossController ctrl = GameObject.FindFirstObjectByType<BossController>();
                    if (!ctrl.isDead)
                    {
                        ctrl.BossDeath();

                    }
                }
            }
        }
    }
}
