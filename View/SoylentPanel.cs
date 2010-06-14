﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

using Soylent.Model;
using Soylent.Model.Shortn;
using Soylent.View.Shortn;

namespace Soylent.View
{
    public partial class SoylentPanel : UserControl
    {
        public static string HOSTNAME = "HITViewHost";

        //TODO: figure out where to actually store this.
        public Dictionary<int, HITData> jobMap { get; private set; }

        public SoylentPanel()
        {
            InitializeComponent();
            jobMap = new Dictionary<int,HITData>();
        }

        private void WPFContainer_Load(object sender, EventArgs e)
        {
        }

        //public HITView addHIT(string name, string text)
        public HITView addHIT(string name, HITData data, int jobNumber)
        {
            // Create the ElementHost control for hosting the
            // WPF UserControl.
            ElementHost host = new ElementHost();
            host.Name = HOSTNAME;
            host.Dock = DockStyle.Fill;

            // Create the WPF UserControl.

            //HITView hs = new HITView(name, text);
            HITView hs;
            if (name == ShortnJob.HIT_TYPE)
            {
                hs = new ShortnView(name, data as ShortnData);
            }
            else
            {
                hs = new HITView(name, data);
            }
            jobMap[jobNumber] = data;

            // Assign the WPF UserControl to the ElementHost control's
            // Child property.
            host.Child = hs;

            // Add the ElementHost control to the form's
            // collection of child controls.
            this.Controls.Add(host);
            return hs;
        }

        public IEnumerable<HITView> getHITs()
        {
            List<HITView> hits = new List<HITView>();
            var temp = Controls.Find(HOSTNAME, true);
            ElementHost[] hostControls = (ElementHost[]) Controls.Find(HOSTNAME, true);
            return from control in hostControls select control.Child as HITView;
            //return null;
        }
    }
}