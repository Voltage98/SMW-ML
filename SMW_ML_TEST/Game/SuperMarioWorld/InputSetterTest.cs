﻿using NUnit.Framework;
using SMW_ML.Game.SuperMarioWorld;
using SMW_ML_TEST.Emulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMW_ML_TEST.Game.SuperMarioWorld
{
    [TestFixture]
    internal class InputSetterTest
    {
        private InputSetter? inputSetter;
        private DataFetcher? dataFetcher;
        private MockEmulatorAdapter? mea;

        [SetUp]
        public void SetUp()
        {
            mea = new MockEmulatorAdapter();
            dataFetcher = new DataFetcher(mea);
            inputSetter = new InputSetter(dataFetcher);
        }

        [Test]
        public void GetInputCount()
        {
            Assert.Ignore("Not implemented yet");
        }

        [Test]
        public void SetInputs()
        {
            Assert.Ignore("Not implemented yet");
        }
    }
}