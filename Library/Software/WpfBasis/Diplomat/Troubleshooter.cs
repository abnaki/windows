using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

using Abnaki.Windows.Software.Wpf.NetAid;

namespace Abnaki.Windows.Software.Wpf.Diplomat
{
    public class Troubleshooter
    {
        public static string Email { get; set; }

        /// <summary>
        /// Effective if Email exists
        /// </summary>
        public static IList<string> AdvisorLines { get; set; }

        static Troubleshooter()
        {
            AdvisorLines = new[]
                {
                    "If you need to report an important issue, please follow these instructions.",
                    "Hit the Email button.   In the Subject, add a few descriptive words.", 
                    "Discuss what you did, what you saw, and what you expected.",
                    "To capture an image, focus in this software, Alt-PrtSc, and paste in email.",
                    "",
                    "Hit Save Log and create a file.",
                    "Optionally, review it and replace private information with ##.",
                    "Attach the log file to the email and save draft.",
                    "",
                    "Before sending email, try to repeat the problem.",
                    "If an upgrade is available, try upgrading, and repeat.",
                    "If your organization has an expert on this software, first show them the problem.",
                    "If not resolved now, send the email.  Thank you."
                };
        }

        public static void Shoot()
        {
            if (string.IsNullOrEmpty(Email))
            {
                // old, without TroubleAdvisor
                bool saved = DialogSaveLog();
                if (saved)
                {
                    string msg = "Completed saving file.\n" +
                        "To communicate an issue with the software author,\n" +
                        "please upload this file, as directed.";

                    Diplomat.Notifier.Notify(msg);
                }
            }
            else
            {
                if (AdvisorLines != null)
                    advisorWindow.Value.Advice = string.Join("\n", AdvisorLines);

                // Need a more sophisiticated control. Some users will upload wrong file and you will spend time discovering the confusion.

                advisorWindow.Value.Show();
            }
        }

        static readonly Lazy<TroubleAdvisor> advisorWindow = new Lazy<TroubleAdvisor>();

        public static bool DialogSaveLog()
        {
            bool saved = false;

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "XML|.xml";
                dialog.DefaultExt = ".xml";
                dialog.Title = "Save Log File";

                if ( dialog.ShowDialogAndOK() )
                {
                    using (new WaitCursor())
                    {
                        Abnaki.Windows.AbnakiLog.Write(dialog.FileName);
                    }
                    saved = true;
                }
            }
            return saved;
        }
    }
}
