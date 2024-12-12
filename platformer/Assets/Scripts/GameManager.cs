using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentGold;
    public int winGoldAmount = 5;
    public TMP_Text goldText;
    public TMP_Text winMessage;
    public SoundEffectsPlayer soundEffectsPlayer;

    // Start is called before the first frame update
    void Start()
    {
        winMessage.gameObject.SetActive(false);
        UpdateGoldText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGold(int goldToAdd)
    {
        currentGold += goldToAdd;
        UpdateGoldText();
        soundEffectsPlayer.CoinSound();

        if (currentGold >= winGoldAmount)
        {
            ShowWinMessage();
        }
    }

    public void UpdateGoldText()
    {
        goldText.text = "Food: " + currentGold + "/"+ winGoldAmount;

    }

    private void ShowWinMessage()
    {
        winMessage.text = "You Win!";
        winMessage.gameObject.SetActive(true);
        StartCoroutine(LoadMenuAfterDelay(3f));
    }

    private IEnumerator LoadMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Menu");
    }

}
