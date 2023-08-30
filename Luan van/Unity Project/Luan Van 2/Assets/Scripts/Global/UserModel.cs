using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserModel 
{
    [SerializeField] private string id;
    [SerializeField] private string name;
    [SerializeField] private string username;

    public string Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string Username { get => username; set => username = value; }
}
