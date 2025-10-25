﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Leauge_Auto_Accept
{
    internal class Settings
    {
        public static string[] currentChamp = { "Unselected", "0" };
        public static string[] currentChampRunes = { "Unselected", "0" };
        public static string[] currentBackupChamp = { "Unselected", "0" };
        public static string[] currentBackupChampRunes = { "Unselected", "0" };
        public static string[] currentBan = { "Unselected", "0" };
        public static string[] currentBackupBan = { "Unselected", "0" };
        public static string[] currentSpell1 = { "Unselected", "0" };
        public static string[] currentSpell2 = { "Unselected", "0" };
        public static bool bravery = false;
        public static bool banCrowdFavourite = false;
        public static string[] crowdFavouraiteChamp1 = { "Unselected", "0" };
        public static string[] crowdFavouraiteChamp2 = { "Unselected", "0" };
        public static string[] crowdFavouraiteChamp3 = { "Unselected", "0" };
        public static string[] crowdFavouraiteChamp4 = { "Unselected", "0" };
        public static string[] crowdFavouraiteChamp5 = { "Unselected", "0" };
        public static bool chatMessagesEnabled = false;
        public static List<string> chatMessages = new List<string>();
        public static bool preloadData = false;
        public static bool instaLock = false;
        public static bool instaBan = false;
        public static bool autoPickOrderTrade = false;
        public static bool instantHover = false;
        public static bool shouldAutoAcceptbeOn = false;
        public static bool autoRestartQueue = false;
        public static bool cancelQueueAfterDodge = false;
        public static bool banAlliedChampions = false;

        public static Language currentLanguage = Language.EnUs;

        public static int pickStartHoverDelay = 10000;
        public static int pickStartlockDelay = 999999999;
        public static int pickEndlockDelay = 1000;
        public static int banStartHoverDelay = 1500;
        public static int banStartlockDelay = 999999999;
        public static int banEndlockDelay = 1000;
        public static int queueMaxTime = 300000;
        public static int chatMessagesDelay = 100;

        public static void settingsModify(int item)
        {
            switch (item)
            {
                case 0:
                    preloadData = !preloadData;
                    break;
                case 1:
                    instaLock = !instaLock;
                    break;
                case 2:
                    instaBan = !instaBan;
                    break;
                case 3:
                    autoPickOrderTrade = !autoPickOrderTrade;
                    break;
                case 4:
                    instantHover = !instantHover;
                    break;
                case 5:
                    autoRestartQueue = !autoRestartQueue;
                    break;
                case 6:
                    cancelQueueAfterDodge = !cancelQueueAfterDodge;
                    break;
                case 7:
                    banAlliedChampions = !banAlliedChampions;
                    break;
                case 8:
                    ToggleLanguage();
                    UI.settingsMenu();
                    return;
                case 9:
                    UI.delayMenu();
                    return;
            }

            settingsSave();
        }

        public static void delayModify(int item, int number)
        {
            switch (item)
            {
                case 0:
                    pickStartHoverDelay = delayCalculateNewValue(pickStartHoverDelay, number);
                    break;
                case 1:
                    pickStartlockDelay = delayCalculateNewValue(pickStartlockDelay, number);
                    break;
                case 2:
                    pickEndlockDelay = delayCalculateNewValue(pickEndlockDelay, number);
                    break;
                case 3:
                    banStartHoverDelay = delayCalculateNewValue(banStartHoverDelay, number);
                    break;
                case 4:
                    banStartlockDelay = delayCalculateNewValue(banStartlockDelay, number);
                    break;
                case 5:
                    banEndlockDelay = delayCalculateNewValue(banEndlockDelay, number);
                    break;
                case 6:
                    queueMaxTime = delayCalculateNewValue(queueMaxTime, number);
                    break;
                case 7:
                    chatMessagesDelay = delayCalculateNewValue(chatMessagesDelay, number);
                    break;
            }

            settingsSave();
        }

        private const int MaxDelaySeconds = 1000000;

        public static int delayCalculateNewValue(int oldValue, int modifier)
        {
            int seconds = ConvertMillisecondsToSeconds(oldValue);
            string newNumString = seconds.ToString();

            if (modifier >= 0)
            {
                newNumString = newNumString == "0" ? modifier.ToString() : newNumString + modifier.ToString();
                if (newNumString.Length > MaxDelaySeconds.ToString().Length)
                {
                    newNumString = MaxDelaySeconds.ToString();
                }
            }
            else
            {
                if (newNumString.Length > 1)
                {
                    newNumString = newNumString.Substring(0, newNumString.Length - 1);
                }
                else
                {
                    newNumString = "0";
                }
            }

            int newSeconds = Int32.Parse(newNumString);
            if (newSeconds > MaxDelaySeconds)
            {
                newSeconds = MaxDelaySeconds;
            }

            return ConvertSecondsToMilliseconds(newSeconds);
        }

        public static int ConvertMillisecondsToSeconds(int milliseconds)
        {
            double seconds = milliseconds / 1000.0;
            int rounded = (int)Math.Round(seconds, MidpointRounding.AwayFromZero);
            return Math.Max(0, rounded);
        }

        private static int ConvertSecondsToMilliseconds(int seconds)
        {
            return Math.Max(0, seconds) * 1000;
        }

        public static void saveSelectedChamp()
        {
            List<itemList> champsFiltered = new List<itemList>();
            if ("unselected".Contains(Navigation.currentInput.ToLower()))
            {
                champsFiltered.Add(new itemList() { name = "Unselected", id = "0" });
            }
            if (UI.currentChampPicker == 4)
            {
                if ("none".Contains(Navigation.currentInput.ToLower()))
                {
                    champsFiltered.Add(new itemList() { name = "None", id = "-1" });
                }
            }
            foreach (var champ in Data.champsSorted)
            {
                if (champ.name.ToLower().Contains(Navigation.currentInput.ToLower()))
                {
                    if (UI.currentChampPicker != 4)
                    {
                        if (!champ.free)
                        {
                            continue;
                        }
                    }
                    champsFiltered.Add(new itemList() { name = champ.name, id = champ.id });
                }
            }

            if (champsFiltered.Count > 0)
            {
                string name;
                string id;
                if (Navigation.currentPos < 0)
                {
                    name = "Unselected";
                    id = "0";
                }
                else
                {
                    name = champsFiltered[Navigation.currentPos].name;
                    id = champsFiltered[Navigation.currentPos].id;
                }
                switch (UI.currentChampPicker)
                {
                    case 0:
                        currentChamp[0] = name;
                        currentChamp[1] = id;
                        break;
                    case 1:
                        currentBackupChamp[0] = name;
                        currentBackupChamp[1] = id;
                        break;
                    case 4:
                        currentBan[0] = name;
                        currentBan[1] = id;
                        break;
                    case 10:
                        currentBackupBan[0] = name;
                        currentBackupBan[1] = id;
                        break;
                    case 5:
                        crowdFavouraiteChamp1[0] = name;
                        crowdFavouraiteChamp1[1] = id;
                        break;
                    case 6:
                        crowdFavouraiteChamp2[0] = name;
                        crowdFavouraiteChamp2[1] = id;
                        break;                        
                    case 7:
                        crowdFavouraiteChamp3[0] = name;
                        crowdFavouraiteChamp3[1] = id;
                        break;
                    case 8:
                        crowdFavouraiteChamp4[0] = name;
                        crowdFavouraiteChamp4[1] = id;
                        break;
                    case 9:
                        crowdFavouraiteChamp5[0] = name;
                        crowdFavouraiteChamp5[1] = id;
                        break;
                }

                settingsSave();
            }
        }

        public static void saveSelectedSpell()
        {
            List<itemList> spellsFiltered = new List<itemList>();
            if ("unselected".Contains(Navigation.currentInput.ToLower()))
            {
                spellsFiltered.Add(new itemList() { name = "Unselected", id = "0" });
            }
            foreach (var spell in Data.spellsSorted)
            {
                if (spell.name.ToLower().Contains(Navigation.currentInput.ToLower()))
                {
                    spellsFiltered.Add(new itemList() { name = spell.name, id = spell.id });
                }
            }

            if (spellsFiltered.Count > 0)
            {
                string name;
                string id;
                if (Navigation.currentPos < 0)
                {
                    name = "Unselected";
                    id = "0";
                }
                else
                {
                    name = spellsFiltered[Navigation.currentPos].name;
                    id = spellsFiltered[Navigation.currentPos].id;
                }
                if (UI.currentSpellSlot == 0)
                {
                    currentSpell1[0] = name;
                    currentSpell1[1] = id;
                }
                else
                {
                    currentSpell2[0] = name;
                    currentSpell2[1] = id;
                }
                settingsSave();
            }
        }

        public static void saveSelectedRune()
        {
            List<itemList> runesFiltered = new List<itemList>();
            if ("unselected".Contains(Navigation.currentInput.ToLower()))
            {
                runesFiltered.Add(new itemList() { name = "Unselected", id = "0" });
            }
            foreach (var spell in Data.runesList)
            {
                if (spell.name.ToLower().Contains(Navigation.currentInput.ToLower()))
                {
                    runesFiltered.Add(new itemList() { name = spell.name, id = spell.id });
                }
            }

            if (runesFiltered.Count > 0)
            {
                string name;
                string id;
                if (Navigation.currentPos < 0)
                {
                    name = "Unselected";
                    id = "0";
                }
                else
                {
                    name = runesFiltered[Navigation.currentPos].name;
                    id = runesFiltered[Navigation.currentPos].id;
                }

                switch (UI.currentChampPicker)
                {
                    case 0:
                        currentChampRunes[0] = name;
                        currentChampRunes[1] = id;
                        break;
                    case 1:
                        currentBackupChampRunes[0] = name;
                        currentBackupChampRunes[1] = id;
                        break;
                }

                settingsSave();
            }
        }

        public static void updateChatMessage()
        {
            if (chatMessages.Count > UI.messageIndex)
            {
                chatMessages[UI.messageIndex] = Navigation.currentInput;
            }
            else
            {
                chatMessages.Add(Navigation.currentInput);
            }
            updateChatMessagesToggle();
            settingsSave();
        }

        public static void deleteChatMessage()
        {
            if (chatMessages.Count > UI.messageIndex)
            {
                chatMessages.RemoveAt(UI.messageIndex);
            }
            updateChatMessagesToggle();

            settingsSave();
        }

        private static void updateChatMessagesToggle()
        {
            if (chatMessages.Count > 0)
            {
                chatMessagesEnabled = true;
            }
            else
            {
                chatMessagesEnabled = false;
            }
        }

        private static string encodeMessagesIntoBase64()
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(string.Join('|', chatMessages));
            string base64String = Convert.ToBase64String(byteArray);

            return base64String;
        }

        private static void decodeMessagesFromBase64(string messages)
        {
            if (messages == "") { return; }
            byte[] byteArray = Convert.FromBase64String(messages);
            string joinedString = Encoding.UTF8.GetString(byteArray);
            chatMessages = new List<string>(joinedString.Split('|'));
        }

        public static void settingsSave()
        {
            string config =
                "champName:" + currentChamp[0] +
                ",champId:" + currentChamp[1] +
                ",champRuneName:" + currentChampRunes[0] +
                ",champRuneId:" + currentChampRunes[1] +
                ",champBackupName:" + currentBackupChamp[0] +
                ",champBackupId:" + currentBackupChamp[1] +
                ",champBackupRuneName:" + currentBackupChampRunes[0] +
                ",champBackupRuneId:" + currentBackupChampRunes[1] +
                ",arenaBravery:" + bravery +
                ",banCrowdFavourite:" + banCrowdFavourite +
                ",arenaCrowdFavourite1Name:" + crowdFavouraiteChamp1[0] +
                ",arenaCrowdFavourite1ChampId:" + crowdFavouraiteChamp1[1] +
                ",arenaCrowdFavourite2Name:" + crowdFavouraiteChamp2[0] +
                ",arenaCrowdFavourite2ChampId:" + crowdFavouraiteChamp2[1] +
                ",arenaCrowdFavourite3Name:" + crowdFavouraiteChamp3[0] +
                ",arenaCrowdFavourite3ChampId:" + crowdFavouraiteChamp3[1] +
                ",arenaCrowdFavourite4Name:" + crowdFavouraiteChamp4[0] +
                ",arenaCrowdFavourite4ChampId:" + crowdFavouraiteChamp4[1] +
                ",arenaCrowdFavourite5Name:" + crowdFavouraiteChamp5[0] +
                ",arenaCrowdFavourite5ChampId:" + crowdFavouraiteChamp5[1] +
                ",banName:" + currentBan[0] +
                ",banId:" + currentBan[1] +
                ",banBackupName:" + currentBackupBan[0] +
                ",banBackupId:" + currentBackupBan[1] +
                ",spell1Name:" + currentSpell1[0] +
                ",spell1Id:" + currentSpell1[1] +
                ",spell2Name:" + currentSpell2[0] +
                ",spell2Id:" + currentSpell2[1] +
                ",autoAcceptOn:" + shouldAutoAcceptbeOn +
                ",preloadData:" + preloadData +
                ",instaLock:" + instaLock +
                ",instaBan:" + instaBan +
                ",pickStartHoverDelay:" + pickStartHoverDelay +
                ",pickStartlockDelay:" + pickStartlockDelay +
                ",pickEndlockDelay:" + pickEndlockDelay +
                ",banStartHoverDelay:" + banStartHoverDelay +
                ",banStartlockDelay:" + banStartlockDelay +
                ",banEndlockDelay:" + banEndlockDelay +
                ",queueMaxTime:" + queueMaxTime +
                ",chatMessagesDelay:" + chatMessagesDelay +
                ",autoPickOrderTrade:" + autoPickOrderTrade +
                ",instantHover:" + instantHover +
                ",autoRestartQueue:" + autoRestartQueue +
                ",cancelQueueAfterDodge:" + cancelQueueAfterDodge +
                ",banAlliedChampions:" + banAlliedChampions +
                ",chatMessages:" + encodeMessagesIntoBase64();

            config += ",language:" + currentLanguage;

            string dirParameter = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Leauge Auto Accept Config.txt";
            using (StreamWriter m_WriterParameter = new StreamWriter(dirParameter, false))
            {
                m_WriterParameter.Write(config);
            }
        }

        public static void toggleBraverySetting()
        {
            bravery = !bravery;
            settingsSave();
        }

        public static void toggleBanCrowdFavouriteSetting()
        {
            banCrowdFavourite = !banCrowdFavourite;
            settingsSave();
        }

        private static void ToggleLanguage()
        {
            currentLanguage = currentLanguage == Language.EnUs ? Language.PtBr : Language.EnUs;
            Localization.SetLanguage(currentLanguage);
            settingsSave();
        }

        public static void toggleAutoAcceptSetting()
        {
            if (MainLogic.isAutoAcceptOn)
            {
                MainLogic.isAutoAcceptOn = false;
                shouldAutoAcceptbeOn = false;
            }
            else
            {
                MainLogic.isAutoAcceptOn = true;
                shouldAutoAcceptbeOn = true;
            }
            settingsSave();
        }

        public static void loadSettings()
        {
            string dirParameter = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Leauge Auto Accept Config.txt";
            if (File.Exists(dirParameter))
            {
                string text = File.ReadAllText(dirParameter);
                string[] commas = text.Split(',');
                foreach (var comma in commas)
                {
                    string[] columns = comma.Split(':');
                    switch (columns[0])
                    {
                        case "champName":
                            currentChamp[0] = columns[1];
                            break;
                        case "champId":
                            currentChamp[1] = columns[1];
                            break;
                        case "champRuneName":
                            currentChampRunes[0] = columns[1];
                            break;
                        case "champRuneId":
                            currentChampRunes[1] = columns[1];
                            break;
                        case "champBackupName":
                            currentBackupChamp[0] = columns[1];
                            break;
                        case "champBackupId":
                            currentBackupChamp[1] = columns[1];
                            break;
                        case "champBackupRuneName":
                            currentBackupChampRunes[0] = columns[1];
                            break;
                        case "champBackupRuneId":
                            currentBackupChampRunes[1] = columns[1];
                            break;
                            break;   
                        case "arenaBravery":
                            bravery = Boolean.Parse(columns[1]);
                            break;
                        case "banCrowdFavourite":
                            banCrowdFavourite = Boolean.Parse(columns[1]);
                            break;
                        case "arenaCrowdFavourite1Name":
                            crowdFavouraiteChamp1[0] = columns[1];
                            break;
                        case "arenaCrowdFavourite1ChampId":
                            crowdFavouraiteChamp1[1] = columns[1];
                            break;
                        case "arenaCrowdFavourite2Name":
                            crowdFavouraiteChamp2[0] = columns[1];
                            break;
                        case "arenaCrowdFavourite2ChampId":
                            crowdFavouraiteChamp2[1] = columns[1];
                            break;
                        case "arenaCrowdFavourite3Name":
                            crowdFavouraiteChamp3[0] = columns[1];
                            break;
                        case "arenaCrowdFavourite3ChampId":
                            crowdFavouraiteChamp3[1] = columns[1];
                            break;
                        case "arenaCrowdFavourite4Name":
                            crowdFavouraiteChamp4[0] = columns[1];
                            break;
                        case "arenaCrowdFavourite4ChampId":
                            crowdFavouraiteChamp4[1] = columns[1];
                            break;
                        case "arenaCrowdFavourite5Name":
                            crowdFavouraiteChamp5[0] = columns[1];
                            break;
                        case "arenaCrowdFavourite5ChampId":
                            crowdFavouraiteChamp5[1] = columns[1];
                            break;
                        case "banName":
                            currentBan[0] = columns[1];
                            break;
                        case "banId":
                            currentBan[1] = columns[1];
                            break;
                        case "banBackupName":
                            currentBackupBan[0] = columns[1];
                            break;
                        case "banBackupId":
                            currentBackupBan[1] = columns[1];
                            break;
                        case "spell1Name":
                            currentSpell1[0] = columns[1];
                            break;
                        case "spell1Id":
                            currentSpell1[1] = columns[1];
                            break;
                        case "spell2Name":
                            currentSpell2[0] = columns[1];
                            break;
                        case "spell2Id":
                            currentSpell2[1] = columns[1];
                            break;
                        case "pickStartHoverDelay":
                            pickStartHoverDelay = Int32.Parse(columns[1]);
                            break;
                        case "pickStartlockDelay":
                            pickStartlockDelay = Int32.Parse(columns[1]);
                            break;
                        case "pickEndlockDelay":
                            pickEndlockDelay = Int32.Parse(columns[1]);
                            break;
                        case "banStartHoverDelay":
                            banStartHoverDelay = Int32.Parse(columns[1]);
                            break;
                        case "banStartlockDelay":
                            banStartlockDelay = Int32.Parse(columns[1]);
                            break;
                        case "banEndlockDelay":
                            banEndlockDelay = Int32.Parse(columns[1]);
                            break;
                        case "queueMaxTime":
                            queueMaxTime = Int32.Parse(columns[1]);
                            break;
                        case "chatMessagesDelay":
                            chatMessagesDelay = Int32.Parse(columns[1]);
                            break;
                        case "autoAcceptOn":
                            shouldAutoAcceptbeOn = Boolean.Parse(columns[1]);
                            break;
                        case "preloadData":
                            preloadData = Boolean.Parse(columns[1]);
                            break;
                        case "instaLock":
                            instaLock = Boolean.Parse(columns[1]);
                            break;
                        case "instaBan":
                            instaBan = Boolean.Parse(columns[1]);
                            break;
                        case "autoPickOrderTrade":
                            autoPickOrderTrade = Boolean.Parse(columns[1]);
                            break;
                        case "instantHover":
                            instantHover = Boolean.Parse(columns[1]);
                            break;
                        case "autoRestartQueue":
                            autoRestartQueue = Boolean.Parse(columns[1]);
                            break;
                        case "cancelQueueAfterDodge":
                            cancelQueueAfterDodge = Boolean.Parse(columns[1]);
                            break;
                        case "banAlliedChampions":
                            banAlliedChampions = Boolean.Parse(columns[1]);
                            break;
                        case "chatMessages":
                            decodeMessagesFromBase64(columns[1]);
                            updateChatMessagesToggle();
                            break;
                        case "language":
                            if (Enum.TryParse(columns[1], out Language parsedLanguage))
                            {
                                currentLanguage = parsedLanguage;
                            }
                            break;
                    }
                }
            }

            Localization.SetLanguage(currentLanguage);
        }
    }
}
