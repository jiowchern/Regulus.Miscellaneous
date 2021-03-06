using NUnit.Framework;
using Regulus.BehaviourTree.Yield;
using System;
using System.Collections.Generic;

namespace Regulus.BehaviourTree.Tests
{


    /// <summary>此類別包含 SequenceNode 的參數化單元測試</summary>


    public partial class SequenceNodeTest
    {

        /// <summary>.ctor() 的測試虛設常式</summary>

        internal SequenceNode ConstructorTest()
        {
            SequenceNode target = new SequenceNode();
            return target;
            // TODO: 將判斷提示加入 方法 SequenceNodeTest.ConstructorTest()
        }

        /// <summary>Tick(Single) 的測試虛設常式</summary>

        internal TICKRESULT TickTest(SequenceNode target, float delta)
        {
            TICKRESULT result = target.Tick(delta);
            return result;
            // TODO: 將判斷提示加入 方法 SequenceNodeTest.TickTest(SequenceNode, Single)
        }

        [NUnit.Framework.Test()]
        public void TestTick1()
        {
            SequenceNode target = new SequenceNode();
            target.Add(new ActionNode<NumberTestNode>(() => new NumberTestNode(3)
                , n => n.Tick
                , n => n.Start
                , n => n.End
                ));

            /*target.Add(new ActionNode<NumberTestNode>(new NumberTestNode(2)
                , n => n.Tick
                , n => n.Start
                , n => n.End
                ));*/

            NUnit.Framework.Assert.AreEqual(TICKRESULT.RUNNING, target.Tick(0));
            NUnit.Framework.Assert.AreEqual(TICKRESULT.RUNNING, target.Tick(0));
            NUnit.Framework.Assert.AreEqual(TICKRESULT.SUCCESS, target.Tick(0));
            NUnit.Framework.Assert.AreEqual(TICKRESULT.FAILURE, target.Tick(0));


        }

        [NUnit.Framework.Test()]
        public void TestTick2()
        {
            SequenceNode target = new SequenceNode();
            target.Add(new ActionNode<SequenceTestNode>(() => new SequenceTestNode(3)
                , n => n.Tick
                , n => n.Start
                , n => n.End
                ));


            NUnit.Framework.Assert.AreEqual(TICKRESULT.RUNNING, target.Tick(0));
            NUnit.Framework.Assert.AreEqual(TICKRESULT.RUNNING, target.Tick(0));
            NUnit.Framework.Assert.AreEqual(TICKRESULT.SUCCESS, target.Tick(0));



        }

        [NUnit.Framework.Test()]
        public void TestTick3()
        {

            Builder builder = new Builder();
            ITicker node = builder
                .Sequence()
                    .Action((delta) => TICKRESULT.FAILURE)
                    .Action((delta) => TICKRESULT.SUCCESS)
                .End().Build();

            NUnit.Framework.Assert.AreEqual(TICKRESULT.FAILURE, node.Tick(0));
            NUnit.Framework.Assert.AreEqual(TICKRESULT.FAILURE, node.Tick(0));



        }

        [NUnit.Framework.Test()]
        public void TestTick4()
        {

            Builder builder = new Builder();
            ITicker node = builder
                .Sequence()
                    .Action((delta) => TICKRESULT.SUCCESS)
                    .Action((delta) => TICKRESULT.SUCCESS)
                .End().Build();

            NUnit.Framework.Assert.AreEqual(TICKRESULT.RUNNING, node.Tick(0));
            NUnit.Framework.Assert.AreEqual(TICKRESULT.SUCCESS, node.Tick(0));

            NUnit.Framework.Assert.AreEqual(TICKRESULT.RUNNING, node.Tick(0));
            NUnit.Framework.Assert.AreEqual(TICKRESULT.SUCCESS, node.Tick(0));

        }

        [NUnit.Framework.Test()]
        public void TestTick5()
        {

            Builder builder = new Builder();
            ITicker node = builder
                .Sequence()
                    .Action((delta) => TICKRESULT.SUCCESS)
                    .Action((delta) => TICKRESULT.FAILURE)
                    .Action((delta) => TICKRESULT.SUCCESS)
                .End().Build();

            NUnit.Framework.Assert.AreEqual(TICKRESULT.RUNNING, node.Tick(0));
            NUnit.Framework.Assert.AreEqual(TICKRESULT.FAILURE, node.Tick(0));

            NUnit.Framework.Assert.AreEqual(TICKRESULT.RUNNING, node.Tick(0));
            NUnit.Framework.Assert.AreEqual(TICKRESULT.FAILURE, node.Tick(0));

        }


