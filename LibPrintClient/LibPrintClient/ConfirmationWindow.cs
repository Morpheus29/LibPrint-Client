﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibPrintClient
{
    public partial class ConfirmationWindow : Form
    {
        public ConfirmationWindow()
        {
            InitializeComponent();

            label2.Text = Variables.parsedconfirm[3].Trim();
        }
    }
}
