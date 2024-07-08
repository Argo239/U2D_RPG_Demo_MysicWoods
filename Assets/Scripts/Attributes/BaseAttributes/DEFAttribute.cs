using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Attributes.BaseAttributes {
    public class DEFAttribute {
        //Defense - DEF  DamageReduction - DR

        public float DEF { get; set; }
        public float DR { get; set; } 

        public DEFAttribute(float def, float dr) { 
            DEF = def; 
            DR = dr; 
        }

        /// <summary>
        /// Return the value after defense
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public float Defense(float damage) {
            return Mathf.Max(damage - DEF, 0);
        }

        /// <summary>
        /// Return the value after damage reduction
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public float DamageReduction(float damage) {
            return Mathf.Min((damage * DR), (damage * 0.8f));
        }

    }
}