        [NUnit.Framework.Test()]
        public void TestTick6()
        {
            bool executee = false;
            Builder builder = new Builder();
            ITicker node = builder
                .Selector()
                    .Sequence()
                        .Action((delta) => TICKRESULT.SUCCESS)
                        .Action((delta) => TICKRESULT.FAILURE)
                        .Action(
                            (delta) =>
                            {
                                executee = true;
                                return TICKRESULT.SUCCESS;
                            })
                    .End()
                .End().Build();

            NUnit.Framework.Assert.AreEqual(TICKRESULT.RUNNING, node.Tick(0));
            NUnit.Framework.Assert.AreEqual(TICKRESULT.FAILURE, node.Tick(0));

            NUnit.Framework.Assert.AreEqual(TICKRESULT.RUNNING, node.Tick(0));
            NUnit.Framework.Assert.AreEqual(TICKRESULT.FAILURE, node.Tick(0));

            NUnit.Framework.Assert.AreEqual(false, executee);



        }

        [NUnit.Framework.Test()]
        public void TestTick7()
        {
            bool executee = false;

            int count = 0;
            Builder builder = new Builder();
            ITicker node = builder
                .Selector()
                    .Sequence()
                        .Action((delta) => TICKRESULT.SUCCESS)
                        .Action((delta) => TICKRESULT.SUCCESS)
                        .Action(
                            (delta) =>
                            {
                                count++;
                                if (count > 3)
                                    return TICKRESULT.SUCCESS;
                                return TICKRESULT.RUNNING;
                            })
                    .End()
                .End().Build();

            NUnit.Framework.Assert.AreEqual(TICKRESULT.RUNNING, node.Tick(0));
            NUnit.Framework.Assert.AreEqual(TICKRESULT.RUNNING, node.Tick(0));
            NUnit.Framework.Assert.AreEqual(TICKRESULT.RUNNING, node.Tick(0));
            NUnit.Framework.Assert.AreEqual(TICKRESULT.RUNNING, node.Tick(0));
            NUnit.Framework.Assert.AreEqual(TICKRESULT.RUNNING, node.Tick(0));
            NUnit.Framework.Assert.AreEqual(TICKRESULT.SUCCESS, node.Tick(0));


            NUnit.Framework.Assert.AreEqual(false, executee);



        }

        private TICKRESULT _Test(float arg)
        {
            throw new NotImplementedException();
        }

        [NUnit.Framework.Test()]
        public void TestTick8()
        {
            int node1_1 = 0;
            int node1_2 = 0;
            int node2_1 = 0;
            Builder builder = new Builder();
            ITicker node = builder
                .Sequence()
                    .Action((delta) =>
                    {
                        node1_1++;
                        return TICKRESULT.SUCCESS; ;
                    })
                    .Action((delta) =>
                    {
                        node1_2++;
                        return TICKRESULT.SUCCESS; ;
                    })
                    .Sequence()
                        .Action((delta) =>
                        {
                            node2_1++;
                            return TICKRESULT.SUCCESS; ;
                        })
                    .End()
                .End()
                .Build();

            node.Tick(0);
            node.Tick(0);
            node.Tick(0);

            NUnit.Framework.Assert.AreEqual(node1_1, 1);
            NUnit.Framework.Assert.AreEqual(node1_2, 1);
            NUnit.Framework.Assert.AreEqual(node2_1, 1);
        }

