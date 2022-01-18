﻿using SharpNeat.BlackBox;
using SharpNeat.Evaluation;
using SMW_ML.Emulator;
using SMW_ML.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMW_ML.Neural.Training.SharpNeat
{
    internal class SMWEvaluationScheme : IBlackBoxEvaluationScheme<double>
    {
        private IEmulatorAdapter emulator;

        public int InputCount => SMWPhenomeEvaluator.INPUT_COUNT;

        public int OutputCount => Input.BUTTON_COUNT;

        public bool IsDeterministic => true; // Change if using random levels

        public IComparer<FitnessInfo> FitnessComparer => PrimaryFitnessInfoComparer.Singleton;

        public FitnessInfo NullFitness => FitnessInfo.DefaultFitnessInfo;

        //We need an emulator instance for every AI.
        public bool EvaluatorsHaveState => true;

        public SMWEvaluationScheme(IEmulatorAdapter emulator)
        {
            this.emulator = emulator;
        }

        public IPhenomeEvaluator<IBlackBox<double>> CreateEvaluator() => new SMWPhenomeEvaluator(emulator);

        public bool TestForStopCondition(FitnessInfo fitnessInfo) => false;
    }
}
