using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _speed = 7.5f;
    [SerializeField] private float _life = 1;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private AudioClip _explosionClip;

    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = GameObject.FindGameObjectWithTag("UI Manager").GetComponent<UIManager>();
    }

    private void Update ()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.2)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 6.26f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.Damage();
            Instantiate(_explosion, transform.position, Quaternion.identity);
            PlayExplosionAudio();
            Destroy(gameObject);
            return;
        }

        Laser laser = other.GetComponentInParent<Laser>();

        if (laser != null)
        {
            Destroy(laser.gameObject);
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _uiManager.UpdateScore();
            PlayExplosionAudio();
            Destroy(gameObject);
        }
    }

    private void PlayExplosionAudio()
    {
        AudioSource.PlayClipAtPoint(_explosionClip, Camera.main.transform.position);
    }
}
