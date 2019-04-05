using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AudioMixer
{
	public partial class MixPanel : UserControl
	{
		public MixPanel()
		{
			this.InitializeComponent();
		}

		public MixPanel(MixInfo mixInfo) : this()
		{
			this.tbName.Text = mixInfo.Name;
		}

		public event EventHandler NameChanged;

		public string MixName
		{
			get
			{
				return this.tbName.Text;
			}
		}

		private void tbName_TextChanged(object sender, EventArgs e)
		{
			if (this.NameChanged != null)
			{
				this.NameChanged.Invoke(this, EventArgs.Empty);
			}
		}
	}
}
