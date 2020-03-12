using UnityEngine;

public enum PowerUpType
{
    Simple, Triple, Speed, Shield
}

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpType _powerType = PowerUpType.Simple;
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private AudioClip _audioClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player == null)
        {
            return;
        }

        player.AddPowerUp(_powerType);
        AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position);
        Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y < -6.2)
        {
            Destroy(gameObject);
        }
    }
}
