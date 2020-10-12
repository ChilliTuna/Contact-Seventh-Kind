using UnityEditor;
using UnityEngine;

public class UserEditor : EditorWindow
{
    public GameObject computerObject;
    public ComputerMainScript.EnumUsers chosenUser;

    private ComputerMainScript computer;
    private User currentUser;

    [MenuItem("Window/User Editor")]
    public static void ShowWindow()
    {
        GetWindow<UserEditor>("User Editor");
    }

    private void OnGUI()
    {
        computerObject = (GameObject)EditorGUILayout.ObjectField("Computer", computerObject, typeof(GameObject), true);
        if (computerObject)
        {
            chosenUser = (ComputerMainScript.EnumUsers)EditorGUILayout.EnumPopup("User", chosenUser);
            computer = computerObject.GetComponent<ComputerMainScript>();

            currentUser = computer.ConvertEnumToUser(chosenUser);

            if (currentUser != null)
            {
                if (currentUser.notes.Count > 0)
                {
                    foreach (Note note in currentUser.notes)
                    {
                        note.subject = EditorGUILayout.TextField("Subject");
                    }
                }

                if (GUILayout.Button("Add new note"))
                {
                    currentUser.notes.Add(new Note());
                }
            }
        }
    }
}