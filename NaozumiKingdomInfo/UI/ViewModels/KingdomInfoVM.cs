using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Library;

namespace NaozumiKingdomInfo.UI.ViewModels
{
    class KingdomInfoVM : ViewModel
    {
        public KingdomInfoVM()
        {
            this.WarsVM = new KingdomInfoWarsVM();
            this.NoblesVM = new KingdomInfoNoblesVM();
            this.SetSelectedCategory(0);
        }

        private void SetSelectedCategory(int index)
        {
            this.WarsVM.IsSelected = false;
            this.NoblesVM.IsSelected = false;

            if (index == 0)
            {
                this.WarsVM.IsSelected = true;
            }else if (index == 1)
            {
                this.NoblesVM.IsSelected = true;
            }
        }



        private KingdomInfoWarsVM WarsVM;
        private KingdomInfoNoblesVM NoblesVM;

    }
}
