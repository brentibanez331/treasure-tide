using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    ParticleSystem explosion;
    MeshRenderer bomb;

    GameManagerScript gameManager;
    CinemachineFreeLook vCam;
    public bool isEndless = false;

    //audio
    public AudioSource bombFX;
    public AudioSource defeatFX;


    private void Awake()
    {
        vCam = GameObject.FindGameObjectWithTag("CinemachineCam").GetComponent<CinemachineFreeLook>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        explosion = gameObject.GetComponentInChildren<ParticleSystem>();
        bomb = gameObject.GetComponentInChildren<MeshRenderer>();
    }

    public void Explode()
    {
        explosion.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //audio
            bombFX.Play();
            defeatFX.Play();


            bomb.enabled = false;
            Explode();
            Destroy(other.gameObject);
            Destroy(gameObject, 3.0f);

            gameManager.goodsCollected = 0;
            gameManager.starCount = 0;  

            gameManager.HideGoal();
            gameManager.OpenResult();
            gameManager.playTimer = false;
            gameManager.mainMenu.PauseGame(vCam);
            if (!isEndless)
            {
                gameManager.resultText.text = "LEVEL COMPLETE!";
            }
            else
            {
                gameManager.resultText.text = "TRY AGAIN!";
            }
            
        }
    }
}