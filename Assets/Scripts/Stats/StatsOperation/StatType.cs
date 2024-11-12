namespace Assets.Scripts.Stats.StatsOperation {
    public class StatType {
        public string Name { get; set; }
        public StatType(string name) => Name = name;
        public override string ToString() => Name;
    }
}
