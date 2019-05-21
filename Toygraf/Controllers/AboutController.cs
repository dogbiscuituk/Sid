namespace ToyGraf.Controllers
{
    using System.Reflection;
    using System.Windows.Forms;
    using ToyGraf.Views;

    public class AboutController
    {
        #region Public Interface

        public AboutController()
        {
            View = new AboutDialog();
            var asm = Assembly.GetExecutingAssembly();
            View.Text = $"About {Application.ProductName}";
            View.lblDescription.Text = asm.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;
            View.lblVersion.Text = Application.ProductVersion;
            View.lblAuthor.Text = Application.CompanyName;
            View.lblCopyright.Text = asm.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;
        }

        public void ShowDialog(IWin32Window owner) => View.ShowDialog(owner);

        #endregion

        #region Private Properties

        private AboutDialog _view;
        private AboutDialog View
        {
            get => _view;
            set
            {
                _view = value;
                View.NewtonsoftLinkLabel.Click += NewtonsoftLinkLabel_Click;
            }
        }

        #endregion

        #region Private Event Handlers

        private void NewtonsoftLinkLabel_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start(View.NewtonsoftLinkLabel.Text);
            View.NewtonsoftLinkLabel.LinkVisited = true;
        }

        #endregion
    }
}
