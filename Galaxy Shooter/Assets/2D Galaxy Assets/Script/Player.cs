using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 7.5f;
    [SerializeField] private int _life = 3;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _shieldPrefab;
    [SerializeField] private GameObject[] _hurt;

    private Transform _galaxy;
    private UIManager _uiManager;
    private ShootManager _shootManager;
    private float _xLimit;
    private float _yLimit;
    private float _speedBooster = 1;
    private GameObject _shieldGameObject;
    private float _shield;
    private MainMenu _mainMenu;
    private Animator _movementAnimator;

    private void Start ()
    {
        _movementAnimator = GetComponent<Animator>();
        _galaxy = GameObject.Find("Galaxy").transform;
        _uiManager = GameObject.FindGameObjectWithTag("UI Manager").GetComponent<UIManager>();
        _uiManager.UpdateLives(_life);
        _shootManager = GetComponent<ShootManager>();
        _xLimit = _galaxy.localScale.x / 2.0f;
        _yLimit = _galaxy.localScale.y / 2.0f;
        _mainMenu = Resources.FindObjectsOfTypeAll<MainMenu>()[0];
    }

    private void Update()
    {
        Movement();
        _shootManager.Shoot();
    }

    public void Damage()
    {
        if (_shield > 0)
        {
            _shield--;

            if (_shield < 1)
            {
                Destroy(_shieldGameObject);
            }
        }
        else
        {
            _life--;
            _uiManager.UpdateLives(_life);

            if (_life == 2)
            {
                _hurt[0].SetActive(true);
            } else if (_life == 1)
            {
                _hurt[1].SetActive(true);
            }

            if (_life < 1)
            {
                Instantiate(_explosion, transform.position, Quaternion.identity);
                _mainMenu.Show();
                Destroy(gameObject);
            }
        }
    }

    public void AddPowerUp(PowerUpType powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUpType.Simple:
            case PowerUpType.Triple:
                _shootManager.SetShootType(powerUpType);
                break;
            case PowerUpType.Speed:
                StartCoroutine(IncreaseSpeed());
                break;
            case PowerUpType.Shield:
                _shieldGameObject = Instantiate(_shieldPrefab, transform);
                _shield = 1;
                break;
        }
    }

    private IEnumerator IncreaseSpeed()
    {
        _speedBooster = 1.5f;
        yield return new WaitForSeconds(5);
        _speedBooster = 1;
    }

    private void Movement()
    {
        float horizontal;
        float vertical;

        #if UNITY_ANDROID && !UNITY_EDITOR
            Vector2 touchPosition = OVRInput.Get(OVRInput.Axis2D.Any);
            horizontal = touchPosition.x;
            vertical = touchPosition.y;
        #else
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        #endif

        if (horizontal != 0)
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * _speedBooster * horizontal);

            if (transform.position.x < -_xLimit)
            {
                transform.position = new Vector3(_xLimit, transform.position.y, 0);
            }

            if (transform.position.x > _xLimit)
            {
                transform.position = new Vector3(-_xLimit, transform.position.y, 0);
            }
        }

        if (vertical != 0)
        {
            transform.Translate(Vector3.up * Time.deltaTime * _speed * _speedBooster * vertical);

            if (transform.position.y < -_yLimit)
            {
                transform.position = new Vector3(transform.position.x, _yLimit, 0);
            }

            if (transform.position.y > _yLimit)
            {
                transform.position = new Vector3(transform.position.x, -_yLimit, 0);
            }
        }

        if (horizontal == 0)
        {
            if (_movementAnimator.GetBool("Right"))
            {
                _movementAnimator.SetBool("Right", false);
            }

            if (_movementAnimator.GetBool("Left"))
            {
                _movementAnimator.SetBool("Left", false);
            }
        }

        if (horizontal > 0 && !_movementAnimator.GetBool("Right"))
        {
            _movementAnimator.SetBool("Right", true);
        } else if (horizontal < 0 && !_movementAnimator.GetBool("Left"))
        {
            _movementAnimator.SetBool("Left", true);
        }
    }
}
