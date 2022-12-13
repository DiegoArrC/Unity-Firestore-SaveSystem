using Firebase.Firestore;

// A struct is created as a custom class to handle document data
[FirestoreData]
public class Users
{
    [FirestoreProperty]
    public int CurrentScore {get; set;}

    [FirestoreProperty]
    public int Level {get; set;}

    [FirestoreProperty]
    public int PreviousScore {get; set;}

    [FirestoreProperty]
    public string Username {get; set;}
}