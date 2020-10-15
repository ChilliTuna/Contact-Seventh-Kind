using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerMainScript : MonoBehaviour
{
    #region Variables

    [HideInInspector]
    public List<User> users = new List<User>() { new User(), new User(), new User(), new User() };

    private List<Button> noteButtons;
    private List<Button> emailButtons;

    [HideInInspector]
    public static string fileName = "ComputerData.dat";

    [Header("Admin Details")]
    public string adminName;

    public string adminPassword;
    public string adminHint;
    public Button adminButton;

    [Header("User 1 Details")]
    public string user1Name;

    public string user1Password;
    public string user1Hint;
    public Button user1Button;

    [Header("User 2 Details")]
    public string user2Name;

    public string user2Password;
    public string user2Hint;
    public Button user2Button;

    [Header("User 3 Details")]
    public string user3Name;

    public string user3Password;
    public string user3Hint;
    public Button user3Button;

    private GameObject loginScreen1;
    private GameObject loginScreen2;
    private GameObject emailScreen1;
    private GameObject emailScreen2;
    private GameObject notesScreen1;
    private GameObject notesScreen2;
    private GameObject appsScreen2;

    private User activeUser;
    private bool isLoggedIn;

    public enum EnumUsers
    {
        None,
        Admin,
        User1,
        User2,
        User3
    }

    #endregion Variables

    // Start is called before the first frame update
    private void Start()
    {
        InitializeUsers();
        GetChildMenus();
        SetLoginMenuText();
    }

    #region Core

    private void InitializeUsers()
    {
        LoadUsers();

        //Admin details
        users[0].name = adminName;
        users[0].password = adminPassword;
        users[0].passwordHint = adminHint;

        //User 1 details
        users[1].name = user1Name;
        users[1].password = user1Password;
        users[1].passwordHint = user1Hint;

        //User 2 details
        users[2].name = user2Name;
        users[2].password = user2Password;
        users[2].passwordHint = user2Hint;

        //User 3 details
        users[3].name = user3Name;
        users[3].password = user3Password;
        users[3].passwordHint = user3Hint;
    }

    private void LoadUsers()
    {
        using (MemoryStream mStream = new MemoryStream())
        {
            byte[] readInput = File.ReadAllBytes(fileName);
            BinaryFormatter bFormatter = new BinaryFormatter();
            mStream.Write(readInput, 0, readInput.Length);
            mStream.Position = 0;
            users = (List<User>)bFormatter.Deserialize(mStream);
        }
    }

    private void GetChildMenus()
    {
        loginScreen1 = GetChildAtPath("Login Screen");
        loginScreen2 = GetChildAtPath("Login Screen 2");
        emailScreen1 = GetChildAtPath("Email Screen");
        emailScreen2 = GetChildAtPath("Email Screen 2");
        notesScreen1 = GetChildAtPath("Notes Screen");
        notesScreen2 = GetChildAtPath("Notes Screen 2");
        appsScreen2 = GetChildAtPath("Apps Screen");
    }

    private void SetLoginMenuText()
    {
        GetChildAtPath("Login Screen/Button_Admin/Text (TMP)").GetComponent<TextMeshProUGUI>().text = users[0].name;
        GetChildAtPath("Login Screen/Button_User1/Text (TMP)").GetComponent<TextMeshProUGUI>().text = users[1].name;
        GetChildAtPath("Login Screen/Button_User2/Text (TMP)").GetComponent<TextMeshProUGUI>().text = users[2].name;
        GetChildAtPath("Login Screen/Button_User3/Text (TMP)").GetComponent<TextMeshProUGUI>().text = users[3].name;
    }

    public static void ActivateGameObject(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public static void DeactivateGameObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    [HideInInspector]
    public User ConvertEnumToUser(EnumUsers p_user)
    {
        switch (p_user)
        {
            case EnumUsers.Admin:
                return users[0];

            case EnumUsers.User1:
                return users[1];

            case EnumUsers.User2:
                return users[2];

            case EnumUsers.User3:
                return users[3];

            default:
                break;
        }
        return null;
    }

    private EnumUsers ConvertUserToEnum(User p_user)
    {
        if (p_user == users[0])
        {
            return EnumUsers.Admin;
        }
        else if (p_user == users[1])
        {
            return EnumUsers.User1;
        }
        else if (p_user == users[2])
        {
            return EnumUsers.User2;
        }
        else if (p_user == users[3])
        {
            return EnumUsers.User3;
        }
        else
        {
            return EnumUsers.None;
        }
    }

    private GameObject GetChildAtPath(string path)
    {
        return gameObject.transform.Find(path).gameObject;
    }

    [HideInInspector]
    public static string FixUnityText(string p_text)
    {
        if (p_text.Length > 0)
        {
            char[] text = p_text.ToCharArray();
            char[] newText = new char[p_text.Length - 1];
            for (int i = 0; i < p_text.Length - 1; i++)
            {
                newText[i] = text[i];
            }
            return newText.ArrayToString();
        }
        else
        {
            return "";
        }
    }

    [HideInInspector]
    public static void ResetList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            list.RemoveAt(i);
        }
    }

    #endregion Core

    #region Login System

    public void ChooseUser(EnumUsers p_user)
    {
        activeUser = ConvertEnumToUser(p_user);
    }

    private void OnEnable()
    {
        AddUserListeners();
    }

    private void SetUser(EnumUsers p_user)
    {
        ChooseUser(p_user);
        SetLoginText();
    }

    private void OnDisable()
    {
        adminButton.onClick.RemoveAllListeners();
        user1Button.onClick.RemoveAllListeners();
        user2Button.onClick.RemoveAllListeners();
        user3Button.onClick.RemoveAllListeners();
    }

    private void AddUserListeners()
    {
        adminButton.onClick.AddListener(() => SetUser(EnumUsers.Admin));
        user1Button.onClick.AddListener(() => SetUser(EnumUsers.User1));
        user2Button.onClick.AddListener(() => SetUser(EnumUsers.User2));
        user3Button.onClick.AddListener(() => SetUser(EnumUsers.User3));
    }

    private void SetLoginText()
    {
        GameObject usernameText = GetChildAtPath("Login Screen 2/Text_Username");
        usernameText.GetComponent<TextMeshProUGUI>().text = activeUser.name;
        GameObject hintText = GetChildAtPath("Login Screen 2/Button_ForgotPassword/Text_Hint");
        hintText.GetComponent<TextMeshProUGUI>().text = activeUser.passwordHint;
    }

    public void LogIn()
    {
        CheckPassword();
        if (isLoggedIn)
        {
            GetChildAtPath("Login Screen 2/Input_Password/Text Area/Text").GetComponent<TextMeshProUGUI>().text = "";
            GetChildAtPath("Login Screen 2/Text_WrongPassword").SetActive(false);
            GetChildAtPath("Login Screen 2/Button_ForgotPassword/Text_Hint").SetActive(false);
            GetChildAtPath("Login Screen 2/Button_ForgotPassword/Text_Forgot").SetActive(true);
            GetChildAtPath("Login Screen 2").SetActive(false);
            GetChildAtPath("Apps Screen").SetActive(true);
        }
    }

    private void CheckPassword()
    {
        if (FixUnityText(GetChildAtPath("Login Screen 2/Input_Password/Text Area/Text").GetComponent<TextMeshProUGUI>().text) == activeUser.password)
        {
            isLoggedIn = true;
        }
        else
        {
            GetChildAtPath("Login Screen 2/Text_WrongPassword").SetActive(true);
            isLoggedIn = false;
        }
    }

    #endregion Login System

    #region Notes System

    public void LoadNotesMenu(Button buttonType)
    {
        ResetList(noteButtons);
        GameObject content = GetChildAtPath("Notes Screen/Window Background/Scroll View/Viewport/Content");
        content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, activeUser.notes.Count * 80);
        if (activeUser.notes.Count > 0)
        {
            buttonType.gameObject.SetActive(true);
            content.transform.Find("Text_NoNotes").gameObject.SetActive(false);
            buttonType.transform.Find("Text_Subject").GetComponent<TextMeshProUGUI>().text = activeUser.notes[0].subject;
            noteButtons.Add(buttonType);
            for (int i = 0; i < activeUser.notes.Count; i++)
            {
                if (i > 0)
                {
                    Button newButton = Instantiate(buttonType);
                    newButton.transform.Find("Text_Subject").GetComponent<TextMeshProUGUI>().text = activeUser.notes[i].subject;
                    newButton.transform.SetParent(buttonType.transform.parent);
                    Vector3 newPos = noteButtons[i - 1].transform.position;
                    newPos.y -= 80;
                    newButton.transform.position = newPos;
                    noteButtons.Add(newButton);
                }
            }
        }
        else
        {
            buttonType.gameObject.SetActive(false);
            content.transform.Find("Text_NoNotes").gameObject.SetActive(true);
        }
    }

    #endregion Notes System
}

[System.Serializable]
public class User
{
    public string name;
    public string password;
    public string passwordHint;

    public List<Note> notes = new List<Note>();
    public List<Email> emails = new List<Email>();
}

[System.Serializable]
public class Note
{
    public string subject = "Subject";
    public string content = "Content";
}

[System.Serializable]
public class Email
{
    public string subject = "Subject";
    public string recipient = "Recipient";
    public string sender = "Sender";
    public string content = "Content";
}