namespace Assets.Scripts.Attributes.BaseAttributes {
    public class SPDAttribute {
        // Speed - SPD  SpeedMultiplier - SPD_MULT

        public float SPD { get; set; }
        public float SPD_MULT { get; set; }
        
        public SPDAttribute(float spd, float spdMult) { 
            SPD = spd;
            SPD_MULT = spdMult;
        }  
    }
}
