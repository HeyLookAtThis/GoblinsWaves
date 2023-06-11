using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float _health;
    [SerializeField] private float _speed;
    [SerializeField] private Player _player;

    private UnityAction _dying;
    private UnityAction _move;
    private UnityAction _tookDamage;

    public event UnityAction OnDying
    {
        add => _dying += value;
        remove => _dying -= value;
    }

    public event UnityAction OnTookDamage
    {
        add => _tookDamage += value;
        remove => _tookDamage -= value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_health > 0)
            _player.Attack(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Arrow>(out Arrow arrow))
            TakeDamage(arrow.Damage);
    }

    private void TakeDamage(float damage)
    {
        _health -= damage;
        _tookDamage?.Invoke();

        if (_health <= 0)
        {
            _dying?.Invoke();
            StartCoroutine(Destroyer());
        }
    }

    private IEnumerator Destroyer()
    {
        float second = 1.0f;
        var waitTime = new WaitForSeconds(second);
        bool isSkeppedTime = false;

        while (isSkeppedTime == false)
        {
            isSkeppedTime = true;
            yield return waitTime;
        }

        if (isSkeppedTime)
        {
            Destroy(gameObject);
            yield break;
        }
    }
}
