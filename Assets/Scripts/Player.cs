using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float speed = 7f;
    float jumpBoost = 5f;
    float jumpRate = 1f;
    float canJump;

    float blinkTimer = 0f;
    float blinkLength = 3f;
    float colorTimer = 0f;
    float colorLength = 10f;

    float kibbleHealth = 10f;
    float scaleMultiplier = 2f;

    //Handles
    Rigidbody2D rb2d;
    Animator anim;
    SpriteRenderer spriteRenderer;
    UIManager uiManager;

    [SerializeField]
    public float currentHealth;
    [SerializeField]
    public float maxHealth = 100f;
    
    [SerializeField]
    public int lives = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        uiManager = GameObject.Find("BackgroundCanvas").GetComponent<UIManager>();

        //sets health
        currentHealth = maxHealth;

        //sets health to health bar value/image
        uiManager.slider.maxValue = maxHealth;
        uiManager.slider.value = maxHealth;

        //sets health bar to green color
        uiManager.fill.color = uiManager.gradient.Evaluate(1f);
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        //gets player moving right/left
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 direction = new Vector2(horizontalInput, 0);
        transform.Translate(direction * speed * Time.deltaTime);

        //lets player jump with up arrow key
        if (Input.GetKeyDown(KeyCode.UpArrow) && Time.time > canJump)
        {
            canJump = Time.time + jumpRate;
            rb2d.AddForce(Vector2.up * jumpBoost, ForceMode2D.Impulse);
        }

        //wraps player around screen
        if (transform.position.x < -9)
        {
            transform.position = new Vector3(9, transform.position.y, 0);
        }
        else if (transform.position.x > 9)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }

        //sets idle animation when player isnt moving
        if (horizontalInput != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        //sets proper animation when player moves left/right
        if (horizontalInput < 0)
        {
            anim.SetBool("runningLeft", true);
        }
        else if (horizontalInput > 0)
        {
            anim.SetBool("runningRight", true);
        }
        else
        {
            anim.SetBool("runningLeft", false);
            anim.SetBool("runningRight", false);
        }
    }

    //flashes player when it takes damage
    public IEnumerator BlinkingPlayer()
    {
        Color color = spriteRenderer.color;

        while (blinkTimer != blinkLength)
        {
            blinkTimer++;

            color.a = 0.5f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.1f);

            color.a = 1f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.1f);
        }

        blinkTimer = 0f;
    }

    public void KibbleHealthPowerup()
    {
        if (currentHealth <= 90)
        {
            currentHealth += kibbleHealth;

            //adjusts health bar when kibble power up is taken
            uiManager.slider.value = currentHealth;
            uiManager.fill.color = uiManager.gradient.Evaluate(uiManager.slider.normalizedValue);

            //adjusts HP text when kibble collides with player
            uiManager.HPText.text = currentHealth.ToString();

        }
        else if (currentHealth > 90)
        {
            currentHealth = maxHealth;

            //adjusts health bar when kibble power up is taken
            uiManager.slider.value = currentHealth;
            uiManager.fill.color = uiManager.gradient.Evaluate(uiManager.slider.normalizedValue);

            //adjusts HP text when kibble collides with player
            uiManager.HPText.text = currentHealth.ToString();
        }
    }

    public void ColorSpeedPowerup()
    {
        speed *= 1.5f;
        StartCoroutine(ColorChange());
    }

    public IEnumerator ColorChange()
    {
        Color color = spriteRenderer.color;

        while (colorTimer != colorLength)
        {
            colorTimer++;

            color.b -= 1f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.05f);

            color.b += 1f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.05f);

            color.g -= 1f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.05f);

            color.g += 1f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.05f);

            color.r -= 1f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.05f);

            color.r += 1f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.05f);
        }

        speed /= 1.5f;
        colorTimer = 0f;
    }

    public void ScalePowerup()
    {
        Vector2 bigScale = transform.localScale;
        bigScale.x *= scaleMultiplier;
        bigScale.y *= scaleMultiplier;
        transform.localScale = bigScale;
        StartCoroutine(ScalePowerDown());
    }

    public IEnumerator ScalePowerDown()
    {
        yield return new WaitForSeconds(3f);
        Vector2 smallScale = transform.localScale;
        smallScale.x /= scaleMultiplier;
        smallScale.y /= scaleMultiplier;
        transform.localScale = smallScale;
    }
}
