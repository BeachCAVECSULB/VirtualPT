using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditorInternal;
using UnityEditor;
using UnityEngine.UIElements;

#endif

public class TactorPlayer : MonoBehaviour
{
    public GameObject tactorAutoSetup;
    public List<TactorAction> tactorActions = new List<TactorAction>(1);
    private TactorAutoSetup tas;
    
    // Start is called before the first frame update
    void Start()
    {
        tas = tactorAutoSetup.GetComponent<TactorAutoSetup>();
    }

    public void Play()
    {
        StartCoroutine(PlayActions());
    }

    private IEnumerator PlayActions()
    {
        foreach (var ta in tactorActions)
        {
            tas.SetGain(ta.tactorIndex, ta.gain);
            tas.SetFreq(ta.tactorIndex, ta.frequency);
            tas.SetDuration(ta.tactorIndex, ta.durationInMs);
            tas.FireTactor(ta.tactorIndex);
            yield return new WaitForSeconds(ta.delayInSeconds);
        }
    }

    [Serializable]
    public class TactorAction
    {
        public int tactorIndex = 0;
        [Range(300,3550)]
        public int frequency = 1300;
        [Range(0,255)]
        public int gain = 128;
        public int durationInMs = 150;
        public float delayInSeconds = 0;
    }
}