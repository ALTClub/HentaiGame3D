using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition
{
    public abstract bool check();
    [Serializable]
    public class Characteristic : Condition
    {
        [SerializeField] string _name, _group, _level;
        public override bool check()
        {
            return GameObject.FindGameObjectWithTag("Player").
                GetComponent<PlayerCharacteristics>().
                GetGroup(_group).
                GetCharacteristic(_name).
                CheckLevel(_level);
        }
    }
}

