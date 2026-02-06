using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaliDali : MonoBehaviour
{
    public GameObject dailyWindow;            // The UI window that shows the daily rewards
    public Image[] rewardImages;              // Array of Image components for each daily reward
    public Sprite closedGiftSprite;           // Sprite for a closed gift
    public Sprite openGiftSprite;             // Sprite for an open gift
    public int currentDay;                    // The current day of rewards
    public Button claimButton;                // Button to claim the reward
    public bool[] rewardClaimed;              // Boolean array to check if reward is claimed
    public float fadeDuration = 1.0f;         // Duration of the window fade-out animation
    public DateTime lastClaimDate;            // The date the player last claimed a reward
    public ScoringOverseer sc;
    // Start is called before the first frame update
    void Start()
    {
        InitializeRewards();
        ShowDailyWindow();
    }

    // Initialize the daily rewards system
    void InitializeRewards()
    {
        // Load the last claim date from PlayerPrefs (convert from string to DateTime)
        string lastClaimDateString = PlayerPrefs.GetString("LastClaimDate", DateTime.MinValue.ToString());
        lastClaimDate = DateTime.Parse(lastClaimDateString);

        // Check if a new day has passed
        if (IsNewDay())
        {
            // If a new day has passed, move to the next reward
            currentDay = PlayerPrefs.GetInt("CurrentDay", 0);
        }
        else
        {
            // If no new day has passed, use the saved day
            currentDay = PlayerPrefs.GetInt("CurrentDay", 0) - 1;
        }

        // Set up claimed rewards
        rewardClaimed = new bool[rewardImages.Length];

        for (int i = 0; i < rewardImages.Length; i++)
        {
            if (PlayerPrefs.GetInt("RewardClaimed" + i, 0) == 1)
            {
                rewardClaimed[i] = true;                    // Mark rewards as claimed
                rewardImages[i].sprite = openGiftSprite;    // Show open gift for claimed rewards
                rewardImages[i].color = Color.white;        // Optional: Use white to keep the original look for claimed rewards
            }
            else
            {
                rewardClaimed[i] = false;                   // Unclaimed rewards
                rewardImages[i].sprite = closedGiftSprite;  // Show closed gift for unclaimed rewards
            }
        }
    }

    // Check if it's a new day
    bool IsNewDay()
    {
        DateTime currentDate = DateTime.Now;
        return currentDate.Date > lastClaimDate.Date;
    }

    // Show the daily rewards window
    void ShowDailyWindow()
    {
        dailyWindow.SetActive(true);                       // Display the daily window
        UpdateRewardDisplay();
    }

    // Update the display of rewards based on the current day
    void UpdateRewardDisplay()
    {
        if (!rewardClaimed[currentDay])
        {
            // Enable the claim button only if today's reward is not claimed
            claimButton.interactable = true;
        }
        else
        {
            claimButton.interactable = false;              // Disable the button if the reward is claimed
        }
    }

    // Call this function when the claim button is pressed
    public void ClaimReward()
    {
        if (!rewardClaimed[currentDay])
        {
            sc.ElevateScore( 10 * currentDay);
            rewardClaimed[currentDay] = true;              // Mark the reward as claimed
            PlayerPrefs.SetInt("RewardClaimed" + currentDay, 1);  // Save the claimed state
            PlayerPrefs.SetInt("CurrentDay", currentDay + 1);     // Move to the next day

            // Save the current date as the last claim date
            PlayerPrefs.SetString("LastClaimDate", DateTime.Now.ToString());

            // Start the coroutine to animate the gift opening and window fade out
            StartCoroutine(OpenGiftAndFadeWindow());
        }
    }

    // Coroutine to open the gift (change sprite) and fade out the window
    IEnumerator OpenGiftAndFadeWindow()
    {
        // Step 1: Change the sprite to the open gift
        rewardImages[currentDay].sprite = openGiftSprite;

        // Step 2: Wait for a short moment to simulate opening the gift
        yield return new WaitForSeconds(1.0f);  // Adjust this delay as needed

        // Step 3: Fade out the daily rewards window
        CanvasGroup canvasGroup = dailyWindow.GetComponent<CanvasGroup>();

        if (canvasGroup != null)
        {
            float startAlpha = canvasGroup.alpha;
            float time = 0f;

            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, time / fadeDuration);
                yield return null;
            }

            // Ensure the window is fully faded out
            canvasGroup.alpha = 0f;
        }

        // Step 4: Disable the window after fade-out
        dailyWindow.SetActive(false);
    }

    // Placeholder function to give the reward based on the current day
    void GiveRewardForDay(int day)
    {
        switch (day)
        {
            case 0:
                // Add your reward logic here, e.g., give coins or items for day 1
                Debug.Log("Reward for Day 1 claimed: 100 coins");
                break;
            case 1:
                // Add your reward logic for day 2
                Debug.Log("Reward for Day 2 claimed: Special item");
                break;
            // Continue for other days...
            default:
                Debug.Log("Reward for another day claimed.");
                break;
        }
    }
}
