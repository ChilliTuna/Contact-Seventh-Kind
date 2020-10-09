using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerMainScript : MonoBehaviour
{
    public List<User> users = new List<User>();

    public string adminName;
    public string adminPassword;
    public string adminHint;
    public string user1Name;
    public string user1Password;
    public string user1Hint;
    public string user2Name;
    public string user2Password;
    public string user2Hint;
    public string user3Name;
    public string user3Password;
    public string user3Hint;

    public Button userButtonAdmin;
    public Button userButton1;
    public Button userButton2;
    public Button userButton3;

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

    // Start is called before the first frame update
    private void Start()
    {
        CreateUsers();
        GetChildMenus();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void CreateUsers()
    {
        users.Add(new User(adminName, adminPassword));
        users.Add(new User(user1Name, user1Password));
        users.Add(new User(user2Name, user2Password));
        users.Add(new User(user3Name, user3Password));
    }

    private void GetChildMenus()
    {
        loginScreen1 = gameObject.transform.Find("Login Screen").gameObject;
        loginScreen2 = gameObject.transform.Find("Login Screen 2").gameObject;
        emailScreen1 = gameObject.transform.Find("Email Screen").gameObject;
        emailScreen2 = gameObject.transform.Find("Email Screen 2").gameObject;
        notesScreen1 = gameObject.transform.Find("Notes Screen").gameObject;
        notesScreen2 = gameObject.transform.Find("Notes Screen 2").gameObject;
        appsScreen2 = gameObject.transform.Find("Apps Screen").gameObject;
    }

    public void ActivateGameObject(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void DeactivateGameObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void ChooseUser(EnumUsers p_user)
    {
        activeUser = ConvertEnumToUser(p_user);
    }

    private User ConvertEnumToUser(EnumUsers p_user)
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

    //Login System
    private void OnEnable()
    {
        AddUserListeners();
    }

    private void SetUserAdmin()
    {
        ChooseUser(EnumUsers.Admin);
        SetLoginText();
    }

    private void SetUser1()
    {
        ChooseUser(EnumUsers.User1);
        SetLoginText();
    }

    private void SetUser2()
    {
        ChooseUser(EnumUsers.User2);
        SetLoginText();
    }

    private void SetUser3()
    {
        ChooseUser(EnumUsers.User3);
        SetLoginText();
    }

    private void OnDisable()
    {
        userButtonAdmin.onClick.RemoveAllListeners();
        userButton1.onClick.RemoveAllListeners();
        userButton2.onClick.RemoveAllListeners();
        userButton3.onClick.RemoveAllListeners();
    }

    private void AddUserListeners()
    {
        userButtonAdmin.onClick.AddListener(() => SetUserAdmin());
        userButton1.onClick.AddListener(() => SetUser1());
        userButton2.onClick.AddListener(() => SetUser2());
        userButton3.onClick.AddListener(() => SetUser3());
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
        }
    }
}

public class User
{
    public string name;
    public string password;
    public string passwordHint;

    public List<Note> notes;
    public List<Email> emails;

    public User(string p_name, string p_password)
    {
        name = p_name;
        password = p_password;
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