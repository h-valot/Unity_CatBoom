using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private RSE_Input inputArrow;

    public void Update() => inputArrow.Call(Input.GetKey(KeyCode.RightArrow) ? (Input.GetKey(KeyCode.LeftArrow) ? 0 : 1) : (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0));
}