        [NUnit.Framework.Test()]
        public void TestParallel1()
        {
            IParent node = new ParallelNode(true);

            node.Add(new Coroutine(() => _Step(3, true)));
            node.Add(new Coroutine(() => _Step(2, true)));
            node.Add(new Coroutine(() => _Step(1, false)));

            TICKRESULT r1 = node.Tick(0);
            TICKRESULT r2 = node.Tick(0);
            TICKRESULT r3 = node.Tick(0);
            TICKRESULT r4 = node.Tick(0);
            TICKRESULT r5 = node.Tick(0);


            TICKRESULT r6 = node.Tick(0);
            TICKRESULT r7 = node.Tick(0);
            TICKRESULT r8 = node.Tick(0);
            TICKRESULT r9 = node.Tick(0);
            TICKRESULT r10 = node.Tick(0);

            Assert.AreEqual(r1, TICKRESULT.RUNNING);
            Assert.AreEqual(r2, TICKRESULT.RUNNING);
            Assert.AreEqual(r3, TICKRESULT.RUNNING);
            Assert.AreEqual(r4, TICKRESULT.RUNNING);
            Assert.AreEqual(r5, TICKRESULT.SUCCESS);
            Assert.AreEqual(r6, TICKRESULT.RUNNING);
            Assert.AreEqual(r7, TICKRESULT.RUNNING);
            Assert.AreEqual(r8, TICKRESULT.RUNNING);
            Assert.AreEqual(r9, TICKRESULT.RUNNING);
            Assert.AreEqual(r10, TICKRESULT.SUCCESS);
        }

        [NUnit.Framework.Test()]
        public void TestParallel2()
        {
            IParent node = new ParallelNode(true);

            node.Add(new Coroutine(() => _Step(3, true)));
            node.Add(new Coroutine(() => _Step(2, false)));
            node.Add(new Coroutine(() => _Step(1, false)));

            TICKRESULT r1 = node.Tick(0);
            TICKRESULT r2 = node.Tick(0);
            TICKRESULT r3 = node.Tick(0);
            TICKRESULT r4 = node.Tick(0);
            TICKRESULT r5 = node.Tick(0);


            TICKRESULT r6 = node.Tick(0);
            TICKRESULT r7 = node.Tick(0);
            TICKRESULT r8 = node.Tick(0);
            TICKRESULT r9 = node.Tick(0);
            TICKRESULT r10 = node.Tick(0);

            Assert.AreEqual(r1, TICKRESULT.RUNNING);
            Assert.AreEqual(r2, TICKRESULT.RUNNING);
            Assert.AreEqual(r3, TICKRESULT.RUNNING);
            Assert.AreEqual(r4, TICKRESULT.RUNNING);
            Assert.AreEqual(r5, TICKRESULT.FAILURE);
            Assert.AreEqual(r6, TICKRESULT.RUNNING);
            Assert.AreEqual(r7, TICKRESULT.RUNNING);
            Assert.AreEqual(r8, TICKRESULT.RUNNING);
            Assert.AreEqual(r9, TICKRESULT.RUNNING);
            Assert.AreEqual(r10, TICKRESULT.FAILURE);
        }



        private IEnumerable<IInstructable> _Step(int step, bool success)
        {
            for (int i = 0; i < step; i++)
            {
                yield return new Wait();
            }


            if (success)
                yield return new Success();
            else
                yield return new Failure();
        }

        [NUnit.Framework.Test()]
        [NUnit.Framework.MaxTime(5000)]
        public void TestParallel3()
        {
            List<int> tokens = new List<int>();
            int token1 = 1;
            int token2 = 2;
            int token3 = 3;
            int token4 = 4;
            int token5 = 5;
            Builder builder = new Builder();
            ITicker ticker = builder.Selector()
                    .Parallel(true)
                        .Action(() => _AddToken(tokens, token1, 0))
                        .Action(() => _AddToken(tokens, token2, 1))
                        .Action(() => _AddToken(tokens, token3, 0))
                    .End()
                    .Parallel(true)
                        .Action(() => _AddToken(tokens, token4, 0))
                        .Action(() => _AddToken(tokens, token5, 0))
                    .End()
                .End().Build();

            while (tokens.Count < 5)
            {
                ticker.Tick(0);
            }
            Assert.AreEqual(token1, tokens[0]);
            Assert.AreEqual(token2, tokens[1]);
            Assert.AreEqual(token3, tokens[2]);
            Assert.AreEqual(token4, tokens[3]);
            Assert.AreEqual(token5, tokens[4]);

        }

        private IEnumerable<IInstructable> _AddToken(List<int> tokens, int token, float time)
        {
            tokens.Add(token);
            yield return new WaitSeconds(time);
            yield return new Failure();
        }
    }



    public class SequenceTestNode
    {
        private int _I;

        public SequenceTestNode(int i)
        {
            _I = i;
        }

        public TICKRESULT Tick(float arg)
        {
            --_I;
            if (_I > 0)
                return TICKRESULT.RUNNING;

            return TICKRESULT.SUCCESS;
        }

        public void Start()
        {

        }

        public void End()
        {

        }


    }
}


