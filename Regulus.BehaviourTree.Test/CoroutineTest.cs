using NUnit.Framework;
using Regulus.BehaviourTree.Yield;
using System.Collections.Generic;

namespace Regulus.BehaviourTree.Tests
{

    class CoroutineTestObject
    {

        public IEnumerable<IInstructable> DirectBreak()
        {
            yield break;
        }

        public IEnumerable<IInstructable> DirectNull()
        {
            yield return null;
        }

        public IEnumerable<IInstructable> DirectCount3ToFailure()
        {

            yield return new Wait();

            yield return new Wait();

            yield return new Failure();

            yield return new Success();
        }

        public IEnumerable<IInstructable> UntilCount3ToSuccess()
        {
            int count = 0;
            yield return new WaitUntil(() =>
            {
                return ++count > 3;
            });


        }


        public IEnumerable<IInstructable> UntilCount3ToSuccessWithWait()
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new Wait();
            }
        }
    }

    public class CoroutineTest
    {
        [NUnit.Framework.Test()]
        public void TestDirectBreak()
        {
            CoroutineTestObject obj = new CoroutineTestObject();
            ITicker coroutine = new Regulus.BehaviourTree.Yield.Coroutine(() => obj.DirectBreak());
            TICKRESULT res1 = coroutine.Tick(0);
            Assert.AreEqual(TICKRESULT.SUCCESS, res1);
        }

        [NUnit.Framework.Test()]
        public void TestDirectNull()
        {
            CoroutineTestObject obj = new CoroutineTestObject();
            ITicker coroutine = new Regulus.BehaviourTree.Yield.Coroutine(() => obj.DirectNull());
            TICKRESULT res1 = coroutine.Tick(0);
            Assert.AreEqual(TICKRESULT.SUCCESS, res1);
        }

        [NUnit.Framework.Test()]
        public void TestDirectCount3ToFailure()
        {
            CoroutineTestObject obj = new CoroutineTestObject();
            ITicker coroutine = new Regulus.BehaviourTree.Yield.Coroutine(() => obj.DirectCount3ToFailure());
            TICKRESULT res1 = coroutine.Tick(0);
            TICKRESULT res2 = coroutine.Tick(0);
            TICKRESULT res3 = coroutine.Tick(0);
            TICKRESULT res4 = coroutine.Tick(0);
            TICKRESULT res5 = coroutine.Tick(0);
            Assert.AreEqual(TICKRESULT.RUNNING, res1);
            Assert.AreEqual(TICKRESULT.RUNNING, res2);
            Assert.AreEqual(TICKRESULT.RUNNING, res3);
            Assert.AreEqual(TICKRESULT.FAILURE, res4);
            Assert.AreEqual(TICKRESULT.RUNNING, res5);
        }

        [NUnit.Framework.Test()]
        public void UntilCount3ToToSuccess()
        {
            CoroutineTestObject obj = new CoroutineTestObject();
            ITicker coroutine = new Regulus.BehaviourTree.Yield.Coroutine(() => obj.UntilCount3ToSuccess());
            TICKRESULT res1 = coroutine.Tick(0);
            TICKRESULT res2 = coroutine.Tick(0);
            TICKRESULT res3 = coroutine.Tick(0);
            TICKRESULT res4 = coroutine.Tick(0);
            TICKRESULT res5 = coroutine.Tick(0);
            TICKRESULT res6 = coroutine.Tick(0);
            Assert.AreEqual(TICKRESULT.RUNNING, res1);
            Assert.AreEqual(TICKRESULT.RUNNING, res2);
            Assert.AreEqual(TICKRESULT.RUNNING, res3);
            Assert.AreEqual(TICKRESULT.RUNNING, res4);
            Assert.AreEqual(TICKRESULT.RUNNING, res5);
            Assert.AreEqual(TICKRESULT.SUCCESS, res6);
        }

        [NUnit.Framework.Test()]
        public void UntilCount3ToToSuccessWithWait()
        {
            CoroutineTestObject obj = new CoroutineTestObject();
            ITicker coroutine = new Regulus.BehaviourTree.Yield.Coroutine(() => obj.UntilCount3ToSuccessWithWait());
            TICKRESULT res1 = coroutine.Tick(0);
            TICKRESULT res2 = coroutine.Tick(0);
            TICKRESULT res3 = coroutine.Tick(0);
            TICKRESULT res4 = coroutine.Tick(0);
            TICKRESULT res5 = coroutine.Tick(0);
            TICKRESULT res6 = coroutine.Tick(0);
            Assert.AreEqual(TICKRESULT.RUNNING, res1);
            Assert.AreEqual(TICKRESULT.RUNNING, res2);
            Assert.AreEqual(TICKRESULT.RUNNING, res3);
            Assert.AreEqual(TICKRESULT.RUNNING, res4);
            Assert.AreEqual(TICKRESULT.SUCCESS, res5);
            Assert.AreEqual(TICKRESULT.RUNNING, res6);
        }



    }
}