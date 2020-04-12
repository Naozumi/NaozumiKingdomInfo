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

namespace zNaozumiKingdomInfo
{
    class Main : MBSubModuleBase
    {
        protected override void OnApplicationTick(float dt)
        {
            base.OnApplicationTick(dt);
            if (Campaign.Current != null)
            {
                if (Campaign.Current.GameStarted)
                {
                    if (InputKey.End.IsPressed())
                    {
                        string output = "Kingdom Members{newline}";

                        foreach (Kingdom kingdom in Kingdom.All)
                        {
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

                                output += "{newline}" + kingdom.Name.ToString() + " - Nobles: " + aliveNobleCount.ToString() + " - Minor Heros: " + aliveMinorFactionHeroCount.ToString() + " - Kids: " + aliveChildCount.ToString();
                            }
                        }

                        InformationManager.ShowInquiry(new InquiryData("Kingdom Info", output, true, false, "Close", "Cancel", null, null, ""), false);
                    }
                }
            }
        }
    }
}
