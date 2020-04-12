using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Library;

namespace NaozumiKingdomInfo.UI.ViewModels
{
    class KingdomInfoWarsVM : ViewModel
    {

        public KingdomInfoWarsVM()
        {
            this.IsSelected = false;
        }

        public bool IsSelected { get; set; }
    }
}
