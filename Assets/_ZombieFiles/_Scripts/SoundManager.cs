using Unity.Hierarchy;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip magnumClip;
    [SerializeField] private AudioClip pistolClip;
    [SerializeField] private AudioClip blackPowderClip;
    [SerializeField] private AudioClip pistolShotClip;
    [SerializeField] private AudioClip revolverShotClip;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlaySound2D(string soundName)
    {
        switch (soundName)
        {
            case "magnumClip":
                audioSource.PlayOneShot(magnumClip);
                break;
            case "pistolClip":
                audioSource.PlayOneShot(pistolClip);
                break;
            case "blackPowderClip":
                audioSource.PlayOneShot(blackPowderClip);
                break;
            case "pistolShotClip":
                audioSource.PlayOneShot(pistolShotClip);
                break;
            case "revolverShotClip":
                audioSource.PlayOneShot(revolverShotClip);
                break;
        }
    }
}
