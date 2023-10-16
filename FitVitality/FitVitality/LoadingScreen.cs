﻿using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Krypton.Toolkit;

namespace FitVitality
{
    public partial class loadingScreen : KryptonForm
    {
        public loadingScreen()
        {
            InitializeComponent();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity >= 1)
            {
                loadTimer.Enabled = true;
                panel3.Width += 10;
                textBox1.Text = ((int)(panel3.Width / 2.41)).ToString() + "%";
                if (panel3.Width >= panel2.Width)
                {
                    loadTimer.Stop();
                    Thread.Sleep(500);
                    panel3.Visible = false;
                    panel2.Visible = false;
                    Thread.Sleep(500);

                    WelcomeScreen welcomeScreen = new WelcomeScreen();
                    for (double i = this.Opacity; i >= 0; i = i - 0.00002)
                    {
                        this.Opacity = i;
                    }
                    Thread.Sleep(500);
                    welcomeScreen.Show();
                    this.Hide();
                }
            }
        }

        private void loadingScreen_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.ControlBox = false;
        }

        private void loadingScreen_Shown(object sender, EventArgs e)
        {
            this.Opacity = 0;
            while (this.Opacity != 1)
            {
                this.Opacity += 0.00004;
            }
        }

        private void loadingScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
