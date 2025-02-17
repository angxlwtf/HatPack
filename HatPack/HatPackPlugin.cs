﻿using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
//code examples borrowed from townofus creators if it aint broke don't fix it LOL
namespace HatPack
{
    [BepInPlugin(Id, "HatPack", Version)]
    [BepInProcess("Among Us.exe")]
    public class HatPackPlugin : BasePlugin
    {
        public const string Version = "3.1.2";

        public const string Id = "hats.pack";

        public Harmony Harmony { get; } = new Harmony(Id);

        public override void Load()
        {
            Assets.LoadAssetBundle();
            Harmony.PatchAll();
        }

        protected internal struct AuthorData
        {
            public string AuthorName;
            public string HatName;
            public string FloorHatName;
            public string ClimbHatName;
            public string LeftImageName;
            public bool NoBounce;
            public bool altShader;
        }
        //Be sure to spell everything right or it will not load all hats after spelling error
        //Must be prefab name not name of asset for hatname
        private static List<AuthorData> authorDatas = new List<AuthorData>()
        {
            new AuthorData {AuthorName = "Berg", HatName = "birdhead", NoBounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "Army", NoBounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "Navy", NoBounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "Marine", NoBounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "Airforce", NoBounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "a_pretty_sus", LeftImageName = "a_pretty_sus_left", NoBounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "ParadoxMonkey", NoBounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "Gina", NoBounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "imsus", LeftImageName = "imsus_left", NoBounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "blackbirdhead", NoBounce = false },
            new AuthorData {AuthorName = "Angel", HatName = "jess", NoBounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "murderghost" , NoBounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "odaidenhat", NoBounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "Omega", NoBounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "reapercostume",FloorHatName ="reaperdead",ClimbHatName = "reaperclimb",LeftImageName = "reapercostumeleft", NoBounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "reapermask",FloorHatName ="reaperdead",ClimbHatName = "reaperclimb",LeftImageName = "reapermaskleft", NoBounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "viking", NoBounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "vikingbeer", NoBounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "pineapple", NoBounce = false},
            //new AuthorData {AuthorName = "Berg", HatName = "willhair", NoBounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "vader",FloorHatName ="vaderdead",ClimbHatName = "vaderclimb", NoBounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "unclesam",FloorHatName ="unclesamdead", NoBounce = false},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Bunpix", NoBounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Cadbury", NoBounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "CatEars", NoBounce = true},
            new AuthorData {AuthorName = "Angel", HatName = "dirtybirb", NoBounce = false},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "DJ", NoBounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "EnbyScarf", NoBounce = false},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Happy", NoBounce = false, altShader = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Carla", NoBounce = false, altShader = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Pika", NoBounce = false, altShader = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Gun", NoBounce = false, altShader = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Espeon", NoBounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Gwendolyn", NoBounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Jester", NoBounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "PizzaRod", NoBounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Sombra", NoBounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Sprxk", NoBounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Swole", NoBounce = false},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "TransScarf", NoBounce = false},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Unicorn", NoBounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Wings", NoBounce = false},
            new AuthorData {AuthorName = "Paradox", HatName = "Dino", NoBounce = true, altShader=true},
            new AuthorData {AuthorName = "Berg", HatName = "Ugg", NoBounce = false},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "SilverSylveon", NoBounce = false},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "DownyCrake", NoBounce = false},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Ram", NoBounce = false , altShader = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Kitsune", NoBounce = true, altShader = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "GlitchedSwole", NoBounce = true, altShader = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "TigerShark", NoBounce = true, altShader = true}
        };

        internal static Dictionary<uint, AuthorData> IdToData = new Dictionary<uint, AuthorData>();

        private static bool _customHatsLoaded = false;
        [HarmonyPatch(typeof(HatManager), nameof(HatManager.GetHatById))]
        public static class AddCustomHats
        {
            public static void Prefix(PlayerControl __instance)
            {
                if (!_customHatsLoaded)
                {
                    var allHats = HatManager.Instance.AllHats;

                    foreach (var data in authorDatas)
                    {
                        HatID++;

                        if (data.FloorHatName != null && data.ClimbHatName != null && data.LeftImageName != null)
                        {
                            System.Console.WriteLine($"Adding {data.HatName} and associated floor/climb hats/left image");
                            if (data.NoBounce)
                            {
                                if (data.altShader == true)
                                {
                                    System.Console.WriteLine($"Adding {data.HatName} with Alt shaders and bounce");
                                    allHats.Add(CreateHat(GetSprite(data.HatName), GetSprite(data.ClimbHatName), GetSprite(data.FloorHatName), null, true, true));
                                }
                                else
                                {
                                    System.Console.WriteLine($"Adding {data.HatName} with bounce enabled");
                                    allHats.Add(CreateHat(GetSprite(data.HatName), GetSprite(data.ClimbHatName), GetSprite(data.FloorHatName), GetSprite(data.LeftImageName), true, false));
                                }
                            }
                            else
                            {
                                System.Console.WriteLine($"Adding {data.HatName} with bounce disabled");
                                allHats.Add(CreateHat(GetSprite(data.HatName), GetSprite(data.ClimbHatName), GetSprite(data.FloorHatName), GetSprite(data.LeftImageName)));
                            }

                        }
                        else
                        {
                            if (data.altShader == true)
                            {
                                System.Console.WriteLine($"Adding {data.HatName} with Alt shaders");
                                allHats.Add(CreateHat(GetSprite(data.HatName), null, null, null, false, true));
                            }
                            else
                            {
                                System.Console.WriteLine($"Adding {data.HatName}");
                                allHats.Add(CreateHat(GetSprite(data.HatName)));
                            }
                        }
                        IdToData.Add((uint)HatManager.Instance.AllHats.Count - 1, data);

                        _customHatsLoaded = true;
                    }



                    _customHatsLoaded = true;
                }
            }

            public static Sprite GetSprite(string name)
                => Assets.LoadAsset(name).Cast<GameObject>().GetComponent<SpriteRenderer>().sprite;

            private static int HatID = 0;

            private static HatBehaviour CreateHat(Sprite sprite, Sprite climb = null, Sprite floor = null, Sprite leftimage = null, bool bounce = false, bool altshader = false)
            {
                var magicShader = DestroyableSingleton<HatManager>.Instance.AllHats[90].Cast<HatBehaviour>().AltShader;
                var newHat = ScriptableObject.CreateInstance<HatBehaviour>();
                newHat.MainImage = sprite;
                newHat.ProductId = $"hat_{sprite.name}";
                newHat.Order = 199 + HatID;
                newHat.InFront = true;
                newHat.NoBounce = bounce;
                newHat.FloorImage = floor;
                newHat.ClimbImage = climb;
                newHat.Free = true;
                newHat.LeftMainImage = leftimage;
                newHat.ChipOffset = new Vector2(-0.1f, 0.4f);
                if (altshader == true) { newHat.AltShader = magicShader; }

                return newHat;
            }
        }
    }
}
