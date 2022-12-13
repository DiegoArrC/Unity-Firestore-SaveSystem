using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Extensions;

public class FirestoreConnector : MonoBehaviour
{
    [SerializeField] Button pointsButton;
    [SerializeField] Button saveButton;
    [SerializeField] Button loadButton;

    // Only the username field is the interactable field, the other fields update on load
    [SerializeField] InputField usernameField;

    [SerializeField] InputField pointsField;
    [SerializeField] InputField previousScoreValue;
    [SerializeField] InputField currentLevelValue;

    FirebaseFirestore db;

    // Start is called before the first frame update
    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>{
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available) {
                db = FirebaseFirestore.DefaultInstance;
            }
            else {
                Debug.Log("Could not connect with Firebase");
            }
        });
        
        pointsButton.onClick.AddListener(UpdatePlayerScore);
        saveButton.onClick.AddListener(SavePlayerStats);
        loadButton.onClick.AddListener(LoadPlayerStats);
        
    }

    void UpdatePlayerScore(){
        int oldScore = int.Parse(pointsField.text);
        var playerName = usernameField.text;

        // creates a struct with the oldScore value and the associated username
        var scoreData  = new Users {
            CurrentScore = oldScore + 1,
            Username = playerName
        };
        DocumentReference countRef = db.Collection("users").Document($"{scoreData.Username}");
        countRef.UpdateAsync("CurrentScore", scoreData.CurrentScore).ContinueWithOnMainThread(task =>
        {
            if (task != null)
            {
                Debug.Log($"{scoreData.Username}'s current score has been updated.");
            }
            // Gets a snapshot of the associated document from firestore to update the points field locally
            db.Collection("users").Document(usernameField.text).GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                var playerData = task.Result.ConvertTo<Users>();
                pointsField.text = playerData.CurrentScore.ToString();
            });
        });
    }

    void SavePlayerStats(){
        var playerName = usernameField.text;
        // Debug.Log(playerName);
        // Debug.Log(playerName.GetType());
        var setCurrentScore = int.Parse(pointsField.text);
        // Debug.Log(setCurrentScore);
        // Debug.Log(setCurrentScore.GetType());
        var prevScore = (setCurrentScore > int.Parse(previousScoreValue.text)) ? setCurrentScore : int.Parse(previousScoreValue.text);
        // Debug.Log(prevScore);
        // Debug.Log(prevScore.GetType());

        // Data struct for the player class
        var playerData = new Users {
            Username = playerName,
            CurrentScore = setCurrentScore,
            PreviousScore = prevScore,
            Level = setCurrentScore / 10
        };

        DocumentReference playerStats= db.Collection("users").Document($"{playerData.Username}");

        // MergeAll will combine all the associated old data with the new data being set, thereby not deleting extraneous fields
        playerStats.SetAsync(playerData, SetOptions.MergeAll).ContinueWithOnMainThread(task =>
        {
            if (task != null) {
            Debug.Log($"Data saved for {playerData.Username}");
            }
        });
    }
    
    void LoadPlayerStats(){
        db.Collection("users").Document(usernameField.text).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task != null)
            {var playerData = task.Result.ConvertTo<Users>();

            // resets the current score field on load
            pointsField.text = "0";
            // loads the associated last score and the currentLevel of that user
            previousScoreValue.text = playerData.PreviousScore.ToString();
            currentLevelValue.text = playerData.Level.ToString();
            }
            else {
                Debug.Log("Unable to process load");
            }
        });
    }
}
