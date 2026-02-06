using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public TextMeshProUGUI leaderboardText;  // Single TextMeshProUGUI for the entire leaderboard
    public string[] randomNames = { "Player1", "Player2", "Player3", "Player4", "Player5", "Player6", "Player7", "Player8", "Player9", "Player10" };
    public int currentScore;                 // Player's current score (set dynamically during gameplay)
    public int topScore;                     // Player's highest score
    private List<int> randomScores;          // List to hold random scores
    private List<string> leaderboardNames;   // List to hold player names for leaderboard

    // OnEnable is called every time the ScoreBoard is enabled
    void OnEnable()
    {
        LoadTopScore();
        CheckForNewTopScore();
        GenerateRandomScores();
        PopulateLeaderboard();
    }

    // Load the player's top score from PlayerPrefs
    void LoadTopScore()
    {
        topScore = PlayerPrefs.GetInt("TopScore", 0);  // Load the player's highest score from PlayerPrefs
    }

    // Check if the current score is greater than the top score, and update if necessary
    void CheckForNewTopScore()
    {
        if (currentScore > topScore)
        {
            topScore = currentScore;                      // Update topScore with current score
            PlayerPrefs.SetInt("TopScore", topScore);     // Save the new top score in PlayerPrefs
        }
    }

    // Generate random scores for other players
    void GenerateRandomScores()
    {
        randomScores = new List<int>();
        leaderboardNames = new List<string>(randomNames); // Copy randomNames to leaderboardNames

        // Generate random scores for 9 players
        for (int i = 0; i < 9; i++)
        {
            randomScores.Add(Random.Range(50, 1000));     // Random scores between 50 and 1000
        }

        // Add player's top score into the list of random scores
        randomScores.Add(topScore);

        // Sort the list in descending order (higher score comes first)
        randomScores.Sort((a, b) => b.CompareTo(a));
    }

    // Populate the leaderboard with random names and sorted scores
    void PopulateLeaderboard()
    {
        leaderboardText.text = "";  // Clear the existing leaderboard text

        for (int i = 0; i < randomScores.Count; i++)
        {
            if (randomScores[i] == topScore)
            {
                leaderboardText.text += "YOU: " + topScore.ToString() + "\n";  // Display player's top score as 'YOU'
            }
            else
            {
                leaderboardText.text += leaderboardNames[i] + ": " + randomScores[i].ToString() + "\n";  // Random player name with their score
            }
        }
    }

    // Method to set the player's current score (to be called from gameplay logic)
    public void SetCurrentScore(int score)
    {
        currentScore = score;
    }
}
