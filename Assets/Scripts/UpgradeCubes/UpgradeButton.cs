using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    private enum variableNames
    {
        // Cube 2
        cube2_CoolDownTime,
        cube2_BulletSpeed,
        cube2_BulletSurvivalTime,
        cube2_BulletSize,

        // Cube 4
        cube4_CoolDownTime,
        cube4_BulletSpeed,
        cube4_BulletSurvivalTime,
        cube4_BulletFlyBackTime,
        cube4_BulletSize,

        // Cube 8
        cube8_CoolDownTime,
        cube8_BulletSpeed,
        cube8_NumBullets,
        cube8_BulletRotationAngle,
        cube8_BulletSurvivalTime,
        cube8_BulletSize,

        // Cube 16
        cube16_CoolDownTime,
        cube16_BulletSpeed,
        cube16_BulletSurvivalTime,
        cube16_BulletSize,
        cube16_FrozenEffectiveTime,
        cube16_FrozenEffectSurvivalTime,

        // Cube 32
        cube32_CoolDownTime,
        cube32_BulletSpeed,
        cube32_BulletSurvivalTime,
        cube32_BulletSize,
        cube32_FireEffectiveTime,
        cube32_FireEffectSurvivalTime,

        // Cube 64
        cube64_CoolDownTime,
        cube64_BulletSpeed,
        cube64_BulletSurvivalTime,
        cube64_BulletSize,
        cube64_NumHits,
        cube64_FindEnemyRadius
    }
    [SerializeField] variableNames featureName;
    [SerializeField] Image[] images;

    private float floatFeature1 = 0f;
    private float floatFeature2 = 0f;
    private float intFeature = 0;

    private float[] floatFeatureList1;
    private float[] floatFeatureList2;
    private int[] intFeatureList;

    private void Start()
    {
        FindFeatureList();
    }

    private void Update()
    {
        FindFeature();

        if (floatFeature1 > 0 && intFeature == 0)
        {
            DisplayBarOneOrTwoFloatFeature();
        }
        else if (intFeature > 0)
        {
            DisplayBarOneIntFeature();
        }
    }

    public void Upgrade()
    {
        int currentCoins = DataPersistenceManager.instance.gameData.totalCoins;
        if (currentCoins < 1000) return;

        SoundManager.Instance.PlaySound(SoundManager.SoundEffects.UpgradeCubeSound);
        SoundManager.Instance.PlaySound(SoundManager.SoundEffects.SpendMoneySound);

        FindFeature();

        if (floatFeature1 > 0 && intFeature == 0)
        {
            SaveDataOneOrTwoFloatFeature();
        }
        else if (intFeature > 0)
        {
            SaveDataOneIntFeature();
        }

    }

    public void SaveDataOneOrTwoFloatFeature()
    {
        if (floatFeature1 == floatFeatureList1[0])
        {
            SaveData(1);
        }
        else if (floatFeature1 == floatFeatureList1[1])
        {
            SaveData(2);
        }
        else if (floatFeature1 == floatFeatureList1[2])
        {
            SaveData(3);
        }
        else if (floatFeature1 == floatFeatureList1[3])
        {
            SaveData(4);
        }
        else if (floatFeature1 == floatFeatureList1[4])
        {
            //
        }
        else
        {
            SaveData(0);
        }
    }

    public void SaveDataOneIntFeature()
    {
        if (intFeature == intFeatureList[0])
        {
            SaveData(1);
        }
        else if (intFeature == intFeatureList[1])
        {
            SaveData(2);
        }
        else if (intFeature == intFeatureList[2])
        {
            SaveData(3);
        }
        else if (intFeature == intFeatureList[3])
        {
            SaveData(4);
        }
        else if (intFeature == intFeatureList[4])
        {
            //
        }
        else
        {
            SaveData(0);
        }
    }

    public void SaveData(int valueIndex)
    {
        DataPersistenceManager.instance.gameData.totalCoins -= 1000;
        FindObjectOfType<Minus1000TextAnimation>().StartAnimation();

        switch (featureName)
        {   //Cube2
            case variableNames.cube2_CoolDownTime:
                DataPersistenceManager.instance.gameData.cube2_CoolDownTime = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube2_BulletSpeed:
                DataPersistenceManager.instance.gameData.cube2_BulletSpeed = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube2_BulletSurvivalTime:
                DataPersistenceManager.instance.gameData.cube2_BulletSurvivalTime = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube2_BulletSize:
                DataPersistenceManager.instance.gameData.cube2_BulletSize = floatFeatureList1[valueIndex];
                break;
            //Cube4
            case variableNames.cube4_CoolDownTime:
                DataPersistenceManager.instance.gameData.cube4_CoolDownTime = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube4_BulletSpeed:
                DataPersistenceManager.instance.gameData.cube4_BulletSpeed = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube4_BulletSurvivalTime:
                DataPersistenceManager.instance.gameData.cube4_BulletSurvivalTime = floatFeatureList1[valueIndex];
                DataPersistenceManager.instance.gameData.cube4_BulletFlyBackTime = floatFeatureList2[valueIndex];
                break;
            case variableNames.cube4_BulletSize:
                DataPersistenceManager.instance.gameData.cube4_BulletSize = floatFeatureList1[valueIndex];
                break;
            //Cube8
            case variableNames.cube8_CoolDownTime:
                DataPersistenceManager.instance.gameData.cube8_CoolDownTime = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube8_BulletSpeed:
                DataPersistenceManager.instance.gameData.cube8_BulletSpeed = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube8_NumBullets:
                DataPersistenceManager.instance.gameData.cube8_NumBullets = intFeatureList[valueIndex];
                DataPersistenceManager.instance.gameData.cube8_BulletRotationAngle = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube8_BulletSurvivalTime:
                DataPersistenceManager.instance.gameData.cube8_BulletSurvivalTime = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube8_BulletSize:
                DataPersistenceManager.instance.gameData.cube8_BulletSize = floatFeatureList1[valueIndex];
                break;
            //Cube16
            case variableNames.cube16_CoolDownTime:
                DataPersistenceManager.instance.gameData.cube16_CoolDownTime = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube16_BulletSpeed:
                DataPersistenceManager.instance.gameData.cube16_BulletSpeed = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube16_BulletSurvivalTime:
                DataPersistenceManager.instance.gameData.cube16_BulletSurvivalTime = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube16_BulletSize:
                DataPersistenceManager.instance.gameData.cube16_BulletSize = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube16_FrozenEffectiveTime:
                DataPersistenceManager.instance.gameData.cube16_FrozenEffectiveTime = floatFeatureList1[valueIndex];
                DataPersistenceManager.instance.gameData.cube16_FrozenEffectSurvivalTime = floatFeatureList2[valueIndex];
                break;
            //Cube32
            case variableNames.cube32_CoolDownTime:
                DataPersistenceManager.instance.gameData.cube32_CoolDownTime = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube32_BulletSpeed:
                DataPersistenceManager.instance.gameData.cube32_BulletSpeed = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube32_BulletSurvivalTime:
                DataPersistenceManager.instance.gameData.cube32_BulletSurvivalTime = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube32_BulletSize:
                DataPersistenceManager.instance.gameData.cube32_BulletSize = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube32_FireEffectiveTime:
                DataPersistenceManager.instance.gameData.cube32_FireEffectiveTime = floatFeatureList1[valueIndex];
                DataPersistenceManager.instance.gameData.cube32_FireEffectSurvivalTime = floatFeatureList2[valueIndex];
                break;
            //Cube64
            case variableNames.cube64_CoolDownTime:
                DataPersistenceManager.instance.gameData.cube64_CoolDownTime = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube64_BulletSpeed:
                DataPersistenceManager.instance.gameData.cube64_BulletSpeed = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube64_BulletSurvivalTime:
                DataPersistenceManager.instance.gameData.cube64_BulletSurvivalTime = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube64_BulletSize:
                DataPersistenceManager.instance.gameData.cube64_BulletSize = floatFeatureList1[valueIndex];
                break;
            case variableNames.cube64_NumHits:
                DataPersistenceManager.instance.gameData.cube64_NumHits = intFeatureList[valueIndex];
                break;
            case variableNames.cube64_FindEnemyRadius:
                DataPersistenceManager.instance.gameData.cube64_FindEnemyRadius = floatFeatureList1[valueIndex];
                break;
        }

    }

    private void FindFeatureList()
    {
        switch (featureName)
        {   //Cube2
            case variableNames.cube2_CoolDownTime:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube2_CoolDownTimeUpgradeList;
                break;
            case variableNames.cube2_BulletSpeed:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube2_BulletSpeedUpgradeList;
                break;
            case variableNames.cube2_BulletSurvivalTime:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube2_BulletSurvivalTimeUpgradeList;
                break;
            case variableNames.cube2_BulletSize:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube2_BulletSizeUpgradeList;
                break;
            //Cube4
            case variableNames.cube4_CoolDownTime:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube4_CoolDownTimeUpgradeList;
                break;
            case variableNames.cube4_BulletSpeed:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube4_BulletSpeedUpgradeList;
                break;
            case variableNames.cube4_BulletSurvivalTime:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube4_BulletSurvivalTimeUpgradeList;
                floatFeatureList2 = DataPersistenceManager.instance.gameData.cube4_BulletFlyBackTimeUpgradeList;
                break;
            case variableNames.cube4_BulletSize:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube4_BulletSizeUpgradeList;
                break;
            //Cube8
            case variableNames.cube8_CoolDownTime:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube8_CoolDownTimeUpgradeList;
                break;
            case variableNames.cube8_BulletSpeed:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube8_BulletSpeedUpgradeList;
                break;
            case variableNames.cube8_NumBullets:
                intFeatureList = DataPersistenceManager.instance.gameData.cube8_NumBulletsUpgradeList;
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube8_BulletRotationAngleUpgradeList;
                break;
            case variableNames.cube8_BulletSurvivalTime:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube8_BulletSurvivalTimeUpgradeList;
                break;
            case variableNames.cube8_BulletSize:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube8_BulletSizeUpgradeList;
                break;
            //Cube16
            case variableNames.cube16_CoolDownTime:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube16_CoolDownTimeUpgradeList;
                break;
            case variableNames.cube16_BulletSpeed:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube16_BulletSpeedUpgradeList;
                break;
            case variableNames.cube16_BulletSurvivalTime:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube16_BulletSurvivalTimeUpgradeList;
                break;
            case variableNames.cube16_BulletSize:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube16_BulletSizeUpgradeList;
                break;
            case variableNames.cube16_FrozenEffectiveTime:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube16_FrozenEffectiveTimeUpgradeList;
                floatFeatureList2 = DataPersistenceManager.instance.gameData.cube16_FrozenEffectSurvivalTimeUpgradeList;
                break;
            //Cube32
            case variableNames.cube32_CoolDownTime:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube32_CoolDownTimeUpgradeList;
                break;
            case variableNames.cube32_BulletSpeed:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube32_BulletSpeedUpgradeList;
                break;
            case variableNames.cube32_BulletSurvivalTime:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube32_BulletSurvivalTimeUpgradeList;
                break;
            case variableNames.cube32_BulletSize:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube32_BulletSizeUpgradeList;
                break;
            case variableNames.cube32_FireEffectiveTime:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube32_FireEffectiveTimeUpgradeList;
                floatFeatureList2 = DataPersistenceManager.instance.gameData.cube32_FireEffectSurvivalTimeUpgradeList;
                break;
            //Cube64
            case variableNames.cube64_CoolDownTime:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube64_CoolDownTimeUpgradeList;
                break;
            case variableNames.cube64_BulletSpeed:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube64_BulletSpeedUpgradeList;
                break;
            case variableNames.cube64_BulletSurvivalTime:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube64_BulletSurvivalTimeUpgradeList;
                break;
            case variableNames.cube64_BulletSize:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube64_BulletSizeUpgradeList;
                break;
            case variableNames.cube64_NumHits:
                intFeatureList = DataPersistenceManager.instance.gameData.cube64_NumHitsUpgradeList;
                break;
            case variableNames.cube64_FindEnemyRadius:
                floatFeatureList1 = DataPersistenceManager.instance.gameData.cube64_FindEnemyRadiusUpgradeList;
                break;
        }
    }

    private void FindFeature()
    {
        switch (featureName)
        {   //Cube2
            case variableNames.cube2_CoolDownTime:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube2_CoolDownTime;
                break;
            case variableNames.cube2_BulletSpeed:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube2_BulletSpeed;
                break;
            case variableNames.cube2_BulletSurvivalTime:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube2_BulletSurvivalTime;
                break;
            case variableNames.cube2_BulletSize:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube2_BulletSize;
                break;
            //Cube4
            case variableNames.cube4_CoolDownTime:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube4_CoolDownTime;
                break;
            case variableNames.cube4_BulletSpeed:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube4_BulletSpeed;
                break;
            case variableNames.cube4_BulletSurvivalTime:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube4_BulletSurvivalTime;
                floatFeature2 = DataPersistenceManager.instance.gameData.cube4_BulletFlyBackTime;
                break;
            case variableNames.cube4_BulletSize:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube4_BulletSize;
                break;
            //Cube8
            case variableNames.cube8_CoolDownTime:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube8_CoolDownTime;
                break;
            case variableNames.cube8_BulletSpeed:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube8_BulletSpeed;
                break;
            case variableNames.cube8_NumBullets:
                intFeature = DataPersistenceManager.instance.gameData.cube8_NumBullets;
                floatFeature1 = DataPersistenceManager.instance.gameData.cube8_BulletRotationAngle;
                break;
            case variableNames.cube8_BulletSurvivalTime:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube8_BulletSurvivalTime;
                break;
            case variableNames.cube8_BulletSize:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube8_BulletSize;
                break;
            //Cube16
            case variableNames.cube16_CoolDownTime:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube16_CoolDownTime;
                break;
            case variableNames.cube16_BulletSpeed:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube16_BulletSpeed;
                break;
            case variableNames.cube16_BulletSurvivalTime:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube16_BulletSurvivalTime;
                break;
            case variableNames.cube16_BulletSize:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube16_BulletSize;
                break;
            case variableNames.cube16_FrozenEffectiveTime:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube16_FrozenEffectiveTime;
                floatFeature2 = DataPersistenceManager.instance.gameData.cube16_FrozenEffectSurvivalTime;
                break;
            //Cube32
            case variableNames.cube32_CoolDownTime:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube32_CoolDownTime;
                break;
            case variableNames.cube32_BulletSpeed:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube32_BulletSpeed;
                break;
            case variableNames.cube32_BulletSurvivalTime:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube32_BulletSurvivalTime;
                break;
            case variableNames.cube32_BulletSize:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube32_BulletSize;
                break;
            case variableNames.cube32_FireEffectiveTime:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube32_FireEffectiveTime;
                floatFeature2 = DataPersistenceManager.instance.gameData.cube32_FireEffectSurvivalTime;
                break;
            //Cube64
            case variableNames.cube64_CoolDownTime:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube64_CoolDownTime;
                break;
            case variableNames.cube64_BulletSpeed:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube64_BulletSpeed;
                break;
            case variableNames.cube64_BulletSurvivalTime:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube64_BulletSurvivalTime;
                break;
            case variableNames.cube64_BulletSize:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube64_BulletSize;
                break;
            case variableNames.cube64_NumHits:
                intFeature = DataPersistenceManager.instance.gameData.cube64_NumHits;
                break;
            case variableNames.cube64_FindEnemyRadius:
                floatFeature1 = DataPersistenceManager.instance.gameData.cube64_FindEnemyRadius;
                break;
        }
    }

    private void DisplayBarOneOrTwoFloatFeature()
    {
        if (floatFeature1 == floatFeatureList1[0])
        {
            images[0].color = Color.green;
            images[1].color = Color.white;
            images[2].color = Color.white;
            images[3].color = Color.white;
            images[4].color = Color.white;
        }
        else if (floatFeature1 == floatFeatureList1[1])
        {
            images[0].color = Color.green;
            images[1].color = Color.green;
            images[2].color = Color.white;
            images[3].color = Color.white;
            images[4].color = Color.white;
        }
        else if (floatFeature1 == floatFeatureList1[2])
        {
            images[0].color = Color.green;
            images[1].color = Color.green;
            images[2].color = Color.green;
            images[3].color = Color.white;
            images[4].color = Color.white;
        }
        else if (floatFeature1 == floatFeatureList1[3])
        {
            images[0].color = Color.green;
            images[1].color = Color.green;
            images[2].color = Color.green;
            images[3].color = Color.green;
            images[4].color = Color.white;
        }
        else if (floatFeature1 == floatFeatureList1[4])
        {
            images[0].color = Color.green;
            images[1].color = Color.green;
            images[2].color = Color.green;
            images[3].color = Color.green;
            images[4].color = Color.green;
        }
        else
        {
            images[0].color = Color.white;
            images[1].color = Color.white;
            images[2].color = Color.white;
            images[3].color = Color.white;
            images[4].color = Color.white;
        }
    }

    private void DisplayBarOneIntFeature()
    {
        if (intFeature == intFeatureList[0])
        {
            images[0].color = Color.green;
            images[1].color = Color.white;
            images[2].color = Color.white;
            images[3].color = Color.white;
            images[4].color = Color.white;
        }
        else if (intFeature == intFeatureList[1])
        {
            images[0].color = Color.green;
            images[1].color = Color.green;
            images[2].color = Color.white;
            images[3].color = Color.white;
            images[4].color = Color.white;
        }
        else if (intFeature == intFeatureList[2])
        {
            images[0].color = Color.green;
            images[1].color = Color.green;
            images[2].color = Color.green;
            images[3].color = Color.white;
            images[4].color = Color.white;
        }
        else if (intFeature == intFeatureList[3])
        {
            images[0].color = Color.green;
            images[1].color = Color.green;
            images[2].color = Color.green;
            images[3].color = Color.green;
            images[4].color = Color.white;
        }
        else if (intFeature == intFeatureList[4])
        {
            images[0].color = Color.green;
            images[1].color = Color.green;
            images[2].color = Color.green;
            images[3].color = Color.green;
            images[4].color = Color.green;
        }
        else
        {
            images[0].color = Color.white;
            images[1].color = Color.white;
            images[2].color = Color.white;
            images[3].color = Color.white;
            images[4].color = Color.white;
        }
    }

}
