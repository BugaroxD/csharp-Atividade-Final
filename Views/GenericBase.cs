using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using lib;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using Models;

namespace Views
{

    public abstract class GenericBase : Form
    {
        public List<GenericField> fields;

        public GenericBase()
        {
            this.fields = new List<GenericField>();
        }
    }
}