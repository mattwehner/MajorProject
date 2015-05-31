using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class DebugController : MonoBehaviour
    {
        private Button _applyButton;
        private Button _revertButton;
        private Button _restoreButton;

        private bool _restoreAvailable;

        public GameSettings NewGameSettings;
        private GameSettings _previousSettings;
        private GameSettings _originalSettings;

        public InputField PlayerMovementSpeed;
        public InputField CharacterSpeed;
        public InputField MinHandHeight;

        void Start () {
	        Debug.Log("DebugSettingOptions is Alive");

            _applyButton = transform.FindChild("ApplyButton").GetComponent<Button>();
            _revertButton = transform.FindChild("RevertButton").GetComponent<Button>();
            _restoreButton = transform.FindChild("RestoreButton").GetComponent<Button>();

            StoreSettings();
        }

        void Update()
        {
            //Checks to see if it is accidentaly active
            if (PublicReferenceList.DebugMenu.activeSelf & !WorldStorage.worldStorage.IsDebugOpen)
            {
                PublicReferenceList.DebugMenu.SetActive(false);
            }
            _restoreButton.interactable = _restoreAvailable;
        }

        private void StoreSettings()
        {
            _originalSettings = new GameSettings(
                Settings.Player.PlayerMovementSpeed,
                Settings.Game.CharacterSpeed,
                Settings.Game.MinHandHeight
                );
            _previousSettings = new GameSettings(
                Settings.Player.PlayerMovementSpeed,
                Settings.Game.CharacterSpeed,
                Settings.Game.MinHandHeight
                );
            NewGameSettings = new GameSettings(
                Settings.Player.PlayerMovementSpeed,
                Settings.Game.CharacterSpeed,
                Settings.Game.MinHandHeight
                );

            PlayerMovementSpeed.text = _originalSettings.PlayerMovementSpeed.ToString();
            CharacterSpeed.text = _originalSettings.CharacterSpeed.ToString();
            MinHandHeight.text = _originalSettings.MinHandHeight.ToString();

            _applyButton.interactable = false;
            _revertButton.interactable = false;
            _restoreButton.interactable = _restoreAvailable;

            Debug.Log("Original Settings Saved");
        }

        public void UpdateSettings()
        {
            _previousSettings.PlayerMovementSpeed = NewGameSettings.PlayerMovementSpeed;
            _previousSettings.CharacterSpeed = NewGameSettings.CharacterSpeed;
            _previousSettings.MinHandHeight = NewGameSettings.MinHandHeight;

            NewGameSettings.PlayerMovementSpeed = float.Parse(PlayerMovementSpeed.text);
            NewGameSettings.CharacterSpeed = float.Parse(CharacterSpeed.text);
            NewGameSettings.MinHandHeight = float.Parse(MinHandHeight.text);

            ChangeCurrentSettings(NewGameSettings);

            _restoreAvailable = true;
            _applyButton.interactable = false;
            Debug.Log("Settings Updated");
        }

        public void RevertSettings()
        {
            PlayerMovementSpeed.text = _previousSettings.PlayerMovementSpeed.ToString();
            CharacterSpeed.text = _previousSettings.CharacterSpeed.ToString();
            MinHandHeight.text = _previousSettings.MinHandHeight.ToString();

            ChangeCurrentSettings(NewGameSettings);

            _revertButton.interactable = false;
            _applyButton.interactable = false;
            Debug.Log("Settings Reverted");
        }

        public void RestoreSettings()
        {
            NewGameSettings.PlayerMovementSpeed = _originalSettings.PlayerMovementSpeed;
            NewGameSettings.CharacterSpeed = _originalSettings.CharacterSpeed;
            NewGameSettings.MinHandHeight = _originalSettings.MinHandHeight;

            PlayerMovementSpeed.text = NewGameSettings.PlayerMovementSpeed.ToString();
            CharacterSpeed.text = NewGameSettings.CharacterSpeed.ToString();
            MinHandHeight.text = NewGameSettings.MinHandHeight.ToString();

            ChangeCurrentSettings(NewGameSettings);

            _restoreAvailable = false;
            Debug.Log("Settings Restored");
        }

        private void ChangeCurrentSettings(GameSettings settingChanges)
        {
            var sc = settingChanges;
            Settings.Player.PlayerMovementSpeed = sc.PlayerMovementSpeed;
            Settings.Game.CharacterSpeed = sc.CharacterSpeed;
            Settings.Game.MinHandHeight = sc.MinHandHeight;
        }
    }
}
