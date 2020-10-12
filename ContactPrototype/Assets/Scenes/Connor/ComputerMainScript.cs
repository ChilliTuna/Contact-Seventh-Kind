using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerMainScript : MonoBehaviour
{
    #region Variables

    public List<User> users = new List<User>();

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

    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        CreateUsers();
        GetChildMenus();
    }

    #region Core
    private void CreateUsers()
    {
        users.Add(new User(adminName, adminPassword, adminHint));
        users.Add(new User(user1Name, user1Password, user1Hint));
        users.Add(new User(user2Name, user2Password, user2Hint));
        users.Add(new User(user3Name, user3Password, user3Hint));
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

    public void ActivateGameObject(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void DeactivateGameObject(GameObject gameObject)
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

    private string FixUnityText(string p_text)
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
    #endregion

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
    #endregion

    #region Notes System



    #endregion
}

public class User
{
    public string name;
    public string password;
    public string passwordHint;

    public List<Note> notes;
    public List<Email> emails;

    public User(string p_name, string p_password, string p_passwordHint)
    {
        name = p_name;
        password = p_password;
        passwordHint = p_passwordHint;
    }
}

public class Note
{
    public string subject;
    public string content;
}

public class Email
{
    public string subject;
    public string recipient;
    public string sender;
    public string content;
}