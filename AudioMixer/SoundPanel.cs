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
	public partial class SoundPanel : UserControl
	{
		public SoundPanel()
		{
			this.InitializeComponent();
		}

		public event EventHandler DeleteButtonClick;

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (this.DeleteButtonClick != null)
			{
				this.DeleteButtonClick.Invoke(this, EventArgs.Empty);
			}
		}
	}
}
