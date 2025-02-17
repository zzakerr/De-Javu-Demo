using TMPro;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    public void Show()
    {
        OnePersonCamera.Instance.CursorLock(false);
        inputField.gameObject.SetActive(true);
        inputField.onEndEdit.AddListener(Save);
    }
    
    private void Save(string arg0)
    {
        Debug.Log("Name Saved: " + arg0);
        Player.Instance.ReturnCamera();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        inputField.onValueChanged.RemoveAllListeners();
    }
}
