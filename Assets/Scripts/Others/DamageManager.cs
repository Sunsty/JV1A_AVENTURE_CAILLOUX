using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{

    public static DamageManager Instance;
    [Header("Configurations")]
    [SerializeField] private DamageText damageTextPrefab;
    public GameObject damagePosition;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowDamageText(float damageAmount)
    {
        DamageText text = Instantiate(damageTextPrefab, damagePosition.transform);
        text.transform.position += Vector3.right * 0.5f;
        text.SetDamageText(damageAmount);
    }
}
