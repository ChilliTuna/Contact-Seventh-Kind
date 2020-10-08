using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerMainScript : MonoBehaviour
{
    public List<User> users = new List<User>();

    public string User1Name;
    public string User1Password;
    public string User2Name;
    public string User2Password;
    public string User3Name;
    public string User3Password;
    public string User4Name;
    public string User4Password;

    private GameObject loginScreen1;
    private GameObject loginScreen2;
    private GameObject emailScreen1;
    private GameObject emailScreen2;
    private GameObject notesScreen1;
    private GameObject notesScreen2;
    private GameObject appsScreen2;

    private User activeUser;

    public enum EnumUsers
    {
        User1,
        User2,
        User3,
        User4
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
        users.Add(new User(User1Name, User1Password));
        users.Add(new User(User2Name, User2Password));
        users.Add(new User(User3Name, User3Password));
        users.Add(new User(User4Name, User4Password));
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

    public void ChooseUser(EnumUsers p_user)
    {
        activeUser = ConvertEnumToUser(p_user);
    }

    private User ConvertEnumToUser(EnumUsers p_user)
    {
        switch (p_user)
        {
            case EnumUsers.User1:
                return users[0];
            case EnumUsers.User2:
                return users[1];
            case EnumUsers.User3:
                return users[2];
            case EnumUsers.User4:
                return users[3];
            default:
                break;
        }
        return null;
    }
}

public class User
{
    public string name;
    public string password;

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