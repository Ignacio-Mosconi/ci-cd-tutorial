using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class VersionText : MonoBehaviour
{
    [SerializeField] private string labelPrefix = "v";

    private TMP_Text versionText;
    
    
    void Awake ()
    {
        versionText = GetComponent<TMP_Text>();
        versionText.text = $"{labelPrefix}{Application.version}";
    }
}
