using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _tensionForse;

    public void Attack(Transform enemy, Arrow arrow, Transform handForArrow)
    {
        Arrow newArrow = Instantiate(arrow, handForArrow.position, Quaternion.identity);
        newArrow.Shoot(_tensionForse, enemy);
    }   
}
