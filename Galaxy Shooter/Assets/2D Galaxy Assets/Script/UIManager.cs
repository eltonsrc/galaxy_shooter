using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _livesCountImages;

    [SerializeField]
    private Image _livesCountDisplay;

    [SerializeField]
    private Text _scoreDisplay;

    private int _score;

    public void UpdateLives(int lives)
    {
        _livesCountDisplay.sprite = _livesCountImages[lives];
    }

    public void UpdateScore()
    {
        _score += 10;
        _scoreDisplay.text = "Score: " + _score;
    }
}
