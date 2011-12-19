using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Storage;
using Smiley.Lib.Util;
using System.IO;
using Microsoft.Xna.Framework.Input;

namespace Smiley.Lib.Framework
{
    [Serializable]
    public class SmileyConfig
    {
        public int MusicVolume { get; set; }
        public int SoundVolume { get; set; }
        public SmileyInputConfig[] Inputs { get; set; }
    }

    [Serializable]
    public class SmileyInputConfig
    {
        public Input Input { get; set; }
        public InputDevice Device { get; set; }
        public int Code { get; set; }
    }

    public class ConfigManager
    {
        private const string ConfigFile = "config";

        private SmileyConfig _config;

        /// <summary>
        /// Gets the game's current configuration data.
        /// </summary>
        public SmileyConfig Config
        {
            get
            {
                if (_config == null)
                {
                    StorageContainer container = SmileyUtil.GetStorageContainer();

                    if (container.FileExists(ConfigFile))
                    {
                        using (Stream stream = container.OpenFile(ConfigFile, FileMode.OpenOrCreate))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(SmileyConfig));
                            _config = (SmileyConfig)serializer.Deserialize(stream);
                        }
                    }
                    else
                    {
                        _config = CreateDefaultConfig();
                    }
                }
                return _config;
            }
        }

        /// <summary>
        /// Saves the current configuration data.
        /// </summary>
        public void SaveConfig()
        {
            StorageContainer container = SmileyUtil.GetStorageContainer();
            container.DeleteFile(ConfigFile);
            using (Stream stream = container.OpenFile(ConfigFile, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SmileyConfig));
                serializer.Serialize(stream, _config);
            }
        }

        private SmileyConfig CreateDefaultConfig()
        {
            SmileyConfig config = new SmileyConfig();
            config.MusicVolume = 100;
            config.SoundVolume = 100;
            config.Inputs = new SmileyInputConfig[10];

            config.Inputs[0] = new SmileyInputConfig
            {
                Input = Input.Ability,
                Device = InputDevice.Keyboard,
                Code = (int)Keys.LeftControl
            };

            config.Inputs[1] = new SmileyInputConfig
            {
                Input = Input.Aim,
                Device = InputDevice.Keyboard,
                Code = (int)Keys.LeftAlt
            };

            config.Inputs[2] = new SmileyInputConfig
            {
                Input = Input.Attack,
                Device = InputDevice.Keyboard,
                Code = (int)Keys.Space
            };

            config.Inputs[3] = new SmileyInputConfig
            {
                Input = Input.Down,
                Device = InputDevice.Keyboard,
                Code = (int)Keys.Down
            };

            config.Inputs[4] = new SmileyInputConfig
            {
                Input = Input.Left,
                Device = InputDevice.Keyboard,
                Code = (int)Keys.Left
            };

            config.Inputs[5] = new SmileyInputConfig
            {
                Input = Input.NextAbility,
                Device = InputDevice.Keyboard,
                Code = (int)Keys.X
            };

            config.Inputs[6] = new SmileyInputConfig
            {
                Input = Input.PreviousAbility,
                Device = InputDevice.Keyboard,
                Code = (int)Keys.Z
            };

            config.Inputs[7] = new SmileyInputConfig
            {
                Input = Input.Right,
                Device = InputDevice.Keyboard,
                Code = (int)Keys.Right
            };

            config.Inputs[8] = new SmileyInputConfig
            {
                Input = Input.Up,
                Device = InputDevice.Keyboard,
                Code = (int)Keys.Up
            };

            config.Inputs[9] = new SmileyInputConfig
            {
                Input = Input.Pause,
                Device = InputDevice.Keyboard,
                Code = (int)Keys.I
            };

            return config;
        }
    }
}
