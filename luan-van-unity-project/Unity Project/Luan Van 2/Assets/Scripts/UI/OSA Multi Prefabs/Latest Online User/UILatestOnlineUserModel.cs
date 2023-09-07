using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    [System.Serializable]
    public class UILatestOnlineUserModel 
    {
        [SerializeField] private string id;
        [SerializeField] private string name;
        [SerializeField] private string username;
        [SerializeField] private string createdAt;
        [SerializeField] private string updatedAt;

        [SerializeField] private int itemIndexOSA;
        [SerializeField] private LatestOnlineUserItemViewsHolder viewsHolder;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Username { get => username; set => username = value; }
        public string CreatedAt { get => createdAt; set => createdAt = value; }
        public string UpdatedAt { get => updatedAt; set => updatedAt = value; }
        public int ItemIndexOSA { get => itemIndexOSA; set => itemIndexOSA = value; }
        public LatestOnlineUserItemViewsHolder ViewsHolder { get => viewsHolder; set => viewsHolder = value; }
    }
}
