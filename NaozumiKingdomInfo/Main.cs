using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.CampaignSystem.Actions;

namespace zNaozumiKingdomInfo
{
    class Main : MBSubModuleBase
    {
        private InputKey MenuKey = InputKey.End;

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            UserSettings userSettings = new UserSettings();
            MenuKey = userSettings.MenuKey;
        }

        protected override void OnApplicationTick(float dt)
        {
            base.OnApplicationTick(dt);
            if (Campaign.Current != null)
            {
                if (Campaign.Current.GameStarted)
                {
                    if (MenuKey.IsPressed())
                    {
                        List<InquiryElement> inquiryElements = new List<InquiryElement>
                            {
                                new InquiryElement("wars", "Wars", null, true, "Shows the current wars between each of the Kingdoms."),
                                new InquiryElement("kingdomheroes", "Kingdom Heroes", null, true, "Shows the number of heroes in each Kingdom."),
                                new InquiryElement("herodeaths", "Kingdom Deaths", null, true, "Shows the number of heroes that have died in each Kingdom.")
                            };
                        InformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData("Kingdom Info", "", inquiryElements, true, true, "Show", "Close", new Action<List<InquiryElement>>(this.OptionSelected), null, ""), true);
                    }
                }
            }
        }

        private void OptionSelected(List<InquiryElement> selections)
        {
            InquiryElement selection = selections.FirstOrDefault<InquiryElement>();
            if (selection != null)
            {
                string a = selection.Identifier as string;
                if (a == "wars")
                {
                    ShowWars();
                }
                else if (a == "kingdomheroes")
                {
                    ShowKingdomHeroes();
                }else if (a == "herodeaths")
                {
                    ShowKingdomHeroDeaths();
                }
            }
        }

        private void ShowWars()
        {
            string output = "";
            bool first = true;

            foreach (Kingdom kingdom1 in Kingdom.All)
            {
                if (!first) output += "\n";
                first = false;
                if (kingdom1 != null)
                {
                    output += kingdom1.Name.ToString() + ": ";
                    bool peace = true;
                    foreach (Kingdom kingdom2 in Kingdom.All)
                    {
                        if (kingdom2 != null)
                        {
                            if (kingdom1.IsAtWarWith(kingdom2))
                            {
                                if (!peace) output += ", ";
                                peace = false;
                                output += kingdom2.Name.ToString();
                            }
                        }
                    }
                    if (peace) output += "At peace";
                }
            }

            InformationManager.ShowInquiry(new InquiryData("Kingdom Info - Wars", output, true, false, "Close", "Cancel", null, null, ""), false);
        }

        private void ShowKingdomHeroes()
        {
            string output = "";
            bool first = true;

            foreach (Kingdom kingdom in Kingdom.All)
            {
                if (!first) output += "\n";
                first = false;
                if (kingdom != null)
                {
                    Double aliveNobleCount = 0;
                    Double aliveMinorFactionHeroCount = 0;
                    Double aliveChildCount = 0;

                    foreach (Clan clan in kingdom.Clans)
                    {
                        foreach (Hero hero in clan.Heroes)
                        {
                            if (hero.IsAlive)
                            {
                                if (!hero.IsChild)
                                {
                                    if (hero.IsNoble)
                                    {
                                        aliveNobleCount++;
                                    }
                                    else if (hero.IsMinorFactionHero)
                                    {
                                        aliveMinorFactionHeroCount++;
                                    }
                                }
                                else if (hero.IsChild && (hero.IsNoble || hero.IsMinorFactionHero))
                                {
                                    aliveChildCount++;
                                }
                            }
                        }
                    }

                    output += kingdom.Name.ToString() + " - Nobles: " + aliveNobleCount.ToString() + " - Minor Heroes: " + aliveMinorFactionHeroCount.ToString() + " - Kids: " + aliveChildCount.ToString();
                }
            }

            InformationManager.ShowInquiry(new InquiryData("Kingdom Info - Kingdom Heroes", output, true, false, "Close", "Cancel", null, null, ""), false);
        }

        private void ShowKingdomHeroDeaths()
        {
            string output = "";
            bool first = true;

            foreach (Kingdom kingdom in Kingdom.All)
            {
                if (!first) output += "\n";
                first = false;
                if (kingdom != null)
                {
                    Double aliveNobleCount = 0;
                    Double aliveMinorFactionHeroCount = 0;
                    Double aliveChildCount = 0;
                    Double dead = 0;

                    foreach (Clan clan in kingdom.Clans)
                    {
                        foreach (Hero hero in clan.Heroes)
                        {
                            if ((hero.IsNoble || hero.IsMinorFactionHero) && hero.IsDead)
                            {
                                dead++;
                            }
                        }
                    }

                    output += kingdom.Name.ToString() + ": " + dead.ToString();
                }
            }

            InformationManager.ShowInquiry(new InquiryData("Kingdom Info - Dead Heroes", output, true, false, "Close", "Cancel", null, null, ""), false);
        }

        private void TrackSettlement(Settlement settlement)
        {
            Campaign.Current.VisualTrackerManager.RegisterObject(settlement);
        }
    }
}
