using System.Collections.Generic;
using UnityEngine;

public class OnOffComponent : MonoBehaviour
{
    [SerializeField] List<ComponentOnOff> components;

    public void OnClickOnOff()
    {
        for (int i = 0; i < components.Count; i++)
        {
            components[i].obj.SetActive(!components[i].isActif);
            components[i].isActif = !components[i].isActif;
        }
    }
}