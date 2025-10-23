using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event jumpEvent;

    private PlayerMovement playerMovement;

    
    void Awake ()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.OnJumped += OnJumped;
    }

    private void OnDestroy ()
    {
        playerMovement.OnJumped -= OnJumped;
    }


    private void OnJumped ()
    {
        AkSoundEngine.PostEvent(jumpEvent.Id, gameObject);
    }
}