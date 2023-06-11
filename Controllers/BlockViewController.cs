using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using WpfStroopTestSuite.Models;
using WpfStroopTestSuite.Pages;
using WpfStroopTestSuite.Repositories;
using Type = WpfStroopTestSuite.Models.Type;

namespace WpfStroopTestSuite.Controllers
{
    internal class BlockViewController
    {
        private readonly BlockView view;
        private readonly TrialSetRepository repository;
        private readonly IEnumerable<TrialSet> trialSets;

        private readonly DispatcherTimer blockTimer;
        private long blockTimerInSeconds;
        private readonly DispatcherTimer trialTimer;
        private long trialTimerInSeconds;
        private readonly DispatcherTimer feedbackTimer;
        private readonly DispatcherTimer intervalTimer;

        public enum States
        {
            Ready,
            Feedback,
            Wait,
        }
        public States State { get; set; }

        // local references
        private TrialData trialData;
        private int score;
        private Trial trial;
        private readonly TrialSet activeTrialSet;
        /// <summary>
        /// Local reference to this current block's duration
        /// </summary>
        private readonly int blockDuration;

        public void SubmitAnswer(Key? keyInput)
        {
            string answer = InterpretAnswerKey(keyInput);
            Result result = trial.GetResult(answer);
            if (result == Result.Correct) score++;

            SaveData(result, score, answer);
            
            EnterFeedbackState(result);
        }

        public void StartBlock()
        {
            StartBlockTimer();
            StartTrialTimer();
            view.ShowTrial(true);
        }

        private void FinishBlock()
        {
            StopAllTimer();
            App.BlockData.EndTime = DateTime.Now;
            App.BlockData.CalculateBlockData();
            App.Subject.SaveBlockData(App.BlockData);
            view.FinishBlock();
        }

        private void SaveData(Result result, int score, string answer)
        {
            // complete trial data
            trialData.EnterData(result, score, DateTime.Now, trial, answer);
            // save the trial data to the block
            App.BlockData.SaveTrialData(trialData);
            // quick calculate current stack of data
            App.BlockData.QuickCalculateBlockData();
        }

        #region property setter functions
        private void StartNewTrial()
        {
            // setup new trial data
            trialData = new TrialData(score);

            // setup new question environment
            //SetActiveTrialSet(); only need to set trialset once
            State = States.Ready;
            trial = GetRandomTrial();
            ResetTrialTimer();
            StartBlockTimer();
            StartTrialTimer();
            ControlView();
        }

        //private void SetActiveTrialSet()
        //{
        //    switch (App.Stage)
        //    {
        //        case Stages.First:
        //            activeTrialSet = trialSets.First();
        //            break;
        //        default:
        //            activeTrialSet = trialSets.Last();
        //            break;
        //    }
        //}
        #endregion

        public Trial GetRandomTrial() { return activeTrialSet.GetRandomTrial(); }

        public void ControlView()
        {
            switch (State)
            {
                case States.Ready:
                    view.ShowTrial(true);
                    view.ShowFeedback(false);
                    view.ShowTimer(true);
                    break;

                case States.Feedback:
                    view.ShowTrial(false);
                    view.ShowFeedback(true);
                    view.ShowTimer(true);
                    break;

                case States.Wait:
                    view.ShowTrial(false);
                    view.ShowFeedback(false);
                    view.ShowTimer(false);
                    break;
            }
            if (State == States.Ready)
            {
                view.SetTrialLabel(trial.Color, trial.Type);

                view.SetTimerText(GetTimeString(App.TrialDuration - trialTimerInSeconds));

                view.SetBlockTimerText(GetTimeString(blockDuration - blockTimerInSeconds));
            }
        }

        #region State Management
        /// <summary>
        /// Checks whether <see cref="BlockViewController.State"/> is currently <seealso cref="States.Ready"/>
        /// </summary>
        /// <returns></returns>
        public bool IsReady()
        {
            if (State == States.Ready) return true;
            return false;
        }

        private void EnterFeedbackState(Result result)
        {
            // stops all other timer
            StopTrialTimer();

            // start feedback timer
            State = States.Feedback;
            view.SetFeedbackLabel(result);
            ControlView();
            StartFeedbackTimer();
        }

