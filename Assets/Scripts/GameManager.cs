using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int _coins;

    [SerializeField]
    private Text _coinText;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R) && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddCoins(int coins)
    {
        _coins++;
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        _coinText.text = _coins.ToString();
    }
}
