using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCollect_Tactors : MonoBehaviour
{
    private static DataCollect_Tactors instance;

    [SerializeField] private GameObject alertArms;
    [SerializeField] private GameObject alertLegs;

    [SerializeField] private GameObject goLeftArm;
    [SerializeField] private GameObject goRightArm;

    [SerializeField] private GameObject goLeftLeg;
    [SerializeField] private GameObject goRightLeg;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public static void ActivateTactor(int input)
    {
        instance.alertArms.SetActive(false);
        instance.alertLegs.SetActive(false);
        instance.goLeftArm.SetActive(false);
        instance.goRightArm.SetActive(false);
        instance.goLeftLeg.SetActive(false);
        instance.goRightLeg.SetActive(false);

        if (input == 2 && instance.alertArms != null)
            instance.alertArms.SetActive(true);
        else if (input == 3 && instance.alertLegs != null)
            instance.alertLegs.SetActive(true);
        else if (input == 4)
        {
            if(instance.goLeftArm)
                instance.goLeftArm.SetActive(true);
            if (instance.goRightArm)
                instance.goRightArm.SetActive(true);
        }
        else if (input == 5)
        {
            if (instance.goLeftLeg)
                instance.goLeftLeg.SetActive(true);
            if (instance.goRightLeg)
                instance.goRightLeg.SetActive(true);
        }
        else if(input == 1)
        {
            instance.alertArms.SetActive(false);
            instance.alertLegs.SetActive(false);
            instance.goLeftArm.SetActive(false);
            instance.goRightArm.SetActive(false);
            instance.goLeftLeg.SetActive(false);
            instance.goRightLeg.SetActive(false);
        }
            
    }

    public static int GetTactorsOn()
    {
        if (instance.alertArms != null)
            if (instance.alertArms.activeInHierarchy)
                return 2;

        if (instance.alertLegs != null)
            if (instance.alertLegs.activeInHierarchy)
                return 3;

        if (instance.goLeftArm != null && instance.goRightArm != null)
            if (instance.goLeftArm.activeInHierarchy && instance.goRightArm.activeInHierarchy)
                return 4;

        if (instance.goLeftLeg != null && instance.goRightLeg != null)
            if (instance.goLeftLeg.activeInHierarchy && instance.goRightLeg.activeInHierarchy)
                return 5;

        return 1;
    }

    //new coroutine function. use with input 4 or 5 or both?
    private IEnumerator ActivateTactorsForDuration(int tactorType)
    {
        ActivateTactor(tactorType);

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Deactivate tactors after 2 seconds
        ActivateTactor(1); // Assuming 1 is the code to deactivate all tactors
    }

}
