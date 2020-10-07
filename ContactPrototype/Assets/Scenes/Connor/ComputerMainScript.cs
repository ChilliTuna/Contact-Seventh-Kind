using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerMainScript : MonoBehaviour
{
    public List<User> users;

    public string User1Name;
    public string User1Password;
    public string User2Name;
    public string User2Password;
    public string User3Name;
    public string User3Password;
    public string User4Name;
    public string User4Password;

    // Start is called before the first frame update
    void Start()
    {
        CreateUsers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateUsers()
    {
        users.Add(new User(User1Name, User1Password));
        users.Add(new User(User2Name, User2Password));
        users.Add(new User(User3Name, User3Password));
        users.Add(new User(User4Name, User4Password));
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