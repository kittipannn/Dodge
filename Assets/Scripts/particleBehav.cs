using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleBehav : MonoBehaviour
{
    ParticleSystem particle;
    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }
    void Start()
    {
        int currentIndex = PlayerPrefs.GetInt("CharacterSelected");
        particle.startColor = GameObject.FindObjectOfType<ShopManager>().shopdata[currentIndex].materialCharacter.color;
    }

}
