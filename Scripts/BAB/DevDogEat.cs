using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevDogEat : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip hoeru;
    [SerializeField] AudioClip taberu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            FoodManager foodManager = other.gameObject.GetComponent<FoodManager>();
            if (foodManager.foodType == FoodManager.FoodType.Safety)
            {
                audioSource.PlayOneShot(hoeru);
            }
            else
            {
                audioSource.PlayOneShot(taberu);
                Destroy(other.gameObject);
            }
        }
    }
}
