using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Enemy))]
public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

    private Animator _animator;
    private Enemy _enemy;

    private Coroutine _colorChanger;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _enemy.OnTookDamage += PlayTakeDamage;
        _enemy.OnDied += PlayDying;
    }

    private void OnDisable()
    {
        _enemy.OnTookDamage -= PlayTakeDamage;
        _enemy.OnDied += PlayDying;
    }

    private void PlayTakeDamage()
    {
        if(_colorChanger != null)
            StopCoroutine(_colorChanger);

        _colorChanger = StartCoroutine(DamageColorSetter());
    }

    private void PlayDying()
    {
        _animator.Play(ACGoblin.State.Die);
    }

    private IEnumerator DamageColorSetter()
    {
        float second = 0.1f;
        float lifeTime = 0.3f;
        float passedTime = 0;

        var waitTime = new WaitForSeconds(second);

        Color defaultColor = _skinnedMeshRenderer.material.color;
        _skinnedMeshRenderer.material.color = Color.red;

        while (passedTime < lifeTime)
        {
            passedTime += second;
            yield return waitTime;
        }

        if(passedTime >=  lifeTime)
        {
            _skinnedMeshRenderer.material.color = defaultColor;
            yield break;
        }
    }
}
