namespace ToyGraf.Controllers
{
    using System.Reflection;
    using System.Windows.Forms;
    using ToyGraf.Views;

    public class AboutController
    {
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

        private AboutDialog _view;
        public AboutDialog View
        {
            get => _view;
            set
            {
                if (View != null)
                    View.NewtonsoftLinkLabel.Click -= NewtonsoftLinkLabel_Click;
                _view = value;
                if (View != null)
                    View.NewtonsoftLinkLabel.Click += NewtonsoftLinkLabel_Click;
            }
        }

        private void NewtonsoftLinkLabel_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start(View.NewtonsoftLinkLabel.Text);
            View.NewtonsoftLinkLabel.LinkVisited = true;
        }
    }
}
