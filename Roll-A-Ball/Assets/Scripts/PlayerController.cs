using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
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
        rb = GetComponent<Rigidbody>();
        SetCountText();
        winText.text = "";
        collectObjectSource.clip = collectObjectSound;
        winSoundSource.clip = winSound;
    }
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            collectObjectSource.Play();
            count += 1;
            SetCountText();
        }
    }
    void SetCountText()
    {
        countText.text = "Score " + count.ToString();
        if(count >= 12)
        {
            winText.text = "You Win!";
            winSoundSource.Play();
        }
    }
}