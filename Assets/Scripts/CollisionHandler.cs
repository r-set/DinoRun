using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private Text _distanceText;
    [SerializeField] private PlatformMovement _platformMovement;
    [SerializeField] private PlayerMovement _playerMovement;
    private float _startTime;
    private float _distanceTraveled = 0.0f;
    private bool _gameEnded = false;

    private void Start()
    {
        _startTime = Time.time;
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Object") && !_gameEnded)
        {
            _playerMovement.animator.SetBool("isFalling", true);

            float elapsedTime = Time.time - _startTime;
            _distanceTraveled = _platformMovement.platformSpeed * elapsedTime;
            _distanceText.text = "Distance: " + _distanceTraveled.ToString("F0") + "m";
            _gameEnded = true;
            StartCoroutine(EndGameAnimation());
        }
    }

    private IEnumerator EndGameAnimation()
    {
        yield return new WaitForSeconds(2.2f);
        Time.timeScale = 0;
    }
}