using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public bool IsStarted;
    [SerializeField]
    private SpawnManager _spawnManager;

    public void Show()
    {
        _spawnManager.StopCoroutines();
        IsStarted = false;
        gameObject.SetActive(true);
    }

    private void Update () {
        if (!IsStarted && (Input.GetKey(KeyCode.Space) || Click()))
        {
            _spawnManager.SpawnPlayer();
            _spawnManager.StartCoroutines();
            IsStarted = true;
            gameObject.SetActive(false);
        }
    }

    private bool Click()
    {
        return OVRInput.Get(OVRInput.Button.PrimaryTouchpad)
               || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);
    }
}
