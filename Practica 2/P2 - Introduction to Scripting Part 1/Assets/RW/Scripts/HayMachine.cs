using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HayMachine : MonoBehaviour
{
   public float Speed;
   public float horizontalBoundary = 22;
   public GameObject HayMachinePrefab;
   public float shootInterval; 
   private float shootTimer; 


   //For switching the color of the HayMachine
    public Transform modelParent;
    public GameObject blueModelPrefab;
    public GameObject yellowModelPrefab;
    public GameObject redModelPrefab;

   // Start is called before the first frame update
    void Start()
    {
        LoadModel();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateShooting();
    }

    void UpdateMovement(){
        float HorizonatlInput = Input.GetAxisRaw("Horizontal");
        /*print(HorizonatlInput); /*To debug*/
        
        if(HorizonatlInput < 0 && transform.position.x > -horizontalBoundary)/*Moving to the left*/
        {
            transform.Translate(Speed*transform.right*(-1)*Time.deltaTime);
        }
        else if(HorizonatlInput > 0 && transform.position.x < horizontalBoundary)/*Moving to the right*/
        {
            transform.Translate(Speed*transform.right*Time.deltaTime);
        }

    }

    void Shooting()
    {    
        /*Instanciate the object*/
        Instantiate(HayMachinePrefab,transform.position,Quaternion.identity);
        SoundManager.Instance.PlayShootClip();
    }

    void UpdateShooting()
    {
        shootTimer -= Time.deltaTime;
    
        if (shootTimer <= 0 && Input.GetKey(KeyCode.Space))
        {
            shootTimer = shootInterval;
            Shooting();
        }
    }

    private void LoadModel()
    {
        Destroy(modelParent.GetChild(0).gameObject);

        switch (GameSettings.hayMachineColor)
        {
            case HayMachineColor.Blue:
                Instantiate(blueModelPrefab, modelParent);
            break;

            case HayMachineColor.Yellow:
                Instantiate(yellowModelPrefab, modelParent);
            break;

            case HayMachineColor.Red:
                Instantiate(redModelPrefab, modelParent);
            break;
        }
    }
}
