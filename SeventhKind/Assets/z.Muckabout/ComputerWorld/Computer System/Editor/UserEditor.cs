using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class UserEditor : EditorWindow
{
    private bool hasLoaded = false;

    public GameObject computerObject;
    public ComputerMainScript.EnumUsers chosenUser;

    private ComputerMainScript computer;
    private User currentUser;

    private Vector2 scrollPos = new Vector2();

    [MenuItem("Window/User Editor")]
    public static void ShowWindow()
    {
        GetWindow<UserEditor>("User Editor");
    }

    private void OnGUI()
    {
        if (!hasLoaded)
        {
            computerObject = (GameObject)EditorGUILayout.ObjectField("Computer screen", computerObject, typeof(GameObject), true);
            if (computerObject)
            {
                computer = computerObject.GetComponent<ComputerMainScript>();
                LoadUsers();
                hasLoaded = true;
            }
        }
        else
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);
            if (computerObject)
            {
                chosenUser = (ComputerMainScript.EnumUsers)EditorGUILayout.EnumPopup("User", chosenUser);

                currentUser = computer.ConvertEnumToUser(chosenUser);
            }
            if (currentUser != null)
            {
                if (currentUser.notes != null)
                {
                    for (int i = 0; i < currentUser.notes.Count; i++)
                    {
                        EditorGUILayout.LabelField("Note #" + (i + 1).ToString());
                        {
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Subject");
                            currentUser.notes[i].subject = EditorGUILayout.TextField(currentUser.notes[i].subject);
                            EditorGUILayout.EndHorizontal();
                        }
                        {
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Content");
                            currentUser.notes[i].content = EditorGUILayout.TextArea(currentUser.notes[i].content, GUILayout.Height(80));
                            EditorGUILayout.EndHorizontal();
                        }
                        {
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Notepad Text");
                            currentUser.notes[i].notepadText = EditorGUILayout.TextArea(currentUser.notes[i].notepadText, GUILayout.Height(80));
                            EditorGUILayout.EndHorizontal();
                        }
                    }
                }

                if (GUILayout.Button("Add new note"))
                {
                    currentUser.notes.Add(new Note());
                }

                if (GUILayout.Button("Remove last note"))
                {
                    currentUser.notes.RemoveAt(currentUser.notes.Count - 1);
                }

                EditorGUILayout.Space(10);

                if (currentUser.emails != null)
                {
                    for (int i = 0; i < currentUser.emails.Count; i++)
                    {
                        EditorGUILayout.LabelField("Email #" + (i + 1).ToString());
                        {
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Subject");
                            currentUser.emails[i].subject = EditorGUILayout.TextField(currentUser.emails[i].subject);
                            EditorGUILayout.EndHorizontal();
                        }
                        {
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Recipient");
                            currentUser.emails[i].recipient = EditorGUILayout.TextField(currentUser.emails[i].recipient);
                            EditorGUILayout.EndHorizontal();
                        }
                        {
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Sender");
                            currentUser.emails[i].sender = EditorGUILayout.TextField(currentUser.emails[i].sender);
                            EditorGUILayout.EndHorizontal();
                        }
                        {
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Content");
                            currentUser.emails[i].content = EditorGUILayout.TextArea(currentUser.emails[i].content, GUILayout.Height(80));
                            EditorGUILayout.EndHorizontal();
                        }
                        {
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Notepad Text");
                            currentUser.emails[i].notepadText = EditorGUILayout.TextArea(currentUser.emails[i].notepadText, GUILayout.Height(80));
                            EditorGUILayout.EndHorizontal();
                        }
                    }
                }

                if (GUILayout.Button("Add new email"))
                {
                    currentUser.emails.Add(new Email());
                }

                if (GUILayout.Button("Remove last email"))
                {
                    currentUser.emails.RemoveAt(currentUser.emails.Count - 1);
                }

                EditorGUILayout.Space(10);

                if (GUILayout.Button("Save"))
                {
                    SaveUsers();
                }

                if (GUILayout.Button("Cancel"))
                {
                    LoadUsers();
                }
            }
            EditorGUILayout.EndScrollView();
        }
    }

    private void SaveUsers()
    {
        if (computer)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(ComputerMainScript.fileName, FileMode.Create)))
            {
                byte[] writeData;
                using (MemoryStream mStream = new MemoryStream())
                {
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    bFormatter.Serialize(mStream, computer.users);
                    writeData = mStream.ToArray();
                }
                writer.Write(writeData);
            }
        }
    }

    private void LoadUsers()
    {
        using (MemoryStream mStream = new MemoryStream())
        {
            try
            {
                byte[] readInput = File.ReadAllBytes(ComputerMainScript.fileName);
                mStream.Write(readInput, 0, readInput.Length);
            }
            catch
            {
                return;
            }
            BinaryFormatter bFormatter = new BinaryFormatter();
            mStream.Position = 0;
            computer.users = (List<User>)bFormatter.Deserialize(mStream);
        }
    }
}