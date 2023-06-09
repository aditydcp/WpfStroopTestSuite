using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfStroopTestSuite.Models;
using System.Windows.Media;

namespace WpfStroopTestSuite
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Global state reference
        public static Stages Stage { get; set; } = Stages.First;

        // Global subject data reference
        public static SubjectData Subject { get; set; } = new SubjectData();

        // Global block data reference
        public static BlockData BlockData { get; set; } = new BlockData(Stage);

        protected override void OnStartup(StartupEventArgs e)
        {
            //MainWindow = new SubjectForm();
            MainWindow = new MainWindow()
            //{
            //    DataContext = new MainController()
            //};
            ;
            MainWindow.Show();

            base.OnStartup(e);
        }

        /// <summary>
        /// Refreshes <see cref="App.BlockData"/> to a new instance
        /// with the current value of <see cref="App.Stage"/>.
        /// </summary>
        public static void ResetBlockData()
        {
            BlockData = new BlockData(Stage);
        }

        #region Data Exports
        /// <summary>
        /// Save the data about subject into the subjects list.
        /// </summary>
        public static void SaveSubjectData()
        {
            string path = ConfigurationManager.AppSettings.Get("RunDataPath") + "SubjectRecords.csv";
            //var hollow = new { something = string.Empty }; // needed to make extra space in the file
            List<RecordSubject> subject = new()
            {
                new RecordSubject(Subject),
            };
            StreamWriter writer;
            CsvWriter csv;
            FileStream? stream;

            // if file exist, append current SubjectData to the file
            if (File.Exists(path))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = false,
                };
                stream = File.Open(path, FileMode.Append);
                writer = new StreamWriter(stream);
                csv = new CsvWriter(writer, config);
            }
            // if file does not exist, create entire file
            else
            {
                stream = null;
                writer = new StreamWriter(path);
                csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            }

            using (stream)
            using (writer)
            using (csv)
                //csv.WriteRecord(hollow);
                csv.WriteRecords(subject);
        }

        /// <summary>
        /// Save the data of this run into a .csv file.
        /// </summary>
        /// <param name="filename">
        /// Set the name of the file.
        /// If set to default, the filename would consists of subject's credentials.
        /// </param>
        public static void SaveRunData(string filename = "default")
        {
            if (filename == "default")
            {
                filename =
                    Subject.GetSubjectName("d") +
                    Subject.GetSubjectId("d") + "_" +
                    "G-" + Subject.GetGroupId("d");
            }

            List<RecordData> records = new();
            foreach (BlockData block in Subject.Blocks)
            {
                foreach (TrialData trial in block.Trials)
                {
                    records.Add(new RecordData(Subject, block, trial));
                }
            }

            var path = ConfigurationManager.AppSettings.Get("RunDataPath");
            using var writer = new StreamWriter(path + filename + ".csv");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(records);
        }
        #endregion

        #region Global Static Parameters
        /// <summary>
        /// Starting score for each block.
        /// </summary>
        public static int InitialScore { get; } =
            int.Parse(ConfigurationManager.AppSettings.Get("InitialScore"));

        /// <summary>
        /// Duration for each trial/question in seconds.
        /// </summary>
        public static int TrialDuration { get; } =
            int.Parse(ConfigurationManager.AppSettings.Get("TrialDurationInSeconds"));

        /// <summary>
        /// Duration for each block in seconds.
        /// </summary>
        public static int BlockDuration { get; } =
            int.Parse(ConfigurationManager.AppSettings.Get("BlockDurationInSeconds"));

        /// <summary>
        /// Duration for the First block (introductory block) in seconds.
        /// </summary>
        public static int IntroDuration { get; } =
            int.Parse(ConfigurationManager.AppSettings.Get("IntroDurationInSeconds"));

        /// <summary>
        /// Duration for the visual feedback in ms.
        /// </summary>
        public static int FeedbackDuration { get; } =
            int.Parse(ConfigurationManager.AppSettings.Get("FeedbackDurationInMs"));

        /// <summary>
        /// Wait between feedback state and the next trial in ms.
        /// </summary>
        public static int TrialIntervalDuration { get; } =
            int.Parse(ConfigurationManager.AppSettings.Get("TrialIntervalDurationInMs"));
        #endregion

        public static T? FindParentOfType<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentDepObj = child;
            do
            {
                parentDepObj = VisualTreeHelper.GetParent(parentDepObj);
                if (parentDepObj is T parent) return parent;
            }
            while (parentDepObj != null);
            return null;
        }
    }
}
