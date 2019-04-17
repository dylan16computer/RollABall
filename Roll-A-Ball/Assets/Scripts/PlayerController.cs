using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour //Inheriting MonoBehaviour class
{
    //Declaring variables and UnityEngine/UnityEngineUI class variables
    private Rigidbody rb;
    public float speed;
    private int count = 0;
    public Text countText;
    public Text winText;
    public AudioClip collectObjectSound;
    public AudioClip winSound;
    public AudioSource winSoundSource;
    public AudioSource collectObjectSource;
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); //Assigning 'rb' the RigidBody Component
        SetCountText(); //Calling method to display score on-screen
        winText.text = ""; //Setting UI text to empty string
        collectObjectSource.clip = collectObjectSound; //Setting the deafult audio clip to play when collecting objects
        winSoundSource.clip = winSound; //Setting the default audio clip to play when finishing the game
    }
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); //Assigning float variables the value of the horizontal and verticle axes
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical); //Creating an instance of the Vector3 class, using the two float variables
        rb.AddForce(movement * speed); //Adding force as movement variable value multiplied by speed variable value
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up")) //If the GameObject collided with has a tag of "Pick Up" (i.e., if the collectable objects are picked up), run the following code
        {
            other.gameObject.SetActive(false); //Setting the active state of the object picked up to false, thereby removing its visibility
            collectObjectSource.Play(); //Playing the audio of collecting a collectable object
            count += 1; //Incrementing the value of count by an integer of 1
            SetCountText(); //Calling the method
        }
    }
    void SetCountText()
    {
        countText.text = "Score " + count.ToString(); //Displaying the score to the screen and converting the integer value of count to a string
        if(count >= 12) //Once count is equal to or greater than 12 (i.e., when all collectable objects are collected) run the following code
        {
            winText.text = "You Win!"; //Displaying the game winning message to the screen
            winSoundSource.Play(); //Playing the game winning audio
        }
    }
}