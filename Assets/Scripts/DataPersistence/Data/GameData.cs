using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int totalCoins;

    // Cube 2
    public float cube2_CoolDownTime;
    public float[] cube2_CoolDownTimeUpgradeList = new float[] { 0.18f, 0.16f, 0.14f, 0.12f, 0.1f};
    public float cube2_BulletSpeed;
    public float[] cube2_BulletSpeedUpgradeList = new float[] { 25f, 30f, 35f, 40f, 45f};
    public float cube2_BulletSurvivalTime;
    public float[] cube2_BulletSurvivalTimeUpgradeList = new float[] { 0.7f, 0.9f, 1.1f, 1.3f, 1.5f};
    public float cube2_BulletSize;
    public float[] cube2_BulletSizeUpgradeList = new float[] { 0.5f, 0.7f, 0.9f, 1.1f, 1.3f};

    // Cube 4
    public float cube4_CoolDownTime;
    public float[] cube4_CoolDownTimeUpgradeList = new float[] { 0.45f, 0.4f, 0.35f, 0.3f, 0.25f};
    public float cube4_BulletSpeed;
    public float[] cube4_BulletSpeedUpgradeList = new float[] { 25f, 30f, 35f, 40f, 45f };
    public float cube4_BulletSurvivalTime;
    public float[] cube4_BulletSurvivalTimeUpgradeList = new float[] { 1.4f, 1.8f, 2.2f, 2.6f, 3f};
    public float cube4_BulletFlyBackTime;
    public float[] cube4_BulletFlyBackTimeUpgradeList = new float[] { 0.7f, 0.9f, 1.1f, 1.3f, 1.5f};
    public float cube4_BulletSize;
    public float[] cube4_BulletSizeUpgradeList = new float[] { 1f, 1.2f, 1.4f, 1.6f, 1.8f};

    // Cube 8
    public float cube8_CoolDownTime;
    public float[] cube8_CoolDownTimeUpgradeList = new float[] { 0.45f, 0.4f, 0.35f, 0.3f, 0.25f };
    public float cube8_BulletSpeed;
    public float[] cube8_BulletSpeedUpgradeList = new float[] { 25f, 30f, 35f, 40f, 45f };
    public int cube8_NumBullets;
    public int[] cube8_NumBulletsUpgradeList = new int[] { 7, 9, 11, 13, 15};
    public float cube8_BulletRotationAngle;
    public float[] cube8_BulletRotationAngleUpgradeList = new float[] { 25f, 20f, 15f, 10f, 5f };
    public float cube8_BulletSurvivalTime;
    public float[] cube8_BulletSurvivalTimeUpgradeList = new float[] { 0.7f, 0.9f, 1.1f, 1.3f, 1.5f };
    public float cube8_BulletSize;
    public float[] cube8_BulletSizeUpgradeList = new float[] { 1f, 1.2f, 1.4f, 1.6f, 1.8f };

    // Cube 16
    public float cube16_CoolDownTime;
    public float[] cube16_CoolDownTimeUpgradeList = new float[] { 0.45f, 0.4f, 0.35f, 0.3f, 0.25f };
    public float cube16_BulletSpeed;
    public float[] cube16_BulletSpeedUpgradeList = new float[] { 25f, 30f, 35f, 40f, 45f };
    public float cube16_BulletSurvivalTime;
    public float[] cube16_BulletSurvivalTimeUpgradeList = new float[] { 1.2f, 1.4f, 1.6f, 1.8f, 2f };
    public float cube16_BulletSize;
    public float[] cube16_BulletSizeUpgradeList = new float[] { 1.2f, 1.4f, 1.6f, 1.8f, 2f };
    public float cube16_FrozenEffectiveTime;
    public float[] cube16_FrozenEffectiveTimeUpgradeList = new float[] { 1.2f, 1.4f, 1.6f, 1.8f, 2f };
    public float cube16_FrozenEffectSurvivalTime;
    public float[] cube16_FrozenEffectSurvivalTimeUpgradeList = new float[] { 4.2f, 4.4f, 4.6f, 4.8f, 5f };

    // Cube 32
    public float cube32_CoolDownTime;
    public float[] cube32_CoolDownTimeUpgradeList = new float[] { 0.45f, 0.4f, 0.35f, 0.3f, 0.25f };
    public float cube32_BulletSpeed;
    public float[] cube32_BulletSpeedUpgradeList = new float[] { 25f, 30f, 35f, 40f, 45f };
    public float cube32_BulletSurvivalTime;
    public float[] cube32_BulletSurvivalTimeUpgradeList = new float[] { 1.2f, 1.4f, 1.6f, 1.8f, 2f };
    public float cube32_BulletSize;
    public float[] cube32_BulletSizeUpgradeList = new float[] { 1.2f, 1.4f, 1.6f, 1.8f, 2f };
    public float cube32_FireEffectiveTime;
    public float[] cube32_FireEffectiveTimeUpgradeList = new float[] { 1.2f, 1.4f, 1.6f, 1.8f, 2f };
    public float cube32_FireEffectSurvivalTime;
    public float[] cube32_FireEffectSurvivalTimeUpgradeList = new float[] { 4.2f, 4.4f, 4.6f, 4.8f, 5f };

    // Cube 64
    public float cube64_CoolDownTime;
    public float[] cube64_CoolDownTimeUpgradeList = new float[] { 0.45f, 0.4f, 0.35f, 0.3f, 0.25f };
    public float cube64_BulletSpeed;
    public float[] cube64_BulletSpeedUpgradeList = new float[] { 120f, 140f, 160f, 180f, 200f};
    public float cube64_BulletSurvivalTime;
    public float[] cube64_BulletSurvivalTimeUpgradeList = new float[] { 2.2f, 2.4f, 2.6f, 2.8f, 3f};
    public float cube64_BulletSize;
    public float[] cube64_BulletSizeUpgradeList = new float[] { 1.2f, 1.4f, 1.6f, 1.8f, 2f };
    public int cube64_NumHits;
    public int[] cube64_NumHitsUpgradeList = new int[] { 7, 9, 11, 13, 15};
    public float cube64_FindEnemyRadius;
    public float[] cube64_FindEnemyRadiusUpgradeList = new float[] { 15f, 20f, 25f, 30f, 35f };

    public GameData()
    {
        totalCoins = 0;

        // Cube 2
        cube2_CoolDownTime = 0.2f; //done
        cube2_BulletSpeed = 20f; // done
        cube2_BulletSurvivalTime = 0.5f; // done
        cube2_BulletSize = 0.3f; // done

        // Cube 4
        cube4_CoolDownTime = 0.5f; // done
        cube4_BulletSpeed = 20f; // done
        cube4_BulletSurvivalTime = 1f; // done
        cube4_BulletFlyBackTime = 0.5f; // done
        cube4_BulletSize = 0.8f; // done

        // Cube 8
        cube8_CoolDownTime = 0.5f; //done
        cube8_BulletSpeed = 20f; //done
        cube8_NumBullets = 5; //done
        cube8_BulletRotationAngle = 30f; //done
        cube8_BulletSurvivalTime = 0.5f; //done
        cube8_BulletSize = 0.8f; //done

        // Cube 16
        cube16_CoolDownTime = 0.5f; // done
        cube16_BulletSpeed = 20f; // done
        cube16_BulletSurvivalTime = 1f; //done
        cube16_BulletSize = 1f; //done
        cube16_FrozenEffectiveTime = 1f; //done
        cube16_FrozenEffectSurvivalTime = 4f; //done

        // Cube 32
        cube32_CoolDownTime = 0.5f; //done
        cube32_BulletSpeed = 20f; //done
        cube32_BulletSurvivalTime = 1f; // done
        cube32_BulletSize = 1f; //done
        cube32_FireEffectiveTime = 1f; //done
        cube32_FireEffectSurvivalTime = 4f; //done

        // Cube 64
        cube64_CoolDownTime = 0.5f; //done
        cube64_BulletSpeed = 100f; //done
        cube64_BulletSurvivalTime = 2f; // done
        cube64_BulletSize = 1f; // done
        cube64_NumHits = 5; // done
        cube64_FindEnemyRadius = 10f; // done
    }
}
