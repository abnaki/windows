using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace Abnaki.Windows.Software.Wpf.Diplomat
{
    public class Troubleshooter
    {
        public static bool DialogSaveLog()
        {
            bool saved = false;

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "XML|.xml";
                dialog.DefaultExt = ".xml";
                dialog.Title = "Save Log File";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Abnaki.Windows.Log.Write(dialog.FileName);
                    saved = true;
                }
            }
            return saved;
        }
    }
}
