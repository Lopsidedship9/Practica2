using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed; 
    public float gotHayDestroyDelay; 
    private bool hitByHay;

    //For the Sheep Droper
    public float dropDestroyDelay; 
    private Collider myCollider; 
    private Rigidbody myRigidbody;

    //For the spawner
    private SheepSpawner sheepSpawner;

    //For the heart
    public float heartOffset; 
    public GameObject heartPrefab;  



    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    private void HitByHay()
    {
        hitByHay = true;
        runSpeed = 0;
        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
        TweenScale tweenScale = gameObject.AddComponent<TweenScale>();
        tweenScale.targetScale = 0;
        tweenScale.timeToReachTarget = gotHayDestroyDelay;
        Destroy(gameObject, gotHayDestroyDelay);
        SoundManager.Instance.PlaySheepHitClip();
        GameStateManager.Instance.SavedSheep();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Haybale") && !hitByHay) 
        {
            Destroy(other.gameObject);
            HitByHay();
        }
        else if (other.CompareTag("DropSheep"))
        {
            Drop();
        }
    }

    private void Drop()
    {
        myRigidbody.isKinematic = false;
        myCollider.isTrigger = false;
        SoundManager.Instance.PlaySheepDroppedClip();
        sheepSpawner.RemoveSheepFromList(gameObject);
        GameStateManager.Instance.DroppedSheep();
        Destroy(gameObject, dropDestroyDelay);

    }

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }
}