using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Operations;

public class MachineSlot : MonoBehaviour
{
    [SerializeField]
    private Player playerScript = null;
    [SerializeField]
    private Lever leverScript = null;
    [SerializeField]
    private AudioSource cameraAudioSource = null;
    [SerializeField]
    private AudioSource audioSource = null;
    [SerializeField]
    private AudioClip gameOverSong = null;
    [SerializeField]
    private AudioClip bigWinSFX = null;
    [SerializeField]
    private AudioClip normalWinSFX = null;
    [SerializeField]
    private AudioClip loseSFX = null;
    [SerializeField]
    private Reel reel0 = null;
    [SerializeField]
    private Reel reel1 = null;
    [SerializeField]
    private Reel reel2 = null;

    private bool CheckWinningCombinations()
    {
        if (
            reel0.GetSlotId() == 0
            && reel1.GetSlotId() == 0
            && reel2.GetSlotId() == 0
        )
        {
            audioSource.PlayOneShot(bigWinSFX, 1.0f);
            playerScript.AddBalance(100, 100);
            return true;
        } else if (
            reel0.GetSlotId() == reel1.GetSlotId()
            && reel0.GetSlotId() == reel2.GetSlotId()
        )
        {
            audioSource.PlayOneShot(normalWinSFX, 1.0f);
            playerScript.AddBalance(100, 10);
            return true;
        }
        audioSource.PlayOneShot(loseSFX, 1.0f);
        return false;
    }

    public void SpinSlots()
    {
        playerScript.ReduceBalance(100);

        reel0.AssignSlotId(Probability.GetOdd());
        reel1.AssignSlotId(Probability.GetOdd());
        reel2.AssignSlotId(Probability.GetOdd());

        reel0.StartSpinning();
        reel1.StartSpinning();
        reel2.StartSpinning();
    }

    public void CheckResults()
    {
        bool playerWon = CheckWinningCombinations();
        if (!playerWon && playerScript.CheckBalance() <= 0)
        {
            cameraAudioSource.clip = gameOverSong;
            cameraAudioSource.Play();
            return;
        }
        leverScript.TurnOnTouchable();
    }
}
