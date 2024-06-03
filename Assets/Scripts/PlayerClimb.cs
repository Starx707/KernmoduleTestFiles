using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    [SerializeField]
    private ThirdPersonMovementScript _movementScript;
    [SerializeField]
    private CharacterController _characterController;

    private bool _isClimbing;
    private bool _isAtLadder;
    private Ladder _currentLadder;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ladder>(out _currentLadder))
        {
            _isAtLadder = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Ladder>())
        {
            _currentLadder = null;
            _isAtLadder = false;
        }
    }

    private void Update()
    {
        if (_isAtLadder && !_isClimbing && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ClimbLadder());
        }
    }

    private IEnumerator ClimbLadder()
    {
        Vector3 startingPos = transform.position;
        Vector3 endPos = _currentLadder.GetEndPosition(startingPos);
        _movementScript.enabled = false;
        _isClimbing = true;
        for (int i = 0; i <= 100; i++)
        {
            transform.position = Vector3.Lerp(startingPos, endPos, i / 100f);
            yield return new WaitForSeconds(0.01f);
        }
        _isClimbing = false;
        _movementScript.enabled = true;
    }
}
