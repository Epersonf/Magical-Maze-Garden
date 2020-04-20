using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall
{
    public static MagicBallComponent Generate(CharacterComponent emissor, Vector3 position)
    {
        GameObject instance = MonoBehaviour.Instantiate(emissor.characterInformation.magicBallPrefab, position, Quaternion.identity);
        instance.transform.localScale *= emissor.transform.localScale.x;
        MagicBallComponent mBall = instance.AddComponent(typeof(MagicBallComponent)) as MagicBallComponent;
        instance.GetComponent<MeshRenderer>().material = emissor.characterInformation.magicBallMaterial;
        mBall.SetStats(emissor, emissor.characterInformation.damage, emissor.characterInformation.range, emissor.transform.forward);
        return mBall;
    }
}
