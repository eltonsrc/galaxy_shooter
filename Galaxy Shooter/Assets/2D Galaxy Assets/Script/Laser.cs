using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 10;

    private void Update ()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 6)
        {
            Destroy(gameObject);
        }
    }
}
