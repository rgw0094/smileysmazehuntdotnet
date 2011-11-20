using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;
using Smiley.Lib.Data;
using System.IO;
using Smiley.Lib.Utils;

namespace Smiley.Lib.Services
{
    /// <summary>
    /// Manages saving and loading games. When a file is loaded, all of
    /// its information is stored internally which is then retrieved or
    /// changed during gameplay.
    /// </summary>
    public class SaveManager
    {
        #region Constructors

        /// <summary>
        /// Constructs a new SaveManager.
        /// </summary>
        public SaveManager()
        {
            Saves = new List<SaveFile>();
            foreach (SaveSlot save in Enum.GetValues(typeof(SaveSlot)))
            {
                Saves.Add(LoadFile(save.GetDescription()));
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the list of available save files.
        /// </summary>
        public List<SaveFile> Saves
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the currently loaded save file.
        /// </summary>
        public SaveFile CurrentSave
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the relevant hint based on the current game progress.
        /// </summary>
        public int CurrentHint
        {
            get
            {
                if (CurrentSave.HasKilledBoss[Boss.ConservatoryBoss])
                    return 11;
                else if (CurrentSave.HasKilledBoss[Boss.TutBoss])
                    return 10;
                else if (CurrentSave.HasKilledBoss[Boss.LovecraftBoss])
                    return 9;
                else if (CurrentSave.HasKilledBoss[Boss.CandyBoss])
                    return 8;
                else if (CurrentSave.HasKilledBoss[Boss.FireBoss2])
                    return 7;
                else if (CurrentSave.HasKilledBoss[Boss.MushroomBoss])
                    return 6;
                else if (CurrentSave.HasKilledBoss[Boss.DespairBoss])
                    return 5;
                else if (CurrentSave.HasKilledBoss[Boss.DesertBoss])
                    return 4;
                else if (CurrentSave.HasKilledBoss[Boss.ForestBoss])
                    return 3;
                else if (CurrentSave.HasKilledBoss[Boss.SnowBoss])
                    return 2;
                else if (CurrentSave.HasKilledBoss[Boss.FireBoss])
                    return 1;
                else
                    return 0;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Saves the current file.
        /// </summary>
        public void Save()
        {
            SaveFile(CurrentSave);
        }

        /// <summary>
        /// Deletes the save file in the given save slot.
        /// </summary>
        /// <param name="saveSlot"></param>
        public void Delete(SaveSlot saveSlot)
        {
            File.Delete(saveSlot.GetDescription());
        }

        #endregion

        #region Private Methods

        private void SaveFile(SaveFile file)
        {
            using (BitStream output = new BitStream())
            {
                output.Open(file.Name, BitStreamMode.Write);
            }
        }

        /// <summary>
        /// Loads the save file with the given file name, or returns an empty save
        /// if the file doens't exist.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private SaveFile LoadFile(string fileName)
        {
            SaveFile file = new SaveFile(fileName);

            if (!File.Exists(fileName))
                return file;

            //Select the specified save file
            using (BitStream input = new BitStream())
            {
                input.Open(fileName, BitStreamMode.Read);

                file.TimePlayed = TimeSpan.FromTicks(input.ReadBits(64));

                //Load abilties
                foreach (Ability ability in Enum.GetValues(typeof(Ability)))
                {
                    file.HasAbility[ability] = input.ReadBit();
                }

                //Load keys
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        file.NumKeys[i, j] = input.ReadByte();
                    }
                }

                //Load gems
                foreach (Level level in Enum.GetValues(typeof(Level)))
                {
                    foreach (Gem gem in Enum.GetValues(typeof(Gem)))
                    {
                        file.NumGems[level][gem] = input.ReadByte();
                    }
                }

                //Load money
                file.Money = input.ReadByte();

                //Load upgrades
                foreach (Upgrade upgrade in Enum.GetValues(typeof(Upgrade)))
                {
                    file.NumUpgrades[upgrade] = input.ReadByte();
                }

                //Load which bosses have been slain
                foreach (Boss boss in Enum.GetValues(typeof(Boss)))
                {
                    file.HasKilledBoss[boss] = input.ReadBit();
                }

                //Load player zone and location
                file.Level = (Level)input.ReadByte();
                file.GridX = input.ReadByte();
                file.GridY = input.ReadByte();

                //Health and mana
                file.PlayerHealth = Convert.ToDouble(input.ReadByte() / 4);
                file.PlayerMana = Convert.ToDouble(input.ReadByte());

                //Load changed shit
                int numChanges = input.ReadBits(16);
                for (int i = 0; i < numChanges; i++)
                {
                    file.ChangeTile((Level)input.ReadByte(), input.ReadByte(), input.ReadByte());
                }

                //Load Stats
                file.NumTongueLicks = input.ReadByte();
                file.NumEnemiesKilled = input.ReadByte();
                file.PixelsTraversed = input.ReadBits(24);

                //Tutorial Man
                file.AdviceManEncounterCompleted = input.ReadBit();

                for (int i = 0; i < 3; i++)
                {
                    //TODO:
                    //smh->player->gui->setAbilityInSlot(input.ReadBits(5), i);
                }

                foreach (Level level in Enum.GetValues(typeof(Level)))
                {
                    file.HasVisitedLevel[level] = input.ReadBit();
                }

                file.Difficulty = (Difficulty)input.ReadByte();

                //Exploration data
                foreach (Level level in Enum.GetValues(typeof(Level)))
                {
                    for (int j = 0; j < 256; j++)
                    {
                        for (int k = 0; k < 256; k++)
                        {
                            file.Explored[level][j, k] = input.ReadBit();
                        }
                    }
                }
            }

            file.TimeFileLoaded = DateTime.Now.TimeOfDay;
            return file;
        }

        #endregion
    }
}
