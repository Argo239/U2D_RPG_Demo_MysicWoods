public class ComboManager {
    private readonly ComboListData _comboListData;
    public int _currentIndex { get; private set; } = -1;
    public ComboStepData _CurrentStep => 
        (_currentIndex >= 0 && _currentIndex <  _comboListData.comboSteps.Count) ? _comboListData.comboSteps[_currentIndex] : null;

    /// <summary>
    /// Get the current combo step or null if none.
    /// </summary>
    /// <param name="comboListData"></param>
    public ComboManager(ComboListData comboListData) {
        _comboListData = comboListData;
    }

    /// <summary>
    /// Start or restart the combo from the first step
    /// </summary>
    public void StartCombo() {
        _currentIndex = 0;
    }

    /// <summary>
    /// Try to advance to the next step.
    /// Returns true if succeeded, false if mo more steps
    /// </summary>
    /// <returns></returns>
    public bool TryNext() {
        if (_currentIndex + 1 < _comboListData.comboSteps.Count) {
            _currentIndex++;
            return true;
        }
        return false;
    }
 
    /// <summary>
    /// Reset the combo back to none
    /// </summary>
    public void Reset() {
        _currentIndex = -1;
    }
}