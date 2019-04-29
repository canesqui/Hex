using Hex.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Model
{
    public class ResponseModel: ObservableObject
    {
        public string Result { get; set; }

        //public event PropertyChangedEventHandler PropertyChanged;
    }
}
