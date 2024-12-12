using System.Collections;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public PlayerController player;
    public TMP_Text healthText;
    public float invincibilityLength;
    private float invincibilityCounter;

    public Renderer playerRenderer; // Reference to the player's Renderer
    public SoundEffectsPlayer soundEffectsPlayer;

    private bool isRespawning;
    private Vector3 respawnPoint;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
        respawnPoint = player.transform.position;
    }

    void Update()
    {
        if (player.transform.position.y < -50)
        {
            Respawn();
        }

        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
        }
    }

    public void Respawn()
    {
        player.transform.position = respawnPoint;
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    public void hurtPlayer(int damage, Vector3 direction)
    {
        if (invincibilityCounter <= 0)
        {
            soundEffectsPlayer.DamageSound();
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Respawn();
            }
            else
            {
                UpdateHealthText();
                player.KnockBack(direction);
                invincibilityCounter = invincibilityLength;
                StartCoroutine(FlashDuringInvincibility());
            }
        }
    }

    public void healPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = "Health: " + currentHealth;
    }

    private IEnumerator FlashDuringInvincibility()
    {
        while (invincibilityCounter > 0)
        {
            playerRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            playerRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
