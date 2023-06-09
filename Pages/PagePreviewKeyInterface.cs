using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfStroopTestSuite.Pages
{
    internal interface IPagePreviewKey
    {
        /// <summary>
        /// Page-specific OnPreviewKeyDown method.
        /// </summary>
        public abstract void OnPreviewKeyDown(object sender, KeyEventArgs e);
    }
}
