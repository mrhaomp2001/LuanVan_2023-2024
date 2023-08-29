using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSetting : MonoBehaviour
{
    private static string endpoint;

    [SerializeField] private string endpointInit;

    public static string Endpoint { get => endpoint; set => endpoint = value; }

    private void Start()
    {
        endpoint = endpointInit;
    }
}
