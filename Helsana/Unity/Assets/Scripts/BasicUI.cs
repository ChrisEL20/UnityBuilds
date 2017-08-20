using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicUI : MonoBehaviour {

    public Image timerImage;
    public Text coinText;
    public GameObject endLevelUI;
    public GameObject infoImage;

    public Text endeLevelCoinText;
    public Text endLevelTimeText;

    public GameManager gameManagerComponent;
    
	void Start () {
        this.UpdateCoinText();
        this.UpdateTimerImage();
        StartCoroutine(this.HideInfoImage());
	}

    IEnumerator HideInfoImage()
    {
        yield return new WaitForSeconds(5f);

        if (this.infoImage != null)
            this.infoImage.SetActive(false);
    }
	

    public void UpdateTimerImage()
    {
        if (this.gameManagerComponent != null && this.timerImage != null)
        {
            this.timerImage.fillAmount = this.gameManagerComponent.currentPlayTime / this.gameManagerComponent.totalPlayTime;
        }
    }

    public void UpdateCoinText()
    {
        if (this.coinText != null)
            this.coinText.text =  GlobalVariables.totalCoins.ToString();
    }

    public void ShowEndLevelUI()
    {
        if (this.endLevelUI != null && this.timerImage != null)
        {
            this.timerImage.gameObject.SetActive(false);
            this.endLevelUI.SetActive(true);

            int score = (int)GlobalVariables.totalCoins + ((int)this.gameManagerComponent.currentPlayTime / 2);

            this.endeLevelCoinText.text =  score.ToString();
            this.endLevelTimeText.text = "Used time: " + (this.gameManagerComponent.totalPlayTime - this.gameManagerComponent.currentPlayTime);
        }
    }

    public void TimeIsOversUI()
    {
        if (this.endLevelUI != null && this.timerImage != null)
        {
            this.timerImage.gameObject.SetActive(false);
            this.endLevelUI.SetActive(true);

            this.endeLevelCoinText.text = GlobalVariables.totalCoins.ToString();
            this.endLevelTimeText.text = "Game Over";
        }
    }
}
