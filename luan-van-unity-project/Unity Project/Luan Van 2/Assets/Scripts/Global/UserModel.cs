using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserModel 
{
    [SerializeField] private string id;
    [SerializeField] private string name;
    [SerializeField] private string username;
    [SerializeField] private string createdAt;
    [SerializeField] private string updatedAt;
    [SerializeField] private string avatarPath;

    public string Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string Username { get => username; set => username = value; }
    public string CreatedAt { get => createdAt; set => createdAt = value; }
    public string UpdatedAt { get => updatedAt; set => updatedAt = value; }
    public string AvatarPath { get => avatarPath; set => avatarPath = value; }
}