        // Enter Wait State
        private void EnterWaitState()
        {
            State = States.Wait;
            StopFeedbackTimer();
            StartIntervalTimer();
            ControlView();
        }

        // Enter ready state
        private void EnterReadyState()
        {
            State = States.Ready;
            StopIntervalTimer();
            StartTrialTimer();

            //start a new trial
            StartNewTrial();
        }
        #endregion

        #region Timers
        public void StartBlockTimer() { if (!blockTimer.IsEnabled) blockTimer.Start(); }
        public void StopBlockTimer() { if (blockTimer.IsEnabled) blockTimer.Stop(); }

        public void StartTrialTimer() { if (!trialTimer.IsEnabled) trialTimer.Start(); }
        public void StopTrialTimer() { if (trialTimer.IsEnabled) trialTimer.Stop(); }
        public void ResetTrialTimer()
        {
            StopTrialTimer();
            trialTimerInSeconds = 0;
            StartTrialTimer();
        }

        public void StartFeedbackTimer() { if (!feedbackTimer.IsEnabled) feedbackTimer.Start(); }
        public void StopFeedbackTimer() { if (feedbackTimer.IsEnabled) feedbackTimer.Stop(); }

        public void StartIntervalTimer() { if (!intervalTimer.IsEnabled) intervalTimer.Start(); }
        public void StopIntervalTimer() { if (intervalTimer.IsEnabled) intervalTimer.Stop(); }

        public void StopAllTimer()
        {
            StopBlockTimer();
            StopFeedbackTimer();
            StopTrialTimer();
            StopIntervalTimer();
        }

        #region Timer Ticks functions
        private void BlockTimerTicks(object? sender, EventArgs e)
        {
            blockTimerInSeconds++;
            view.SetBlockTimerText(GetTimeString(blockDuration - blockTimerInSeconds));
            if (blockTimerInSeconds >= blockDuration) { FinishBlock(); }
        }

        private void QuestionTimerTicks(object? sender, EventArgs e)
        {
            trialTimerInSeconds++;
            view.SetTimerText(GetTimeString((App.TrialDuration - trialTimerInSeconds)));
            if (trialTimerInSeconds >= App.TrialDuration) { SubmitAnswer(null); }
        }

        private void FeedbackTimerTicks(object? sender, EventArgs e)
        {
            EnterWaitState();
        }

        private void IntervalTimerTicks(object? sender, EventArgs e)
        {
            EnterReadyState();
        }
        #endregion
        #endregion

        public static string GetTimeString(long timeInSeconds)
        {
            return ((timeInSeconds) / 60 / 10).ToString() + ((timeInSeconds) / 60 % 10).ToString() +
                ":" + ((timeInSeconds) % 60 / 10).ToString() + ((timeInSeconds) % 60 % 10).ToString();
        }

        private static string InterpretAnswerKey(Key? key)
        {
            if (key == null) return "None";
            if (key == Key.R) return "Red";
            if (key == Key.G) return "Green";
            if (key == Key.B) return "Blue";
            if (key == Key.Y) return "Yellow";
            return key.ToString();
        }

        public BlockViewController(BlockView view)
        {
            // setup reference to view and repository
            this.view = view;
            repository = new TrialSetRepository();
            trialSets = repository.GetAllTrialSets();

            // setup block timer
            blockTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            blockTimer.Tick += BlockTimerTicks;
            blockTimerInSeconds = 0;

            // setup the question timer
            trialTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            trialTimer.Tick += QuestionTimerTicks;
            trialTimerInSeconds = 0;

            // setup the feedback timer
            feedbackTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(App.FeedbackDuration)
            };
            feedbackTimer.Tick += FeedbackTimerTicks;

            // setup the interval timer
            intervalTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(App.TrialIntervalDuration)
            };
            intervalTimer.Tick += IntervalTimerTicks;

            // set initial properties
            App.BlockData.StartTime = DateTime.Now;
            if (App.Stage == Stages.First)
            {
                blockDuration = App.IntroDuration;
                activeTrialSet = trialSets.First();
            }
            else
            {
                blockDuration = App.BlockDuration;
                activeTrialSet = trialSets.Last();
            }
            State = States.Wait; // start at wait state
            score = App.InitialScore;
            
            trial = GetRandomTrial();
            trialData = new TrialData();
            StartNewTrial();
        }
    }
}