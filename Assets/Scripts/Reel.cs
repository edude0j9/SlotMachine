using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{
    [SerializeField]
    private MachineSlot machineSlotScript = null;
    [SerializeField]
    private Sprite slot0 = null;
    [SerializeField]
    private Sprite slot1 = null;
    [SerializeField]
    private Sprite slot2 = null;
    [SerializeField]
    private Sprite slot3 = null;
    [SerializeField]
    private Sprite slot4 = null;
    [SerializeField]
    private Sprite slot5 = null;
    [SerializeField]
    private Sprite slot6 = null;
    [SerializeField]
    private SpriteRenderer spriteRenderer = null;
    [SerializeField]
    private int reelId = 0;
    [SerializeField]
    private AudioClip slotSpinSFX = null;
    [SerializeField]
    private AudioClip slotStopSFX = null;
    [SerializeField]
    private AudioSource audioSource = null;

    private Sprite[] slotSprites;
    private int slotId = 0;
    private int spinningSlotId = 0;
    private bool spinning = false;
    private float spinTime = 0.1f;

    private float GetSpinningTimeout()
    {
        return 2f + reelId;
    }

    private void Start()
    {
        slotSprites = new Sprite[] {slot0, slot1, slot3, slot4, slot5, slot6};
    }

    private IEnumerator ContinueSpinning()
    {
        yield return new WaitForSeconds(spinTime);
        spinningSlotId = spinningSlotId >= 5 ? 0 : spinningSlotId + 1;
        if (spinning)
        {
            SetSlot(spinningSlotId);
            audioSource.PlayOneShot(slotSpinSFX, 0.3f);
            StartCoroutine(ContinueSpinning());
        }
    }

    private IEnumerator StopSpinning()
    {
        yield return new WaitForSeconds(GetSpinningTimeout());
        audioSource.PlayOneShot(slotStopSFX, 0.7f);
        SetSlot(slotId);
        spinning = false;
        if (reelId == 2)
        {
            machineSlotScript.CheckResults();
        }
    }

    public void StartSpinning()
    {
        spinning = true;
        StartCoroutine(ContinueSpinning());
        StartCoroutine(StopSpinning());
    }

    public void AssignSlotId(int slot)
    {
        slotId = slot;
    }

    public int GetSlotId()
    {
        return slotId;
    }

    private void SetSlot(int slot)
    {
        spriteRenderer.sprite = slotSprites[slot];
    }
}
