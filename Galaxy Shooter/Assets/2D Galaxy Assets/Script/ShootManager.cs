using System.Collections;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    [SerializeField] private GameObject _simpleLaserPrefab;
    [SerializeField] private GameObject _tripleLaserPrefab;
    [SerializeField] private float _fireRate = 0.25f;

    private PowerUpType _currentShootType = PowerUpType.Simple;
    private float _canFire = 0;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetShootType(PowerUpType powerUpType)
    {
        _currentShootType = powerUpType;

        if (_currentShootType != PowerUpType.Simple)
        {
            StartCoroutine(ShootPowerUpCooldown());
        }
    }

    private IEnumerator ShootPowerUpCooldown()
    {
        yield return new WaitForSeconds(5);
        _currentShootType = PowerUpType.Simple;
    }

    public void Shoot()
    {
        bool trigger;

        #if UNITY_ANDROID && !UNITY_EDITOR
            trigger = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);
        #else
            trigger = Input.GetButton("Fire1");
        #endif

        if ((Input.GetKey(KeyCode.Space) || trigger) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            _audioSource.Play();
            Instantiate(GetPrefab(), transform.position, Quaternion.identity);
        }
    }

    private GameObject GetPrefab()
    {
        switch (_currentShootType)
        {
            case PowerUpType.Simple:
                return _simpleLaserPrefab;
            case PowerUpType.Triple:
                return _tripleLaserPrefab;
        }

        return null;
    }
}
