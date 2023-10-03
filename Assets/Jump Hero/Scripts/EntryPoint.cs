using Scellecs.Morpeh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Installer installer;
    private void Awake()
    {
        installer.gameObject.SetActive(true);
    }
}
