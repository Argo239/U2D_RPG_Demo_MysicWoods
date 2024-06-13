namespace Assets.Scripts.Attributes.BaseAttributes {
    public class DEFAttribute {
        //Defense - DEF  DamageReduction - DR

        public float DEF { get; set; }
        public float DR { get; set; } 

        public DEFAttribute(float def, float dr) { 
            DEF = def; 
            DR = dr; 
        }
    }
}
