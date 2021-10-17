using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    protected float speed;
    protected float damage;

    [SerializeField]
    GameObject enemyParticle;

    //Handles
    SpawnManager spawnManager;
    UIManager uiManager;
    Timer timer;

    void Start()
    {
        spawnManager = GameObject.Find("Main Camera").GetComponent<SpawnManager>();
        uiManager = GameObject.Find("BackgroundCanvas").GetComponent<UIManager>();
        timer = GameObject.Find("Timer").GetComponent<Timer>();
    }

    virtual protected void Update()
    {
        //moves enemy down
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        //destroys enemy once it leaves game scene
        if (transform.position.y < -7f)
        {
            Destroy(this.gameObject);
        }
    }

    //Detects collision with player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                //collision with scale power-up active
                Vector2 playerScale = player.transform.localScale;
                if (playerScale.x > 2 && playerScale.y > 2)
                {
                    //player.currentHealth -= 0f;
                    Instantiate(enemyParticle, transform.position, Quaternion.identity);
                }
                else
                {
                    player.currentHealth -= damage;

                    //updates health bar according to current health
                    uiManager.slider.value = player.currentHealth;
                    uiManager.fill.color = uiManager.gradient.Evaluate(uiManager.slider.normalizedValue);

                    //updates HP text
                    uiManager.HPText.text = player.currentHealth.ToString();

                    player.StartCoroutine(player.BlinkingPlayer());
                }

                //reset health and decrease lives
                if (player.currentHealth <= 0)
                {
                    player.currentHealth = 100;

                    //updates HP text
                    uiManager.HPText.text = 100.ToString();

                    //resets health bar
                    uiManager.slider.value = player.maxHealth;
                    uiManager.fill.color = uiManager.gradient.Evaluate(1f);

                    player.lives--;

                    if (player.lives == 2)
                    {
                        //makes third paw disapear so one paw is empty
                        uiManager.paws[2].enabled = false;
                    }
                    else if (player.lives == 1)
                    {
                        //makes second paw disapear so two paws are empty
                        uiManager.paws[1].enabled = false;
                    }
                    //if player runs out of lives
                    else if (player.lives <= 0)
                    {
                        //destory player
                        Destroy(other.gameObject);

                        //stops spawning enemies & powerups
                        spawnManager.spawnActive = false;

                        //makes first paw disapear so all paws are empty
                        uiManager.paws[0].enabled = false;

                        //set health bar to empty
                        uiManager.slider.value = 0;

                        //sets HP text to 0
                        uiManager.HPText.text = 0.ToString();

                        //blinks gameover text on and off
                        uiManager.StartCoroutine(uiManager.GameOverBlinkText());

                        //stops timer at death & adjusts timer text
                        timer.isRunning = false;
                        timer.deathMin = (int)Time.realtimeSinceStartup / 60;
                        timer.deathSec = (int)Time.realtimeSinceStartup % 60;
                        uiManager.timerText.text = timer.deathMin + ":" + timer.deathSec.ToString("0#");
                    }
                }
            }

            //destroy enemy after collision
            Destroy(gameObject);
        }
    }
}
