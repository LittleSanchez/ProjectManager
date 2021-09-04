using ExpBag.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.UI.ViewModels
{
    public class NewModuleViewModel : ViewModelBase
    {
        ObservableCollection<ExpModuleFileDTO> Files;

        public NewModuleViewModel()
        {
            Files = new ObservableCollection<ExpModuleFileDTO>();
        }
    }
}
