using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    [SerializeField]
    private float _transitionTime;

    [SerializeField]
    private AnimationCurve _transitionCurve;

    [SerializeField]
    private RectTransform _transitionObject;

    private float _screenWidth;
    private bool _animationFinished;

    private void Start()
    {
        _screenWidth = Screen.width;
        StartCoroutine(SceneEnterAnim());
    }

    public void TransitionToScene(string scene)
    {
        StartCoroutine(SceneTransitionTo(scene));
    }

    private IEnumerator SceneEnterAnim()
    {
        //sets object anchoring
        _transitionObject.anchorMin = new Vector2(0, 0);
        _transitionObject.anchorMax = new Vector2(0, 1);
        _transitionObject.pivot = new Vector2(0, 0.5f);

        //plays the animation
        _transitionObject.sizeDelta = new Vector2(Screen.width, 0);
        float currentAnimTime = 0;
        while (currentAnimTime < _transitionTime)
        {
            float step = _transitionCurve.Evaluate(1 - (currentAnimTime / _transitionTime));
            _transitionObject.sizeDelta = new Vector2(step * _screenWidth, 0);
            currentAnimTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }

    private IEnumerator SceneTransitionTo(string scene)
    {
        //sets object anchoring
        _transitionObject.anchorMin = new Vector2(1, 0);
        _transitionObject.anchorMax = new Vector2(1, 1);
        _transitionObject.pivot = new Vector2(1, 0.5f);

        //plays the animation
        float currentAnimTime = _transitionTime;
        while (currentAnimTime > 0)
        {
            float step = _transitionCurve.Evaluate(1 - (currentAnimTime / _transitionTime));
            _transitionObject.sizeDelta = new Vector2(step * _screenWidth, 0);
            currentAnimTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        SceneManager.LoadScene(scene);
        yield return null;
    }
}
